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
        public ProjectHelper Create(AccountData account, ProjectData project)
        {
            if (IsProjectPresent(project)) { Remove(account, project); }
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData projectData = new Mantis.ProjectData();
            projectData.name = project.Name;
            projectData.description = project.Description;
            client.mc_project_add(account.Name, account.Password, projectData);

            return this;
        }
        public ProjectHelper Remove(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData projectData = new Mantis.ProjectData();
            string index = GetProjectIndex(account, project.Name);
            projectData.id = index;
            client.mc_project_delete(account.Name, account.Password, index);
            return this;
        }
        public ProjectHelper Remove(int index)
        {
            manager.ManagementMenu.GoToProjectManagementPage();

            SelectProject(index);
            RemoveProject();
            AcceptProjectRemoving();
            ReturnToProjectPage();

            return this;
        }
        public ProjectHelper Remove(ProjectData project)
        {
            manager.ManagementMenu.GoToProjectManagementPage();

            SelectProject(project);
            RemoveProject();
            AcceptProjectRemoving();
            ReturnToProjectPage();

            return this;
        }

        public ProjectHelper InitProjectCreation()
        {
            driver.FindElement(By.XPath("//form[@action='manage_proj_create_page.php']"))
                .FindElement(By.CssSelector("button")).Click();
            return this;
        }
        public ProjectHelper FillProjectForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
            Type(By.Id("project-description"), project.Description);

            return this;
        }
        public ProjectHelper SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
            return this;
        }
        public ProjectHelper ReturnToProjectPage()
        {
            manager.ManagementMenu.GoToProjectManagementPage();
            return this;
        }
        public ProjectHelper SelectProject(int index)
        {
            driver.FindElement(
                By.XPath("//div[@class='widget-main no-padding'][descendant::form[@action='manage_proj_create_page.php']]//table/tbody/tr[" + index + "]/td[1]/a"))
                .Click();
            return this;
        }
        public ProjectHelper SelectProject(ProjectData project)
        {
            driver.FindElements(
                By.XPath("//div[@class='widget-main no-padding'][descendant::form[@action='manage_proj_create_page.php']]//table/tbody/tr/td[1]/a"))
                .Where(t => t.Text == project.Name)
                .FirstOrDefault()
                .Click();
            return this;
        }
        public ProjectHelper RemoveProject()
        {
            driver
                .FindElement(By.XPath("//input[@class='btn btn-primary btn-sm btn-white btn-round']")).Click();
            return this;
        }
        public ProjectHelper AcceptProjectRemoving()
        {
            driver
                .FindElement(By.XPath("//input[@class='btn btn-primary btn-white btn-round']")).Click();
            return this;
        }
        public int GetProjectCount()
        {
            manager.ManagementMenu.GoToProjectManagementPage();
            return driver.FindElements(
                By.XPath("//div[@class='widget-main no-padding'][descendant::form[@action='manage_proj_create_page.php']]//table/tbody/tr"))
                .Count;
        }
        public List<ProjectData> GetProjectList()
        {
            manager.Login.LoginAsAdministrator();
            List<ProjectData> projectList = new List<ProjectData>();
            manager.ManagementMenu.GoToProjectManagementPage();
            ICollection<IWebElement> elements = driver.FindElements(
                By.XPath("//div[@class='widget-main no-padding'][descendant::form[@action='manage_proj_create_page.php']]//table/tbody/tr/td[1]")
                );
            foreach (IWebElement element in elements)
            {
                projectList.Add(new ProjectData()
                {
                    Name = element.Text
                });
            }
            return projectList;
        }
        public List<ProjectData> GetProjectList(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] mantisProjects = client.mc_projects_get_user_accessible(account.Name, account.Password);
            List<ProjectData> projects = new List<ProjectData>();
            foreach (Mantis.ProjectData project in mantisProjects)
            {
                projects.Add(new ProjectData()
                {
                    Name = project.name,
                    Description = project.description,
                    Id = project.id
                });
            }
            return projects;
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
        public string GetProjectIndex(AccountData account, string projectName) {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            return client.mc_project_get_id_from_name(account.Name, account.Password, projectName);
        }
    }
}
