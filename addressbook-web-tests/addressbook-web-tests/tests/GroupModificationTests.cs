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
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            // Preparation
            GroupData data = new GroupData("123456");
            app.Navigator.GoToGroupsPage();
            app.Groups.CreateIfNeeded(data);

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData oldData = oldGroups[0];

            // Action
            GroupData newData = new GroupData("qwerty7");
            newData.Header = "Header";
            newData.Footer = "Footer";
            app.Groups.Modify(0, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);

                }
            }

            // Verification
        }
    }
}