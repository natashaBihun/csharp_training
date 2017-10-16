using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class SearchTests : AuthTestBase
    {
        [Test]
        public void TestSearch() {
            int numberOfSearchResults = appManager.Contacts.GetNumberOfSearchResults();
            int countOfContacts = appManager.Contacts.GetContactCount();

            Assert.AreEqual(numberOfSearchResults, countOfContacts);
        }
    }
}
