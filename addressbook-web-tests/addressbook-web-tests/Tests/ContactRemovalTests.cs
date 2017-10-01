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
        public void ContactRemovalTest() {
            int contactInedx = 3;

            if (appManager.Contacts.IsContactWithIndexPresent(contactInedx))
            {
                appManager.Contacts.Remove(contactInedx);
            }
            else
            {
                contactInedx = 1;
                if (appManager.Contacts.IsContactPresent())
                {
                    appManager.Contacts.Remove(contactInedx);
                }
                else
                {
                    appManager.Contacts.Create(new ContactData() { FirstName = "new contact" });
                    appManager.Contacts.Remove(contactInedx);
                }
            }
        }
    }
}
