﻿using DpdtInject.Injector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpdtInject.Tests.Scope.Transient.Generic.TwoInterfacesSingleObject
{
    public partial class ScopeTransientGenericTwoInterfacesSingleObject_Cluster : DefaultCluster
    {
        public override void Load()
        {
            Bind<IA0, IA1>()
                .To<A>()
                .WithTransientScope()
                ;

        }

        public class ScopeTransientGenericTwoInterfacesSingleObject_ClusterTester
        {
            public void PerformClusterTesting()
            {
                var cluster = new FakeCluster<ScopeTransientGenericTwoInterfacesSingleObject_Cluster>(
                    null
                    );

                var a0 = cluster.Get<IA0>();
                Assert.IsNotNull(a0);
                Assert.IsTrue(a0.GetType() == typeof(A));

                var a1 = cluster.Get<IA1>();
                Assert.IsNotNull(a1);
                Assert.IsTrue(a1.GetType() == typeof(A));

                Assert.AreNotSame(a0, a1);
            }
        }

    }


    public interface IA0
    {

    }

    public interface IA1
    {

    }

    public class A : IA0, IA1
    {

    }
}
