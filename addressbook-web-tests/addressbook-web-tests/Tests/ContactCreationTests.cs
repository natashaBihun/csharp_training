using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contactData = new ContactData() {
                FirstName = "Natasha",
                LastName = "Bihun",
                Title = "Contact info",
                Company = "SharpMinds",
                Address = "Ukraine",
                HomePhone = "0978271133",
                MobilePhone = "0978271138",
                Email = "nbihun@sharpminds.com",
                BDay = "20",
                BMonth = "October",
                BYear = "1991",
                NameOfGroup = "[none]"
            };
            List<ContactData> oldContacts = appManager.Contacts.GetContactList();
            appManager.Contacts.Create(contactData);

            Assert.AreEqual(oldContacts.Count + 1, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactList();
            oldContacts.Add(contactData);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
