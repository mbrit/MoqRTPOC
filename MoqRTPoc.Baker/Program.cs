﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Moq.Baking;
using SQLite;

namespace MoqRTPoc.Baker
{
    class Program
    {
        private static string RootFolder = @"C:\Code\MoqRTPoc\MoqRTPoc.WinRTTest\bin\Debug\AppX\";

        static void Main(string[] args)
        {
            try
            {
                // path...
                var path = Path.Combine(RootFolder, "MoqRTPoc.WinRTTest.dll");
                
                // watch...
                var asm = Assembly.LoadFrom(path);

                // find the moqrt runtime class - we have to do this via reflection...
                Type rt = null;
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                foreach (var referenceName in asm.GetReferencedAssemblies().ToList().Where(v => v.Name.StartsWith("Moq")))
                {
                    var reference = Assembly.Load(referenceName);
                    rt = reference.GetType("Moq.MoqRTRuntime");
                }

                // reset the baking database...
                var bakingPath = Path.Combine(RootFolder, "MoqRTBaking.db");
                if (File.Exists(bakingPath))
                    File.Delete(bakingPath);

                // run...
                var configureMethod = rt.GetMethod("ConfigureBaking", BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
                configureMethod.Invoke(null, new object[] { bakingPath });

                // more...
                var setRunningMethodMethod = rt.GetMethod("SetRunningMethod", BindingFlags.Static | BindingFlags.Public);

                // walk...
                var testInstances = new List<object>();
                foreach (var type in asm.GetTypes())
                {
                    // walk the attributes...
                    foreach (var attr in type.GetCustomAttributes())
                    {
                        if (attr.GetType().Name == "TestClassAttribute") // fudge - easier than referencing the WinRT test framework...
                            testInstances.Add(Activator.CreateInstance(type));
                    }
                }

                // walk each type...
                foreach (var testInstance in testInstances)
                {
                    foreach (var method in testInstance.GetType().GetMethods())
                    {
                        foreach (var attr in method.GetCustomAttributes())
                        {
                            if (attr.GetType().Name == "TestMethodAttribute")
                            {
                                Console.Write(string.Format("Running '{0}' on '{1}': ", method.Name, testInstance.GetType().Name));

                                // tell it we're doing it...
                                setRunningMethodMethod.Invoke(null, new object[] { testInstance, method });

                                // run it...
                                try
                                {
                                    method.Invoke(testInstance, null);
                                    Console.Write("No failure");
                                }
                                catch (Exception ex)
                                {
                                    bool first = true;
                                    var walk = ex;
                                    while (walk != null)
                                    {
                                        if (first)
                                            first = false;
                                        else
                                            Console.Write(", ");

                                        Console.Write(walk.GetType().Name);
                                        walk = walk.InnerException;
                                    }
                                }

                                Console.WriteLine();
                            }
                        }
                    }
                }

                // load...
                Console.WriteLine("=========================================");
                Console.WriteLine("BAKING INSTRUCTIONS:");
                Console.WriteLine(bakingPath);
                Console.WriteLine();

                // read them out...
                using (var conn = new SQLiteConnection(bakingPath))
                {
                    foreach (var instance in conn.Table<InstanceItem>())
                    {
                        foreach (var method in conn.Table<MethodItem>().Where(v => v.InstanceItemId == instance.Id))
                        {
                            foreach (var baking in conn.Table<BakingItem>().Where(v => v.MethodItemId == method.Id))
                                Console.WriteLine(string.Format("{0}\r\n\t{1}\r\n\t\t{2}", instance.Type, method.Name, baking.Expression));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (Debugger.IsAttached)
                    Console.ReadLine();
            }
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            int index = args.Name.IndexOf(",");
            string path = Path.Combine(@"C:\Code\MoqRTPoc\MoqRT\bin\Debug", args.Name.Substring(0, index) + ".dll");
            if (File.Exists(path))
                return Assembly.LoadFrom(path);
            else
                return null;
        }

        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.FullPath);
        }
    }
}
