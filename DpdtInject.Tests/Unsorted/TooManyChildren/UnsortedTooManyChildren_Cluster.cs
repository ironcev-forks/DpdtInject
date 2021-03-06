﻿using DpdtInject.Injector;
using DpdtInject.Injector.Excp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpdtInject.Tests.Unsorted.TooManyChildren
{
    public partial class UnsortedTooManyChildren_Cluster : DefaultCluster
    {
        public override void Load()
        {
            Bind<IA>()
                .To<A0>()
                .WithTransientScope()
                ;

            Bind<IA>()
                .To<A1>()
                .WithTransientScope()
                ;

            Bind<IB>()
                .To<B>()
                .WithTransientScope()
                ;
        }

        public class UnsortedTooManyChildren_ClusterTester
        {
            public void PerformClusterTesting()
            {
                var cluster = new FakeCluster<UnsortedTooManyChildren_Cluster>(
                    null
                    );

                //try
                //{
                //    var b0 = cluster.Get<IB>();

                //    Assert.Fail("this line should never be executed");
                //}
                //catch(DpdtException excp)
                //when (excp.Type == DpdtExceptionTypeEnum.DuplicateBinding && excp.AdditionalArgument == typeof(IA).FullName)
                //{
                //    //this is ok
                //}
            }
        }

    }


    public interface IA
    {

    }

    public class A0 : IA
    {

    }
    public class A1 : IA
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
