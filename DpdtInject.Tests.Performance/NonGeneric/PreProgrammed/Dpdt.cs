﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace DpdtInject.Tests.Performance.NonGeneric.PreProgrammed
{

    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    [GcServer(true)]
    public class Dpdt
    {
        private DpdtPerformanceModule _kernel;

        [GlobalSetup]
        public void Setup()
        {
            _kernel = new DpdtPerformanceModule(
                );

            _kernel.Get(typeof(IA));
            _kernel.Get(typeof(IB));
            _kernel.Get(typeof(IC));
        }

        [Benchmark]
        public void Actual()
        {
            _kernel.Get(typeof(IA));
            _kernel.Get(typeof(IB));
            _kernel.Get(typeof(IC));
        }

    }

}
