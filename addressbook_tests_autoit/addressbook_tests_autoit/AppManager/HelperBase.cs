using AutoItX3Lib;

namespace addressbook_tests_autoit
{
    public class HelperBase
    {
        protected ApplicationManager applicationManager;
        protected string WINTITLE;
        protected AutoItX3 autoX;

        public HelperBase(ApplicationManager applicationManager)
        {
            this.applicationManager = applicationManager;
            this.WINTITLE = ApplicationManager.WINTITLE;
            this.autoX = applicationManager.AutoX;
        }
    }
}