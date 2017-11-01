using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public GroupHelper(ApplicationManager applicationManager)
            : base(applicationManager){}

        public List<GroupData> GetGroupList() {
            List<GroupData> groupList = new List<GroupData>();

            OpenGroupsDialogue();
            string count = autoX.ControlTreeView(
                GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", 
                "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = autoX.ControlTreeView(
                GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "GetText", "#0|#"+i, "");
                groupList.Add(new GroupData() { Name = item});
            }
            CloseGroupsDialogue();

            return groupList;
        }

        public void Add(GroupData newGroup)
        {
            OpenGroupsDialogue();
            autoX.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            autoX.Send(newGroup.Name);
            autoX.Send("{ENTER}");
            CloseGroupsDialogue();
        }

        private void CloseGroupsDialogue()
        {
            autoX.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }

        private void OpenGroupsDialogue()
        {
            autoX.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            autoX.WinWait(GROUPWINTITLE);
        }
    }
}
