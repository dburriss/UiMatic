using System.Collections.Generic;

namespace UiMatic
{
    public interface ICanSearch
    {
        IElement FindById(string selector);
        IElement FindByName(string selector);
        IElement FindByCss(string selector);
        IElement FindByXpath(string selector);

        IEnumerable<IElement> FindElementsById(string selector);
        IEnumerable<IElement> FindElementsByName(string selector);
        IEnumerable<IElement> FindElementsByCss(string selector);
        IEnumerable<IElement> FindElementsByXpath(string selector);
        IEnumerable<IElement> FindElementsByTagName(string selector);
    }
}
