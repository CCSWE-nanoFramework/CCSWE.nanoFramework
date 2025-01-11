namespace CCSWE.nanoFramework.NeoPixel.Drivers
{
    /// <summary>
    /// Commands required for the WS2812C LED.
    /// </summary>
    /// <remarks>I haven't tested this as I don't have any of these LEDs.</remarks>
    public class Ws2812C: NeoPixelDriver
    {
        /// <summary>
        /// Create a new <see cref="Ws2812C"/> chipset.
        /// </summary>
        /// <param name="colorOrder">The color order of the LED.</param>
        public Ws2812C(ColorOrder colorOrder = ColorOrder.GRB) : base(0.3f, 1.09f, 1.09f, 0.32f, 300, colorOrder)
        {
        }
    }
}
