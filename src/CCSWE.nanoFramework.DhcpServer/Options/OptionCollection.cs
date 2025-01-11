using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace CCSWE.nanoFramework.DhcpServer.Options
{
    /// <summary>
    /// Represents a collection of DHCP options.
    /// </summary>
    /// <remarks>
    /// The <see cref="OptionCollection"/> behaves like a dictionary and does not support multiple instances of the same <see cref="OptionCode"/>.
    /// </remarks>
    public class OptionCollection: IEnumerable
    {
        private int _length;
        private readonly object _lock = new();
        private readonly Hashtable _options = new();

        /// <summary>
        /// The length of the <see cref="OptionCollection"/> when serialized.
        /// </summary>
        public int Length => _length + 1;

        /// <summary>
        /// Add or replace an <see cref="IOption"/>.
        /// </summary>
        public void Add(IOption option)
        {
            lock (_lock)
            {
                if (TryGet(option.Code, out var existing))
                {
                    _length -= existing.OptionLength;
                }

                _length += option.OptionLength;
                _options[option.Code] = option;
            }
        }

        /// <summary>
        /// Determines whether the <see cref="OptionCollection"/> contains an <see cref="IOption"/>.
        /// </summary>
        /// <param name="code">The <see cref="OptionCode"/> to check.</param>
        /// <returns><see langword="true"/> if the <see cref="OptionCollection"/> contains the option; otherwise <see langword="false"/>.</returns>
        public bool Contains(byte code) => _options.Contains(code);

        /// <summary>
        /// Determines whether the <see cref="OptionCollection"/> contains an <see cref="IOption"/>.
        /// </summary>
        /// <param name="code">The <see cref="OptionCode"/> to check.</param>
        /// <returns><see langword="true"/> if the <see cref="OptionCollection"/> contains the option; otherwise <see langword="false"/>.</returns>
        public bool Contains(OptionCode code) => Contains((byte)code);

        /// <summary>
        /// Gets an <see cref="IOption"/>.
        /// </summary>
        /// <param name="code">The <see cref="OptionCode"/> to get.</param>
        /// <returns>An <see cref="IOption"/> if it exists; otherwise <see langword="null"/>.</returns>
        public IOption? Get(byte code)
        {
            lock (_lock)
            {
                return Contains(code) ? (IOption)_options[code] : null;
            }
        }

        /// <summary>
        /// Gets an <see cref="IOption"/>.
        /// </summary>
        /// <param name="code">The <see cref="OptionCode"/> to get.</param>
        /// <returns>An <see cref="IOption"/> if it exists; otherwise <see langword="null"/>.</returns>
        public IOption? Get(OptionCode code) => Get((byte)code);

        /// <summary>
        /// Converts the <see cref="OptionCollection"/> to a <see cref="T:byte[]"/>.
        /// </summary>
        public byte[] GetBytes()
        {
            lock (_lock)
            {
                var options = new byte[Length];
                var index = 0;

                foreach (var item in this)
                {
                    if (item is not IOption option)
                    {
                        continue;
                    }

                    index += Converter.CopyTo(option.GetBytes(), options, index);
                }

                options[index] = (byte)OptionCode.End;

                return options;
            }
        }

        /// <inheritdoc />
        public IEnumerator GetEnumerator() => _options.Values.GetEnumerator();

        /// <summary>
        /// Gets the value set for an <see cref="IOption"/>.
        /// </summary>
        /// <returns>The value if set; otherwise <paramref name="defaultValue"/>.</returns>
        public IPAddress GetOrDefault(OptionCode code, IPAddress defaultValue)
        {
            if (!TryGet(code, out var option))
            {
                return defaultValue;
            }

            if (option is not IPAddressOption knownOption)
            {
                throw new ArgumentException();
            }

            return knownOption.Deserialize();
        }

        /// <summary>
        /// Gets the value set for an <see cref="IOption"/>.
        /// </summary>
        /// <returns>The value if set; otherwise <paramref name="defaultValue"/>.</returns>
        public string GetOrDefault(OptionCode code, string defaultValue)
        {
            if (!TryGet(code, out var option))
            {
                return defaultValue;
            }

            if (option is not StringOption knownOption)
            {
                throw new ArgumentException();
            }

            return knownOption.Deserialize();
        }

        /// <summary>
        /// Gets the value set for an <see cref="IOption"/>.
        /// </summary>
        /// <returns>The value if set; otherwise <paramref name="defaultValue"/>.</returns>
        public TimeSpan GetOrDefault(OptionCode code, TimeSpan defaultValue)
        {
            if (!TryGet(code, out var option))
            {
                return defaultValue;
            }

            if (option is not TimeSpanOption knownOption)
            {
                throw new ArgumentException();
            }

            return knownOption.Deserialize();
        }

        internal static OptionCollection Parse(byte[] data)
        {
            var index = MessageIndex.Options;

            if (data[index] == (byte)OptionCode.Pad)
            {
                throw new ArgumentException();
            }

            var options = new OptionCollection();

            while (data[index] != (byte)OptionCode.End)
            {
                options.Add(Parse(data, ref index));
            }

            return options;
        }

        private static IOption Parse(byte[] options, ref ushort index)
        {
            var code = options[index++];
            var length = options[index++];

            var data = new byte[length];
            Converter.CopyTo(options, index, data, 0, length);

            index += length;

            if (IPAddressOption.IsKnownOption(code))
            {
                return new IPAddressOption(code, data);
            }

            if (MessageTypeOption.IsKnownOption(code))
            {
                return new MessageTypeOption(data);
            }

            if (ParameterRequestListOption.IsKnownOption(code))
            {
                return new ParameterRequestListOption(data);
            }

            if (StringOption.IsKnownOption(code))
            {
                return new StringOption(code, data);
            }

            if (TimeSpanOption.IsKnownOption(code))
            {
                return new TimeSpanOption(code, data);
            }

            return new UnknownOption(code, data);
        }

        /// <summary>
        /// Remove an option from the collection.
        /// </summary>
        public void Remove(OptionCode code)
        {
            lock (_lock)
            {
                if (_options.Contains(code))
                {
                    _options.Remove(code);
                }
            }
        }

        /// <summary>
        /// Tries to retrieve an <see cref="IOption"/> from the collection.
        /// </summary>
        /// <param name="code">The <see cref="OptionCode"/> to retrieve.</param>
        /// <param name="option">The <see cref="IOption"/>.</param>
        /// <returns><see langword="true"/> if the option exists; otherwise <see langword="false"/>.</returns>
        public bool TryGet(byte code, [NotNullWhen(true)] out IOption? option)
        {
            option = Get(code);

            return option is not null;
        }

        /// <summary>
        /// Tries to retrieve an <see cref="IOption"/> from the collection.
        /// </summary>
        /// <param name="code">The <see cref="OptionCode"/> to retrieve.</param>
        /// <param name="option">The <see cref="IOption"/>.</param>
        /// <returns><see langword="true"/> if the option exists; otherwise <see langword="false"/>.</returns>
        public bool TryGet(OptionCode code, [NotNullWhen(true)] out IOption? option) => TryGet((byte)code, out option);
    }
}
