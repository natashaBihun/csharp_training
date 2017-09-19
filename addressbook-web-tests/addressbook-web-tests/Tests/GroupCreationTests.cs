using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData() {
                Name = "name",
                Header = "header",
                Footer = "footer"
            };

            appManager.Groups.Create(group);
           // appManager.Logout.Logout();
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData() {
                Name = "",
                Header = "",
                Footer = ""
            };

            appManager.Groups.Create(group);
          //  appManager.Logout.Logout();
        }
    }
}
