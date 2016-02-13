namespace UiMatic
{
    public interface IInput : IHasSelector, IHavePreferredSelectorType
    {
        string Value { get; set; }
    }
}
