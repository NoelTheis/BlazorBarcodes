using System;
using System.Collections.Generic;
using System.Text;

namespace BarcoderWrapper.Barcodes
{
    public enum BarcodeTypes
    {
        [Barcode("Aztec Code", Aztec)]
        Aztec,
        [Barcode("Code-128", Code128)]
        Code128,
        [Barcode("Code-39", Code39)]
        Code39,
        [Barcode("Code-93", Code93)]
        Code93,
        [Barcode("Data Matrix", DataMatrix)]
        DataMatrix,
        [Barcode("EAN", Ean)]
        Ean,
        [Barcode("RM4SCC / KIX", Kix)]
        Kix,
        [Barcode("PDF417", Pdf417)]
        Pdf417,
        [Barcode("QR Code", Qr)]
        Qr,
        [Barcode("Royal Mail", RoyalMail)]
        RoyalMail,
        [Barcode("Interleaved 2 of 5", TwoToFive)]
        TwoToFive,
        [Barcode("UPC-A", UpcA)]
        UpcA,
        [Barcode("UPC-E", UpcE)]
        UpcE
    }
}
