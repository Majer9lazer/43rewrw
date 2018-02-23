//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Threading;

//namespace ConsoleApp10
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
            
//           ProcessStartInfo processStartInfo = new ProcessStartInfo();
//            processStartInfo.FileName = @"powershell";
//            processStartInfo.Arguments = "ls";

//            processStartInfo.RedirectStandardOutput = true;
//            processStartInfo.UseShellExecute = false;

//            Process.Start(processStartInfo);

//            AppDomain domain = AppDomain.CurrentDomain;
//            using (var process = Process.Start(processStartInfo))
//            {
//                using (StreamReader st = process.StandardOutput)
//                {
//                    File.AppendAllText(domain.BaseDirectory + "a.txt", st.ReadToEnd());
//                }
//            }

//            PerformanceCounter performanceCounter =
//                new PerformanceCounter("Process", "% Processor Time", "chrome", true); ;

//            while (true)
//            {
//                Console.WriteLine(performanceCounter.NextValue() /
//                    Environment.ProcessorCount);
//                Thread.Sleep(250);
//            }

//            AppDomain domain = AppDomain.CurrentDomain;

//            foreach (var item in domain.GetAssemblies())
//            {
//                Console.WriteLine(item.FullName);
//                foreach (var type in item.GetTypes())
//                {
//                    Console.WriteLine(type.FullName);
//                }
//            }
//            Console.WriteLine();

//            domain.Load(new AssemblyName("ConsoleApp9"));

//            foreach (var item in domain.GetAssemblies())
//            {
//                foreach (var type in item.GetTypes().Where(p => p.FullName.Contains("ConsoleApp9")))
//                {
//                    Console.WriteLine(type.FullName);
//                }
//            }

//            AppDomain domain = AppDomain.CurrentDomain;
//            string pathToAssembly = domain.BaseDirectory + "ConsoleApp9.exe";

//            Assembly externalDomainAssembly = Assembly.LoadFrom(pathToAssembly);
//            Type secretUserType = externalDomainAssembly.GetType("ConsoleApp9.SecrerUser");
//            ConstructorInfo constructor = secretUserType.GetConstructor(Type.EmptyTypes);

//            object secretUserObject = constructor.Invoke(new object[] { });

//            MethodInfo voidMethodInfo = secretUserType.GetMethod("SayYourSecret");
//            voidMethodInfo.Invoke(secretUserObject, new object[] { });

//            string pathToDiffAssembly = domain.BaseDirectory + "ConsoleApp9.exe";
//            Assembly domainAssembly = Assembly.LoadFrom(pathToDiffAssembly);
//            Type program = domainAssembly.GetType("ConsoleApp9.Program");

//            foreach (var item in program.GetMethods())
//            {
//                Console.WriteLine(item);
//            }
//            program.GetMethod("Main", BindingFlags.Public | BindingFlags.Static)
//                .Invoke(null, new object[] { new string[] { "HACK " } });
//            /*

//              var csc = new CSharpCodeProvider(new Dictionary<string, string>() {});
//            var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, "test.exe", true);
//            parameters.GenerateExecutable = true;

//            string code = @"
//              using System;
//              public class Test {
//              public static void Main() {
//                    Console.WriteLine(DateTime.Now);
//                    Console.ReadLine();
//              }
//            }";
//            CompilerResults results = csc.CompileAssemblyFromSource(parameters, code);
//            results.Errors.Cast<CompilerError>().ToList().ForEach(error => Console.WriteLine(error.ErrorText));



//            CompilerParameters CompilerParams = new CompilerParameters();
//            string outputDirectory = Directory.GetCurrentDirectory();

//            CompilerParams.GenerateInMemory = true;
//            CompilerParams.TreatWarningsAsErrors = false;
//            CompilerParams.GenerateExecutable = true;
//            CompilerParams.CompilerOptions = "/optimize";
//            CompilerParams.OutputAssembly = "test.exe";

//            string[] references = { "System.dll" };
//            CompilerParams.ReferencedAssemblies.AddRange(references);

//            CSharpCodeProvider provider = new CSharpCodeProvider();
//            CompilerResults compile = provider.CompileAssemblyFromSource(CompilerParams, code);

//            Process.Start(@"C:\Users\Iskander\source\repos\ConsoleApp6\ConsoleApp6\bin\Debug\test.exe");
//             */

//            Console.ReadLine();
//        }
//    }
//}


