using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemovingContactFromGroupTest()
        {
            List<GroupData> groups = GroupData.GetAll();
            List<ContactData> contacts = ContactData.GetAll();

            groups = appManager.Groups.IsGroupPresents(groups);
            contacts = appManager.Contacts.IsContactPresents(contacts);

            GroupData group = groups.First();
            ContactData contact = contacts.First();
            List<ContactData> oldList = group.GetContacts();

            if (appManager.Groups.SelectGroupWithContact(groups, contacts) != null) {
                group = appManager.Groups.SelectGroupWithContact(groups, contacts);
            }
            else {
                appManager.Contacts.AddContactToGroup(contact, group);
            }
            oldList = group.GetContacts();
            contact = oldList.FirstOrDefault();

            appManager.Contacts.RemoveContactFromGroup(group, contact);

            List<ContactData> newList = group.GetContacts();
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
