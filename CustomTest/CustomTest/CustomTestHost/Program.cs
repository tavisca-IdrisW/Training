using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using CustomAttributes;
using System.Globalization;

namespace CustomTestHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Assembly assembly = Assembly.Load(args[0]);
            List<string> ignored = new List<string>();
            List<string> tested = new List<string>();
            if (args.Length == 0 || args.Length > 2)
            {
                throw new Exception("Invalid Number of Command-line Arguments!!!");
            }
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsDefined(typeof(TestClass)))
                {
                    foreach (MethodInfo methInfo in type.GetMethods())
                    {
                        var attrArray = methInfo.GetCustomAttributes();

                        //if (methInfo.GetCustomAttributes(typeof(TestMethod), true).Any())
                        if (TestMethod.Exists(methInfo))
                        {
                            if (args.Length == 2)
                            {
                                //if (methInfo.GetCustomAttributes(typeof(TestCategory), true).Any())
                                if (TestCategories.Exists(methInfo))
                                {

                                    //if (methInfo.GetCustomAttributes(typeof(Ignore), true).Any())
                                    if (Ignore.Exists(methInfo))
                                    {
                                        ignored.Add(methInfo.Name);
                                        continue;
                                    }

                                    TestCategories attr = (Attribute)methInfo.GetCustomAttributes(typeof(TestCategories), true).FirstOrDefault() as TestCategories;

                                    if (attr.Categories.ToUpper().Contains(args[1].ToUpper()))
                                    {
                                        tested.Add(methInfo.Name);
                                    }
                                    //else
                                    //    ignored.Add(methInfo.Name);

                                }
                                else
                                {
                                    ignored.Add(methInfo.Name);
                                }
                            }
                            else
                            {
                                if (Ignore.Exists(methInfo))
                                {
                                    ignored.Add(methInfo.Name);
                                    continue;
                                }
                                else
                                {
                                    tested.Add(methInfo.Name);
                                }
                            }
                        }

                    }
                }
            }

            Console.WriteLine("The Tested Methods are: ");

            foreach (string test in tested)
            {
                Console.WriteLine(test);
            }

            Console.WriteLine("\n\nThe Ignored Methods are: ");

            foreach (string ignore in ignored)
            {
                Console.WriteLine(ignore);
            }
            Console.ReadLine();
        }
    }
}
