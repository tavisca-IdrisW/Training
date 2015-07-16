using System;
using CustomAttributes;

namespace CustomTest
{
    class ClassD
    {
        public void MethodShouldBeIgnored1()
        { }

        [TestMethod]
        public void MethodShouldBeIgnored2()
        { }

        [TestMethod]
        public void MethodShouldBeIgnored3()
        { }
    }
}
