using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class NavigationHelper : HelperBase
    {
        private string _baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager) {
            _baseURL = baseURL;
        }
        public void GoToHomePage()
        {
            if (driver.Url == _baseURL + "/addressbook/") {
                return;
            }
            driver.Navigate().GoToUrl(_baseURL + "/addressbook/");
        }
        public void GoToGroupsPage()
        {
            if (driver.Url == _baseURL + "/addressbook/group.php"
                && IsElementPresent(By.Name("new"))) {
                return;
            }
            driver.FindElement(By.LinkText("groups")).Click();
        }
        public void GoToContactsPage()
        {
            if (driver.Url == _baseURL + "/addressbook/")
            {
                return;
            }
            driver.FindElement(By.LinkText("home")).Click();
        }
    }
}
