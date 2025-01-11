namespace CCSWE.nanoFramework.NeoPixel.Drivers
{
    /// <summary>
    /// Commands required for the WS2812B LED as specified in https://cdn-shop.adafruit.com/datasheets/WS2812B.pdf
    /// </summary>
    public class Ws2812B: NeoPixelDriver
    {
        /// <summary>
        /// Create a new <see cref="Ws2812B"/> chipset.
        /// </summary>
        /// <param name="colorOrder">The color order of the LED.</param>
        public Ws2812B(ColorOrder colorOrder = ColorOrder.GRB) : base(0.4f, 0.85f, 0.8f, 0.45f, 50, colorOrder)
        {
        }
    }
}
