using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AutoItX3Lib;

namespace addressbook_tests_autoit
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book";
        private AutoItX3 _autoX;
        private GroupHelper _groupHelper;

        public AutoItX3 AutoX
        {
            get
            {
                return _autoX;
            }
        }
        public GroupHelper Groups
        {
            get
            {
                return _groupHelper;
            }
        }

        public ApplicationManager() {
            _autoX = new AutoItX3();
            _autoX.Run(@"C:\Program Files (x86)\GAS Softwares\Free Address Book\AddressBook.exe", "", _autoX.SW_SHOW);
            _autoX.WinWait(WINTITLE);
            _autoX.WinActivate(WINTITLE);
            _autoX.WinWaitActive(WINTITLE);
            _groupHelper = new GroupHelper(this);
        }
        public void Stop() {
            _autoX.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d510");
        }
    }

}
