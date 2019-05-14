using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            // Preparation
            ContactData contact = new ContactData("test");
            app.Navigator.GoToHomePage();
            app.Contacts.CreateIfNeeded(contact);

            // Action
            app.Contacts.Remove(1);

            // Verification
            Assert.IsFalse(app.Contacts.CurrentContactExist(1));
        }
    }
}