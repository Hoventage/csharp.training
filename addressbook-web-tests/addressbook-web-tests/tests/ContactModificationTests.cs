using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            // Preparation
            ContactData contact = new ContactData("qwerty7");
            app.Navigator.GoToHomePage();
            app.Contacts.CreateIfNeeded(0, contact);

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldData = oldContacts[0];

            // Actions
            ContactData newData = new ContactData("FirstLine");
            newData.Lastname = "LastLine";
            app.Contacts.ModifyById(oldData, newData);

            // Verification
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData item in newContacts)
            {
                if (item.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Firstname, item.Firstname);
                    Assert.AreEqual(newData.Lastname, item.Lastname);
                }
            }
        }
    }
}