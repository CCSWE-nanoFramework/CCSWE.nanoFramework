using System;
using System.Device.Gpio;
using CCSWE.nanoFramework.NeoPixel.Drivers;

// ReSharper disable RedundantArgumentDefaultValue
namespace CCSWE.nanoFramework.NeoPixel.Benchmarks
{
    internal static class StripParameters
    {
        private static NeoPixelDriver? _driver;

        public static ushort Count { get; private set; }

        public static NeoPixelDriver Driver => _driver ?? throw new InvalidOperationException($"{nameof(StripParameters)} have not been initialized.");

        public static byte Pin { get; private set; }

        public static void Initialize(DeviceType deviceType)
        {
            switch (deviceType)
            {
                case DeviceType.T4:
                {
                    Initialize(19, 3, new Ws2812B(ColorOrder.GRB));
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(deviceType));
                }
            }
        }

        public static void Initialize(byte ledPin, ushort count, NeoPixelDriver driver)
        {
            Initialize(ledPin, count, driver, 0, PinValue.High);
        }

        public static void Initialize(byte ledPin, ushort count, NeoPixelDriver driver, byte powerPin, PinValue powerPinValue)
        {
            _driver = driver;

            Count = count;
            Pin = ledPin;

            if (powerPin > 0)
            {
                new GpioController().OpenPin(powerPin, PinMode.Output).Write(powerPinValue);
            }
        }
    }
}
