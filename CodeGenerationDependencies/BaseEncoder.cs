using Barcoder;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarcoderWrapper.Encoders
{
    public abstract class BaseEncoder : IEncoder
    {
        public bool CanEncode(
            string content,
            out string? errorMessage)
        {
            try
            {
                Encode(content);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return false;
            }
            errorMessage = null;
            return true;
        }

        public abstract IBarcode Encode(string content);
    }
}
