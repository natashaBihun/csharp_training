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
            appManager.Groups.IsAnyGroupForRemovePresent();
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
                appManager.Groups.IsAnyGroupForRemovePresent();
            }
        }
    }
}
