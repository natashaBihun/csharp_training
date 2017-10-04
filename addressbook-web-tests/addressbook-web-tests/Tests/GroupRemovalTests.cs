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
            if (!appManager.Groups.IsGroupPresent())
            {
                appManager.Groups.Create(new GroupData() { Name = "new group" });
            }
            appManager.Groups.Remove(1);
        }

        [Test]
        public void GroupRemovalByIndexTest()
        {
            int groupIndex = 2;

            if (appManager.Groups.IsGroupPresent(groupIndex))
            {
                appManager.Groups.Remove(groupIndex);
            }
            else
            {
                GroupRemovalTest();
            }
        }
    }
}
