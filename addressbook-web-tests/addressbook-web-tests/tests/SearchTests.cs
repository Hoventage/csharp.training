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
    public class SearchTests : AuthTestBase
    {
        [Test]
        public void SearchTest()
        {
            app.Contacts.ContactSearchField("s");
            //app.Contacts.GetNumberOfSearchResults();
            //app.Contacts.GetDisplayedContacts();
            //System.Console.Out.Write(app.Contacts.GetNumberOfSearchResults());

            // Verification
            Assert.AreEqual(app.Contacts.GetDisplayedContacts(), app.Contacts.GetNumberOfSearchResults());
        }
    }
}