using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
       [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = appManager.Groups.GetGroupList();
            if (!appManager.Groups.IsGroupPresent())
            {
                appManager.Groups.Create(new GroupData() { Name = "new group" });
            }
            appManager.Groups.Remove(1);

            Assert.AreEqual(oldGroups.Count - 1, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
            GroupData toBeRemoved = oldGroups[0];
            if (oldGroups.Count != 0) oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups) {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }

        [Test]
        public void GroupRemovalByIndexTest()
        {
            List<GroupData> oldGroups = appManager.Groups.GetGroupList();
            int groupIndex = 2;

            if (appManager.Groups.IsGroupPresent(groupIndex))
            {
                appManager.Groups.Remove(groupIndex);
            }
            else
            {
                if (!appManager.Groups.IsGroupPresent())
                {
                    appManager.Groups.Create(new GroupData() { Name = "new group" });
                }
                groupIndex = 1;
                appManager.Groups.Remove(groupIndex);
            }

            Assert.AreEqual(oldGroups.Count - 1, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
            GroupData toBeRemoved = oldGroups[groupIndex - 1];
            if (oldGroups.Count >= groupIndex) oldGroups.RemoveAt(groupIndex - 1);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
