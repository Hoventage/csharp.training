using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
            this.manager = manager;
        }
        public GroupHelper Remove(int p)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(p);
            RemoveGroup();
            manager.Navigator.GoToGroupsPage();
            return this;
        }
        public GroupHelper Modify(int p, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(p);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            manager.Navigator.GoToGroupsPage();
            return this;
        }
        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            manager.Navigator.GoToGroupsPage();
            return this;
        }

        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }
        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }
        public bool CurrentGroupExist(int index)
        {
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + index + "]"));
        }
        public void CreateIfNeeded(GroupData group)
        {
            if (!CurrentGroupExist(1))
            {
                Create(group);
            }
        }
        
        public bool AssertGroupFields(GroupData group)
        {
            return driver.FindElement(By.Name("group_name")).GetAttribute("value")
                   == group.Name
                && driver.FindElement(By.Name("group_header")).GetAttribute("value")
                   == group.Header
                && driver.FindElement(By.Name("group_footer")).GetAttribute("value")
                   == group.Footer;
        }
        public void AssertGroupFields(int index, GroupData group)
        {
            SelectGroup(index);
            InitGroupModification();
            Assert.IsTrue(AssertGroupFields(group));
        }

        //public void CheckAndCreateBeforeAction(int index)
        //{
        //    if(!CurrentGroupExist(index))
        //    {
        //        while(driver.FindElements(By.XPath("//input[@name='selected[]']")).Count < index)
        //         {
        //            GroupData group = new GroupData("abc");
        //            Create(group);
        //         }
        //    }
        //}
    }
}