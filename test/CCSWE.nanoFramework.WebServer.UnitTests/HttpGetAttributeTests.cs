﻿using System;
using CCSWE.nanoFramework.WebServer.Http;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.WebServer.UnitTests
{
    [TestClass]
    public class HttpGetAttributeTests
    {
        [TestMethod]
        public void Constructor_Sets_Correct_HttpMethods()
        {
            var sut = new HttpGetAttribute();

            Assert.AreEqual(1, sut.HttpMethods.Length);
            Assert.IsTrue(HttpMethods.IsGet(sut.HttpMethods[0]));
        }

        [TestMethod]
        public void Constructor_Sets_Correct_HttpMethods_When_Template_Is_Supplied()
        {
            var sut = new HttpGetAttribute("RouteTemplate");

            Assert.AreEqual(1, sut.HttpMethods.Length);
            Assert.IsTrue(HttpMethods.IsGet(sut.HttpMethods[0]));
        }

        [TestMethod]
        public void Constructor_Throws_Exception_When_Template_Is_Null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () =>
            {
                var attribute = new HttpGetAttribute(null!);
            });
        }
    }

}
