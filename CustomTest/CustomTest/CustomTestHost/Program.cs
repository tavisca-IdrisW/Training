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
            //Loads an assembly into the reflection-only context, where it can be examined but not executed.
            Assembly assembly = Assembly.Load(args[0]);
            List<string> ignored = new List<string>();
            List<string> tested = new List<string>();
            if (args.Length == 2)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsDefined(typeof(TestClass)))
                    {
                        foreach (MethodInfo methInfo in type.GetMethods())
                        {
                            var attrArray = methInfo.GetCustomAttributes();

                            if (methInfo.GetCustomAttributes(typeof(TestMethod), true).Any())
                            {
                                if (methInfo.GetCustomAttributes(typeof(Ignore), true).Any())
                                {
                                    ignored.Add(methInfo.Name);
                                }
                                else if (methInfo.GetCustomAttributes(typeof(TestCategory), true).Any())
                                {
                                    TestCategory attr = (Attribute)methInfo.GetCustomAttributes(typeof(TestCategory), true).FirstOrDefault() as TestCategory;
                                    if (attr.Category.Equals(args[1], StringComparison.OrdinalIgnoreCase))
                                        tested.Add(methInfo.Name);
                                    else
                                        ignored.Add(methInfo.Name);

                                }
                                else
                                {
                                    ignored.Add(methInfo.Name);
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsDefined(typeof(TestClass)))
                    {
                        foreach (MethodInfo methInfo in type.GetMethods())
                        {
                            var attrArray = methInfo.GetCustomAttributes();

                            if (methInfo.GetCustomAttributes(typeof(TestMethod), true).Any())
                            {
                                if (methInfo.GetCustomAttributes(typeof(Ignore), true).Any())
                                {
                                    ignored.Add(methInfo.Name);
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
