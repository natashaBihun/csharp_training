using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        }
    }
}
