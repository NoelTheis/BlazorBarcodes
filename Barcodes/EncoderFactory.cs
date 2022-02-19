using BarcoderWrapper.Barcodes;
using System;

namespace BarcoderWrapper.Encoders
{
    public static class EncoderFactory
    {
        public static IEncoder CreateEncoder(BarcodeTypes type)
        {
            return type switch
            {
                BarcodeTypes.Aztec => new AztecEncoder(),
                BarcodeTypes.Code128 => new Code128Encoder(),
                BarcodeTypes.Code39 => new Code39Encoder(),
                BarcodeTypes.Code93 => new Code93Encoder(),
                BarcodeTypes.DataMatrix => new DataMatrixEncoder(),
                BarcodeTypes.Ean => new EanEncoder(),
                BarcodeTypes.Kix => new KixEncoder(),
                BarcodeTypes.Pdf417 => new Pdf417Encoder(),
                BarcodeTypes.Qr => new QrEncoder(),
                BarcodeTypes.RoyalMail => new RoyalMailFourStateCodeEncoder(),
                BarcodeTypes.TwoToFive => new TwoToFiveEncoder(),
                BarcodeTypes.UpcA => new UpcAEncoder(),
                BarcodeTypes.UpcE => new UpcEEncoder(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
