using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace UiMatic.SeleniumWebDriver.Controls
{
    public class RadioGroup : IRadioGroup
    {
        private readonly IDriver driver;

        public Selector Selector { get; set; }
        public SelectorType PreferredSelectorType { get { return SelectorType.Name; } }
        public RadioGroup(IDriver driver)
        {
            this.driver = driver;
        }

        public IEnumerable<IRadioButton> Group
        {
            get
            {
                var el = this.Selector.ReturnElements(this.driver);
                return ExtractButtons(el);
            }
        }

        private IEnumerable<IRadioButton> ExtractButtons(IEnumerable<IElement> elements)
        {
            foreach (var radio in elements)
            {                
                yield return new RadioButton(radio);
            }
        }

        public bool SelectWhere(Expression<Func<IRadioButton, bool>> filter)
        {
            var filtered = Group.AsQueryable().Where(filter);
            if(filtered.Any())
            {
                filtered.First().Click();
                return true;
            }
            return false;
        }

        public IRadioButton SelectedValue
        {
            get
            {
                var radioButtons = Selector.ReturnElements(this.driver);
                foreach (var button in radioButtons)
                {
                    var isChecked = button.GetAttribute("checked") != null;
                    if (isChecked)
                        return new RadioButton(button);
                }
                return null;
            }
        }
    }


 
}
