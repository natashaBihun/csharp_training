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
            ProjectData project = new ProjectData() {
                Name = "1"
            };
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
