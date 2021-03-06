﻿using System;
using System.Diagnostics;

namespace DpdtInject.Tests.Performance.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dpdt = new DpdtInject.Tests.Performance.TimeConsume.BigTree0.Dpdt();
            var dryioc = new DpdtInject.Tests.Performance.TimeConsume.BigTree0.DryIoc();
            var mr = new DpdtInject.Tests.Performance.TimeConsume.BigTree0.Microresolver();

            Check(
                dpdt.GetType().Name,
                () => dpdt.Setup(),
                () => dpdt.GenericTest()
                );

            Check(
                dryioc.GetType().Name,
                () => dryioc.Setup(),
                () => dryioc.GenericTest()
                );

            Check(
                mr.GetType().Name,
                () => mr.Setup(),
                () => mr.GenericTest()
                );

        }

        private static void Check(
            string type,
            Action setup,
            Action doTest
            )
        {
            Console.WriteLine($"{type} Start");

            Console.WriteLine($"{type} Setup");
            setup();

            Console.WriteLine($"{type} Warmup");
            for (var index = 0; index < 100; index++)
            {
                doTest();
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var sw = new Stopwatch();
            sw.Start();
            for (var index = 0; index < 100; index++)
            {
                doTest();
            }
            sw.Stop();
            Console.WriteLine($"{type}: Elapsed ms: {sw.ElapsedMilliseconds}");

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
