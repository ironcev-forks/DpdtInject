﻿using DpdtInject.Injector;
using DpdtInject.Injector.Module.Bind;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpdtInject.Tests.Scope.Singleton.Generic.SingleObjectDefinedArgument
{
    public partial class ScopeSingletonGenericSingleObjectDefinedArgument_Cluster : DefaultCluster
    {
        const string CustomMesage = "custom message";

        public override void Load()
        {
            Bind<IA>()
                .To<A>()
                .WithSingletonScope()
                .Configure(new ConstructorArgument("message", CustomMesage))
                ;

        }

        public class ScopeSingletonGenericSingleObjectDefinedArgument_ClusterTester
        {
            public void PerformClusterTesting()
            {
                var cluster = new FakeCluster<ScopeSingletonGenericSingleObjectDefinedArgument_Cluster>(
                    null
                    );

                var a0 = cluster.Get<IA>();
                Assert.IsNotNull(a0);
                Assert.AreEqual(CustomMesage, a0.Message);
            }
        }

    }


    public interface IA
    {
        string Message { get; }
    }

    public class A : IA
    {
        public string Message { get; }

        public A(string message)
        {
            Message = message;
        }

    }
}
