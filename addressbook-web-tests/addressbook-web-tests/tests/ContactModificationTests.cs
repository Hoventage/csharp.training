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
            app.Navigator.GoToHomePage();
            ContactData newData = new ContactData("FirstLine");
            newData.Lastname = "LastLine";

            // Actions
            if (app.Contacts.CurrentContactExist(1))
            {
                app.Contacts.Modify(1, newData);
            }
            else
            {
                ContactData data = new ContactData("qwerty7");
                app.Contacts.Create(data);
                app.Contacts.Modify(1, newData);
            }

            // Verification

        }
    }
}