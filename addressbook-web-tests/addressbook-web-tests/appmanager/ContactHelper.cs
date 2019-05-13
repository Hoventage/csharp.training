using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
            this.manager = manager;
        }
        public ContactHelper Modify(int q, ContactData newData)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(q);
            FillNewContactFields(newData);
            SubmitContactModification();
            manager.Navigator.GoToHomePage();
            return this;
        }
        public void CreateBeforeModifyIfNeeded(int q, ContactData newData)
        {
            if (CurrentContactExist(1))
            {
                if (CurrentContactExist(q))
                {
                    Modify(q, newData);
                }
                else
                {
                    //ContactData contact = new ContactData("Zapp");
                    //Create(contact);
                    Modify(1, newData);
                }
            }
            else
            {
                ContactData contact = new ContactData("Zapp");
                Create(contact);
                Modify(1, newData);
            }
        }
        public ContactHelper Remove(int q)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(q);
            RemoveContact();
            SumbitContactRemove();
            manager.Navigator.GoToHomePage();
            return this;
        }
        public void CreateBeforeRemoveIfNeeded(int p)
        {
            if (CurrentContactExist(1))
            {
                if (CurrentContactExist(p))
                {
                    Remove(p);
                }
                else
                {
                    //ContactData contact = new ContactData("Little");
                    //Create(contact);
                    Remove(1);
                }
            }
            else
            {
                ContactData contact = new ContactData("Little");
                Create(contact);
                Remove(1);
            }
        }
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.OpenNewContactPage();
            FillNewContactFields(contact);
            SubmitContactCreation();
            manager.Navigator.GoToHomePage();
            return this;
        }
        
        public ContactHelper FillNewContactFields(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            //driver.FindElement(By.Name("nickname")).Clear();
            //driver.FindElement(By.Name("nickname")).SendKeys(contact.Nickname);
            //driver.FindElement(By.Name("title")).Clear();
            //driver.FindElement(By.Name("title")).SendKeys(contact.Title);
            //driver.FindElement(By.Name("company")).Clear();
            //driver.FindElement(By.Name("company")).SendKeys(contact.Company);
            //driver.FindElement(By.Name("address")).Clear();
            //driver.FindElement(By.Name("address")).SendKeys(contact.Address);
            //driver.FindElement(By.Name("home")).Clear();
            //driver.FindElement(By.Name("home")).SendKeys(contact.Home);
            //driver.FindElement(By.Name("mobile")).Clear();
            //driver.FindElement(By.Name("mobile")).SendKeys(contact.Mobile);
            //driver.FindElement(By.Name("work")).Clear();
            //driver.FindElement(By.Name("work")).SendKeys(contact.Work);
            //driver.FindElement(By.Name("fax")).Clear();
            //driver.FindElement(By.Name("fax")).SendKeys(contact.Fax);
            //driver.FindElement(By.Name("email")).Clear();
            //driver.FindElement(By.Name("email")).SendKeys(contact.Email);
            //driver.FindElement(By.Name("email2")).Clear();
            //driver.FindElement(By.Name("email2")).SendKeys(contact.Email2);
            //driver.FindElement(By.Name("email3")).Clear();
            //driver.FindElement(By.Name("email3")).SendKeys(contact.Email3);
            //driver.FindElement(By.Name("homepage")).Clear();
            //driver.FindElement(By.Name("homepage")).SendKeys(contact.Homepage);
            //driver.FindElement(By.Name("bday")).Click();
            //new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText("18");
            //driver.FindElement(By.XPath("//option[@value='18']")).Click();
            //driver.FindElement(By.Name("bmonth")).Click();
            //new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText("December");
            //driver.FindElement(By.XPath("//option[@value='December']")).Click();
            //driver.FindElement(By.Name("byear")).Click();
            //driver.FindElement(By.Name("byear")).Clear();
            //driver.FindElement(By.Name("byear")).SendKeys(contact.Byear);
            //driver.FindElement(By.Name("aday")).Click();
            //new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText("17");
            //driver.FindElement(By.XPath("(//option[@value='17'])[2]")).Click();
            //driver.FindElement(By.Name("amonth")).Click();
            //new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText("June");
            //driver.FindElement(By.XPath("(//option[@value='June'])[2]")).Click();
            //driver.FindElement(By.Name("ayear")).Click();
            //driver.FindElement(By.Name("ayear")).Clear();
            //driver.FindElement(By.Name("ayear")).SendKeys(contact.Ayear);
            //driver.FindElement(By.XPath("(//option[@value='7'])[3]")).Click();
            //driver.FindElement(By.Name("address2")).Click();
            //driver.FindElement(By.Name("address2")).Clear();
            //driver.FindElement(By.Name("address2")).SendKeys(contact.Address2);
            //driver.FindElement(By.Name("phone2")).Clear();
            //driver.FindElement(By.Name("phone2")).SendKeys(contact.Phone2);
            //driver.FindElement(By.Name("notes")).Clear();
            //driver.FindElement(By.Name("notes")).SendKeys(contact.Notes);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            return this;
        }

        public ContactHelper SumbitContactRemove()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index1)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index1 + "]")).Click();
            return this;
        }
        public bool CurrentContactExist(int index1)
        {
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + index1 + "]"));
        }

        //public void CheckAndCreateBeforeAction(int index)
        //{
        //    if(!CurrentContactExist(index))
        //    {
        //        while(driver.FindElements(By.XPath("(//img[@alt='Edit']")).Count < index)
        //         {
        //            ContactData contact = new ContactData("abc");
        //            Create(contact);
        //         }
        //    }
        //}

        //public void CheckExistAndCreate(int p)
        //{
        //    if (!CurrentContactExist(p))
        //    {
        //        ContactData contact = new ContactData("Little");
        //        Create(contact);
        //    }
        //}
    }
}