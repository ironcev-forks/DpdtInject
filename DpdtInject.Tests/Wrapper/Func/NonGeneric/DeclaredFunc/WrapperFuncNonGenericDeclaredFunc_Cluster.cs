﻿using DpdtInject.Injector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpdtInject.Tests.Wrapper.Func.NonGeneric.DeclaredFunc
{
    public partial class WrapperFuncNonGenericDeclaredFunc_Cluster : DefaultCluster
    {
        public static readonly A AInstance = new A();
        public static readonly Func<IA> Funca = () => AInstance;

        public override void Load()
        {

            Bind<Func<IA>>()
                .WithConstScope(Funca)
                ;

            Bind<IB>()
                .To<B>()
                .WithTransientScope()
                ;
        }

        public class WrapperFuncNonGenericDeclaredFunc_ClusterTester
        {
            public void PerformClusterTesting()
            {
                var cluster = new FakeCluster<WrapperFuncNonGenericDeclaredFunc_Cluster>(
                    null
                    );

                var b0 = (IB) cluster.Get(typeof(IB));
                Assert.IsNotNull(b0);
                Assert.IsNotNull(b0.A);
                Assert.AreSame(AInstance, b0.A);

                var aff0 = (Func<Func<IA>>)cluster.Get(typeof(Func<Func<IA>>));
                Assert.IsNotNull(aff0);
                var af0 = aff0();
                Assert.AreSame(Funca, af0);
                var a0 = af0();
                Assert.AreSame(AInstance, a0);

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
        IA A { get; }
    }

    public class B : IB
    {
        public IA A { get; }

        public B(Func<IA> af)
        {
            A = af();
        }

    }

}
