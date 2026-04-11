using CCSWE.nanoFramework.NeoPixel;
using CCSWE.nanoFramework.NeoPixel.Drivers;
using nanoFramework.TestFramework;

namespace UnitTests.CCSWE.nanoFramework.NeoPixel;
[TestClass]
public class NeoPixelDriverTests
{
    // Use the typical ESP32 APB clock frequency so tests match real-device behaviour.
    // With ClockDivider=2 this gives 0.025 µs per RMT tick.
    private const float SourceClockFrequency = 80_000_000f;

    // ------------------------------------------------------------------ Ws2812B

    [TestMethod]
    public void Ws2812B_should_have_correct_color_order()
    {
        var driver = new Ws2812B(ColorOrder.GRB, SourceClockFrequency);

        Assert.AreEqual(ColorOrder.GRB, driver.ColorOrder);
    }

    [TestMethod]
    public void Ws2812B_should_have_correct_zero_pulse()
    {
        // T0H = 0.4 µs → 0.4 / 0.025 = 16 ticks
        // T0L = 0.85 µs → 0.85 / 0.025 = 34 ticks
        var driver = new Ws2812B(ColorOrder.GRB, SourceClockFrequency);

        Assert.AreEqual((byte)16, driver.ZeroPulse.Duration0);
        Assert.AreEqual((byte)128, driver.ZeroPulse.Level0);
        Assert.AreEqual((byte)34, driver.ZeroPulse.Duration1);
        Assert.AreEqual((byte)0, driver.ZeroPulse.Level1);
    }

    [TestMethod]
    public void Ws2812B_should_have_correct_one_pulse()
    {
        // T1H = 0.8 µs → 0.8 / 0.025 = 32 ticks
        // T1L = 0.45 µs → 0.45 / 0.025 = 18 ticks
        var driver = new Ws2812B(ColorOrder.GRB, SourceClockFrequency);

        Assert.AreEqual((byte)32, driver.OnePulse.Duration0);
        Assert.AreEqual((byte)128, driver.OnePulse.Level0);
        Assert.AreEqual((byte)18, driver.OnePulse.Duration1);
        Assert.AreEqual((byte)0, driver.OnePulse.Level1);
    }

    [TestMethod]
    public void Ws2812B_should_have_correct_reset_pulse()
    {
        // Reset = 50 µs → 50 / 0.025 = 2000 ticks
        // 2000 encoded as two bytes: low = 2000 % 256 = 208, high = 2000 / 256 = 7
        var driver = new Ws2812B(ColorOrder.GRB, SourceClockFrequency);

        Assert.AreEqual((byte)208, driver.ResetPulse.Duration0);
        Assert.AreEqual((byte)7, driver.ResetPulse.Level0);
        Assert.AreEqual((byte)208, driver.ResetPulse.Duration1);
        Assert.AreEqual((byte)7, driver.ResetPulse.Level1);
    }

    // ------------------------------------------------------------------ Ws2812C

    [TestMethod]
    public void Ws2812C_should_have_correct_color_order()
    {
        var driver = new Ws2812C(ColorOrder.GRB, SourceClockFrequency);

        Assert.AreEqual(ColorOrder.GRB, driver.ColorOrder);
    }

    [TestMethod]
    public void Ws2812C_should_have_correct_zero_pulse()
    {
        // T0H = 0.3 µs → 0.3 / 0.025 = 12 ticks
        // T0L = 1.09 µs → 1.09 / 0.025 = 43.6 → 43 ticks (truncated)
        var driver = new Ws2812C(ColorOrder.GRB, SourceClockFrequency);

        Assert.AreEqual((byte)12, driver.ZeroPulse.Duration0);
        Assert.AreEqual((byte)128, driver.ZeroPulse.Level0);
        Assert.AreEqual((byte)43, driver.ZeroPulse.Duration1);
        Assert.AreEqual((byte)0, driver.ZeroPulse.Level1);
    }

    [TestMethod]
    public void Ws2812C_should_have_correct_one_pulse()
    {
        // T1H = 1.09 µs → 1.09 / 0.025 = 43.6 → 43 ticks (truncated)
        // T1L = 0.32 µs → 0.32 / 0.025 = 12.8 → 12 ticks (truncated)
        var driver = new Ws2812C(ColorOrder.GRB, SourceClockFrequency);

        Assert.AreEqual((byte)43, driver.OnePulse.Duration0);
        Assert.AreEqual((byte)128, driver.OnePulse.Level0);
        Assert.AreEqual((byte)12, driver.OnePulse.Duration1);
        Assert.AreEqual((byte)0, driver.OnePulse.Level1);
    }

    [TestMethod]
    public void Ws2812C_should_have_correct_reset_pulse()
    {
        // Reset = 300 µs → 300 / 0.025 = 12000 ticks
        // 12000 encoded as two bytes: low = 12000 % 256 = 224, high = 12000 / 256 = 46
        var driver = new Ws2812C(ColorOrder.GRB, SourceClockFrequency);

        Assert.AreEqual((byte)224, driver.ResetPulse.Duration0);
        Assert.AreEqual((byte)46, driver.ResetPulse.Level0);
        Assert.AreEqual((byte)224, driver.ResetPulse.Duration1);
        Assert.AreEqual((byte)46, driver.ResetPulse.Level1);
    }
}
