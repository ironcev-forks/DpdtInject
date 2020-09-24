using DpdtInject.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace DpdtInject.Tests.Cluster.DifferentObjectDifferentCluster5Func
{
    [TestClass]
    public class ClusterDifferentObjectDifferentCluster5FuncFixture
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Test()
        {
            var preparation = new Preparator(
                TestContext,
                nameof(ClusterDifferentObjectDifferentCluster5FuncModule.ClusterDifferentObjectDifferentCluster5FuncModuleTester),
                nameof(TestResources.ClusterDifferentObjectDifferentCluster5FuncModule),
                TestResources.ClusterDifferentObjectDifferentCluster5FuncModule
                );

            preparation.Check();

            Assert.AreEqual(0, preparation.DiagnosticReporter.ErrorCount, "Error count");
            Assert.AreEqual(0, preparation.DiagnosticReporter.WarningCount, "Warning count");
        }

    }
}
