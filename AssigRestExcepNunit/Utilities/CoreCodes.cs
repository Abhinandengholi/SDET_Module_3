using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Serilog;

namespace AssigRestExcepNunit.Utilities
{
    public class CoreCodes
    {
        protected RestClient client;
        protected ExtentReports extent;
        ExtentSparkReporter sparkReporter;
        protected ExtentTest test;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string currDir = Directory.GetParent(@"../../../").FullName;
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currDir + "/ExtentReports/extent-report"
                + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");

            extent.AttachReporter(sparkReporter);
            string logfilepath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                 .WriteTo.Console()
                 .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day)
                 .CreateLogger();
        }
        [SetUp]

        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }
    }
}

