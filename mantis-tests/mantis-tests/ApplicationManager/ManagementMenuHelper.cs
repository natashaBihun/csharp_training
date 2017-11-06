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
    public class ManagementMenuHelper : HelperBase
    {
        private string _baseURL;
        public ManagementMenuHelper(ApplicationManager manager, string baseURL)
            : base(manager) {
            _baseURL = baseURL;
        }
        public void GoToHomePage()
        {
            if (driver.Url == _baseURL + "/my_view_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl(_baseURL + "/my_view_page.php");
        }
        public void GoToManagementPage()
        {
            if (driver.Url == _baseURL + "/manage_overview_page.php"
                && IsElementPresent(By.XPath("//ul[@class='nav nav-tabs padding-18']")))
            {
                return;
            }
            driver.Navigate().GoToUrl(_baseURL + "/manage_overview_page.php");
        }
        public void GoToProjectManagementPage()
        {
            if (driver.Url == _baseURL + "/manage_proj_page.php"
                && IsElementPresent(By.XPath("//input[@name='manage_proj_create_page_token']")))
            {
                return;
            }
            driver.Navigate().GoToUrl(_baseURL + "/manage_proj_page.php");
        }
    }
}
