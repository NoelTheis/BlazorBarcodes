using Barcoder;
using Barcoder.Renderers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlazorBarcodes
{
    public class MarkupGenerator
    {

        private Func<string, IBarcode> EncodingFunction { get; }
        private IRenderer Renderer { get; }

        public MarkupGenerator(
            Func<string, IBarcode> encodingFunction, 
            IRenderer renderer)
        {
            EncodingFunction = encodingFunction;
            Renderer = renderer;
        }

        public async Task<string> GenerateAsync(
            string content)
        {
            var barcode = EncodingFunction(content);

            using var stream = new MemoryStream();
            using var reader = new StreamReader(stream);

            Renderer.Render(barcode, stream);
            stream.Position = 0;

            return await reader.ReadToEndAsync();
        }

    }
}
