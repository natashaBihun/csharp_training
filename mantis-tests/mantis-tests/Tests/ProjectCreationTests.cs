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
            int projectCount = appManager.Project.GetProjectCount();

            ProjectData project = new ProjectData()
            {
                Name = "new project",
                Description = "description of project"
            };
            if (appManager.Project.IsProjectPresent(project)) {
                appManager.Project.Remove(project);
            }
            appManager.Project.Create(project);

            Assert.AreEqual(projectCount + 1, appManager.Project.GetProjectCount());
        }
    }
}
