using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssueTests : TestBase
    {
        [Test]
        public void AddNewIssueTest() {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
            List<ProjectData> projects = appManager.Project.GetProjectList();
            if (projects.Count == 0) {
                appManager.Login.LoginAsAdministrator();
                appManager.Project.Create(new ProjectData() {
                    Name = "Project for new issue"
                });                
            }
            appManager.Login.Logout();
            ProjectData project = projects[0];

            IssueData issue = new IssueData()
            {
                Category = "General",
                Summary = "some text",
                Description= "some long text"
            };
            appManager.API.CreateNewIssue(account, project, issue);
        }
    }
}
