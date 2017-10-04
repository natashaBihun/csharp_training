using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData() {
                Name = "new name",
                Header = null,
                Footer = "new footer"
            };
            List<GroupData> oldGroups = appManager.Groups.GetGroupList();

            if (!appManager.Groups.IsGroupPresent())
            {
                appManager.Groups.Create(new GroupData() { Name = "new group" });
            }
            appManager.Groups.Modify(1, newData);

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
            if (oldGroups.Count != 0) oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void GroupModificationByIndexTest()
        {
            GroupData newData = new GroupData()
            {
                Name = "new name",
                Header = null,
                Footer = "new footer"
            };
            List<GroupData> oldGroups = appManager.Groups.GetGroupList();
            int groupIndex = 2;

            if (appManager.Groups.IsGroupPresent(groupIndex))
            {
                appManager.Groups.Modify(groupIndex, newData);
            }
            else
            {
                if (!appManager.Groups.IsGroupPresent())
                {
                    appManager.Groups.Create(new GroupData() { Name = "new group" });
                }
                appManager.Groups.Modify(1, newData);
            }

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
            if (oldGroups.Count != 0) oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
