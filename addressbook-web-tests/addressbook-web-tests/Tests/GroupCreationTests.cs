using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        [Test, TestCaseSource("GroupDataFromJSONFile")]
        public void GroupCreationTest(GroupData group)
        {            
            List<GroupData> oldGroups = GroupData.GetAll();
            appManager.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData()
            {
                Name = "name'",
                Header = "header",
                Footer = "footer"
            };
            List<GroupData> oldGroups = GroupData.GetAll();
            appManager.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestDBGroupConnectivity() {
            foreach(ContactData contact in GroupData.GetAll()[0].GetContacts()){
                System.Console.Out.WriteLine(contact);
            }
        }

        public static IEnumerable<GroupData> RandomGroupDataProvider() {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCSVFile() {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(Path.Combine(TestContext.CurrentContext.WorkDirectory, @"groups.csv"));
            foreach (string line in lines) {
                string[] parts = line.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }            
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXMLFile()
        {
            return (List<GroupData>) 
                new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(Path.Combine(TestContext.CurrentContext.WorkDirectory, @"groups.xml")));
        }

        public static IEnumerable<GroupData> GroupDataFromJSONFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(File.ReadAllText(Path.Combine(TestContext.CurrentContext.WorkDirectory, @"groups.json")));
        }

        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Open(Path.Combine(TestContext.CurrentContext.WorkDirectory, @"groups.xlsx"));
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            Excel.Range range = worksheet.UsedRange;

            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = Convert.ToString(range.Cells[i, 1].Value),
                    Header = Convert.ToString(range.Cells[i, 2].Value),
                    Footer = Convert.ToString(range.Cells[i, 3].Value)
                });
            }

            workbook.Close();
            application.Visible = false;
            application.Quit();
            return groups;
        }
    }
}
