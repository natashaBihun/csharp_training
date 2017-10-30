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

            if (groups.Count != 0 && contacts.Count != 0)
            {
                for (int i = 0; i < groups.Count; i++)
                {
                    List<ContactData> oldList = groups[i].GetContacts();
                    if (oldList.Count != 0)
                    {
                        ContactData contact = oldList.FirstOrDefault();

                        appManager.Contacts.RemoveContactFromGroup(groups[i], contact);

                        List<ContactData> newList = groups[i].GetContacts();
                        oldList.RemoveAt(0);
                        oldList.Sort();
                        newList.Sort();
                        Assert.AreEqual(oldList, newList);
                        break;
                    }
                    else if (i == (groups.Count - 1)) System.Console.Out.WriteLine("All groups haven't mapped any contacts");
                }
            }
            else System.Console.Out.WriteLine("No groups or contacts");
        }
    }
}
