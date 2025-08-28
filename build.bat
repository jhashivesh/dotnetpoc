@echo off
echo Building Hello World COM Wrapper Solution...
echo.

echo Building HelloWorldLib (x64)...
dotnet build HelloWorldLib\HelloWorldLib.csproj -c Release -p:PlatformTarget=x64
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to build HelloWorldLib
    pause
    exit /b 1
)

echo.
echo Building TestClient32 (x64)...
dotnet build TestClient32\TestClient32.csproj -c Release -p:PlatformTarget=x64
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to build TestClient32
    pause
    exit /b 1
)

echo.
echo Building HelloWorldLib32 (x86)...
dotnet build HelloWorldLib32\HelloWorldLib32.csproj -c Release -p:PlatformTarget=x86
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to build HelloWorldLib32
    pause
    exit /b 1
)

echo.
echo Building TestClient32Bit (x86)...
dotnet build TestClient32Bit\TestClient32Bit.csproj -c Release -p:PlatformTarget=x86
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to build TestClient32Bit
    pause
    exit /b 1
)

echo.
echo Build completed successfully!
echo.
echo Files generated:
echo   - HelloWorldLib\bin\Release\net8.0\HelloWorldLib.dll (64-bit)
echo   - HelloWorldLib\bin\Release\net8.0\HelloWorldLib.comhost.dll (64-bit)
echo   - HelloWorldLib\bin\Release\net8.0\HelloWorldLib.manifest (64-bit)
echo   - TestClient32\bin\Release\net8.0\TestClient32.exe (64-bit)
echo   - HelloWorldLib32\bin\Release\net8.0\HelloWorldLib32.dll (32-bit)
echo   - HelloWorldLib32\bin\Release\net8.0\HelloWorldLib32.comhost.dll (32-bit)
echo   - HelloWorldLib32\bin\Release\net8.0\HelloWorldLib32.manifest (32-bit)
echo   - TestClient32Bit\bin\Release\net8.0\TestClient32Bit.exe (32-bit)
echo.
pause
