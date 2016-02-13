using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UiMatic
{
    /// <summary>
    /// Only works with Name selector
    /// </summary>
    public interface IRadioGroup : IHasSelector, IHavePreferredSelectorType
    {
        IRadioButton SelectedValue { get; }
        IEnumerable<IRadioButton> Group { get; }
        bool SelectWhere(Expression<Func<IRadioButton, bool>> filter);
    }

    public interface IRadioButton
    {
        string Name { get; }
        string Value { get; }
        void Click();
    }
}
