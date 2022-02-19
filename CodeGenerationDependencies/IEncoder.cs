using Barcoder;

namespace BarcoderWrapper.Encoders
{
    public interface IEncoder
    {
        public IBarcode Encode(string content);

        public bool CanEncode(
            string content,
            out string? errorMessage);
    }
}
