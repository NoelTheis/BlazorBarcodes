﻿using BlazorBarcodes.Encoders;
using System.Linq;
using System.Reflection;

namespace BlazorBarcodes.Barcodes
{
    public static class BarcodeTypesExtensions
    {
        public static IEncoder? GetEncoder(
            this BarcodeTypes value)
        {
            return value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<Barcode>(false)?
                .Encoder;
        }
    }
}
