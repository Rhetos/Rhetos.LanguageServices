﻿/*
    Copyright (C) 2014 Omega software d.o.o.

    This file is part of Rhetos.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Rhetos.Dsl;
using Rhetos.LanguageServices.CodeAnalysis.Parsing;

namespace Rhetos.LanguageServices.CodeAnalysis.Services
{
    public class ConceptQueries
    {
        private readonly RhetosProjectContext rhetosProjectContext;
        private readonly XmlDocumentationProvider xmlDocumentationProvider;

        public ConceptQueries(RhetosProjectContext rhetosProjectContext, XmlDocumentationProvider xmlDocumentationProvider)
        {
            this.rhetosProjectContext = rhetosProjectContext;
            this.xmlDocumentationProvider = xmlDocumentationProvider;
        }

        public string GetFullDescription(string keyword)
        {
            var signatures = GetSignaturesWithDocumentation(keyword);
            if (signatures == null) return null;

            var fullDescription = string.Join("\n\n", signatures.Select(sig => $"{sig.Signature}\n{sig.Documentation}"));
            return fullDescription;
        }

        public List<RhetosSignature> GetSignaturesWithDocumentation(string keyword)
        {
            if (string.IsNullOrEmpty(keyword) || !rhetosProjectContext.Keywords.TryGetValue(keyword, out var keywordTypes))
                return null;

            var signatures = keywordTypes.Select(CreateRhetosSignature);
            return signatures.ToList();
        }

        private RhetosSignature CreateRhetosSignature(ConceptType conceptType)
        {
            var prefix = "    ";

            var signature = ConceptTypeTools.SignatureDescription(conceptType);
            var documentation = $"{prefix}* defined by {conceptType.AssemblyQualifiedName}";
            var xmlDocumentation = xmlDocumentationProvider.GetDocumentation(conceptType, prefix);
            if (!string.IsNullOrEmpty(xmlDocumentation)) documentation = $"{xmlDocumentation}\n{documentation}";

            return new RhetosSignature()
            {
                ConceptType = conceptType,
                Parameters = ConceptTypeTools.GetParameters(conceptType),
                Signature = signature,
                Documentation = documentation
            };
        }

        public List<ConceptType> ValidConceptsForParent(ConceptType parentConceptType)
        {
            var result = new List<ConceptType>();
            foreach (var conceptType in rhetosProjectContext.DslSyntax.ConceptTypes)
            {
                if (conceptType == parentConceptType) continue;

                var members = conceptType.Members;
                var parentNestedMember = members.FirstOrDefault(member => member.IsParentNested);
                var firstMember = members.FirstOrDefault();

                // is first member valid?
                if (firstMember == null || !firstMember.IsKey || !firstMember.IsConceptInfo)
                    firstMember = null;

                var parentMember = parentNestedMember ?? firstMember;
                if (parentMember == null) continue;

                if (parentMember.IsConceptInfoInterface
                    || (parentMember.ConceptType?.IsAssignableFrom(parentConceptType) ?? false))
                {
                    result.Add(conceptType);
                }
            }

            return result;
        }
    }
}