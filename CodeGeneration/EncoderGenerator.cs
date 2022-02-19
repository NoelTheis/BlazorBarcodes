using Barcoder;
using BlazorBarcodes.Encoders;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CodeGeneration
{
    [Generator]
    public class EncoderGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var encoderTypes = context
                .GetAssemblySymbol(nameof(Barcoder))
                .GetNamespaceSymbol(nameof(Barcoder))
                .GetAllTypes()
                .Where(x => x.IsStatic && x.DeclaredAccessibility == Accessibility.Public)
                .Where(x => x.GetMethods().Any(m => m.Name == "Encode" && m.DeclaredAccessibility == Accessibility.Public));

            foreach (var typeSymbol in encoderTypes.Where(x => x.Name.Contains("Encoder")))
                AddEncoderClassSource(context, typeSymbol);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif
        }

        private void AddEncoderClassSource(
            GeneratorExecutionContext context,
            INamedTypeSymbol typeSymbol)
        {
            var className = GenerateEncoderClassName(typeSymbol);
            var sourceName = context.GenerateSourceFileName(className);
            var source = GenerateEncoderClass(typeSymbol);
            context.AddSource(sourceName, source);
        }

        private string GenerateEncoderClassName(INamedTypeSymbol typeSymbol)
        {
            return typeSymbol.Name;
        }

        private string GenerateEncoderClass (INamedTypeSymbol typeSymbol)
        {
            StringBuilder sb = new();
            sb.Append($@"
using {typeSymbol.ContainingNamespace.ToDisplayString()};
using {nameof(Barcoder)};
namespace {nameof(BlazorBarcodes)}.{nameof(BlazorBarcodes.Encoders)}
{{
    public class {GenerateEncoderClassName(typeSymbol)} : {nameof(BaseEncoder)}
    {{");
            var encodeMethodSymbol = GetEncodeMethod(typeSymbol);
            foreach (var parameterSymbol in encodeMethodSymbol.Parameters.Skip(1))
                sb.Append($@"
        {GenerateProperty(parameterSymbol)}");

            sb.Append($@"
        {GenerateEncodeMethod(encodeMethodSymbol)}");

            sb.Append($@"
    }}
}}
");
            return sb.ToString();
        }

        private IMethodSymbol GetEncodeMethod(INamedTypeSymbol typeSymbol)
        {
            return typeSymbol.GetMethods().First(x => x.Name == "Encode");
        }

        private string GenerateProperty(IParameterSymbol parameter)
        {
            StringBuilder sb = new();
            sb.Append($@"public {parameter.Type.ToDisplayString()} {parameter.Name.FirstLetterToUpper()} {{ get; set; }}");
            if (parameter.HasExplicitDefaultValue)
            {
                if (parameter.ExplicitDefaultValue is null)
                    sb.Append($@" = null;");
                else
                {
                    if(parameter.ExplicitDefaultValue is bool)
                        sb.Append($@" = {parameter.ExplicitDefaultValue.ToString().FirstLetterToLower()};");                  
                    else
                        sb.Append($@" = {parameter.ExplicitDefaultValue};");
                }
                    
            }

            return sb.ToString();
        }

        private string GenerateEncodeMethod(IMethodSymbol encodeMethodSymbol)
        {
            StringBuilder sb = new();

            var barcodeType = encodeMethodSymbol
                .ContainingType
                .ContainingNamespace
                .GetAllTypes()
                .FirstOrDefault(x => x.AllInterfaces.Any(x => x.Name == nameof(IBarcode)));

            var returnTypeName = barcodeType is null ? nameof(IBarcode) : barcodeType.Name;
            var cast = barcodeType is null ? "" : $"({barcodeType.Name})";

            sb.Append($@"public override {returnTypeName} Encode(string content)
        {{
            return {cast}{encodeMethodSymbol.ToFullName()}(
                content");

            foreach (var parameter in encodeMethodSymbol.Parameters.Skip(1))
                sb.Append($@",
                {parameter.Name.FirstLetterToUpper()}");

            sb.Append($@");
        }}");
            return sb.ToString();
        }
    }
}
