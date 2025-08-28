# Hello World COM Wrapper for .NET Core

This project demonstrates how to create a COM wrapper using .NET Core that can be consumed by 32-bit clients without requiring COM registration. It uses the latest .NET Core COM hosting features and manifest files for no-registration COM usage.

## Project Structure

```
dotnetpoc/
├── HelloWorldLib/                 # Main COM library (x64)
│   ├── HelloWorldLib.csproj      # Project file with COM interop settings
│   ├── HelloWorldService.cs      # COM-visible service class
│   ├── AssemblyInfo.cs           # Assembly-level COM attributes
│   └── HelloWorldLib.manifest    # COM manifest for no-registration
├── TestClient32/                  # 32-bit test client (x86)
│   ├── TestClient32.csproj       # Project file targeting x86
│   └── Program.cs                 # Test client implementation
├── HelloWorldCOM.sln              # Visual Studio solution file
├── build.bat                     # Windows batch build script
├── test.bat                      # Windows batch test script
├── test.ps1                      # PowerShell test script
└── README.md                      # This file
```

## Features

- **64-bit .NET Core COM Library**: Built with .NET 8.0 and COM interop support
- **No-Registration COM**: Uses manifest files instead of registry registration
- **32-bit Client Support**: Can be consumed by 32-bit applications
- **Modern COM Hosting**: Leverages .NET Core's built-in COM hosting capabilities
- **Multiple Test Methods**: Includes various COM methods for testing

## Prerequisites

- .NET 8.0 SDK or later
- Windows 10/11 (64-bit)
- Visual Studio 2022 or later (optional, for IDE support)

## Building the Solution

### Option 1: Using the Build Script (Recommended)

```cmd
build.bat
```

### Option 2: Using dotnet CLI

```cmd
# Build the COM library (x64)
dotnet build HelloWorldLib\HelloWorldLib.csproj -c Release -p:PlatformTarget=x64

# Build the test client (x86)
dotnet build TestClient32\TestClient32.csproj -c Release -p:PlatformTarget=x86
```

## Testing the COM Wrapper

### Option 1: Using the Test Script (Recommended)

```cmd
test.bat
```

### Option 2: Using PowerShell

```powershell
# Basic test
.\test.ps1

# Verbose test with debugging
.\test.ps1 -Verbose -Debug
```

### Option 3: Manual Testing

```cmd
# Set COM environment variables
set COMPlus_EnableComHosting=1

# Run the test client
TestClient32\bin\Release\net8.0\TestClient32.exe
```

## How It Works

### 1. COM Library (HelloWorldLib)

- **Target**: x64 platform for 64-bit compatibility
- **COM Attributes**: Uses `[ComVisible]`, `[Guid]`, `[ClassInterface]`, and `[ProgId]`
- **COM Hosting**: Enabled via `EnableComHosting=true` in project file
- **Methods Available**:
  - `GetHelloWorld()` - Returns a simple hello world message
  - `GetGreeting(string name)` - Returns a personalized greeting
  - `GetDotNetVersion()` - Returns the .NET version
  - `GetCurrentTime()` - Returns current timestamp
  - `Add(int a, int b)` - Performs simple arithmetic

### 2. COM Manifest (HelloWorldLib.manifest)

- **Purpose**: Enables no-registration COM usage
- **Architecture**: Specifies x64 (amd64) processor architecture
- **COM Class**: Defines the COM class with CLSID and ProgID
- **Proxy Stub**: Includes interface proxy stub information

### 3. Test Client (TestClient32)

- **Target**: x86 platform (32-bit)
- **COM Access**: Uses late binding via `Type.GetTypeFromProgID()`
- **Method Invocation**: Uses reflection to call COM methods
- **Error Handling**: Comprehensive error handling and reporting

## COM Interop Details

### Key Attributes Used

```csharp
[ComVisible(true)]                    // Makes the class visible to COM
[Guid("12345678-1234-1234-1234-123456789012")]  // Unique identifier
[ClassInterface(ClassInterfaceType.AutoDual)]     // Dual interface
[ProgId("HelloWorldLib.HelloWorldService")]      // Programmatic identifier
```

### Project Configuration

```xml
<PropertyGroup>
  <PlatformTarget>x64</PlatformTarget>
  <EnableComHosting>true</EnableComHosting>
  <ComVisible>true</ComVisible>
</PropertyGroup>
```

## Troubleshooting

### Common Issues

1. **"Could not find COM type" Error**

   - Ensure the COM library is built successfully
   - Check that `HelloWorldLib.comhost.dll` exists
   - Verify the manifest file is in the output directory

2. **"Access Denied" Errors**

   - Run as Administrator for best results
   - Check Windows Defender/antivirus settings
   - Verify COM security settings

3. **Build Failures**
   - Ensure .NET 8.0 SDK is installed
   - Check that all project files are present
   - Verify platform targets are correctly set

### Debug Mode

Use the PowerShell script with debug flag for detailed information:

```powershell
.\test.ps1 -Debug
```

### Environment Variables

The following environment variables are automatically set:

- `COMPlus_EnableComHosting=1`
- `COMPlus_EnableComHostingInServerProcess=1`

## Advanced Usage

### Custom COM Methods

To add new COM methods, simply add them to the `HelloWorldService` class with appropriate COM attributes:

```csharp
[ComVisible(true)]
public string CustomMethod(string input)
{
    return $"Processed: {input}";
}
```

### Multiple COM Classes

You can add multiple COM-visible classes by creating new classes with unique GUIDs and ProgIDs.

### COM Events

For COM events, implement `IDispatch` or use `[ComSourceInterfaces]` attribute.

## Performance Considerations

- **First Call**: Initial COM activation may take longer due to .NET Core startup
- **Subsequent Calls**: Method calls are optimized after the first invocation
- **Memory Management**: Always call `Marshal.ReleaseComObject()` when done with COM objects

## Security Notes

- COM objects run in the same process as the host
- No cross-process isolation by default
- Consider security implications when exposing .NET types via COM

## License

This project is provided as-is for educational and demonstration purposes.

## Support

For issues or questions:

1. Check the troubleshooting section above
2. Verify all prerequisites are met
3. Ensure you're running on a supported Windows version
4. Check that .NET Core runtime is properly installed
