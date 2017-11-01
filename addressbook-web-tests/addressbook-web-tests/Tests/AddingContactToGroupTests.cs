using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            List<GroupData> groups = GroupData.GetAll();
            List<ContactData> contacts = ContactData.GetAll();

            groups = appManager.Groups.IsGroupPresents(groups);
            contacts = appManager.Contacts.IsContactPresents(contacts);

            GroupData group = groups[0];
            ContactData contact = contacts[0];
            List<ContactData> oldList = group.GetContacts();

            if (appManager.Groups.SelectGroupWithoutContact(groups, contacts) != null)
            {
                group = appManager.Groups.SelectGroupWithoutContact(groups, contacts);
            }
            else
            {
                appManager.Contacts.Create(new ContactData() { FirstName = "Name", LastName = "Surname" });
                contacts = ContactData.GetAll();
            }
            oldList = group.GetContacts();
            contact = appManager.Contacts.SelectContact(oldList, contacts);

            appManager.Contacts.AddContactToGroup(contact, group);


            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
