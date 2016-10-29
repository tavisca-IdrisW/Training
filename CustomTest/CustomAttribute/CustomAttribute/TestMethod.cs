using System;
using System.Reflection;

namespace CustomAttributes
{
    public class TestMethod : Attribute
    {
        public static bool Exists(MethodInfo type)
        {
            foreach (object attribute in type.GetCustomAttributes(true))
            {
                if (attribute is TestMethod)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
