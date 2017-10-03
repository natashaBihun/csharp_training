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
            int groupIndex = 3;

            if (appManager.Groups.IsGroupPresent())
            {
                if (appManager.Groups.IsGroupWithIndexPresent(groupIndex))
                {
                    appManager.Groups.Remove(groupIndex);
                }
                else
                {
                    groupIndex = 1;
                    appManager.Groups.Remove(groupIndex);
                }
            }
            else
            {
                appManager.Groups.Create(new GroupData() { Name = "new group" });
                groupIndex = 1;
                appManager.Groups.Remove(groupIndex);
            }            
            List<GroupData> oldGroups = appManager.Groups.GetGroupList();
            appManager.Groups.Remove(0);

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
