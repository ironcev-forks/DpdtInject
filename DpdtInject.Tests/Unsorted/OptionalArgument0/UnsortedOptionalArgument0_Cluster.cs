﻿using DpdtInject.Injector;
using DpdtInject.Injector.Excp;
using DpdtInject.Injector.Module.Bind;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpdtInject.Tests.Unsorted.OptionalArgument0
{
    public partial class UnsortedOptionalArgument0_Cluster : DefaultCluster
    {
        public const string DefaultMessage = "default message";
        public const string CustomMessage = "custom message";

        public override void Load()
        {
            Bind<IA>()
                .To<A>()
                .WithSingletonScope()
                .Configure(new ConstructorArgument("message", CustomMessage))
                ;
        }

        public class UnsortedOptionalArgument0_ClusterTester
        {
            public void PerformClusterTesting()
            {
                var cluster = new FakeCluster<UnsortedOptionalArgument0_Cluster>(
                    null
                    );

                var a = cluster.Get<IA>();
                Assert.IsNotNull(a);
                Assert.AreEqual(CustomMessage, a.Message);

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

        public A(string message = UnsortedOptionalArgument0_Cluster.DefaultMessage)
        {
            Message = message;
        }

    }

}
