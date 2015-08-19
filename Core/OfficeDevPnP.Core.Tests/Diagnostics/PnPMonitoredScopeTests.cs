﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeDevPnP.Core.Diagnostics;

namespace OfficeDevPnP.Core.Tests.Diagnostics
{
    [TestClass]
    public class PnPMonitoredScopeTests
    {
        [TestMethod]
        public void TextException()
        {
            using (var ctx = TestCommon.CreateClientContext())
            {
                using (var scope1 = new PnPMonitoredScope("1.0"))
                {

                    Assert.IsTrue(scope1.CorrelationId != Guid.Empty);
                    Assert.IsTrue(scope1.Parent == null);

                    using (var scope1_1 = new PnPMonitoredScope("1.1"))
                    {
                        Assert.IsTrue(scope1_1.Parent != null);

                        using (var scope1_1_1 = new PnPMonitoredScope("1.1.1"))
                        {
                            Assert.IsTrue(scope1_1_1.Parent != null && scope1_1_1.Parent != scope1);
                        }

                        using (var scope1_1_2 = new PnPMonitoredScope("1.1.2"))
                        {
                            Assert.IsTrue(scope1_1_2.Parent != null && scope1_1_2.Parent == scope1_1);

                            using (var scope1_1_2_1 = new PnPMonitoredScope("1.1.2.1"))
                            {
                                Assert.IsTrue(scope1_1_2_1.Parent != null && scope1_1_2_1.Parent == scope1_1_2);
                            }

                        }
                    }
                }
            }
        }
    }
}
