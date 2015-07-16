using System;
using CustomAttributes;

namespace CustomTest
{
    
    [TestClass]
    public class ClassA
    {
        [TestMethod]
        [TestCategory("A")]
        public void MethodShouldBeTested()
        { }

        [Ignore]
        public void MethodShouldBeIgnored()
        { }

        [TestMethod]
        [TestCategory("A")]
        [Ignore]
        public void MethodShouldAlsoBeIgnored()
        { }

        [TestMethod]
        [TestCategory("B")]     
        public void MethodShouldBeIgnoredIfCategoryIsA()
        { }
    }
}
