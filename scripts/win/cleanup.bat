echo off

echo.
echo Cleaning up...
echo.

del ..\..\.DS_Store /s
del ..\..\archive\.DS_Store /s
del ..\..\build\.DS_Store /s
del ..\..\src\backend\.DS_Store /s
del ..\..\src\frontend\.DS_Store /s

rmdir ..\..\build\bin /s /q
mkdir ..\..\build\bin

rmdir ..\..\src\.vs /s /q

rmdir ..\..\src\backend\.config /s /q
rmdir ..\..\src\backend\bin /s /q
rmdir ..\..\src\backend\obj /s /q
del ..\..\src\backend\Properties\PublishProfiles\FolderProfile.pubxml.user /s
del ..\..\src\backend\WebApp.Backend.csproj.user /s
del ..\..\src\backend\backend.sln /s

rmdir ..\..\src\frontend\build /s /q
rmdir ..\..\src\frontend\node_modules /s /q
rmdir ..\..\src\frontend\obj /s /q
del ..\..\src\frontend\package-lock.json /s
del ..\..\src\frontend\WebApp.frontend.esproj.user /s

pause
