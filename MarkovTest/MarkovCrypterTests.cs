using MarkovEncode;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Security.Cryptography;
using System.Threading;

namespace MarkovTest
{
    public class MarkovCrypterTests
    {
        private MarkovCrypter crypter;

        [SetUp]
        public void Setup()
        {
            crypter = new();
        }

        [Test]
        public void EncryptTest()
        {
            Assert.AreEqual(crypter.Encrypt(115), "74b");
        }

        [Test]
        public void DecryptTest()
        {
            Assert.AreEqual(crypter.Decrypt("74b"), 115);
        }

        [Test]
        public void DecryptNotProperValue()
        {
            Assert.Catch<CryptographicException>(() =>
            {
                crypter.Decrypt("74a");
            });
        }

        [Test]
        public void DecryptBigValue()
        {
            Assert.Catch<Exception>(() =>
            {
                crypter.Decrypt("256a");
            });
        }

        [Test]
        [Timeout(1000)]
        public void NotInfiniteLoop()
        {
            Assert.Catch<ArgumentException>(() =>
            {
                crypter.Decrypt("74");
            });
        }
    }
}
