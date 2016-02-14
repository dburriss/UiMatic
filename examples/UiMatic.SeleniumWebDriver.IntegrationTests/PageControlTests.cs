using UiMatic.SeleniumWebDriver.IntegrationTests.Pages;
using Xunit;

namespace UiMatic.SeleniumWebDriver.IntegrationTests
{
    public class PageControlTests : TestBase
    {
        [Theory]
        [InlineData(TestTarget.Chrome)]
        public void Login_And_Save_Profile_Gives_Success_Message(TestTarget target)
        {
            using (IDriver driver = GetDriver(target))
            {
                var contactPage = Page.Create<ContactPage>(driver).Go<ContactPage>();
                contactPage.FirstName.Value = "Devon";
                contactPage.LastName.Value = "Burriss";
                contactPage.Email.Value = "bob@builder.com";
                contactPage.Gender.SelectWhere(x => x.Value == "Male");
                contactPage.LeadSource.SelectWhere(x => x.Value == "A friend");
                contactPage.Skills.SelectWhere(x => int.Parse(x.Value) % 2 == 0);//select the even values
                contactPage.Message.Value = "Almost there... Casper going to click the button now.";
                contactPage.Newsletter.Click();
                bool newsletter = contactPage.Newsletter.IsChecked;
                var successPage = contactPage.SaveBtn.Click();

                var message = successPage.GetMessage();

                Assert.True(newsletter);
                Assert.Equal("Thank you.", message);
            }
        }
    }
}
