using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration
{
    public static class IAssemblySymbolExtensions
    {
        public static IEnumerable<INamedTypeSymbol> GetAllTypes(this IAssemblySymbol assemblySymbol)
        {
            return assemblySymbol.GlobalNamespace.GetAllTypes();
        }

        public static INamespaceSymbol GetNamespaceSymbol(this IAssemblySymbol assemblySymbol, string name)
        {
            return assemblySymbol.GlobalNamespace.GetNamespaceMembers().First(x => x.Name == name);
        }
    }
}
