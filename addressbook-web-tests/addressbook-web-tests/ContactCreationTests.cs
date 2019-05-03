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
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            OpenNewContactPage();
            ContactData contact = new ContactData("name");
            contact.Lastname = "surname";
            FillNewContactFields(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            Logout();
        }
    }
}
