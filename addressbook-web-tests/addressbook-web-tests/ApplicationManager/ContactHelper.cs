﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            _contactCache = null;
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            _contactCache = null;
            return this;
        }
        public ContactHelper RemoveContact() {
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
            if (_contactCache == null) {
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
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string firstEmail = driver.FindElement(By.Name("email")).GetAttribute("value");
            string secondEmail = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string thirdEmail = driver.FindElement(By.Name("email3")).GetAttribute("value");
            return new ContactData() {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = firstEmail,
                SecondEmail = secondEmail,
                ThirdEmail = thirdEmail            
            };
        }
        public int GetNumberOfSearchResults() {
            manager.Navigator.GoToContactsPage();

            string text = driver.FindElement(By.TagName("label")).Text;
            Match match = new Regex(@"\d+").Match(text);

            return Int32.Parse(match.Value);
        }
    }
}
