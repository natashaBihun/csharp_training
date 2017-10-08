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

            Assert.AreEqual(oldContacts.Count - 1, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactList();
            ContactData toBeRemoved = oldContacts[0];
            if (oldContacts.Count != 0) oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
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
                contactInedx = 1;
                appManager.Contacts.Remove(contactInedx);
            }

            Assert.AreEqual(oldContacts.Count - 1, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactList();
            ContactData toBeRemoved = oldContacts[contactInedx - 1];
            if (oldContacts.Count >= contactInedx) oldContacts.RemoveAt(contactInedx - 1);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
