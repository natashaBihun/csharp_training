using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : TestBase
    {
        [Test]
        public void ProjectRemovalTest() {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
            List<ProjectData> projects = appManager.Project.GetProjectList(account);
            int projectCount = projects.Count;

            if (projectCount == 0)
            {
                appManager.Project.Create(account,
                    new ProjectData()
                    {
                        Name = "project",
                        Description = "description"
                    });
                projectCount++;
            }
            appManager.Project.Remove(account, projects[0]);

            Assert.AreEqual(projectCount - 1, appManager.Project.GetProjectList(account).Count);
        }
    }
}
