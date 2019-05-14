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
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            // Preparation
            GroupData data = new GroupData("123456");
            app.Navigator.GoToGroupsPage();
            app.Groups.CreateIfNeeded(data);

            // Action
            GroupData newData = new GroupData("qwerty7");
            newData.Header = "Header";
            newData.Footer = "Footer";
            app.Groups.Modify(1, newData);

            // Verification

        }
    }
}