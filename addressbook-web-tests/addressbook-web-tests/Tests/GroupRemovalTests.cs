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

            appManager.Groups.IsAnyGroupForRemovePresent();

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
            if (oldGroups.Count != 0) oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
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
                appManager.Groups.IsAnyGroupForRemovePresent();
            }

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
            if (oldGroups.Count != 0) oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
