using Barcoder;
using Barcoder.Renderers;
using BlazorBarcodes.Barcodes;
using BlazorBarcodes.Encoders;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorBarcodes
{
    public partial class BarcodeComponent
    {
        [Parameter]
        public string Style { get; set; } = "";

        [Parameter]
        public string? Content { get; set; }

        /// <summary>
        /// Type of barcode to generate
        /// </summary>
        [Parameter]
        public BarcodeTypes BarcodeType { get; set; }

        /// <summary>
        /// Encoder to used for barcode generation, overrides <seealso cref="BarcodeComponent"/>
        /// </summary>
        [Parameter]
        public IEncoder? Encoder
        {
            get => encoder is null ? BarcodeType.GetEncoder() : encoder;
            set
            {
                encoder = value;
            }
        }
        private IEncoder? encoder;

        /// <summary>
        /// Encoding function to used for barcode generation, overrides <seealso cref="Encoder"/>
        /// </summary>
        [Parameter]
        public Func<string, IBarcode>? EncodingFunction
        {
            get
            {
                if(encodingFunction is not null)
                    return encodingFunction;
                if (Encoder is not null)
                    return Encoder.Encode;
                return null;
            }

            set
            {
                encodingFunction = value;
            }
        }
        private Func<string, IBarcode>? encodingFunction;

        [Parameter]
        public bool IncludeEANContentAsText { get; set; }

        [Parameter]
        public RenderFragment<string>? ErrorTemplate { get; set; }

        private string Markup { get; set; } = "";

        private string ErrorMessage { get; set; } = "";

        private IRenderer Renderer => new Barcoder.Renderer.Svg.SvgRenderer(IncludeEANContentAsText);

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                ErrorMessage = "";
                if (Content is not null && EncodingFunction is not null) 
                {
                    MarkupGenerator generator = new(
                        EncodingFunction,
                        Renderer);

                    Markup = await generator.GenerateAsync(Content);
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }

            StateHasChanged();
        }

        [Obsolete($"{nameof(ShowExceptionMessages)} is obsolete, use {nameof(ErrorTemplate)} instead.")]
        [Parameter]
        public bool ShowExceptionMessages { get; set; } = true;
    }
}
