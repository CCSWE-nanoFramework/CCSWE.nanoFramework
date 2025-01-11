using nanoFramework.Hardware.Esp32.Rmt;

namespace CCSWE.nanoFramework.NeoPixel.Rmt
{
    /// <summary>
    /// Specifies the duration and level for an RMT pulse similar to <see cref="RmtCommand"/>
    /// </summary>
    public struct NeoPixelPulse
    {
        /// <summary>
        /// The duration in RMT cycles of the start of the pulse.
        /// </summary>
        public byte Duration0;

        /// <summary>
        /// The level of the start of the pulse.
        /// </summary>
        public byte Level0;

        /// <summary>
        /// The duration in RMT cycles of the end of the pulse.
        /// </summary>
        public byte Duration1;

        /// <summary>
        /// The level of the end of the pulse.
        /// </summary>
        public byte Level1;

        /// <summary>
        /// Creates a new <see cref="NeoPixelPulse"/> from a byte array.
        /// </summary>
        /// <param name="data">An array containing the <see cref="Duration0"/>, <see cref="Level0"/>, <see cref="Duration1"/>, and <see cref="Level1"/> values.</param>
        public NeoPixelPulse(byte[] data) : this(data[0], data[1], data[2], data[3])
        {

        }

        /// <summary>
        /// Creates a new <see cref="NeoPixelPulse"/> from a byte array.
        /// </summary>
        /// <param name="duration0">The duration in RMT cycles of the start of the pulse.</param>
        /// <param name="level0">The level of the start of the pulse.</param>
        /// <param name="duration1">The duration in RMT cycles of the end of the pulse.</param>
        /// <param name="level1">The level of the end of the pulse.</param>
        public NeoPixelPulse(byte duration0, byte level0, byte duration1, byte level1)
        {
            Duration0 = duration0;
            Level0 = level0;
            Duration1 = duration1;
            Level1 = level1;
        }
    }
}
