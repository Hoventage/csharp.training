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
            app.Navigator.GoToHomePage();

            // Action
            app.Contacts.CreateBeforeRemoveIfNeeded(5);

            // Verification
            
        }
    }
}