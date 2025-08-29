using System;
using System.Runtime.InteropServices;
using HelloWorldLib.Interop;

namespace HelloWorldLib
{
    /// <summary>
    /// 32-bit COM-visible Hello World service
    /// </summary>
    [ComVisible(true)]
    [Guid("87654321-4321-4321-4321-210987654321")]
    [ProgId("HelloWorldLib.HelloWorldService32")]
    [ClassInterface(ClassInterfaceType.None)]
    public class HelloWorldService : IHelloWorldService
    {
        /// <summary>
        /// Gets a simple hello world message
        /// </summary>
        /// <returns>Hello World message</returns>
        public string GetHelloWorld()
        {
            return "Hello World from .NET Core COM Wrapper (32-bit)!";
        }

        /// <summary>
        /// Gets a personalized greeting
        /// </summary>
        /// <param name="name">Name to greet</param>
        /// <returns>Personalized greeting</returns>
        public string GetGreeting(string name)
        {
            return $"Hello {name}! Welcome to .NET Core COM Interop (32-bit)!";
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
            return $"32-bit .NET Core on {Environment.OSVersion.Platform}";
        }
    }
}
