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

            appManager.Groups.IsAnyGroupForModifyPresent(newData);
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
                appManager.Groups.IsAnyGroupForModifyPresent(newData);
            }
        }
    }
}
