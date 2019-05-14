using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            // Preparation
            ContactData contact = new ContactData("qwerty7");
            app.Navigator.GoToHomePage();
            app.Contacts.CreateIfNeeded(contact);

            // Actions
            ContactData newData = new ContactData("FirstLine");
            newData.Lastname = "LastLine";
            app.Contacts.Modify(1, newData);

            // Verification
            app.Contacts.AssertContactFields(1, newData);
        }
    }
}