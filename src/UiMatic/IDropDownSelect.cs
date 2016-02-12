using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ChimpLab.UiMatic
{
    public interface IDropDownSelect : IHasSelector, IHavePreferredSelectorType
    {
        ISelectionOption SelectedValue { get; }
        IEnumerable<ISelectionOption> Options { get; }
        bool SelectWhere(Expression<Func<ISelectionOption, bool>> filter);
    }

    public interface ISelectionOption
    {
        string Text { get; }
        string Value { get; }
        void Click();
    }
}
