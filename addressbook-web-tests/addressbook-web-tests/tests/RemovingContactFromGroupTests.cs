using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void TestRemovingContactFromGroup()
        {
            //preparation
            ContactData newContact = new ContactData("test");
            app.Navigator.GoToHomePage();
            app.Contacts.CreateIfNeeded(0, newContact);

            GroupData newGroup = new GroupData("123456");
            app.Navigator.GoToGroupsPage();
            app.Groups.CreateIfNeeded(0, newGroup);

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contactToBeRemoved = oldList.FirstOrDefault();
            app.Contacts.CheckContactInGroupExist(contactToBeRemoved, oldList, group);
            
            if (contactToBeRemoved == null)
            {
                contactToBeRemoved = group.GetContacts().FirstOrDefault();
            }
            
            //action
            app.Contacts.RemoveContactFromGroup(contactToBeRemoved, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contactToBeRemoved);
            newList.Sort();
            oldList.Sort();

            //verification
            Assert.AreEqual(oldList, newList);
        }
    }
}