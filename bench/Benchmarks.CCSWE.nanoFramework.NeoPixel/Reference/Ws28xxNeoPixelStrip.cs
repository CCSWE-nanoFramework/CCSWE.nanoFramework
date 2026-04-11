using System.Drawing;
using Iot.Device.Ws28xx.Esp32;

namespace Benchmarks.CCSWE.nanoFramework.NeoPixel.Reference;

// ReSharper disable once InconsistentNaming
internal class Ws28xxNeoPixelStrip
{
    private readonly ushort _count;
    private readonly Ws2812c _ws28xx;

    public Ws28xxNeoPixelStrip(byte pin, ushort count)
    {
        _count = count;

        _ws28xx = new Ws2812c(pin, count);
    }

    public ushort Count => _count;

    public void Fill(Color color)
    {
        _ws28xx.Image.Clear(color);
    }

    public void SetLed(int index, Color color)
    {
        for (var i = 0; i < _count; i++)
        {
            _ws28xx.Image.SetPixel(i, 0, color);
        }
    }

    public void Update()
    {
        _ws28xx.Update();
    }
}