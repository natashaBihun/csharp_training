using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace addressbook_tests_white
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book";
        private GroupHelper _groupHelper;

        public GroupHelper Groups
        {
            get
            {
                return _groupHelper;
            }
        }

        public Window MainWindow { get; private set; }
        public ApplicationManager() {
            Application app = Application.Launch(@"C:\Program Files (x86)\GAS Softwares\Free Address Book\AddressBook.exe");
            MainWindow = app.GetWindow(WINTITLE);
            _groupHelper = new GroupHelper(this);
        }
        public void Stop() {
            MainWindow.Get<Button>("uxExitAddressButton").Click();
        }
    }

}
