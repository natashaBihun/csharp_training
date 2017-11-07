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
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
            List<ProjectData> projects = appManager.Project.GetProjectList(account);
            int projectCount = projects.Count;

            ProjectData project = new ProjectData()
            {
                Name = "new project",
                Description = "description of project"
            };
            if (appManager.Project.IsProjectPresent(project)) {
                appManager.Project.Remove(project);
                projectCount--;
            }
            appManager.Project.Create(account, project);

            Assert.AreEqual(projectCount + 1, appManager.Project.GetProjectList(account).Count);
        }
    }
}
