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
            appManager.Login.LoginAsAdministrator();
            int projectCount = appManager.Project.GetProjectCount();

            if (projectCount == 0)
            {
                appManager.Project.Create(new ProjectData()
                    {
                        Name = "project",
                        Description = "description"
                    });
                projectCount++;
            }
            appManager.Project.Remove(1);

            Assert.AreEqual(projectCount - 1, appManager.Project.GetProjectCount());
        }
    }
}
