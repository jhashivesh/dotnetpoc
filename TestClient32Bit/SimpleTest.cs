using System;
using System.Reflection;
using System.IO;

namespace TestClient32Bit
{
    public class SimpleTest
    {
        public static void RunTest()
        {
            Console.WriteLine("Simple Direct Assembly Test (32-bit)");
            Console.WriteLine("====================================");
            Console.WriteLine();

            try
            {
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                string assemblyPath = Path.Combine(currentDir, "HelloWorldLib32.dll");
                
                Console.WriteLine($"Looking for assembly at: {assemblyPath}");
                
                if (!File.Exists(assemblyPath))
                {
                    Console.WriteLine("ERROR: HelloWorldLib32.dll not found");
                    return;
                }

                Console.WriteLine("✓ Found HelloWorldLib32.dll");
                
                // Load the assembly
                var assembly = Assembly.LoadFile(assemblyPath);
                Console.WriteLine($"✓ Loaded assembly: {assembly.FullName}");
                
                // Get the type
                var type = assembly.GetType("HelloWorldLib.HelloWorldService");
                if (type == null)
                {
                    Console.WriteLine("ERROR: Could not find HelloWorldService type");
                    return;
                }
                
                Console.WriteLine("✓ Found HelloWorldService type");
                
                // Create instance
                var instance = Activator.CreateInstance(type);
                if (instance == null)
                {
                    Console.WriteLine("ERROR: Could not create instance");
                    return;
                }
                
                Console.WriteLine("✓ Created HelloWorldService instance");
                Console.WriteLine();

                // Test methods
                TestMethod(instance, "GetHelloWorld");
                TestMethod(instance, "GetGreeting", "Developer");
                TestMethod(instance, "GetDotNetVersion");
                TestMethod(instance, "GetCurrentTime");
                TestMethod(instance, "Add", 10, 20);
                TestMethod(instance, "GetPlatformInfo");
                
                Console.WriteLine("\n✓ All tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        static void TestMethod(object instance, string methodName, params object[] parameters)
        {
            try
            {
                var result = instance.GetType().InvokeMember(
                    methodName,
                    BindingFlags.InvokeMethod,
                    null,
                    instance,
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
