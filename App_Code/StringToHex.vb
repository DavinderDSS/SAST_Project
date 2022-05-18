Imports Microsoft.VisualBasic
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
'Imports System.Net
'Imports System.Data
'Imports System.Configuration
'Imports System.Text.ASCIIEncoding
'Imports System.net.Mime

Public Class StringToHex
    ''**************Code for Encrypt and decrypt added on 13-12-2019 is ended here
    Public Shared Function ConvertStringToHex(ByVal input As String, ByVal encoding As Encoding) As String
        Dim stringBytes() As Byte
        Dim b As Byte

        stringBytes = Encoding.GetBytes(input)
        Dim sbBytes = New StringBuilder(stringBytes.Length * 2)

        For Each b In stringBytes
            sbBytes.AppendFormat("{0:X2}", b)
        Next

        Return sbBytes.ToString
    End Function

    Public Shared Function ConvertHexToString(ByVal hexInput As String, ByVal encoding As Encoding) As String
        Dim numberChars = hexInput.Length
        Dim bytes() As Byte
        bytes = New Byte(numberChars / 2 - 1) {}
        Dim i As Integer
        For i = 0 To numberChars - 1 Step 2
            bytes(i / 2) = Convert.ToByte(hexInput.Substring(i, 2), 16)
            ''Dim bytes() As Byte
            ''bytes = System.Text.Encoding.UTF8.GetBytes(hexInput.Substring(i, 2))
        Next

        Return encoding.GetString(bytes)
    End Function
End Class
