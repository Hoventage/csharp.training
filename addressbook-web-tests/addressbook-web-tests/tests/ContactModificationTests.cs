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

            // Action
            ContactData newData = new ContactData("LastLine");
            newData.Lastname = null;
            app.Contacts.CreateBeforeModifyIfNeeded(5, newData);

            // Verification

        }
    }
}