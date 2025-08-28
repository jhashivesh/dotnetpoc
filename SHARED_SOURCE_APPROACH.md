# Shared Source Approach for Multi-Architecture COM Libraries

## 🎯 **Problem Solved**

Instead of maintaining duplicate `HelloWorldService.cs` files for 32-bit and 64-bit architectures, we now use a **single shared source file** with **conditional compilation**.

## 📁 **New Project Structure**

```
dotnetpoc/
├── Shared/                           # Shared source code
│   └── HelloWorldService.cs         # Single source file for both architectures
├── HelloWorldLib/                    # 64-bit COM library
│   ├── HelloWorldLib.csproj         # References shared source
│   ├── AssemblyInfo.cs              # 64-bit assembly info
│   └── HelloWorldLib.manifest       # 64-bit COM manifest
├── HelloWorldLib32/                  # 32-bit COM library
│   ├── HelloWorldLib32.csproj       # References shared source + X86 define
│   ├── AssemblyInfo.cs              # 32-bit assembly info
│   └── HelloWorldLib32.manifest     # 32-bit COM manifest
├── TestClient32/                     # 64-bit test client
└── TestClient32Bit/                  # 32-bit test client
```

## 🔧 **How It Works**

### **1. Shared Source File (`Shared/HelloWorldService.cs`)**

```csharp
[ComVisible(true)]
#if X86
    [Guid("87654321-4321-4321-4321-210987654321")]
    [ProgId("HelloWorldLib.HelloWorldService")]
#else
    [Guid("12345678-1234-1234-1234-123456789012")]
    [ProgId("HelloWorldLib.HelloWorldService")]
#endif
[ClassInterface(ClassInterfaceType.AutoDual)]
public class HelloWorldService
{
    public string GetHelloWorld()
    {
#if X86
        return "Hello World from .NET Core COM Wrapper (32-bit)!";
#else
        return "Hello World from .NET Core COM Wrapper (64-bit)!";
#endif
    }

    // ... other methods with similar conditional compilation
}
```

### **2. Project References**

**64-bit Project (`HelloWorldLib.csproj`):**

```xml
<ItemGroup>
  <Compile Include="..\Shared\HelloWorldService.cs" />
</ItemGroup>
```

**32-bit Project (`HelloWorldLib32.csproj`):**

```xml
<PropertyGroup>
  <PlatformTarget>x86</PlatformTarget>
  <DefineConstants>X86</DefineConstants>
</PropertyGroup>

<ItemGroup>
  <Compile Include="..\Shared\HelloWorldService.cs" />
</ItemGroup>
```

## ✅ **Benefits**

1. **🚫 No Code Duplication** - Single source file to maintain
2. **🔧 Easy Maintenance** - Changes in one place affect both architectures
3. **🎯 Architecture-Specific Behavior** - Different GUIDs, messages, etc.
4. **📦 Clean Project Structure** - Clear separation of concerns
5. **🔄 Consistent Behavior** - Same logic, different outputs per architecture

## 🎨 **Conditional Compilation Symbols**

- **`X86`** - Defined for 32-bit builds
- **Default** - Used for 64-bit builds (when `X86` is not defined)

## 🔍 **What Gets Compiled Differently**

| Feature              | 32-bit (X86)                                               | 64-bit (Default)                                           |
| -------------------- | ---------------------------------------------------------- | ---------------------------------------------------------- |
| **GUID**             | `87654321-4321-4321-4321-210987654321`                     | `12345678-1234-1234-1234-123456789012`                     |
| **Hello Message**    | "Hello World from .NET Core COM Wrapper (32-bit)!"         | "Hello World from .NET Core COM Wrapper (64-bit)!"         |
| **Greeting Message** | "Hello {name}! Welcome to .NET Core COM Interop (32-bit)!" | "Hello {name}! Welcome to .NET Core COM Interop (64-bit)!" |
| **Platform Info**    | "32-bit .NET Core on {platform}"                           | "64-bit .NET Core on {platform}"                           |

## 🚀 **Usage Examples**

### **Building Both Architectures**

```cmd
.\build.bat
```

### **Testing 64-bit Client**

```cmd
.\TestClient32\bin\Release\net8.0\TestClient32.exe
```

### **Testing 32-bit Client**

```cmd
.\TestClient32Bit\bin\Release\net8.0\TestClient32Bit.exe
```

## 🔧 **Adding New Methods**

To add a new method that works for both architectures:

1. **Add to `Shared/HelloWorldService.cs`**
2. **Use conditional compilation if needed**
3. **Both projects automatically get the new method**

### **Example:**

```csharp
public string GetSystemInfo()
{
#if X86
    return $"32-bit system running .NET {Environment.Version}";
#else
    return $"64-bit system running .NET {Environment.Version}";
#endif
}
```

## 📋 **Best Practices**

1. **Keep shared logic in the shared file**
2. **Use conditional compilation only for architecture-specific differences**
3. **Maintain consistent method signatures across architectures**
4. **Test both architectures after any changes**
5. **Document any architecture-specific behavior**

## 🎉 **Result**

- **Single source file** to maintain
- **Both architectures** supported
- **No code duplication**
- **Easy to extend** and modify
- **Clean, maintainable** codebase
