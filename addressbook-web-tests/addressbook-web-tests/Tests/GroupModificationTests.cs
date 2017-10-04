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

            if (!appManager.Groups.IsGroupPresent())
            {
                appManager.Groups.Create(new GroupData() { Name = "new group" });
            }
            appManager.Groups.Modify(1, newData);
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
        }
    }
}
