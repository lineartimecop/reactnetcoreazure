#!/usr/bin/env bash

echo
echo "Running WebApp backend..."
echo

cd -- "$(dirname -- "$BASH_SOURCE")"
cd ../../src/backend

dotnet run

echo
read -p "Press any key to continue..."
