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

            appManager.Contacts.IsAnyContactForRemovePresent();

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
                appManager.Contacts.IsAnyContactForRemovePresent();
            }

            List<ContactData> newContacts = appManager.Contacts.GetContactList();
            if (oldContacts.Count != 0) oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
