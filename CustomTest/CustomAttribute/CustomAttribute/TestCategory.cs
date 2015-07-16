using System;

namespace CustomAttributes
{
    public class TestCategory : Attribute
    {
        public string Category { get; set; }

        public TestCategory(string category)
        {
            Category = category;
        }
    }
}
