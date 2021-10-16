using System;
using System.Linq;
using System.Security.Cryptography;

namespace MarkovEncode
{
    public class MarkovCrypter
    {
        private static readonly string[][] replaces = new string[4][]
        {
            new string[] { "a0", "0a" },
            new string[] { "a1", "1b" },
            new string[] { "b0", "1a" },
            new string[] { "b1", "0b" }
        };

        public string Encrypt(byte value)
        {
            string bin = 'a' + Convert.ToString(value, 2);
            int index = 0;
            int length = replaces.Length;

            while (bin.Last() != 'a' && bin.Last() != 'b')
            {
                bin = bin.Replace(replaces[index % length][0], replaces[index % length][1]);
                index += 1;
            }

            value = Convert.ToByte(bin[0..^1], 2);
            return $"{value}{bin.Last()}";
        }

        public byte Decrypt(string value)
        {
            if (value.Last() != 'a' && value.Last() != 'b')
                throw new ArgumentException(message: "Encrypted value must end with a or b", paramName: nameof(value));

            int index = 0;
            int length = replaces.Length;
            byte bin = Convert.ToByte(value[0..^1]);
            value = Convert.ToString(bin, 2) + value.Last();

            while (value.First() != 'a' && value.First() != 'b')
            {
                value = value.Replace(replaces[index % length][1], replaces[index % length][0]);
                index += 1;
            }

            if (value.First() != 'a')
                throw new CryptographicException();

            return Convert.ToByte(value[1..], 2);
        }
    }
}
