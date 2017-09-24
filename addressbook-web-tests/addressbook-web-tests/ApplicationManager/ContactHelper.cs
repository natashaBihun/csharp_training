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

            SelectContact(contactIndex);
            InitContactModification();
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
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + contactIndex + "]")).Click();

            return this;
        }
        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.CssSelector("img[alt=\"Edit\"]")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contactData)
        {
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contactData.FirstName);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contactData.LastName);
            driver.FindElement(By.Name("title")).Clear();
            driver.FindElement(By.Name("title")).SendKeys(contactData.Title);
            driver.FindElement(By.Name("company")).Clear();
            driver.FindElement(By.Name("company")).SendKeys(contactData.Company);
            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys(contactData.Address);
            driver.FindElement(By.Name("home")).Clear();
            driver.FindElement(By.Name("home")).SendKeys(contactData.HomePhone);
            driver.FindElement(By.Name("mobile")).Clear();
            driver.FindElement(By.Name("mobile")).SendKeys(contactData.MobilePhone);
            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys(contactData.Email);
            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contactData.BDay);
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contactData.BMonth);
            driver.FindElement(By.Name("byear")).Clear();
            driver.FindElement(By.Name("byear")).SendKeys(contactData.BYear);
            //new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText(contactData.NameOfGroup);
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

    }
}
