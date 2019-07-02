using System;
using System.IO;
using NUnit.Framework;


namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]
        public void SetUpConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localfile = File.Open("C:/Users/Filimonov-I/source/repos/Hoventage/csharp.training/mantis-tests/mantis-tests/config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localfile);
            }
        }
        
        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData("testuser1", "password", "testuser1@localhost.localdomain");

            app.Registration.Register(account);
        }
        [OneTimeTearDown]
        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }
}