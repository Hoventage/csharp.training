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


        [Test, TestCaseSource("RandomContactDataProvider")]
        public void TestProjectDeletion(ProjectData project)
        {
            //preparation
            List<ProjectData> oldProjects = app.Project.GetProjectList();
            if (oldProjects == null)
            {
                app.Project.Create(project);
                oldProjects = app.Project.GetProjectList();
            }

            //action
            app.Project.Delete(0);

            //verification
            Assert.AreEqual(oldProjects.Count - 1, app.Project.GetProjectCount());
            List<ProjectData> newProjects = app.Project.GetProjectList();
            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            
            Assert.AreEqual(oldProjects.Count, newProjects.Count);
        }
    }
}