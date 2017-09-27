using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredantials() {
            //prepare
            appManager.Auth.Logout();

            //action
            AccountData newAccountData = new AccountData("admin", "secret");
            appManager.Auth.Login(newAccountData);

            //verification
            Assert.IsTrue(appManager.Auth.IsLoggedIn());
        }

        [Test]
        public void LoginWithInvalidCredantials()
        {
            //prepare
            appManager.Auth.Logout();

            //action
            AccountData newAccountData = new AccountData("admin", "123456");
            appManager.Auth.Login(newAccountData);

            //verification
            Assert.IsFalse(appManager.Auth.IsLoggedIn());
        }
    }
}
