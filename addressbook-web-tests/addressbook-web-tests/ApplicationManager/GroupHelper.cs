﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {
        private List<GroupData> _groupCache = null;
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public GroupHelper Create(GroupData group) {
            manager.Navigator.GoToGroupsPage();

            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();

            return this;
        }        

        public GroupHelper Modify(int groupIndex, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            
            SelectGroup(groupIndex);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();

            return this;
        }
        public GroupHelper Modify(GroupData group, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(group.Id);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();

            return this;
        }
        public GroupHelper Remove(int groupIndex) {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(groupIndex);
            RemoveGroup();
            ReturnToGroupsPage();

            return this;
        }
        public GroupHelper Remove(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(group.Id);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }
        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }
        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);

            return this;
        }
        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            _groupCache = null;
            return this;
        }
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            _groupCache = null;
            return this;
        }
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public GroupHelper SelectGroup(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])")).Click();
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            _groupCache = null;
            return this;
        }
        public bool IsGroupPresent(int index)
        {
            manager.Navigator.GoToGroupsPage();
            return driver.FindElements(By.CssSelector("span.group")).Count() >= index;
        }
        public bool IsGroupPresent()
        {
            manager.Navigator.GoToGroupsPage();
            return IsElementPresent(By.TagName("span")) && IsElementPresent(By.ClassName("group"));         
        }     
        public List<GroupData> GetGroupList()
        {
            if (_groupCache == null) {
                _groupCache = new List<GroupData>();

                manager.Navigator.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    _groupCache.Add(new GroupData(element.Text)
                        {
                            Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                        }
                    );
                }
            }

            return new List<GroupData>(_groupCache);
        }
        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        public List<GroupData> IsGroupPresents(List<GroupData> groups)
        {
            if (groups.Count == 0)
            {
                Create(new GroupData() { Name = "new group" });
                groups = GroupData.GetAll();
            }

            return groups;
        }

        public GroupData SelectGroupWithoutContact(List<GroupData> groups, List<ContactData> contacts)
        {
            for (int i = 0; i < groups.Count; i++)
            {
                List<ContactData> contactsList = groups[i].GetContacts();
                if (contactsList.Count < contacts.Count)
                {
                    return groups[i];
                }
            }
            return null;
        }
        public GroupData SelectGroupWithContact(List<GroupData> groups, List<ContactData> contacts)
        {
            for (int i = 0; i < groups.Count; i++)
            {
                List<ContactData> contactsList = groups[i].GetContacts();
                if (contactsList.Count != 0)
                {
                    return groups[i];
                }
            }
            return null;
        }
    }
}
