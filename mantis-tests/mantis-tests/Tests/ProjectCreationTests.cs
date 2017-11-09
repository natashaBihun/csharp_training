using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : TestBase
    {
        [Test]
        public void ProjectCreationTest() {
            appManager.Login.LoginAsAdministrator();
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
            List<ProjectData> oldProjectList = appManager.Project.GetProjectList(account);
            int projectCount = oldProjectList.Count;

            ProjectData project = new ProjectData()
            {
                Name = "new project",
                Description = "description of project"
            };
            if (appManager.Project.IsProjectPresent(project)) {
                appManager.Project.Remove(project);
                oldProjectList = appManager.Project.GetProjectList(account);
                projectCount--;
            }
            appManager.Project.Create(project);

            List<ProjectData> newProjectList = appManager.Project.GetProjectList(account);
            Assert.AreEqual(projectCount + 1, newProjectList.Count);

            oldProjectList.Add(project);
            oldProjectList.Sort();
            newProjectList.Sort();
            Assert.AreEqual(oldProjectList, newProjectList);
        }
    }
}
