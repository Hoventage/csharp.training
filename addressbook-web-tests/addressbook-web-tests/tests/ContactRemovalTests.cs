using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

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

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            
            // Action
            app.Contacts.Remove(0);

            // Verification
            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();

            ContactData toBeRemoved = oldContacts[0];
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData item in newContacts)
            {
                Assert.AreNotEqual(item.Id, toBeRemoved.Id);
            }
        }
    }
}