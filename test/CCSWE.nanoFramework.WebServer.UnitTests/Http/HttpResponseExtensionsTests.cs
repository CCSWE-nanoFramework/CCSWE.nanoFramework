using System;
using System.IO;
using System.Net;
using CCSWE.nanoFramework.Net;
using CCSWE.nanoFramework.WebServer.Http;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http;
using nanoFramework.TestFramework;

// ReSharper disable StringLiteralTypo
namespace CCSWE.nanoFramework.WebServer.UnitTests.Http
{
    [TestClass]
    public class HttpResponseExtensionsTests
    {
        private static byte[] GetByteArray()
        {
            var random = new Random();
            const int count = HttpResponseExtensions.BufferSize * 2;
            var byteArray = new byte[count];

            random.NextBytes(byteArray);
            return byteArray;
        }

        private static Stream GetStream()
        {
            return new MemoryStream(GetByteArray());
        }

        [TestMethod]
        public void StatusCode_sets_StatusCode()
        {
            var response = new HttpResponseMock();

            response.StatusCode((HttpStatusCode)420);

            Assert.AreEqual(420, response.StatusCode);
        }

        [TestMethod]
        public void StatusCode_sets_StatusDescription()
        {
            const string statusDescription = "Giggity";
            var response = new HttpResponseMock();

            response.StatusCode((HttpStatusCode)69, statusDescription);

            Assert.AreEqual(statusDescription, response.StatusDescription);
        }

        [TestMethod]
        public void Write_correctly_outputs_byte_array()
        {
            var output = GetByteArray();
            var response = new HttpResponseMock();

            response.Write(output);

            Assert.AreEqual(output.Length, response.Body.Length);
            Assert.AreEqual(output.Length, response.ContentLength);

            response.Body.Seek(0, SeekOrigin.Begin);

            for (var i = 0; i < output.Length; i++)
            {
                Assert.AreEqual(output[i], response.Body.ReadByte());
            }
        }

        [TestMethod]
        public void Write_correctly_outputs_stream()
        {
            using var output = GetStream();
            var response = new HttpResponseMock();

            response.Write(output);

            Assert.AreEqual(output.Length, response.Body.Length);
            Assert.AreEqual(output.Length, response.ContentLength);

            response.Body.Seek(0, SeekOrigin.Begin);
            output.Seek(0, SeekOrigin.Begin);

            for (var i = 0; i < output.Length; i++)
            {
                Assert.AreEqual(output.ReadByte(), response.Body.ReadByte());
            }
        }

        [TestMethod]
        public void Write_sets_content_type_when_sending_byte_array()
        {
            const string expected = MimeType.Text.Html;
            var response = new HttpResponseMock();

            response.Write(GetByteArray(), expected);

            Assert.AreEqual(expected, response.ContentType);
        }

        [TestMethod]
        public void Write_sets_content_type_when_sending_stream()
        {
            const string expected = MimeType.Text.Html;
            var response = new HttpResponseMock();

            response.Write(GetStream(), expected);

            Assert.AreEqual(expected, response.ContentType);
        }

        [TestMethod]
        public void Write_sets_default_content_type_when_sending_byte_array()
        {
            const string expected = MimeType.Application.Octet;
            var response = new HttpResponseMock();

            response.Write(GetByteArray());

            Assert.AreEqual(expected, response.ContentType);
        }

        [TestMethod]
        public void Write_sets_default_content_type_when_sending_stream()
        {
            const string expected = MimeType.Application.Octet;
            var response = new HttpResponseMock();

            response.Write(GetStream());

            Assert.AreEqual(expected, response.ContentType);
        }
    }
}
