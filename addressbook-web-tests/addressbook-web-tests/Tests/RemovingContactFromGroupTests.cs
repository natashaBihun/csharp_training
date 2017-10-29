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
            ContactData contact = ContactData.GetAll()[0];
            List<GroupData> oldList = contact.GetGroups();
            GroupData group = GroupData.GetAll().FirstOrDefault();

            appManager.Contacts.RemoveContactFromGroup(group, contact);

            List<GroupData> newList = contact.GetGroups();
            oldList.RemoveAt(0);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
