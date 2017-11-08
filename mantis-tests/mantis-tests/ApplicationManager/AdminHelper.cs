using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string _baseURL;
        public AdminHelper(ApplicationManager manager, String baseURL)
            : base(manager) {
            _baseURL = baseURL;
        }

        public List<AccountData> GetAllAccounts() {
            List<AccountData> accounts = new List<AccountData>();
            IWebDriver driver = OpenAppAndLogin();
            //driver.Url = _baseURL + "/manage_user_page.php";
            manager.ManagementMenu.GoToManageUserPage();

            IList<IWebElement> rows =
                driver.FindElements(By.XPath("//div[@class='table-responsive']//tr/td[1]"));
            foreach (IWebElement row in rows)
            {
                IWebElement link = row.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match match = Regex.Match(href, @"\d+$");
                string id = match.Value;

                accounts.Add(new AccountData()
                {
                    Id = id,
                    Name = name
                });
            }
            return accounts;
        }
        public void DeleteAccount(AccountData account) {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = _baseURL + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.XPath("//form[@action='manage_user_delete.php']//input[@type='submit']"));
            driver.FindElement(By.XPath("//input[@class='btn btn-primary btn-white btn-round']")).Click();
        }

        private IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = _baseURL + "/login_page.php";

            Type(By.Name("username"), "administrator");
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();

            Type(By.Name("password"), "root");
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();

            return driver;
        }
    }
}
