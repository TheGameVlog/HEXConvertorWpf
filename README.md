# HEXConvertorWpf

Simple to Use Utility to Convert Colors from RGB to HEX , HEX to RGB and Hex to CMYK

# Code
```vb
Dim rgb() As Int32 = hex.HexToRgb(HtxtHex.Text)
HtxtR.Text = rgb(0)
HtxtG.Text = rgb(1)
HtxtB.Text = rgb(2)

Dim hex As New HexConvertors
Dim rgb() As Int32 = hex.HexToRgb(txtHexToCMYK.Text)
Dim cmyk() As Double = hex.hexToCMYK(txtHexToCMYK.Text)
txtC.Text = Math.Round(cmyk(0), 3)
txtM.Text = Math.Round(cmyk(1), 3)
txtY.Text = Math.Round(cmyk(2), 3)
txtK.Text = Math.Round(cmyk(3), 3)

```

# Screenshot

![Screenshot](https://github.com/TheGameVlog/HEXConvertorWpf/blob/master/Screenshot.png?raw=true "Screenshot")
