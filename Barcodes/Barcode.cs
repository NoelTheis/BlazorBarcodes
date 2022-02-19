using BlazorBarcodes.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlazorBarcodes.Barcodes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class Barcode : Attribute
    {
        public string Name { get; }
        public BarcodeTypes Type { get; }
        public IEncoder Encoder => EncoderFactory.CreateEncoder(Type);

        public Barcode(string name, BarcodeTypes type)
        {
            Name = name;
            Type = type;
        }

        public static IEnumerable<Barcode> All { get; }
            = typeof(BarcodeTypes)
            .GetMembers()
            .Select(x => x.GetCustomAttribute<Barcode>())
            .Where(x => x is not null)
            .Cast<Barcode>();
    }
}
