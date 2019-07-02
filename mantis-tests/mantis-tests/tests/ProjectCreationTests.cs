using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        public static IEnumerable<ProjectData> RandomContactDataProvider()
        {
            List<ProjectData> projects = new List<ProjectData>();
            for (int i = 0; i < 1; i++)
            {
                projects.Add(new ProjectData(GenerateRandomString(10))
                {
                });
            }
            return projects;
        }


        [Test, TestCaseSource("RandomContactDataProvider")]
        public void TestProjectCreation(ProjectData project)
        {
            //preparation
            AccountData account = new AccountData("administrator", "root") { };
            Mantis.ProjectData[] oldProjects = app.API.GetProjectsThroughAPI(account, project);

            //action
            app.Project.Create(project);

            //verification
            Assert.AreEqual(oldProjects.Length + 1, app.Project.GetProjectCount());
            Mantis.ProjectData[] newProjects = app.API.GetProjectsThroughAPI(account, project);

            //oldProjects.Add(project);
            //oldProjects.Sort();
            //newProjects.Sort();

            Assert.AreEqual(oldProjects.Length + 1, newProjects.Length);
        }

        //[Test]
        //public void AddNewProjectThroughAPI()
        //{
        //    AccountData account = new AccountData()
        //    {
        //        Username = "administrator",
        //        Password = "root"
        //    };

        //    ProjectData project = new ProjectData()
        //    {
        //        Name = "The first of API tests"
        //    };

        //    app.API.CreateProjectThroughAPI(account, project);

        //}

    }
}