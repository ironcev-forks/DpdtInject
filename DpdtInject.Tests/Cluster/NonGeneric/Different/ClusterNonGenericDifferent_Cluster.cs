﻿using DpdtInject.Injector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpdtInject.Tests.Cluster.NonGeneric.Different
{
    public partial class ClusterNonGenericDifferent_RootCluster : DefaultCluster
    {
        public override void Load()
        {
            Bind<IA>()
                .To<A>()
                .WithTransientScope()
                ;
        }
    }

    public partial class ClusterNonGenericDifferent_ChildCluster : DefaultCluster
    {
        public override void Load()
        {
            Bind<IB>()
                .To<B>()
                .WithTransientScope()
                ;
        }
    }

    public class ClusterNonGenericDifferent_ClusterTester
    {
        public void PerformClusterTesting()
        {
            var rootCluster = new FakeCluster<ClusterNonGenericDifferent_RootCluster>(
                null
                );
            var childCluster = new FakeCluster<ClusterNonGenericDifferent_ChildCluster>(
                rootCluster
                );

            var a = (IA)rootCluster.Get(typeof(IA));
            Assert.IsNotNull(a);

            var b0 = (IB)childCluster.Get(typeof(IB));
            Assert.IsNotNull(b0);
            Assert.IsNotNull(b0.A);
        }
    }


    public interface IA
    {

    }

    public class A : IA
    {

    }

    public interface IB
    {
        IA A { get; }
    }

    public class B : IB
    {
        public IA A { get; }

        public B(IA a)
        {
            A = a;
        }

    }
}
