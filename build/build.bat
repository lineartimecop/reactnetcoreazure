echo off

echo.
echo Building WebApp backend...
echo.

rmdir bin /s /q

cd ..\src\backend

rmdir .config /s /q
rmdir bin /s /q
rmdir obj /s /q
del Properties\PublishProfiles\FolderProfile.pubxml.user /s
del WebApp.Backend.csproj.user /s
del backend.sln /s

dotnet publish -c Release -o ..\..\build\bin\WebApp.backend WebApp.backend.csproj

echo.
echo Building WebApp frontend...
echo.

cd ..\frontend

rmdir build /s /q
rmdir obj /s /q
rmdir node_modules /s /q
del package-lock.json /s

call npm install
call npm run build

move build ..\..\build\bin\WebApp

echo.
echo Production builds created in build/bin directory
echo.

pause
