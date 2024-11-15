#!/usr/bin/env bash

echo
echo "Building WebApp frontend..."
echo

cd -- "$(dirname -- "$BASH_SOURCE")"

rm -rf ../../src/frontend/build
rm -rf ../../src/frontend/obj
rm -rf ../../src/frontend/node_modules
rm -rf ../../src/frontend/package-lock.json

cd ../../src/frontend
npm install

echo
read -p "Press any key to continue..."
