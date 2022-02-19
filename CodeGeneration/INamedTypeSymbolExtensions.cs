using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration
{
    public static class INamedTypeSymbolExtensions
    {
        public static IEnumerable<IMethodSymbol> GetMethods(this INamedTypeSymbol namedTypeSymbol)
        {
            List<IMethodSymbol> methods = new();
            foreach (var member in namedTypeSymbol.GetMembers())
                if (member is IMethodSymbol method)
                    methods.Add(method);

            return methods;
        }
    }
}
