using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.InputDevices;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowItems;
using System.Windows.Automation;

namespace addressbook_tests_white
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public GroupHelper(ApplicationManager applicationManager)
            : base(applicationManager){}

        public List<GroupData> GetGroupList() {
            List<GroupData> groupList = new List<GroupData>();
            Window dialogue = OpenGroupsDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];

            foreach (TreeNode item in root.Nodes)
            {
                groupList.Add(new GroupData() { Name = item.Text });
            }

            CloseGroupsDialogue(dialogue);

            return groupList;
        }

        public void CloseGroupsDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxCloseAddressButton").Click();
        }

        public Window OpenGroupsDialogue()
        {
            applicationManager.MainWindow.Get<Button>("groupButton").Click();
            return applicationManager.MainWindow.ModalWindow(GROUPWINTITLE);
        }

        public void Add(GroupData newGroup)
        {
            Window dialogue = OpenGroupsDialogue();
            dialogue.Get<Button>("uxNewAddressButton").Click();
            TextBox textBox = (TextBox)dialogue.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textBox.Enter(newGroup.Name);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
            CloseGroupsDialogue(dialogue);
        }

        public void Remove(GroupData toBeRemoved)
        {
            Window droupEditorDialogue = OpenGroupsDialogue();
            Tree tree = droupEditorDialogue.Get<Tree>("uxAddressTreeView");
            tree.Node("Contact groups", toBeRemoved.Name).Select();       
            droupEditorDialogue.Get<Button>("uxDeleteAddressButton").Click();
            applicationManager.MainWindow.Get<RadioButton>("uxDeleteGroupsOnlyRadioButton").Click();
            applicationManager.MainWindow.Get<Button>("uxOKAddressButton").Click();
            CloseGroupsDialogue(droupEditorDialogue);
        }
    }
}
