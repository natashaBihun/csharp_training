using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace mantis_tests
{
    public class ApplicationManager
    {
        private static ThreadLocal<ApplicationManager> appManager = new ThreadLocal<ApplicationManager>();

        protected IWebDriver driver;
        protected string baseURL;

        public IWebDriver Driver {
            get {
                return driver;
            }
        }

        public RegistrationHelper Registration { get; private set; }
        public FtpHelper Ftp { get; private set; }
        public JamesHelper James { get; private set; }
        public MailHelper Mail { get; private set; }
        public ProjectHelper Project { get; private set; }
        public LoginHelper Login { get; private set; }
        public ManagementMenuHelper ManagementMenu { get; private set; }

        private ApplicationManager() {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"C:\\Program Files\\Mozilla Firefox\\firefox.exe";
            options.UseLegacyImplementation = true;
            driver = new FirefoxDriver(options);
            baseURL = "http://localhost/mantisbt-2.8.0";
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);
            Project = new ProjectHelper(this);
            Login = new LoginHelper(this);
            ManagementMenu = new ManagementMenuHelper(this, baseURL);
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
                newInstance.driver.Url = @"http://localhost/mantisbt-2.8.0/login_page.php";
                appManager.Value = newInstance;
            }
            return appManager.Value;
        }
    }
}
