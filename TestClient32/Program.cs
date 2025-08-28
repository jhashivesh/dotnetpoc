using System.Runtime.InteropServices;
using System.Reflection;

namespace TestClient32
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("64-bit Test Client for .NET Core COM Wrapper");
            Console.WriteLine("=============================================");
            Console.WriteLine();

            // First, try the simple direct assembly test
            Console.WriteLine("Testing Direct Assembly Loading:");
            Console.WriteLine("===============================");
            SimpleTest.RunTest();
            
            Console.WriteLine("\n" + "=".PadRight(50, '='));
            Console.WriteLine();

            // Then try the COM interop test
            Console.WriteLine("Testing COM Interop:");
            Console.WriteLine("====================");
            TestComInterop();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void TestComInterop()
        {
            try
            {
                // Try to load the COM library directly
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                string comHostPath = Path.Combine(currentDir, "HelloWorldLib.comhost.dll");
                
                if (!File.Exists(comHostPath))
                {
                    Console.WriteLine($"ERROR: COM host DLL not found at: {comHostPath}");
                    Console.WriteLine("Make sure the COM library is built and copied to the test client directory.");
                    return;
                }

                Console.WriteLine($"✓ Found COM host DLL at: {comHostPath}");
                
                // Try to create COM object using late binding
                Type? comType = Type.GetTypeFromProgID("HelloWorldLib.HelloWorldService");
                
                if (comType == null)
                {
                    Console.WriteLine("WARNING: Could not find COM type via ProgID, trying alternative approach...");
                    
                    // Try to load the assembly directly without version checking
                    try
                    {
                        var assembly = Assembly.LoadFile(Path.Combine(currentDir, "HelloWorldLib.dll"));
                        comType = assembly.GetType("HelloWorldLib.HelloWorldService");
                        
                        if (comType == null)
                        {
                            Console.WriteLine("ERROR: Could not find HelloWorldService type in assembly");
                            return;
                        }
                        
                        Console.WriteLine("✓ Found HelloWorldService type via direct assembly loading");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERROR: Failed to load assembly: {ex.Message}");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("✓ Found COM type via ProgID");
                }

                // Create instance
                object? comObject = Activator.CreateInstance(comType);
                
                if (comObject == null)
                {
                    Console.WriteLine("ERROR: Could not create COM object instance");
                    return;
                }

                Console.WriteLine("✓ Successfully created COM object instance");
                Console.WriteLine();

                // Test various methods
                TestMethod(comObject, "GetHelloWorld");
                TestMethod(comObject, "GetGreeting", "Developer");
                TestMethod(comObject, "GetDotNetVersion");
                TestMethod(comObject, "GetCurrentTime");
                TestMethod(comObject, "Add", 10, 20);

                // Clean up - only call ReleaseComObject for true COM objects
                if (Marshal.IsComObject(comObject))
                {
                    Marshal.ReleaseComObject(comObject);
                    Console.WriteLine("\n✓ COM object released successfully");
                }
                else
                {
                    Console.WriteLine("\n✓ .NET object cleanup completed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        static void TestMethod(object comObject, string methodName, params object[] parameters)
        {
            try
            {
                var result = comObject.GetType().InvokeMember(
                    methodName,
                    System.Reflection.BindingFlags.InvokeMethod,
                    null,
                    comObject,
                    parameters
                );

                Console.WriteLine($"✓ {methodName}: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ {methodName}: ERROR - {ex.Message}");
            }
        }
    }
}
