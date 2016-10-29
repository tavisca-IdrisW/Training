using System;
using CustomAttributes;

namespace CustomTest
{
    
    [TestClass]
    public class ClassA
    {
        [TestMethod]
        [TestCategories("A")]
        public void MethodShouldBeTestedIfCategoryA()
        { }

        [Ignore]
        public void MethodShouldBeIgnored()
        { }

        [TestMethod]
        [TestCategories("A")]
        [Ignore]
        public void MethodShouldAlsoBeIgnored()
        { }

        [TestMethod]
        [TestCategories("B")]     
        public void MethodShouldBeIgnoredIfCategoryIsA()
        { }
    }
}
