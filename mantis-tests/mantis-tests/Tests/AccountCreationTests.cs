using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [SetUp]
        public void SetUpConfig() {
            appManager.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = 
                File.Open(Path.Combine(TestContext.CurrentContext.WorkDirectory, @"config_inc.php"), FileMode.Open)) {
                appManager.Ftp.UploadFile("/config_inc.php", localFile);
            }                
        }

        [Test]
        public void AccountRegistrationTest()
        {
            AccountData account = new AccountData() {
                Name = "testuser",
                Password = "password",
                Email = "testuser@localhost.localdomain"
            };
            List<AccountData> accounts = appManager.Admin.GetAllAccounts();
            AccountData existingAccount = accounts.Find(t => t.Name == account.Name);

            if (existingAccount != null) {
                appManager.Admin.DeleteAccount(existingAccount);
            }            

            appManager.James.Delete(account);
            appManager.James.Add(account);

            appManager.Registration.Register(account);
        }

        [TearDown]
        public void RestoreConfig()
        {
            appManager.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }
}
