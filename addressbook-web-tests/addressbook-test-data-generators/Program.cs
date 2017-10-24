using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WebAddressbookTests;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            string fileName = args[1];
            string format = args[2];
            List<GroupData> groups = new List<GroupData>();

            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });
            }
            if (format == "excel") {
                WriteGroupsToExcelFile(groups, fileName);
            } else {
                StreamWriter writer = new StreamWriter(fileName);
                if (format == "csv")
                {
                    WriteGroupsToCSVFile(groups, writer);
                }
                else if (format == "xml")
                {
                    WriteGroupsToXMLFile(groups, writer);
                }
                else if (format == "json")
                {
                    WriteGroupsToJSONFile(groups, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format " + format);
                }
                writer.Close();
            }

        }

        static void WriteGroupsToExcelFile(List<GroupData> groups, string fileName)
        {
            Excel.Application application = new Excel.Application();
            application.Visible = true;
            Excel.Workbook workbook = application.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            int row = 1;
            foreach (GroupData group in groups)
            {
                worksheet.Cells[row, 1] = group.Name;
                worksheet.Cells[row, 2] = group.Header;
                worksheet.Cells[row, 3] = group.Footer;
                row++;
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(fullPath);
            workbook.SaveAs(fullPath);
            workbook.Close();
            application.Visible = false;
            application.Quit();
        }

        static void WriteGroupsToCSVFile(List<GroupData> groups, StreamWriter writer) {
            foreach (GroupData group in groups) {
                writer.WriteLine(String.Format("${0},${1},${2}", group.Name, group.Header, group.Footer));
            }
        }

        static void WriteGroupsToXMLFile(List<GroupData> groups, StreamWriter writer) {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void WriteGroupsToJSONFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
