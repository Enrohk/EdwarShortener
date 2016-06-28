using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EdwardShortener.Tests
{
    [TestClass]
    public class Crc32Test
    {
        [TestMethod]
        public void TestCrc32()
        {
            System.Data.HashFunction.CRC crc = new System.Data.HashFunction.CRC();
            string url = "http://www.google.com";
            byte[] urlToByte = System.Text.Encoding.Default.GetBytes(url);
            byte[] urlHash = crc.ComputeHash(urlToByte);
            byte[] urlHashDecode = crc.ComputeHash(urlHash);
            string urlDecode = System.Text.Encoding.Default.GetString(urlHashDecode);
            Assert.AreEqual(url, urlDecode);
           
        }
    }
}
