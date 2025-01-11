namespace CCSWE.nanoFramework.NeoPixel.Benchmarks
{
    public abstract class NeoPixelStripBenchmarkBase
    {
        public const int IterationCount = 100;

        public abstract void Setup();

        public abstract void Fill();
    }
}
