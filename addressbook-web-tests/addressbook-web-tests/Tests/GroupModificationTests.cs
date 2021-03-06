﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData() {
                Name = "new name",
                Header = null,
                Footer = "new footer"
            };
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeModify = oldGroups[0];

            if (!appManager.Groups.IsGroupPresent())
            {
                appManager.Groups.Create(new GroupData() { Name = "new group" });
            }
            appManager.Groups.Modify(toBeModify, newData);

            Assert.AreEqual(oldGroups.Count, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            if (oldGroups.Count != 0) oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups) {
                if (group.Id == toBeModify.Id) {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
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
            GroupData oldData = oldGroups[0];

            if (appManager.Groups.IsGroupPresent(groupIndex))
            {
                oldData = oldGroups[groupIndex - 1];
                appManager.Groups.Modify(groupIndex, newData);
            }
            else
            {
                if (!appManager.Groups.IsGroupPresent())
                {
                    appManager.Groups.Create(new GroupData() { Name = "new group" });
                }
                groupIndex = 1;
                appManager.Groups.Modify(groupIndex, newData);                
            }

            Assert.AreEqual(oldGroups.Count, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
            if (oldGroups.Count >= groupIndex) oldGroups[groupIndex - 1].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
