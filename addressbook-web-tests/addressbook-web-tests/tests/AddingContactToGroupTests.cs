using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
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
            ContactData contact =  ContactData.GetAll().Except(oldList).FirstOrDefault();
            app.Contacts.CheckContactWithoutGroupExist(contact, newContact);

            if (contact == null)
            {
                contact = newContact;
            }

            //action
            app.Contacts.AddContactToGroup(contact, group);
            
            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();

            //verification
            Assert.AreEqual(oldList, newList);
        }
    }
}