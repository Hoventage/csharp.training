using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using static System.Net.WebRequestMethods;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        public RegistrationHelper Registration { get; }
        public FtpHelper Ftp { get; }
        public ManagementMenuHelper Menu { get; }
        public LoginHelper LoginHelper { get; }
        public ProjectManagementHelper Project { get; }

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            baseURL = "http://localhost:8080/mantisbt-2.21.1";
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            LoginHelper = new LoginHelper(this);
            Project = new ProjectManagementHelper(this);
            Menu = new ManagementMenuHelper(this, baseURL);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }
        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
        public ManagementMenuHelper MenuHelper
        {
            get
            {
                return Menu;
            }
        }
        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }
        public LoginHelper Auth
        {
            get
            {
                return LoginHelper;
            }
        }

        public ProjectManagementHelper Proj
        {
            get
            {
                return Project;
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://localhost:8080/mantisbt-2.21.1/login_page.php";
                app.Value = newInstance;
            }
            return app.Value;
        }
    }
}