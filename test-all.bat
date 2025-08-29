@echo off
echo ========================================
echo .NET Core COM Interoperability Test Suite
echo ========================================
echo.

echo Building solution...
dotnet build --configuration Release
if %ERRORLEVEL% neq 0 (
    echo Build failed!
    pause
    exit /b 1
)
echo Build completed successfully!
echo.

echo ========================================
echo Testing 64-bit COM Client
echo ========================================
echo.
dotnet run --project TestClient64 --configuration Release
echo.

echo ========================================
echo Testing 32-bit COM Client
echo ========================================
echo.
dotnet run --project TestClient32Bit --configuration Release
echo.

echo ========================================
echo All tests completed!
echo ========================================
pause
