using System;

namespace ChimpLab.UiMatic
{
    public interface IDriver : ICanSearch, IDisposable
    {
        void Navigate(ViewContainer viewContainer);
        void Navigate(string path);
    }
}
