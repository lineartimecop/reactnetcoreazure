#!/usr/bin/env bash

echo
echo "Building WebApp backend..."
echo

cd -- "$(dirname -- "$BASH_SOURCE")"

rm -rf bin

cd ../src/backend

rm -rf .config
rm -rf bin
rm -rf obj
rm -rf Properties/PublishProfiles/FolderProfile.pubxml.user
rm -rf WebApp.Backend.csproj.user
rm -rf backend.sln

dotnet publish -c Release -o ../../build/bin/WebApp.backend WebApp.backend.csproj

echo
echo "Building WebApp frontend..."
echo

cd ../frontend

rm -rf build
rm -rf obj
rm -rf node_modules
rm -rf package-lock.json

npm install
npm run build

mv build ../../build/bin/WebApp

echo
read -p "Production builds created in build/bin directory"
