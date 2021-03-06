﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace WebAddressbookTests
{
    public class ApplicationManager
    {
        private static ThreadLocal<ApplicationManager> appManager = new ThreadLocal<ApplicationManager>();

        protected IWebDriver driver;
        protected string baseURL;

        protected LoginHelper loginHelper;
        protected LogoutHelper logoutHelper;
        protected NavigationHelper navigationHelper;
        protected GroupHelper groupHelper;
        protected ContactHelper contactHelper;

        public IWebDriver Driver {
            get {
                return driver;
            }
        }
        public LoginHelper Auth {
            get {
                return loginHelper;
            }
        }
        public LogoutHelper Logout {
            get  {
                return logoutHelper;
            }
        }
        public NavigationHelper Navigator {
            get {
                return navigationHelper;
            }
        }
        public GroupHelper Groups {
            get {
                return groupHelper;
            }
        }
        public ContactHelper Contacts {
            get {
                return contactHelper;
            }
        }

        private ApplicationManager() {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"C:\\Program Files\\Mozilla Firefox\\firefox.exe";
            options.UseLegacyImplementation = true;
            driver = new FirefoxDriver(options);
            baseURL = "http://localhost";

            loginHelper = new LoginHelper(this);
            logoutHelper = new LogoutHelper(this);
            navigationHelper = new NavigationHelper(this, baseURL);
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }

        ~ApplicationManager() {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()  {
            if (!appManager.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigator.GoToHomePage();
                appManager.Value = newInstance;
            }
            return appManager.Value;
        }
    }
}
