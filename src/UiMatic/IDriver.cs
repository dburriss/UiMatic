using System;

namespace UiMatic
{
    public interface IDriver : ICanSearch, IDisposable
    {
        IConfiguration Configuration { get; set; }
        void Navigate(ViewContainer viewContainer);
        void Navigate(string path);
    }
}
