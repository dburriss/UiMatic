using System;

namespace ChimpLab.UiMatic
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SelectorAttribute : Attribute
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Css { get; private set; }
        public string XPath { get; private set; }


        public SelectorAttribute(string id = null, string name = null, string css = null, string xpath = null)
        {
            Id = id;
            Name = name;
            Css = css;
            XPath = xpath;
        }
    }
}