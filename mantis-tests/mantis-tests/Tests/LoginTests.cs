using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginTest() {
            appManager.Login.Logout();
            
            AccountData account = new AccountData()
            {
                Name = "testuser15",
                Password = "password"
            };
            appManager.Login.Login(account);
            Assert.IsTrue(appManager.Login.IsLoggedIn());
        }
    }
}
