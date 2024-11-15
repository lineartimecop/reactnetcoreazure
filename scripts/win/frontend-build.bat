echo off

echo.
echo Building WebApp frontend...
echo.

rmdir ..\..\src\frontend\build /s /q
rmdir ..\..\src\frontend\obj /s /q
rmdir ..\..\src\frontend\node_modules /s /q
del ..\..\src\frontend\package-lock.json /s
cd ..\..\src\frontend

call npm install

pause
