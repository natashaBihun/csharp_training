using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_tests_white
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest() {
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData toBeRemoved = oldGroups[0];

            if (oldGroups.Count <= 1)
            {
                app.Groups.Add(new GroupData() { Name = "new group item" });
                oldGroups = app.Groups.GetGroupList();
            }
            app.Groups.Remove(toBeRemoved);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            if (oldGroups.Count != 0) oldGroups.RemoveAt(0);
            oldGroups.Sort();
            newGroups.Sort();
            System.Console.Out.WriteLine("old " + oldGroups.Count);
            System.Console.Out.WriteLine("new " + newGroups.Count);
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
