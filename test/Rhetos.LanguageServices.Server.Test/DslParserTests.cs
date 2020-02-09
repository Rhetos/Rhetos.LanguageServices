﻿using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhetos.Dsl;
using Rhetos.LanguageServices.Server.Parsing;
using Rhetos.LanguageServices.Server.Services;
using Rhetos.LanguageServices.Server.Tools;
using Rhetos.Logging;
using Rhetos.Utilities;
using Rhetos.Utilities.ApplicationConfiguration;

namespace Rhetos.LanguageServices.Server.Test
{
    [TestClass]
    public class DslParserTests
    {
        private readonly IServiceProvider serviceProvider;
        private readonly RhetosAppContext rhetosAppContext;
        public DslParserTests()
        {
            Assembly.Load("Rhetos.Dsl.DefaultConcepts");
            serviceProvider = TestCommon.CreateTestServiceProvider();

            rhetosAppContext =  serviceProvider.GetService<RhetosAppContext>();
            rhetosAppContext.InitializeFromCurrentDomain();
        }

        [TestMethod]
        public void InitializeFromCurrentDomain()
        {
            Console.WriteLine($"Keywords: {rhetosAppContext.Keywords.Count}, ConceptTypes: {rhetosAppContext.ConceptInfoTypes.Length}.");
        }


        // TODO: missing asserts
        [TestMethod]
        public void Test1()
        {
            var script = @"
Module TestModule
{
    Entity Pero
    {
        Logging;
        Reference blo;
    }
    Entity Empty
    { 
 
    }
    Entity After;
}

";
            var rheDocument = serviceProvider.GetService<RhetosDocumentFactory>().CreateNew();
            rheDocument.UpdateText(script);

            var dslParser = new DslParser(rheDocument.Tokenizer, rhetosAppContext.ConceptInfoInstances, serviceProvider.GetService<ILogProvider>());
            var parsedConcepts = dslParser.ParseConceptsWithCallbacks(
                (tokenReader, keyword) =>
                {
                    var reader = tokenReader as TokenReader;
                    var token = rheDocument.Tokenizer.GetTokens()[reader.PositionInTokenList];
                    Console.WriteLine($"OnKeyword, token={token.Value}, keyword={keyword}.");
                },
                OnMemberRead,
                null);

            Console.WriteLine(parsedConcepts.Count());
            foreach (var parsedConcept in parsedConcepts)
            {
                Console.WriteLine($"{parsedConcept.GetType().Name}: {parsedConcept.GetKey()}");
            }
        }

        private void OnMemberRead(ITokenReader tokenReader, IConceptInfo conceptInfo, ConceptMember member, ValueOrError<object> value)
        {
            Console.WriteLine($"OnMemberRead: conceptInfo={conceptInfo.GetType().Name}, member={member.Name}, value={value.ToString()}.");
        }

        [TestMethod]
        public void DslModel()
        {
            var rhetosAppEnvironment = RhetosAppEnvironmentProvider.Load(@"C:\Projects\RhetosSasa\Source\Rhetos");
            var configuration = new ConfigurationBuilder()
                .AddRhetosAppEnvironment(rhetosAppEnvironment)
                .AddKeyValue("ConnectionStrings:ServerConnectionString:ConnectionString", "stub")
                .Build();

            var containerBuilder = new RhetosContainerBuilder(configuration, serviceProvider.GetService<ILogProvider>(), LegacyUtilities.GetListAssembliesDelegate(configuration));
            containerBuilder.AddRhetosRuntime();
            var container = containerBuilder.Build();

            var dslModel = container.Resolve<IDslModel>();
            Console.WriteLine(dslModel.Concepts.Count());
            foreach (var conceptInfo in dslModel.Concepts.Where(a => a.GetKey().Contains("GlavniModul")))
            {
                Console.WriteLine(conceptInfo.GetKey());
            }
        }
    }
}
