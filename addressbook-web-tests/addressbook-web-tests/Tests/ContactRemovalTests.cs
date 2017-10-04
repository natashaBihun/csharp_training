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
            List<ContactData> oldContacts = appManager.Contacts.GetContactList();

            if (!appManager.Contacts.IsContactPresent())
            {
                appManager.Contacts.Create(new ContactData() { FirstName = "new contact" });
            }
            appManager.Contacts.Remove(1);

            List<ContactData> newContacts = appManager.Contacts.GetContactList();
            if (oldContacts.Count != 0) oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }


        [Test]
        public void ContactRemovalByIndexTest()
        {
            List<ContactData> oldContacts = appManager.Contacts.GetContactList();
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

            List<ContactData> newContacts = appManager.Contacts.GetContactList();
            if (oldContacts.Count != 0) oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
