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
        public GroupHelper(ApplicationManager manager) : base(manager) { }

        internal List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            OpenGroupsDialogue();
            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.62e4491", "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.62e4491", "GetText", "#0|#"+i, "");

                list.Add(new GroupData()
                {
                    Name = item
                });
            }
            
            CloseGroupsDialogue();
            return list;
        }
        public void CreateGroupIfNeeded(List<GroupData> groups, GroupData group)
        {
            if (groups == null)
            {
                Add(group);
            }
        }
        public void Remove(int index)
        {
            OpenGroupsDialogue();
            SelectGroup(index);
            aux.ControlClick(GROUPWINTITLE, "", "LOCATOR");//не удалось запустить приложение, поэтому вместо корректного локатора просто "маркер"
            CloseGroupsDialogue();
        }
        public void SelectGroup(int index)
        {
            aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.62e4491", "Select", "#0|#" + index, "");
        }
        internal void Add(GroupData newGroup)
        {
            OpenGroupsDialogue();
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.62e4493");
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");
            CloseGroupsDialogue();
        }
        private void CloseGroupsDialogue()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.62e4494");
        }
        private void OpenGroupsDialogue()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.62e44910");
            aux.WinWait(GROUPWINTITLE);
        }
    }
}