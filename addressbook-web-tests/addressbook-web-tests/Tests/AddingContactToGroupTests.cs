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
        public void AddingContactToGroupTest() {
            List<GroupData> groups = GroupData.GetAll();
            List<ContactData> contacts = ContactData.GetAll();

            if (groups.Count != 0 && contacts.Count != 0 )
            {
                for (int i = 0; i < groups.Count; i++) {
                    List<ContactData> oldList = groups[i].GetContacts();
                    if (oldList.Count < contacts.Count)
                    {
                        string contactId = contacts.Select(t => t.Id)
                            .Except(oldList.Select(t => t.Id))
                            .ToList()
                            .FirstOrDefault();

                        ContactData contact = contacts.Where(t => t.Id == contactId).FirstOrDefault();

                        appManager.Contacts.AddContactToGroup(contact, groups[i]);

                        List<ContactData> newList = groups[i].GetContacts();
                        oldList.Add(contact);
                        oldList.Sort();
                        newList.Sort();
                        Assert.AreEqual(oldList, newList);
                        break;
                    }
                    else if(i == (groups.Count - 1)) System.Console.Out.WriteLine("All groups have mapped all contacts");
                }
            }
            else System.Console.Out.WriteLine("No groups or contacts");                      
        }
    }
}
