using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        private List<ContactData> _contactCache = null;
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }
        public ContactHelper Create(ContactData contactData)
        {
            manager.Navigator.GoToContactsPage();

            InitContactCreation();
            FillContactForm(contactData);
            SubmitContactCreation();
            ReturnToContactsPage();

            return this;
        }
        public ContactHelper Modify(int contactIndex, ContactData newContactData)
        {
            manager.Navigator.GoToContactsPage();

            //SelectContact(contactIndex);
            InitContactModification(contactIndex);
            FillContactForm(newContactData);
            SubmitContactModification();
            ReturnToContactsPage();

            return this;
        }
        public ContactHelper Modify(ContactData contact, ContactData newContactData)
        {
            manager.Navigator.GoToContactsPage();

            //SelectContact(contactIndex);
            InitContactModification(contact.Id);
            FillContactForm(newContactData);
            SubmitContactModification();
            ReturnToContactsPage();

            return this;
        }
        public ContactHelper Remove(int contactIndex)
        {
            manager.Navigator.GoToContactsPage();

            SelectContact(contactIndex);
            RemoveContact();
            ReturnToContactsPage();

            return this;
        }
        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.GoToContactsPage();

            SelectContact(contact.Id);
            RemoveContact();
            ReturnToContactsPage();

            return this;
        }
        private ContactHelper ReturnToContactsPage()
        {
            manager.Navigator.GoToContactsPage();
            return this;
        }
        public ContactHelper SelectContact(int contactIndex)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])" + "[" + contactIndex + "]")).Click();
            return this;
        }
        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.Id(id)).Click();
            return this;
        }
        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper InitContactModification(int contactIndex)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])" + "[" + contactIndex + "]")).Click();
            return this;
        }
        public ContactHelper InitContactModification(string id)
        {
            driver.FindElement(By.XPath("(//a[img[@alt='Edit']][@href='edit.php?id=" + id + "'])")).Click();
            return this;
        }
        public ContactHelper InitViewDetailsOfContact(int contactIndex)
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'])" + "[" + contactIndex + "]")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contactData)
        {
            Type(By.Name("firstname"), contactData.FirstName);
            Type(By.Name("lastname"), contactData.LastName);
            Type(By.Name("middlename"), contactData.MiddleName);
            Type(By.Name("nickname"), contactData.NickName);
            Type(By.Name("company"), contactData.Company);
            Type(By.Name("title"), contactData.Title);
            Type(By.Name("address"), contactData.Address);
            Type(By.Name("home"), contactData.HomePhone);
            Type(By.Name("mobile"), contactData.MobilePhone);
            Type(By.Name("work"), contactData.WorkPhone);
            Type(By.Name("fax"), contactData.Fax);
            Type(By.Name("email"), contactData.Email);
            Type(By.Name("email2"), contactData.SecondEmail);
            Type(By.Name("email3"), contactData.ThirdEmail);
            Type(By.Name("homepage"), contactData.HomePage);
            SelectingType(By.Name("bday"), contactData.BDay);
            SelectingType(By.Name("bmonth"), contactData.BMonth);
            Type(By.Name("byear"), contactData.BYear);
            SelectingType(By.Name("aday"), contactData.ADay);
            SelectingType(By.Name("amonth"), contactData.AMonth);
            Type(By.Name("ayear"), contactData.AYear);
            if (IsElementPresent(By.Name("new_group")))
            {
                SelectingType(By.Name("new_group"), contactData.NameOfGroup);
            }
            Type(By.Name("address2"), contactData.SecondaryAddress);
            Type(By.Name("phone2"), contactData.SecondaryHomePhone);
            Type(By.Name("notes"), contactData.Notes);
            // ERROR: Caught exception [Error: Dom locators are not implemented yet!]

            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            _contactCache = null;
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            _contactCache = null;
            return this;
        }
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            _contactCache = null;
            return this;
        }

        public bool IsContactPresent(int index)
        {
            manager.Navigator.GoToContactsPage();
            return driver.FindElements(By.XPath("//tr[@name='entry']")).Count() >= index;

        }
        public bool IsContactPresent()
        {
            manager.Navigator.GoToContactsPage();
            return IsElementPresent(By.TagName("tr")) && IsElementPresent(By.Name("entry"));
        }
        public List<ContactData> GetContactList()
        {
            if (_contactCache == null)
            {
                _contactCache = new List<ContactData>();

                manager.Navigator.GoToContactsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name='entry']"));
                ICollection<IWebElement> elementsFirstName = driver.FindElements(By.XPath("//tr[@name='entry']/td[3]"));
                ICollection<IWebElement> elementsLastName = driver.FindElements(By.XPath("//tr[@name='entry']/td[2]"));

                for (int i = 0; i < elements.Count; i++)
                {
                    _contactCache.Add(new ContactData()
                    {
                        Id = elements.ElementAt(i).FindElement(By.TagName("input")).GetAttribute("value"),
                        FirstName = elementsFirstName.ElementAt(i).Text,
                        LastName = elementsLastName.ElementAt(i).Text
                    });
                }
            }

            return new List<ContactData>(_contactCache);
        }
        public int GetContactCount()
        {
            return driver.FindElements(By.XPath("//tr[@name='entry']")).Count;
        }
        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToContactsPage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;
            string allEmails = cells[4].Text;

            return new ContactData()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification(index + 1);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string firstEmail = driver.FindElement(By.Name("email")).GetAttribute("value");
            string secondEmail = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string thirdEmail = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");
            string bday = driver.FindElement(By.Name("bday")).GetAttribute("value");
            string bmonth = driver.FindElement(By.Name("bmonth")).GetAttribute("value");
            string byear = driver.FindElement(By.Name("byear")).GetAttribute("value");
            string aday = driver.FindElement(By.Name("aday")).GetAttribute("value");
            string amonth = driver.FindElement(By.Name("amonth")).GetAttribute("value");
            amonth = amonth.Substring(0, 1).ToUpper() + amonth.Substring(1, amonth.Length - 1).ToLower();
            string ayear = driver.FindElement(By.Name("ayear")).GetAttribute("value");
            string address2 = driver.FindElement(By.Name("address2")).GetAttribute("value");
            string phone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");

            return new ContactData()
            {
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middlename,
                NickName = nickname,
                Company = company,
                Title = title,
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Fax = fax,
                Email = firstEmail,
                SecondEmail = secondEmail,
                ThirdEmail = thirdEmail,
                HomePage = homepage,
                BDay = bday,
                BMonth = bmonth,
                BYear = byear,
                ADay = aday,
                AMonth = amonth,
                AYear = ayear,
                SecondaryAddress = address2,
                SecondaryHomePhone = phone2,
                Notes = notes
            };
        }
        public string GetContactInformationFromDetailsPage(int index)
        {
            manager.Navigator.GoToContactsPage();
            InitViewDetailsOfContact(index + 1);
            string details = driver.FindElement(By.Id("content")).Text;
            return Regex.Replace(details, "\r\n\r\n", "\r\n");
        }
        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToContactsPage();

            string text = driver.FindElement(By.TagName("label")).Text;
            Match match = new Regex(@"\d+").Match(text);

            return Int32.Parse(match.Value);
        }
        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToContactsPage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(t => t.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }
        public void RemoveContactFromGroup(GroupData group, ContactData contact)
        {
            manager.Navigator.GoToContactsPage();
            SetGroupFilter(group.Id);
            SelectContact(contact.Id);
            SelectGroupToRemove(group.Id, contact.Id);
        }
        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }
        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }
        public void SelectGroupToRemove(string groupId, string contactId)
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                (from gcr in db.GCR
                 .Where(t => t.ContactId == contactId && t.GroupId == groupId)
                 select gcr).Delete();
            }
        }
        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }
        private void SetGroupFilter(string id)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByValue(id);
        }

        public List<ContactData> IsContactPresents(List<ContactData> contacts)
        {
            if (contacts.Count == 0)
            {
                Create(new ContactData() { FirstName = "Name", LastName = "Surname" });
                contacts = ContactData.GetAll();
            }

            return contacts;
        }

        public ContactData SelectContact(List<ContactData> oldList, List<ContactData> contacts)
        {
            string contactId = contacts.Select(t => t.Id)
                .Except(oldList.Select(t => t.Id))
                .ToList()
                .FirstOrDefault();
            ContactData contact = contacts.Where(t => t.Id == contactId).FirstOrDefault();
            return contact;
        }
    }
}
