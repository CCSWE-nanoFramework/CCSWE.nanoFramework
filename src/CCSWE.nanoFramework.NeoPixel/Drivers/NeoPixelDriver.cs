using CCSWE.nanoFramework.NeoPixel.Rmt;
using nanoFramework.Hardware.Esp32.Rmt;

namespace CCSWE.nanoFramework.NeoPixel.Drivers
{
    /// <summary>
    /// Provides the commands required to control the NeoPixel.
    /// </summary>
    public abstract class NeoPixelDriver
    {
        /// <summary>
        /// The frequency of the clock used for RMT timing.
        /// </summary>
        /// <remarks>Get frequency of clock used by RMT.</remarks>
        protected float SourceClockFrequency = RmtChannel.SourceClockFrequency; 

        /// <summary>
        /// Creates the driver to provide the commands required to control the NeoPixel.
        /// </summary>
        /// <param name="zeroPulseHigh">The time in microseconds a zero pulse is high (T0H).</param>
        /// <param name="zeroPulseLow">The time in microseconds a zero pulse is low (T0L).</param>
        /// <param name="onePulseHigh">The time in microseconds a one pulse is high (T1H).</param>
        /// <param name="onePulseLow">The time in microseconds a one pulse is low (T1L).</param>
        /// <param name="resetDuration">The time in microseconds for a reset command.</param>
        /// <param name="colorOrder">The <see cref="ColorOrder"/> of the pixels.</param>
        protected NeoPixelDriver(float zeroPulseHigh, float zeroPulseLow, float onePulseHigh, float onePulseLow, float resetDuration, ColorOrder colorOrder)
        {
            ColorOrder = colorOrder;
            OnePulse = GetDataPulse(onePulseHigh, onePulseLow);
            ZeroPulse = GetDataPulse(zeroPulseHigh, zeroPulseLow);
            ResetPulse = GetResetPulse(resetDuration);
        }

        /// <summary>
        /// The clock divider. (Real helpful right?)
        /// </summary>
        public byte ClockDivider { get; set; } = 2;

        /// <summary>
        /// Gets the <see cref="ColorOrder"/> used by this chipset.
        /// </summary>
        public ColorOrder ColorOrder { get; }

        /// <summary>
        /// The number of microseconds per RMT cycle. This is effectively the smallest pulse duration that can be sent.
        /// </summary>
        protected float MicrosecondsPerRmtCycle => 1_000_000.0f / RmtCyclesPerSecond;

        /// <summary>
        /// The pulse data for a one pulse (T1).
        /// </summary>
        public NeoPixelPulse OnePulse { get; }

        /// <summary>
        /// The pulse data for a reset pulse.
        /// </summary>
        public NeoPixelPulse ResetPulse { get; }

        /// <summary>
        /// The number of RMT cycles that occur in one second.
        /// </summary>
        private float RmtCyclesPerSecond => SourceClockFrequency / ClockDivider;

        /// <summary>
        /// The pulse data for a zero pulse (T0).
        /// </summary>
        public NeoPixelPulse ZeroPulse { get; }

        /// <summary>
        /// Creates the zero or one data pulse.
        /// </summary>
        /// <param name="highDuration">The time in microseconds the pulse is high.</param>
        /// <param name="lowDuration">The time in microseconds the pulse is low.</param>
        protected NeoPixelPulse GetDataPulse(float highDuration, float lowDuration) => 
            new((byte)(highDuration / MicrosecondsPerRmtCycle), 128, (byte) (lowDuration / MicrosecondsPerRmtCycle), 0);

        /// <summary>
        /// Create the reset pulse command.
        /// </summary>
        /// <param name="resetDuration">The time in microseconds for a reset command.</param>
        /// <returns>The reset pulse command.</returns>
        protected NeoPixelPulse GetResetPulse(float resetDuration)
        {
            var result = new byte[4];
            var duration0 = (ushort)(resetDuration / MicrosecondsPerRmtCycle);
            var duration1 = (ushort)(resetDuration / MicrosecondsPerRmtCycle);

            var remaining = duration0 % 256;
            result[0] = (byte)remaining;
            result[1] = (byte)((duration0 - remaining) / 256);

            remaining = duration1 % 256;
            result[2] = (byte)remaining;
            result[3] = (byte)((duration1 - remaining) / 256);

            return new NeoPixelPulse(result);
        }
    }
}
