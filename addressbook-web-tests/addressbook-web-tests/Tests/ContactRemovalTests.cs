using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            appManager.Contacts.IsAnyContactForRemovePresent();
        }


        [Test]
        public void ContactRemovalByIndexTest()
        {
            int contactInedx = 2;

            if (appManager.Contacts.IsContactPresent(contactInedx))
            {
                appManager.Contacts.Remove(contactInedx);
            }
            else
            {
                appManager.Contacts.IsAnyContactForRemovePresent();
            }
        }
    }
}
