﻿using System;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhetos.LanguageServices.Server.Services;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Rhetos.LanguageServices.Server.Test
{
    /// <summary>
    /// Test class
    /// Second line documentation
    /// </summary>
    [TestClass]
    [DeploymentItem("Rhetos.LanguageServices.Server.Test.xml")]
    public class XmlDocumentationProviderTests
    {
        private readonly ILoggerFactory logFactory;

        public XmlDocumentationProviderTests()
        {
            logFactory = LoggerFactory.Create(b => b.AddConsole().SetMinimumLevel(LogLevel.Trace));
        }

        [TestMethod]
        public void XmlDocForType()
        {
            var provider = new XmlDocumentationProvider(logFactory.CreateLogger<XmlDocumentationProvider>());
            var xmlDoc = provider.GetDocumentation(typeof(XmlDocumentationProviderTests));

            Console.WriteLine(xmlDoc);
            Assert.AreEqual("Test class\nSecond line documentation", xmlDoc);
        }
    }
}
