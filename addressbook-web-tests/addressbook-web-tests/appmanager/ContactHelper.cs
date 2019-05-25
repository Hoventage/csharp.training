using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;

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
        public ContactHelper Remove(int q)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(q);
            RemoveContact();
            SumbitContactRemove();
            manager.Navigator.GoToHomePage();
            return this;
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
            //driver.FindElement(By.Name("home")).SendKeys(contact.HomePhone);
            //driver.FindElement(By.Name("mobile")).Clear();
            //driver.FindElement(By.Name("mobile")).SendKeys(contact.MobilePhone);
            //driver.FindElement(By.Name("work")).Clear();
            //driver.FindElement(By.Name("work")).SendKeys(contact.WorkPhone);
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
            contactCache = null;
            return this;
        }

        public ContactHelper SumbitContactRemove()
        {
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
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
            contactCache = null;
            return this;
        }

        //public ContactHelper InitContactModification(int index)
        //{
        //    driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
        //    return this;
        //}

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }
        public bool CurrentContactExist(int index)
        {
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]"));
        }
        public void CreateIfNeeded(ContactData contact)
        {
            if (!CurrentContactExist(0))
            {
                Create(contact);
            }
        }
        public bool AssertContactFields(ContactData contact)
        {
            return driver.FindElement(By.Name("firstname")).GetAttribute("value") == contact.Firstname
                && driver.FindElement(By.Name("lastname")).GetAttribute("value") == contact.Lastname;
        }
        public void AssertContactFields(int index, ContactData contact)
        {
            InitContactModification(index);
            Assert.IsTrue(AssertContactFields(contact));
        }
        // Cache for groups list and fill list with needed elements
        private List<ContactData> contactCache = null;

        // Поработать с CSS, XPath. Рассмотреть данный метод еще раз
        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));
                foreach (IWebElement element in elements)
                {
                    var lastname = element.FindElement(By.XPath("./td[2]"));
                    var firstname = element.FindElement(By.XPath("./td[3]"));

                    contactCache.Add(new ContactData(firstname.Text, lastname.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }
        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count;
        }


        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string eMail = driver.FindElement(By.Name("email")).GetAttribute("value");
            string eMail2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string eMail3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstname, lastname)
            {
            Address = address,
            HomePhone = homePhone,
            MobilePhone = mobilePhone,
            WorkPhone = workPhone,
            Email = eMail,
            Email2 = eMail2,
            Email3 = eMail3
            };
        }
        public void InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
        }
        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastname = cells[1].Text;
            string firstname = cells[2].Text;
            string address = cells[3].Text;
            string allMails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstname, lastname)
            {
                Address = address,
                AllPhones = allPhones,
                AllMails = allMails
            };
        }
        public void ViewContact(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
        }
        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value); 
        }
    }
}