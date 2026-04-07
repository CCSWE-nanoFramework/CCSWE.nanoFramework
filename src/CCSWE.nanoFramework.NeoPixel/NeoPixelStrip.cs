using nanoFramework.Hardware.Esp32.Rmt;
using System;
using System.Drawing;
using CCSWE.nanoFramework.NeoPixel.Drivers;
using CCSWE.nanoFramework.NeoPixel.Rmt;

namespace CCSWE.nanoFramework.NeoPixel;

/// <summary>
/// Represents a strip of NeoPixel LEDs.
/// </summary>
public class NeoPixelStrip : IDisposable
{
    private const byte BitsPerLed = 24;

    private readonly ColorOrder _colorOrder;
    private readonly byte[] _data;
    private bool _disposed;
    private readonly object _lock = new();
    private readonly NeoPixelPulse _onePulse;
    private readonly TransmitterChannel _transmitterChannel;
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

        var totalBits = BitsPerLed * Count;
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
    /// Releases the RMT channel when the instance is garbage collected.
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
    /// Fills the entire strip with a <see cref="Color"/>.
    /// </summary>
    /// <param name="color">The <see cref="Color"/>.</param>
    public void Fill(Color color)
    {
        WriteLeds(0, Count - 1, color.ToBytes(_colorOrder));
    }

    /// <summary>
    /// Fills the entire strip with a brightness-scaled <see cref="Color"/>.
    /// </summary>
    /// <param name="color">The <see cref="Color"/>.</param>
    /// <param name="brightness">The brightness value between 0.0 and 1.0.</param>
    public void Fill(Color color, float brightness)
    {
        Fill(ColorConverter.ScaleBrightness(color, brightness));
    }

    private static int GetStartIndex(int index) => index * BitsPerLed * 4;

    /// <summary>
    /// Sets the <see cref="Color"/> for a single LED.
    /// </summary>
    /// <param name="index">The zero-based index of the LED.</param>
    /// <param name="color">The <see cref="Color"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="index"/> is outside the range [0, <see cref="Count"/>).
    /// </exception>
    public void SetLed(int index, Color color)
    {
        SetLeds(index, index, color);
    }

    /// <summary>
    /// Sets the brightness-scaled <see cref="Color"/> for a single LED.
    /// </summary>
    /// <param name="index">The zero-based index of the LED.</param>
    /// <param name="color">The <see cref="Color"/>.</param>
    /// <param name="brightness">The brightness value between 0.0 and 1.0.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="index"/> is outside the range [0, <see cref="Count"/>).
    /// </exception>
    public void SetLed(int index, Color color, float brightness)
    {
        SetLeds(index, index, color, brightness);
    }

    /// <summary>
    /// Sets the <see cref="Color"/> for a contiguous range of LEDs. The brightness is scaled
    /// once and the same scaled color is written to every LED in the range.
    /// </summary>
    /// <param name="startIndex">The zero-based index of the first LED (inclusive).</param>
    /// <param name="endIndex">The zero-based index of the last LED (inclusive).</param>
    /// <param name="color">The <see cref="Color"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="startIndex"/> or <paramref name="endIndex"/> is outside
    /// the range [0, <see cref="Count"/>), or when <paramref name="startIndex"/> is greater
    /// than <paramref name="endIndex"/>.
    /// </exception>
    public void SetLeds(int startIndex, int endIndex, Color color)
    {
        if (startIndex < 0 || startIndex >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        }

        if (endIndex < startIndex || endIndex >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(endIndex));
        }

        WriteLeds(startIndex, endIndex, color.ToBytes(_colorOrder));
    }

    /// <summary>
    /// Sets the brightness-scaled <see cref="Color"/> for a contiguous range of LEDs. The
    /// brightness is scaled once and the same scaled color is written to every LED in the range.
    /// </summary>
    /// <param name="startIndex">The zero-based index of the first LED (inclusive).</param>
    /// <param name="endIndex">The zero-based index of the last LED (inclusive).</param>
    /// <param name="color">The <see cref="Color"/>.</param>
    /// <param name="brightness">The brightness value between 0.0 and 1.0.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="startIndex"/> or <paramref name="endIndex"/> is outside
    /// the range [0, <see cref="Count"/>), or when <paramref name="startIndex"/> is greater
    /// than <paramref name="endIndex"/>.
    /// </exception>
    public void SetLeds(int startIndex, int endIndex, Color color, float brightness)
    {
        SetLeds(startIndex, endIndex, ColorConverter.ScaleBrightness(color, brightness));
    }

    /// <summary>
    /// Sends the buffered LED data to the strip.
    /// </summary>
    public void Update()
    {
        _transmitterChannel.SendData(_data, false);
    }

    private void WriteLeds(int startIndex, int endIndex, byte[] colorBytes)
    {
        var commandIndex = GetStartIndex(startIndex);

        for (var ledIndex = startIndex; ledIndex <= endIndex; ledIndex++)
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
}