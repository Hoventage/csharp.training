﻿using System;
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
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void ContactInformationTest()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            // Verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllMails, fromForm.AllMails);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }
        [Test]
        public void ContactDetailsInformationTest()
        {
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetails(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            // Verification
            //Assert.AreEqual(fromDetails, fromForm);
            Assert.AreEqual(fromDetails.AllInfoFromDetails, fromForm.AllInfoFromDetails);
        }
    }
}