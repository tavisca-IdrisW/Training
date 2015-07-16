using System;
using CustomAttributes;

namespace CustomTest
{
    [TestClass]
    class ClassB
    {
        [TestMethod]
        public void MethodShouldBeInTested()
        { }

        [TestMethod]
        [Ignore]
        public void MethodShouldBeInIgnored()
        { }

        public void MethodBC()
        { }

        public void MethodBD()
        { }
    }
}
