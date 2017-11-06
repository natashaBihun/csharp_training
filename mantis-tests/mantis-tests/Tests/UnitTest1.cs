using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class UnitTest1 : TestBase
    {
        [Test]
        public void TestMethod1()
        {
            AccountData account = new AccountData() {
                Name = "xxx",
                Password = "yyy"
            };
            Assert.IsFalse(appManager.James.Verify(account));
            appManager.James.Add(account);
            Assert.IsTrue(appManager.James.Verify(account));
            appManager.James.Delete(account);
            Assert.IsFalse(appManager.James.Verify(account));
        }
    }
}
