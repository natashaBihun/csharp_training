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
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
            List<ProjectData> oldProjectList = appManager.Project.GetProjectList(account);
            int projectCount = oldProjectList.Count;

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
            appManager.Project.Remove(1);

            List<ProjectData> newProjectList = appManager.Project.GetProjectList(account);
            Assert.AreEqual(projectCount - 1, newProjectList.Count);

            if (oldProjectList.Count != 0) oldProjectList.RemoveAt(0);        
            oldProjectList.Sort();
            newProjectList.Sort();
            Assert.AreEqual(oldProjectList, newProjectList);
        }
    }
}
