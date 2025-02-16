using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;

namespace Automation.CasinoTests
{
    public class CasinoTestBase : BaseDrivers
    {
        protected CasinoPage Casino;
        protected Dictionary<string, TestStatus> PipelineTestsStatuses = new Dictionary<string, TestStatus>();

        [SetUp]
        public void InitializeCasinoTest()
        {
            Casino = new CasinoPage(Driver);

            string className = GetType().Name;
            if (className.Equals("CasinoTests"))
            {
                if (Driver.Url != Casino.Url)
                {
                    GenericMethods.GoToUrl(Casino.Url);
                }
            }
            else
            {
                throw new Exception("Invalid class name");
            }
        }

        [TearDown]
        public void FinalizeCasinoTest()
        {
            UpdatePipelineTestStatus();
        }

        private void UpdatePipelineTestStatus()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            TestStatus testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            if (testStatus == TestStatus.Failed && !PipelineTestsStatuses.ContainsKey(testName))
            {
                PipelineTestsStatuses.Add(testName, testStatus);
            }
            else if (testStatus == TestStatus.Passed && PipelineTestsStatuses.ContainsKey(testName))
            {
                PipelineTestsStatuses.Remove(testName);
            }
        }
    }
}



