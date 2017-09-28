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
            groupIndex = groupIndex + 1;
            manager.Navigator.GoToGroupsPage();

            SelectGroup(groupIndex);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();

            return this;
        }
        public GroupHelper Remove(int groupIndex) {
            groupIndex = groupIndex + 1;
            manager.Navigator.GoToGroupsPage();

            SelectGroup(groupIndex);
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
            return this;
        }
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }
        public GroupHelper SelectGroup(int index)
        {
            if (IsGroupPresent())
            {
                if (IsGroupWithIndexPresent(index))
                {
                    driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
                }
                else {
                    driver.FindElement(By.Name("selected[]")).Click();
                }
                
            }
            else {
                Create(new GroupData() { Name = "new group"});
                driver.FindElement(By.Name("selected[]")).Click();
            }
            return this;
        }
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }
        public bool IsGroupWithIndexPresent(int index)
        {
            if (driver.FindElements(By.ClassName("group")).Count() >= index) {
                return true;
            }
            else {
                return false;
            }
        }
        public bool IsGroupPresent()
        {
            return IsElementPresent(By.TagName("span")) && IsElementPresent(By.ClassName("group"));
        }
        public List<GroupData> GetGroupList()
        {
            List<GroupData> groups = new List<GroupData>();

            manager.Navigator.GoToGroupsPage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
            foreach (IWebElement element in elements) {
                groups.Add(new GroupData(element.Text));
            }

            return groups;
        }
    }
}