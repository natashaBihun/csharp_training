using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager appManager;
        public static Random random = new Random();

        [SetUp]
        public void SetupApplicationManager()
        {
            appManager = ApplicationManager.GetInstance();
        }

        public static string GenerateRandomString(int max)
        {            
            int length = Convert.ToInt32(random.NextDouble() * max);
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(random.NextDouble() * 65)));
            }

            return builder.ToString();
        }
    }
}
