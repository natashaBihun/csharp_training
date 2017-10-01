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
            int groupIndex = 3;

            if (appManager.Groups.IsGroupPresent())
            {
                if (appManager.Groups.IsGroupWithIndexPresent(groupIndex))
                {
                    appManager.Groups.Modify(groupIndex, newData);
                }
                else
                {
                    groupIndex = 1;
                    appManager.Groups.Modify(groupIndex, newData);
                }
            }
            else
            {
                appManager.Groups.Create(new GroupData() { Name = "new group" });
                groupIndex = 1;
                appManager.Groups.Modify(groupIndex, newData);
            }
        }
    }
}
