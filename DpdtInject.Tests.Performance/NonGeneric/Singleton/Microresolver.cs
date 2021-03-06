﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using MicroResolver;

namespace DpdtInject.Tests.Performance.NonGeneric.Singleton
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    [GcServer(true)]
    public class Microresolver
    {
        protected ObjectResolver _kernel;

        [GlobalSetup]
        public void Setup()
        {
            _kernel = ObjectResolver.Create();

            _kernel.Register<IA, A>(Lifestyle.Singleton);
            _kernel.Register<IB, B>(Lifestyle.Singleton);
            _kernel.Register<IC, C>(Lifestyle.Singleton);

            _kernel.Compile();

            _kernel.Resolve(typeof(IA));
            _kernel.Resolve(typeof(IB));
            _kernel.Resolve(typeof(IC));
        }

        [Benchmark]
        public void NonGenericSingleton()
        {
            _kernel!.Resolve(typeof(IA));
            _kernel!.Resolve(typeof(IB));
            _kernel!.Resolve(typeof(IC));
        }

    }
}
