using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration
{
    public static class GenerationExecutionContextExtensions
    {
        public static IAssemblySymbol GetAssemblySymbol(this GeneratorExecutionContext context, string name)
        {
            return context.Compilation.SourceModule.ReferencedAssemblySymbols.First(x => x.Name == name);
        }
        public static string GenerateSourceFileName(this GeneratorExecutionContext context, string className)
        {
            return $"{className}.g.cs";
        }
    }
}
