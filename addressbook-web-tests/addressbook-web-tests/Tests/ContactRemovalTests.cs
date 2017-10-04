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
            if (!appManager.Contacts.IsContactPresent())
            {
                appManager.Contacts.Create(new ContactData() { FirstName = "new contact" });
            }
            appManager.Contacts.Remove(1);
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
                if (!appManager.Contacts.IsContactPresent())
                {
                    appManager.Contacts.Create(new ContactData() { FirstName = "new contact" });
                }
                appManager.Contacts.Remove(1);
            }
        }
    }
}
