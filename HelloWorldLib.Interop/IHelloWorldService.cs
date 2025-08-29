using System;
using System.Runtime.InteropServices;

namespace HelloWorldLib.Interop
{
    /// <summary>
    /// COM interface for HelloWorld service
    /// </summary>
    [ComVisible(true)]
    [Guid("11111111-1111-1111-1111-111111111111")]
    public interface IHelloWorldService
    {
        /// <summary>
        /// Gets a simple hello world message
        /// </summary>
        /// <returns>Hello World message</returns>
        string GetHelloWorld();

        /// <summary>
        /// Gets a personalized greeting
        /// </summary>
        /// <param name="name">Name to greet</param>
        /// <returns>Personalized greeting</returns>
        string GetGreeting(string name);

        /// <summary>
        /// Gets the current .NET version
        /// </summary>
        /// <returns>.NET version information</returns>
        string GetDotNetVersion();

        /// <summary>
        /// Gets the current timestamp
        /// </summary>
        /// <returns>Current date and time</returns>
        DateTime GetCurrentTime();

        /// <summary>
        /// Performs a simple calculation
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Sum of the two numbers</returns>
        int Add(int a, int b);

        /// <summary>
        /// Gets the platform architecture
        /// </summary>
        /// <returns>Platform architecture information</returns>
        string GetPlatformInfo();
    }
}
