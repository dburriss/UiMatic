using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UiMatic
{
    public interface IClickable : IHasSelector, IHavePreferredSelectorType
    {
        void Click();
        void Click(Action action);
    }
}
