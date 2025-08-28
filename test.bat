@echo off
echo Testing Hello World COM Wrapper...
echo.

echo Checking if required files exist...
if not exist "HelloWorldLib\bin\Release\net8.0\HelloWorldLib.dll" (
    echo ERROR: HelloWorldLib.dll not found. Please build the solution first.
    pause
    exit /b 1
)

if not exist "HelloWorldLib\bin\Release\net8.0\HelloWorldLib.comhost.dll" (
    echo ERROR: HelloWorldLib.comhost.dll not found. Please build the solution first.
    pause
    exit /b 1
)

if not exist "TestClient32\bin\Release\net8.0\TestClient32.exe" (
    echo ERROR: TestClient32.exe not found. Please build the solution first.
    pause
    exit /b 1
)

echo All required files found.
echo.

echo Setting up COM environment...
set COMPlus_EnableComHosting=1

echo Running 32-bit test client...
echo.
TestClient32\bin\Release\net8.0\TestClient32.exe

echo.
echo Test completed.
pause
