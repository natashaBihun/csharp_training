using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contactData) {
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
        public ContactHelper Remove(int contactIndex)
        {
            manager.Navigator.GoToContactsPage();

            SelectContact(contactIndex);
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
        public ContactHelper FillContactForm(ContactData contactData)
        {
            Type(By.Name("firstname"), contactData.FirstName);
            Type(By.Name("lastname"), contactData.LastName);
            Type(By.Name("title"), contactData.Title);
            Type(By.Name("company"), contactData.Company);
            Type(By.Name("address"), contactData.Address);
            Type(By.Name("home"), contactData.HomePhone);
            Type(By.Name("mobile"), contactData.MobilePhone);
            Type(By.Name("email"), contactData.Email);
            SelectingType(By.Name("bday"), contactData.BDay);
            SelectingType(By.Name("bmonth"), contactData.BMonth);
            Type(By.Name("byear"), contactData.BYear);
            if(IsElementPresent(By.Name("new_group"))) {
                SelectingType(By.Name("new_group"), contactData.NameOfGroup);
            }
            // ERROR: Caught exception [Error: Dom locators are not implemented yet!]

            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }
        public ContactHelper RemoveContact() {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();           
            driver.SwitchTo().Alert().Accept();
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
        public void IsAnyContactForModifyPresent(ContactData newContactData)
        {
            if (!IsContactPresent())
            {
                Create(new ContactData() { FirstName = "new contact" });
            }
            Modify(1, newContactData);
        }
        public void IsAnyContactForRemovePresent()
        {
            if (!IsContactPresent())
            {
                Create(new ContactData() { FirstName = "new contact" });
            }
            Remove(1);
        }
        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();

            manager.Navigator.GoToContactsPage();
            ICollection<IWebElement> elementsFirstName = driver.FindElements(By.XPath("//tr[@name='entry']/td[3]"));
            ICollection<IWebElement> elementsLastName = driver.FindElements(By.XPath("//tr[@name='entry']/td[2]"));
            for (int i = 0; i < elementsFirstName.Count; i++)
            {
                contacts.Add(new ContactData() { FirstName = elementsFirstName.ElementAt(i).Text, LastName = elementsLastName.ElementAt(i).Text });
            }

            return contacts;
        }
    }
}
