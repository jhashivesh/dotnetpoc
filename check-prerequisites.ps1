# Prerequisites Check Script
Write-Host "Checking Prerequisites for Hello World COM Wrapper" -ForegroundColor Green
Write-Host "==================================================" -ForegroundColor Green
Write-Host

# Check .NET SDK
Write-Host "Checking .NET SDK..." -ForegroundColor Yellow
$dotnetVersion = dotnet --version 2>$null
if ($dotnetVersion) {
    Write-Host "  .NET SDK found: $dotnetVersion" -ForegroundColor Green
    $majorVersion = [int]($dotnetVersion.Split('.')[0])
    if ($majorVersion -ge 8) {
        Write-Host "  .NET 8.0+ SDK confirmed" -ForegroundColor Green
    } else {
        Write-Host "  .NET 8.0+ SDK required (found $dotnetVersion)" -ForegroundColor Red
    }
} else {
    Write-Host "  .NET SDK not found" -ForegroundColor Red
}
Write-Host

# Check Windows version
Write-Host "Checking Windows version..." -ForegroundColor Yellow
$osInfo = Get-CimInstance -ClassName Win32_OperatingSystem
Write-Host "  Windows: $($osInfo.Caption)" -ForegroundColor Green
Write-Host "  Version: $($osInfo.Version)" -ForegroundColor Green
Write-Host "  Architecture: $($osInfo.OSArchitecture)" -ForegroundColor Green
Write-Host

# Check PowerShell version
Write-Host "Checking PowerShell version..." -ForegroundColor Yellow
Write-Host "  PowerShell: $($PSVersionTable.PSVersion)" -ForegroundColor Green
Write-Host

# Check Administrator privileges
Write-Host "Checking Administrator privileges..." -ForegroundColor Yellow
$isAdmin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")
if ($isAdmin) {
    Write-Host "  Running as Administrator" -ForegroundColor Green
} else {
    Write-Host "  Running as regular user" -ForegroundColor Yellow
}
Write-Host

Write-Host "Prerequisites check completed." -ForegroundColor Cyan
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Run: .\build.bat" -ForegroundColor White
Write-Host "2. Run: .\test.bat" -ForegroundColor White
