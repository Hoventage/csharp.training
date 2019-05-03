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
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.OpenNewContactPage();
            ContactData contact = new ContactData("name");
            contact.Lastname = "surname";
            app.Contacts.FillNewContactFields(contact);
            app.Contacts.SubmitContactCreation();
            app.Navigator.ReturnToHomePage();
            app.Auth.Logout();
        }
    }
}
