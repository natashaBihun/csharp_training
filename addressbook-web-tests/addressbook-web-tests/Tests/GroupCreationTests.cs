using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData() {
                Name = "name",
                Header = "header",
                Footer = "footer"
            };

            List<GroupData> oldGroups = appManager.Groups.GetGroupList();
            appManager.Groups.Create(group);

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
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
        }
    }
}
