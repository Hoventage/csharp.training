using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
            this.manager = manager;
        }
        public void Create(ProjectData project)
        {
            manager.MenuHelper.GoToHomePage();
            manager.MenuHelper.GoToControlPage();
            manager.MenuHelper.GoToManageProject();
            InitCreation();
            FillProjectForm(project);
            SubmitCreation();
            manager.MenuHelper.GoToManageProject();
            projectCache = null;
        }

        public void SubmitCreation()
        {
            driver.FindElement(By.CssSelector("input[value='Добавить проект']")).Click();
        }

        internal void Delete(int index)
        {
            manager.MenuHelper.GoToHomePage();
            manager.MenuHelper.GoToControlPage();
            manager.MenuHelper.GoToManageProject();
            SelectProject(index);
            InitDeletion();
            SubmitDeletion();
            manager.MenuHelper.GoToManageProject();
            projectCache = null;
        }

        private void SubmitDeletion()
        {
            driver.FindElement(By.XPath("//input[@class='btn btn-primary btn-white btn-round']")).Click();
        }

        private void InitDeletion()
        {
            driver.FindElement(By.XPath("//input[@class='btn btn-primary btn-sm btn-white btn-round']")).Click();
        }

        private void SelectProject(int index)
        {
            driver.FindElement(By.XPath("//div[@class='widget-box widget-color-blue2']//tbody/tr[" + (index + 1) + "]"))
                .FindElement(By.TagName("a")).Click();
        }

        public void FillProjectForm(ProjectData project)
        {
            driver.FindElement(By.Name("name")).SendKeys(project.Name);
        }

        public void InitCreation()
        {
            driver.FindElement(By.XPath("//button[contains(text(),'Создать новый проект')]")).Click();
        }

        private List<ProjectData> projectCache = null;

        public List<ProjectData> GetProjectList()
        {
            {
                projectCache = new List<ProjectData>();
                manager.MenuHelper.GoToControlPage();
                manager.MenuHelper.GoToManageProject();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector(".col-md-12 > .widget-color-blue2 table tbody tr"));
                foreach (IWebElement element in elements)
                {
                    var Name = element.FindElement(By.XPath("./td[1]"));
                    projectCache.Add(new ProjectData(Name.Text) { });
                }
            }
            return new List<ProjectData>(projectCache);
        }

        public int GetProjectCount()
        {
            return driver.FindElements(By.CssSelector(".col-md-12 > .widget-color-blue2 table tbody tr")).Count;
        }
    }
}