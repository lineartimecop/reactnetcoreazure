#!/usr/bin/env bash
echo
echo "Cleaning up..."
echo

cd -- "$(dirname -- "$BASH_SOURCE")"

rm -rf ../../src/.vs

rm -rf ../../src/backend/.config
rm -rf ../../src/backend/bin
rm -rf ../../src/backend/obj
rm -rf ../../src/backend/Properties/PublishProfiles/FolderProfile.pubxml.user
rm -rf ../../src/backend/WebApp.Backend.csproj.user
rm -rf ../../src/backend/backend.sln

rm -rf ../../src/frontend/build
rm -rf ../../src/frontend/node_modules
rm -rf ../../src/frontend/obj
rm -rf ../../src/frontend/package-lock.json
rm -rf ../../src/frontend/WebApp.frontend.esproj.user

echo
read -p "Press any key to continue..."
