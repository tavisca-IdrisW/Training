using System;
using System.Reflection;

namespace CustomAttributes
{
    public class Ignore : Attribute
    {
        public static bool Exists(MethodInfo type)
        {
            foreach (object attribute in type.GetCustomAttributes(true))
            {
                if (attribute is Ignore)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
