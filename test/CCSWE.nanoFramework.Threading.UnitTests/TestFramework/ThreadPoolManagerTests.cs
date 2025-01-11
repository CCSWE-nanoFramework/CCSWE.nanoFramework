using CCSWE.nanoFramework.Threading.TestFramework;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Threading.UnitTests.TestFramework
{
    [TestClass]
    public class ThreadPoolManagerTests
    {
        [TestMethod]
        public void Reset_sets_ThreadPool_to_initial_state()
        {
            const int initialThreads = 4;
            ThreadPool.SetMinThreads(initialThreads, true);
            Assert.AreEqual(initialThreads, ThreadPool.ThreadCount, "Threads before reset");

            ThreadPoolManager.Reset();
            Assert.AreEqual(0, ThreadPool.ThreadCount, "Threads after reset");
        }
    }
}
