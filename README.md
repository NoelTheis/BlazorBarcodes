# BlazorBarcodes
 
Provides blazor component for rendering 1D & 2D codes. 

This is a really simple wrapper around huysentruitw´s [Barcoder](https://github.com/huysentruitw/barcoder) and as such supports the same barcode types, which at this time are:

* 2 of 5
* Aztec Code
* Codabar
* Code 39
* Code 93
* Code 128
* Code 128 GS1
* Data Matrix (ECC 200)
* Data Matrix GS1
* EAN 8
* EAN 13
* KIX (used by PostNL)  
* PDF 417
* QR Code
* RM4SC (Royal Mail 4 State Code)
* UPC A
* UPC E

## Targets

    .Net 5

## Install

    PM> Install-Package BlazorBarcodes

## Demo

[WASM](https://noeltheis.github.io/BlazorBarcodes/)

## Usage

Add the following using statements.
```csharp
@using BlazorBarcodes
@using BlazorBarcodes.Barcodes
@using BlazorBarcodes.Encoders
```

Pass a string content and either a BarcodeType(enum), Encoder or encoding function to the component.
```csharp
<BarcodeComponent Style="width:200px;height:200px"
                    BarcodeType="@BarcodeTypes.DataMatrix"
                    Content="Hello World!">
</BarcodeComponent>
```
The codes are rendered as SVG by default.

If you´d like to quickly play around with the different barcodes, you create an easy selection like this.

```csharp
<div style="display:flex; flex-direction:column">

    <input type="text" @bind="Content">

    <select @bind="Type">
        @foreach (var barcode in BarcoderWrapper.Barcodes.Barcode.All)
        {
            <option value="@barcode.Type" disabled="@(!barcode.Encoder.CanEncode(Content, out string error))">
                @(string.Format("{0}{1}", barcode.Name, error is not null ? $": {error}" : ""))
            </option>
        }
    </select>

    <BarcodeComponent Style="width:200px;height:200px"
                      BarcodeType="@Type"
                      Content="@Content">
    </BarcodeComponent>
</div>

@code {
    public BarcodeTypes Type { get; set; }

    public string Content { get; set; } = "";
}
```

### Options

Passed to the Barcoder renderer
```csharp
bool IncludeEANContentAsText
```

If you wish to display some sort of error message, in case of a rendering issue, pass an error template to the BarcodeComponent
```csharp
<BarcodeComponent Style="width:200px;height:200px"
                    BarcodeType="@Type"
                    Content="@Content"
                    IncludeEANContentAsText="true">
    <ErrorTemplate>
        @context
    </ErrorTemplate>
</BarcodeComponent>
```

## Encoders
The encoding functions in [Barcoder](https://github.com/huysentruitw/barcoder) have been wrapped in classes implementing my own IEncoder interface, since I wanted to be able to pass encoders around and also saw it as an opportunity to play around with C# source code generators.

You can still use pass the pre-existing encode methods to the component though.

