﻿using DpdtInject.Injector;
using DpdtInject.Injector.Module;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DpdtInject.Injector.Module.Bind;

namespace DpdtInject.Tests.Singleton.Hierarchy2
{
    public partial class SingletonHierarchy2Module : DpdtModule
    {
        public const string Message = "some message";

        public override void Load()
        {
            Bind<IA>()
                .To<A>()
                .WithSingletonScope()
                .InCluster<DefaultCluster>()
                ;

            Bind<IB>()
                .To<B>()
                .WithSingletonScope()
                .InCluster<DefaultCluster>()
                .Configure(new ConstructorArgument("message", Message))
                ;
        }

        public partial class DefaultCluster
        {

        }

        public class SingletonHierarchy2ModuleTester
        {
            public void PerformModuleTesting()
            {
                var module = new FakeModule<SingletonHierarchy2Module>();

                var b0 = module.Get<IB>();
                Assert.IsNotNull(b0);
                Assert.IsNotNull(b0.A);
                Assert.AreEqual(Message, b0.Message);

                var b1 = module.Get<IB>();
                Assert.IsNotNull(b1);
                Assert.IsNotNull(b1.A);

                Assert.AreSame(b0, b1);

                var bb = module.GetAll<IB>();
                Assert.IsNotNull(bb);
                Assert.AreEqual(1, bb.Count);
                Assert.AreSame(b0, bb[0]);
            }
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
        string Message { get; }

        IA A { get; }
    }

    public class B : IB
    {
        public string Message { get; }
        public IA A { get; }

        public B(string message, IA a)
        {
            Message = message;
            A = a;
        }
    }

}
