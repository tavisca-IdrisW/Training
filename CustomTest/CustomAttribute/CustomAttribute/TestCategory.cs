using System;
using System.Reflection;

namespace CustomAttributes
{
    public class TestCategories : Attribute
    {
        public string Categories { get; set; }

        public TestCategories(string categories)
        {
            Categories = categories;
        }

        public static bool Exists(MethodInfo type)
        {
            foreach (object attribute in type.GetCustomAttributes(true))
            {
                if (attribute is TestCategories)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
