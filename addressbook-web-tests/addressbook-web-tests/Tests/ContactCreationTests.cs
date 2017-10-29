using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        [Test, TestCaseSource("ContactDataFromXMLFile")]
        public void ContactCreationTest(ContactData contactData)
        {
            List<ContactData> oldContacts = ContactData.GetAll();
            appManager.Contacts.Create(contactData);

            Assert.AreEqual(oldContacts.Count + 1, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Add(contactData);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void TestDBContactConnectivity()
        {
            DateTime start = DateTime.Now;
            List<ContactData> fromUI = appManager.Contacts.GetContactList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<ContactData> fromDB = ContactData.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }

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

        public static IEnumerable<ContactData> ContactDataFromXMLFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(Path.Combine(TestContext.CurrentContext.WorkDirectory, @"contacts.xml")));
        }

        public static IEnumerable<ContactData> ContactDataFromJSONFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(Path.Combine(TestContext.CurrentContext.WorkDirectory, @"contacts.json")));
        }
    }
}
