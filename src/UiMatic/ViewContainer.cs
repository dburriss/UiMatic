using System;
using System.Collections.Generic;

namespace UiMatic
{
    public abstract class ViewContainer : ICanSearch
    {
        public abstract IElement FindByCss(string selector);
        public abstract IElement FindById(string selector);
        public abstract IElement FindByName(string selector);
        public abstract IElement FindByXpath(string selector);
        public abstract IEnumerable<IElement> FindElementsByCss(string selector);
        public abstract IEnumerable<IElement> FindElementsById(string selector);
        public abstract IEnumerable<IElement> FindElementsByName(string selector);
        public abstract IEnumerable<IElement> FindElementsByTagName(string selector);
        public abstract IEnumerable<IElement> FindElementsByXpath(string selector);

        #if NET46 || NET452 || NET451 || DNX46 || DNX452 || DNX451
        protected void WaitUntil(Predicate<string> breakWhen, Func<string> evaluation, int timeout = 60)
        {
            for (int second = 0; ; second++)
            {
                if (second >= timeout)
                    throw new TimeoutException();
                try
                {
                    if (breakWhen.Invoke(evaluation()))
                        break;
                }
                catch (Exception)
                {
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
        #endif

    }
}
