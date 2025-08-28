# PowerShell script for testing Hello World COM Wrapper
# Run this script as Administrator for best results

param(
    [switch]$Verbose,
    [switch]$Debug
)

Write-Host "Hello World COM Wrapper Test Script" -ForegroundColor Green
Write-Host "====================================" -ForegroundColor Green
Write-Host

# Check if running as administrator
$isAdmin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")
if (-not $isAdmin) {
    Write-Warning "Not running as Administrator. Some COM operations may fail."
    Write-Host
}

# Set COM environment variables
$env:COMPlus_EnableComHosting = "1"
$env:COMPlus_EnableComHostingInServerProcess = "1"

Write-Host "COM Environment Variables Set:" -ForegroundColor Yellow
Write-Host "  COMPlus_EnableComHosting: $env:COMPlus_EnableComHosting"
Write-Host "  COMPlus_EnableComHostingInServerProcess: $env:COMPlus_EnableComHostingInServerProcess"
Write-Host

# Check for required files
$libPath = "HelloWorldLib\bin\Release\net8.0"
$clientPath = "TestClient32\bin\Release\net8.0"

$requiredFiles = @(
    "$libPath\HelloWorldLib.dll",
    "$libPath\HelloWorldLib.comhost.dll",
    "$libPath\HelloWorldLib.manifest",
    "$clientPath\TestClient32.exe"
)

Write-Host "Checking required files..." -ForegroundColor Yellow
foreach ($file in $requiredFiles) {
    if (Test-Path $file) {
        Write-Host "  ✓ $file" -ForegroundColor Green
    } else {
        Write-Host "  ✗ $file" -ForegroundColor Red
        Write-Error "Required file not found: $file"
        exit 1
    }
}
Write-Host

# Test COM registration (optional, for debugging)
if ($Debug) {
    Write-Host "Testing COM registration..." -ForegroundColor Yellow
    try {
        $comType = [System.Type]::GetTypeFromProgID("HelloWorldLib.HelloWorldService")
        if ($comType) {
            Write-Host "  ✓ COM type found via ProgID" -ForegroundColor Green
        } else {
            Write-Host "  ✗ COM type not found via ProgID" -ForegroundColor Red
        }
    } catch {
        Write-Host "  ✗ Error checking COM type: $($_.Exception.Message)" -ForegroundColor Red
    }
    Write-Host
}

# Run the test client
Write-Host "Running 32-bit test client..." -ForegroundColor Yellow
Write-Host

try {
    $process = Start-Process -FilePath "$clientPath\TestClient32.exe" -Wait -PassThru -NoNewWindow
    if ($process.ExitCode -eq 0) {
        Write-Host "Test client completed successfully with exit code: $($process.ExitCode)" -ForegroundColor Green
    } else {
        Write-Host "Test client failed with exit code: $($process.ExitCode)" -ForegroundColor Red
    }
} catch {
    Write-Error "Failed to run test client: $($_.Exception.Message)"
}

Write-Host
Write-Host "Test completed." -ForegroundColor Green
