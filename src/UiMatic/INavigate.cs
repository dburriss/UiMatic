namespace ChimpLab.UiMatic
{
    public interface INavigate<TViewContainer> : IHasSelector, IHavePreferredSelectorType where TViewContainer : ViewContainer
    {
        TViewContainer Click();
    }
}
