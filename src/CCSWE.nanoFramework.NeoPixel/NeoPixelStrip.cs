using nanoFramework.Hardware.Esp32.Rmt;
using System;
using System.Drawing;
using CCSWE.nanoFramework.NeoPixel.Drivers;
using CCSWE.nanoFramework.NeoPixel.Rmt;

namespace CCSWE.nanoFramework.NeoPixel
{
    /// <summary>
    /// Represents a strip of LEDs.
    /// </summary>
    public class NeoPixelStrip: IDisposable
    {
        private const byte BitsPerLed = 24;

        private readonly byte[] _data;
        private bool _disposed;
        private readonly object _lock = new();
        private readonly TransmitterChannel _transmitterChannel;

        private readonly ColorOrder _colorOrder;
        private readonly NeoPixelPulse _onePulse;
        private readonly NeoPixelPulse _zeroPulse;

        /// <summary>
        /// Initializes a new instance of the <see cref="NeoPixelStrip"/> class.
        /// </summary>
        /// <param name="pin">The GPIO pin used for communication with the LED driver.</param>
        /// <param name="count">The number of LEDs in the strip.</param>
        /// <param name="driver">The LED driver.</param>
        public NeoPixelStrip(byte pin, ushort count, NeoPixelDriver driver)
        {
            Count = count;

            var transmitterChannelSettings = new TransmitChannelSettings(pinNumber: pin)
            {
                EnableCarrierWave = false,
                ClockDivider = driver.ClockDivider,
                IdleLevel = false,
            };
            _transmitterChannel = new TransmitterChannel(transmitterChannelSettings);

            var totalBits = 24 * Count;

            _data = new byte[(totalBits + 1) * 4];

            var resetIndex = _data.Length - 4;

            _data[resetIndex + 0] = driver.ResetPulse.Duration0;
            _data[resetIndex + 1] = driver.ResetPulse.Level0;
            _data[resetIndex + 2] = driver.ResetPulse.Duration1;
            _data[resetIndex + 3] = driver.ResetPulse.Level1;

            _colorOrder = driver.ColorOrder;
            _onePulse = driver.OnePulse;
            _zeroPulse = driver.ZeroPulse;

            Clear();
        }

        /// <summary>
        /// Gets the number of LEDs in the strip.
        /// </summary>
        public ushort Count { get; }

        /// <summary>
        /// Close and release the RMT channel.
        /// </summary>
        ~NeoPixelStrip() => Dispose(false);

        /// <summary>
        /// Resets all LEDs to <see cref="Color.Black"/>.
        /// </summary>
        public void Clear()
        {
            Fill(Color.Black);
        }

        /// <summary>
        /// Close and release the RMT channel.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            lock (_lock)
            {
                if (_disposed)
                {
                    return;
                }

                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _transmitterChannel.Dispose();
            }

            _disposed = true;
        }

        /// <summary>
        /// Fill the strip with a <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The <see cref="Color"/>.</param>
        public void Fill(Color color)
        {
            var colorBytes = color.ToBytes(_colorOrder);
            var commandIndex = 0;
            ushort ledIndex;

            for (ledIndex = 0; ledIndex < Count; ledIndex++)
            {
                byte colorIndex;

                for (colorIndex = 0; colorIndex < 3; colorIndex++)
                {
                    byte bitIndex;
                    byte colorByte = colorBytes[colorIndex];
                    for (bitIndex = 0; bitIndex < 8; bitIndex++)
                    {
                        if ((colorByte & 128) != 0)
                        {
                            _data[0 + commandIndex] = _onePulse.Duration0;
                            _data[1 + commandIndex] = _onePulse.Level0;
                            _data[2 + commandIndex] = _onePulse.Duration1;
                            _data[3 + commandIndex] = _onePulse.Level1;
                        }
                        else
                        {
                            _data[0 + commandIndex] = _zeroPulse.Duration0;
                            _data[1 + commandIndex] = _zeroPulse.Level0;
                            _data[2 + commandIndex] = _zeroPulse.Duration1;
                            _data[3 + commandIndex] = _zeroPulse.Level1;
                        }

                        colorByte <<= 1;
                        commandIndex += 4;
                    }
                }
            }
        }

        /// <inheritdoc cref="Fill(System.Drawing.Color,float)"/>
        [Obsolete("Use float instead of double")]
        public void Fill(Color color, double brightness) => Fill(color, (float)brightness);

        /// <summary>
        /// Fill the strip with a <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The <see cref="Color"/>.</param>
        /// <param name="brightness">The brightness value between 0.0 and 1.0.</param>
        public void Fill(Color color, float brightness)
        {
            Fill(ColorConverter.ScaleBrightness(color, brightness));
        }

        private static int GetStartIndex(int index)
        {
            return index * BitsPerLed * 4;
        }

        /// <summary>
        /// Sets the <see cref="Color"/> for a LED. 
        /// </summary>
        /// <param name="index">The index of the LED.</param>
        /// <param name="color">The <see cref="Color"/>.</param>
        public void SetLed(int index, Color color)
        {
            var colorBytes = color.ToBytes(_colorOrder);
            byte colorIndex;
            var commandIndex = GetStartIndex(index);

            for (colorIndex = 0; colorIndex < 3; colorIndex++)
            {
                byte bitIndex;
                byte colorByte = colorBytes[colorIndex];
                for (bitIndex = 0; bitIndex < 8; bitIndex++)
                {
                    if ((colorByte & 128) != 0)
                    {
                        _data[0 + commandIndex] = _onePulse.Duration0;
                        _data[1 + commandIndex] = _onePulse.Level0;
                        _data[2 + commandIndex] = _onePulse.Duration1;
                        _data[3 + commandIndex] = _onePulse.Level1;
                    }
                    else
                    {
                        _data[0 + commandIndex] = _zeroPulse.Duration0;
                        _data[1 + commandIndex] = _zeroPulse.Level0;
                        _data[2 + commandIndex] = _zeroPulse.Duration1;
                        _data[3 + commandIndex] = _zeroPulse.Level1;
                    }

                    colorByte <<= 1;
                    commandIndex += 4;
                }
            }
        }

        /// <inheritdoc cref="SetLed(int,System.Drawing.Color,float)"/>
        [Obsolete("Use float instead of double")]
        public void SetLed(int index, Color color, double brightness) => SetLed(index, color, (float)brightness);

        /// <summary>
        /// Sets the <see cref="Color"/> for a LED. 
        /// </summary>
        /// <param name="index">The index of the LED.</param>
        /// <param name="color">The <see cref="Color"/>.</param>
        /// <param name="brightness">The brightness value between 0.0 and 1.0.</param>
        /// <remarks>If you are using the same <see cref="Color"/> for multiple LEDs it
        /// is more efficient to use <see cref="ColorConverter.ScaleBrightness(Color,float)"/> to
        /// adjust the brightness and pass that to <see cref="SetLed(int,Color)"/></remarks>
        public void SetLed(int index, Color color, float brightness)
        {
            SetLed(index, ColorConverter.ScaleBrightness(color, brightness));
        }

        /// <summary>
        /// Send the data to the LED strip.
        /// </summary>
        public void Update()
        {
            _transmitterChannel.SendData(_data, false);
        }
    }
}
