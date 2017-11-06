using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager)
            : base(manager) { }

        public ProjectHelper Create(ProjectData project)
        {
            manager.ManagementMenu.GoToProjectManagementPage();

            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
            ReturnToProjectPage();

            return this;
        }
        public ProjectHelper Remove(int index)
        {
            manager.ManagementMenu.GoToProjectManagementPage();

            SelectProject(index);
            RemoveProjectCreation();
            ReturnToProjectPage();

            return this;
        }
        public ProjectHelper Remove(ProjectData project)
        {
            manager.ManagementMenu.GoToProjectManagementPage();

            SelectProject(project);
            RemoveProjectCreation();
            ReturnToProjectPage();

            return this;
        }

        private ProjectHelper InitProjectCreation()
        {
            driver.FindElement(By.XPath("//form[@action='manage_proj_create_page.php']"))
                .FindElement(By.CssSelector("button")).Click();
            return this;
        }
        private ProjectHelper FillProjectForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
            Type(By.Id("project-description"), project.Description);

            return this;
        }
        private ProjectHelper SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
            return this;
        }
        private ProjectHelper ReturnToProjectPage()
        {
            manager.ManagementMenu.GoToProjectManagementPage();
            return this;
        }
        private ProjectHelper SelectProject(int index)
        {
            driver.FindElement(
                By.XPath("//div[@class='widget-main no-padding'][descendant::form[@action='manage_proj_create_page.php']]//table/tbody/tr[" + index + "]"))
                .Click();
            return this;
        }
        private ProjectHelper SelectProject(ProjectData project)
        {
            driver.FindElement(
                By.XPath("//div[@class='widget-main no-padding'][descendant::form[@action='manage_proj_create_page.php']]//table/tbody/tr/td[1][.='" + project.Name + "']"))
                .Click();
            return this;
        }
        private ProjectHelper RemoveProjectCreation()
        {
            driver.FindElement(By.Id("project-delete-form")).FindElement(By.XPath("//input[@type='submit']")).Click();
            return this;
        }
        public int GetProjectCount()
        {
            manager.ManagementMenu.GoToProjectManagementPage();
            return driver.FindElements(
                By.XPath("//div[@class='widget-main no-padding'][descendant::form[@action='manage_proj_create_page.php']]//table/tbody/tr"))
                .Count;
        }
        public List<ProjectData> GetProjectList() {
            List<ProjectData> projectList = new List<ProjectData>();
            manager.ManagementMenu.GoToProjectManagementPage();
            ICollection<IWebElement> elements = driver.FindElements(
                By.XPath("//div[@class='widget-main no-padding'][descendant::form[@action='manage_proj_create_page.php']]//table/tbody/tr/td[1]")
                );
            foreach (IWebElement element in elements) {
                projectList.Add(new ProjectData()
                {
                    Name = element.Text
                });
            }
            return projectList;
        }
        public bool IsProjectPresent(ProjectData project)
        {
            List<ProjectData> projectList = GetProjectList();
            foreach (ProjectData item in projectList)
            {
                if (item.Name == project.Name) return true;
            }
            return false;
        }
    }
}
