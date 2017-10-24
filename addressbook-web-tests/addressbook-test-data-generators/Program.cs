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
            string entityType = args[0];
            int count = Convert.ToInt32(args[1]);
            string fileName = args[2];
            string format = args[3];

            if (entityType == "group")
            {
                List<GroupData> groups = GenerateGroupsData(count);
                if (format == "excel")
                {
                    WriteGroupsToExcelFile(groups, fileName);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(fileName);
                    if (format == "csv")
                    {
                        WriteGroupsToCSVFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        WriteToXMLFile(groups, writer);
                    }
                    else if (format == "json")
                    {
                        WriteToJSONFile(groups, writer);
                    }
                    else
                    {
                        System.Console.Out.Write("Unrecognized format " + format);
                    }
                    writer.Close();
                }
            }
            else if (entityType == "contact")
            {
                List<ContactData> contacts = new List<ContactData>();
                contacts = GenerateContactsData(count);
                StreamWriter writer = new StreamWriter(fileName);
                if (format == "xml")
                {
                    WriteToXMLFile(contacts, writer);
                }
                else if (format == "json")
                {
                    WriteToJSONFile(contacts, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognized format " + format);
                }
                writer.Close();
            }
            else {
                System.Console.Out.Write("Unexpected type " + entityType);
            }  
        }

        static List<GroupData> GenerateGroupsData(int count) {
            List<GroupData> groups = new List<GroupData>();

            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });
            }
            return groups;
        }
        static List<ContactData> GenerateContactsData(int count)
        {
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(30))
                {
                    FirstName = TestBase.GenerateRandomString(100),
                    LastName = TestBase.GenerateRandomString(100),
                    Title = TestBase.GenerateRandomString(100),
                    Company = TestBase.GenerateRandomString(100),
                    Address = TestBase.GenerateRandomString(100),
                    HomePhone = TestBase.GenerateRandomString(100),
                    MobilePhone = TestBase.GenerateRandomString(100)
                });
            }
            return contacts;
        }

        static void WriteToXMLFile<T>(List<T> data, StreamWriter writer) {
            new XmlSerializer(typeof(List<T>)).Serialize(writer, data);
        }

        static void WriteToJSONFile<T> (List<T> data, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteGroupsToCSVFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}", group.Name, group.Header, group.Footer));
            }
        }

        static void WriteGroupsToExcelFile(List<GroupData> groups, string fileName)
        {
            Excel.Application application = new Excel.Application();
            // application.Visible = true;
            Excel.Workbook workbook = application.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            int row = 1;
            foreach (GroupData group in groups)
            {
                worksheet.Cells[row, 1].Value = group.Name;
                worksheet.Cells[row, 2].Value = group.Header;
                worksheet.Cells[row, 3].Value = group.Footer;
                row++;
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(fullPath);
            workbook.SaveAs(fullPath);
            workbook.Close();
            // application.Visible = false;
            application.Quit();
        }
    }
}
