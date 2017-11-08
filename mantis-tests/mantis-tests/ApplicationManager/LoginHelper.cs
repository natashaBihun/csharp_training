using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager)
            : base(manager) { }

        internal void LoginAsAdministrator()
        {
            Login(new AccountData()
            {
                Name = "administrator",
                Password = "root"
            });
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            Type(By.Name("username"), account.Name);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            Type(By.Name("password"), account.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }
        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.XPath("//a[@class='dropdown-toggle'][span[@class='user-info']]")).Click();
                driver.FindElement(By.XPath("//li[@class='divider']/following-sibling::li[1]/a")).Click();
            }
        }
        public bool IsLoggedIn()
        {
            return IsElementPresent(By.XPath("//ul[@class='nav ace-nav']"));
        }
        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn() && GetLoggetUserName() == account.Name;
        }
        public string GetLoggetUserName()
        {
            string text = driver.FindElement(By.CssSelector("span.user-info")).Text;
            return text.Substring(1, text.Length - 1);
        }
    }
}
