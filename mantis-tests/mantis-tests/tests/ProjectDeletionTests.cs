using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectDeletionTests : AuthTestBase
    {
        public static IEnumerable<ProjectData> RandomContactDataProvider()
        {
            List<ProjectData> projects = new List<ProjectData>();
            for (int i = 0; i < 5; i++)
            {
                projects.Add(new ProjectData(GenerateRandomString(10))
                {
                });
            }
            return projects;
        }


        [Test]
        public void TestProjectDeletion()
        {
            //preparation
            AccountData account = new AccountData("administrator", "root") { };
            ProjectData project = new ProjectData(GenerateRandomString(10));
            List<ProjectData> oldProjects = app.API.GetProjectsThroughAPI(account);

            if (oldProjects == null)
            {
                app.API.CreateProjectThroughAPI(account, project);
            }

            //action
            app.Project.Delete(0);

            //verification
            Assert.AreEqual(oldProjects.Count - 1, app.Project.GetProjectCount());
            List<ProjectData> newProjects = app.API.GetProjectsThroughAPI(account);


            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}