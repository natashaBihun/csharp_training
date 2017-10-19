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
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30))
                {
                    FirstName = GenerateRandomString(100),
                    LastName = GenerateRandomString(100),
                    Title = GenerateRandomString(100),
                    Company = GenerateRandomString(100),
                    Address = GenerateRandomString(100),
                    HomePhone = GenerateRandomString(100),
                    MobilePhone = GenerateRandomString(100)
                });
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contactData)
        {
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
