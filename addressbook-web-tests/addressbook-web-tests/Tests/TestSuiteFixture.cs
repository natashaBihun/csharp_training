using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [SetUpFixture]
    public class TestSuiteFixture
    {
        // [SetUp]
        [OneTimeSetUp]
        public void InitApplicationManager() {
            ApplicationManager appManager = ApplicationManager.GetInstance();
            appManager.Navigator.GoToHomePage();
            appManager.Auth.Login(new AccountData("admin", "secret"));
        }
    }
}
