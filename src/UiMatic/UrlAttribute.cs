using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiMatic
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UrlAttribute : Attribute
    {
        public string Address { get; private set; }
        public string Key { get; private set; }

        public UrlAttribute(string key = null, string address = null)
        {
            Key = key;
            Address = address;
        }
    }
}
