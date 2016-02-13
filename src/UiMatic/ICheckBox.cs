using System;

namespace UiMatic
{
    public interface ICheckBox : IClickable, IHasSelector, IHavePreferredSelectorType
    {
        //void Click();
        //void Click(Action action);
        string Value { get; }
        bool IsChecked { get; }
    }
}
