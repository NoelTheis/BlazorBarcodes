using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneration
{
    public static class INamespaceSymbolExtensions
    {
        public static IEnumerable<INamedTypeSymbol> GetAllTypes(this INamespaceSymbol namespaceSymbol)
        {
            List<INamedTypeSymbol> types = new(namespaceSymbol.GetTypeMembers());

            foreach (var nameSpace in namespaceSymbol.GetNamespaceMembers())
            {
                var nameSpaceTypes = nameSpace.GetAllTypes();
                types.AddRange(nameSpaceTypes);
            }

            return types;
        }
    }
}
