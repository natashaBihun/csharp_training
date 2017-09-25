﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager appManager;

        [SetUp]
        public void SetupTest()
        {
            appManager = ApplicationManager.GetInstance();
        }
    }
}
