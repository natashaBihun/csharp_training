using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest() {
            ContactData newContactData = new ContactData()
            {
                FirstName = "Nata",
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
                NameOfGroup = null
            };

            if (!appManager.Contacts.IsContactPresent())
            {
                appManager.Contacts.Create(new ContactData() { FirstName = "new contact" });
            }
            appManager.Contacts.Modify(1, newContactData);           
        }



        [Test]
        public void ContactModificationByIndexTest()
        {
            ContactData newContactData = new ContactData()
            {
                FirstName = "Nata",
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
                NameOfGroup = null
            };
            int contactInedx = 2;

            if (appManager.Contacts.IsContactPresent(contactInedx))
            {
                appManager.Contacts.Modify(contactInedx, newContactData);
            }
            else
            {
                ContactModificationTest();
            }
        }
    }
}
