using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneration
{
    public static class ISymbolExtensions
    {
        public static string ToFullName(this ISymbol symbol)
        {
            if (symbol.ContainingSymbol.IsRootNamespace())
                return symbol.Name;

            return $"{symbol.ContainingSymbol.ToFullName()}.{symbol.Name}";
        }

        private static bool IsRootNamespace(this ISymbol symbol)
        {
            return symbol is INamespaceSymbol namespaceSymbol && namespaceSymbol.IsGlobalNamespace;
        }
    }
}
