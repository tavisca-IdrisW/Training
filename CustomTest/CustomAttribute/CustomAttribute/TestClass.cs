using System;
using System.Reflection;

namespace CustomAttributes
{
    public class TestClass : Attribute
    {
        public static bool Exists(MethodInfo type)
        {
            foreach (object attribute in type.GetCustomAttributes(true))
            {
                if (attribute is TestClass)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
