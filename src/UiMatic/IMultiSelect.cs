using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UiMatic
{
    public interface IMultiSelect : IHasSelector, IHavePreferredSelectorType
    {
        IEnumerable<IMultiSelectionOption> SelectedValues { get; }
        IEnumerable<IMultiSelectionOption> Options { get; }
        bool SelectWhere(Expression<Func<IMultiSelectionOption, bool>> filter);
    }

    public interface IMultiSelectionOption : ISelectionOption
    {
        void ControlClick();
    }
}
