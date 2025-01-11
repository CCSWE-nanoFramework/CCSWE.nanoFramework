#if DEBUG
using System;
using System.Diagnostics;
#endif
using System;
using System.Device.Gpio;
using System.Drawing;
using System.Threading;
using CCSWE.nanoFramework.NeoPixel.Benchmarks.Reference;
using nanoFramework.Benchmark;

namespace CCSWE.nanoFramework.NeoPixel.Benchmarks
{
    public class Program
    {
        public static void Main()
        {
#if DEBUG
            Console.WriteLine("Benchmarks should be run in a release build.");
            Debugger.Break();
            return;
#endif
            // Enable LDO2 on UM FeatherS3
            var gpioController = new GpioController();
            gpioController.OpenPin(39, PinMode.Output).Write(PinValue.High);

            Console.WriteLine("Running benchmarks...");

            BenchmarkRunner.RunClass(typeof(DevelopmentBenchmarks));

            BenchmarkRunner.RunClass(typeof(ColorConverterBenchmarks));
            BenchmarkRunner.RunClass(typeof(NeoPixelStripBenchmarks));
            //BenchmarkRunner.RunClass(typeof(SampleNeoPixelStripBenchmarks));
            BenchmarkRunner.RunClass(typeof(Ws28xxNeoPixelStripBenchmarks));

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
