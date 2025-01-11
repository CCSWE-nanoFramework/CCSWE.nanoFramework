using System.Drawing;
using nanoFramework.Hardware.Esp32.Rmt;

namespace CCSWE.nanoFramework.NeoPixel.Benchmarks.Reference
{
    /// <summary>
    /// This is a reference implementation based on https://github.com/nanoframework/Samples/tree/main/samples/Hardware.Esp32.Rmt/NeoPixelStripLowMemory.
    /// The code has been slightly altered to correct pulse timings. This implementation is very simple and much faster than the RmtCommand implementations
    /// (ie: https://github.com/nanoframework/nanoFramework.IoT.Device/tree/develop/devices/Ws28xx.Esp32). The goal is that my implementation should be
    /// as close to as fast as this code while adding additional functionality and flexibility.
    /// </summary>
    internal class SampleNeoPixelStrip
    {
        // 80MHz / 4 => min pulse 0.00us
        protected const byte ClockDivider = 2;
        protected const float MinPulse = 1000000.0f / (80000000.0f / ClockDivider);

        private readonly ushort _count;
        private readonly byte[] _data;

        private readonly byte[] _onePulse;
        private readonly byte[] _zeroPulse;
        private readonly byte[] _resPulse;

        private readonly TransmitterChannel _transmitterChannel;

        public SampleNeoPixelStrip(byte pin, ushort count)
        {
            _count = count;

            var transmitterChannelSettings = new TransmitChannelSettings(pinNumber: pin)
            {
                EnableCarrierWave = false,
                ClockDivider = ClockDivider,
                IdleLevel = false,
            };
            _transmitterChannel = new TransmitterChannel(transmitterChannelSettings);

            var totalBits = 24 * _count;

            _data = new byte[(totalBits + 1) * 4];

            _onePulse = new byte[] { (byte)(0.8 / MinPulse), 128, (byte)(0.45 / MinPulse), 0 };
            _zeroPulse = new byte[] { (byte)(0.4 / MinPulse), 128, (byte)(0.85 / MinPulse), 0 };
            _resPulse = GetResPulse(MinPulse);
        }

        public void Fill(Color color)
        {
            var colorBytes = color.ToBytes(ColorOrder.GRB);
            ushort led;
            int i = 0;
            for (led = 0; led < _count; led++)
            {
                byte col;
                for (col = 0; col < 3; col++)
                {
                    byte bit;
                    for (bit = 0; bit < 8; bit++)
                    {
                        if ((colorBytes[col] & 1 << bit) != 0)// && (led == _ledIndex))
                        {
                            _data[0 + i] = _onePulse[0];
                            _data[1 + i] = _onePulse[1];
                            _data[2 + i] = _onePulse[2];
                            _data[3 + i] = _onePulse[3];
                        }
                        else
                        {
                            _data[0 + i] = _zeroPulse[0];
                            _data[1 + i] = _zeroPulse[1];
                            _data[2 + i] = _zeroPulse[2];
                            _data[3 + i] = _zeroPulse[3];
                        }
                        i = i + 4;
                    }
                }
            }

            //RES
            _data[0 + i] = _resPulse[0];
            _data[1 + i] = _resPulse[1];
            _data[2 + i] = _resPulse[2];
            _data[3 + i] = _resPulse[3];
        }

        private byte[] GetResPulse(float minPulse)
        {
            var result = new byte[4];
            var duration0 = (ushort)(50 / minPulse);
            var duration1 = (ushort)(50 / minPulse);

            var remaining = duration0 % 256;
            result[0] = (byte)remaining;
            result[1] = (byte)((duration0 - remaining) / 256);

            remaining = duration1 % 256;
            result[2] = (byte)remaining;
            result[3] = (byte)((duration1 - remaining) / 256);

            return result;
        }

        public void Update()
        {
            _transmitterChannel.SendData(_data, false);
        }
    }
}
