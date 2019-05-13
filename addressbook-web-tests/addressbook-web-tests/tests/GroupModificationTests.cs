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
            app.Navigator.GoToGroupsPage();
            
            // Actions
            GroupData newData = new GroupData("qwerty7");
            newData.Header = null;
            newData.Footer = null;
            app.Groups.CreateBeforeModifyIfNeeded(1, newData);

            // Verification
            
        }
    }
}