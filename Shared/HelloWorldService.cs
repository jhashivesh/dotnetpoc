using System.Runtime.InteropServices;

namespace HelloWorldLib
{
    /// <summary>
    /// COM-visible Hello World service that can be compiled for both 32-bit and 64-bit
    /// </summary>
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
        /// <summary>
        /// Gets a simple hello world message
        /// </summary>
        /// <returns>Hello World message</returns>
        public string GetHelloWorld()
        {
#if X86
            return "Hello World from .NET Core COM Wrapper (32-bit)!";
#else
            return "Hello World from .NET Core COM Wrapper (64-bit)!";
#endif
        }

        /// <summary>
        /// Gets a personalized greeting
        /// </summary>
        /// <param name="name">Name to greet</param>
        /// <returns>Personalized greeting</returns>
        public string GetGreeting(string name)
        {
#if X86
            return $"Hello {name}! Welcome to .NET Core COM Interop (32-bit)!";
#else
            return $"Hello {name}! Welcome to .NET Core COM Interop (64-bit)!";
#endif
        }

        /// <summary>
        /// Gets the current .NET version
        /// </summary>
        /// <returns>.NET version information</returns>
        public string GetDotNetVersion()
        {
            return Environment.Version.ToString();
        }

        /// <summary>
        /// Gets the current timestamp
        /// </summary>
        /// <returns>Current date and time</returns>
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Performs a simple calculation
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Sum of the two numbers</returns>
        public int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Gets the platform architecture
        /// </summary>
        /// <returns>Platform architecture information</returns>
        public string GetPlatformInfo()
        {
#if X86
            return $"32-bit .NET Core on {Environment.OSVersion.Platform}";
#else
            return $"64-bit .NET Core on {Environment.OSVersion.Platform}";
#endif
        }
    }
}
