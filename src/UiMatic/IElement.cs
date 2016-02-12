namespace ChimpLab.UiMatic
{
    public interface IElement : ICanSearch
    {
        string Text { get; set; }
        void Click();
        bool IsVisible { get; }
        string GetAttribute(string name);
    }
}
