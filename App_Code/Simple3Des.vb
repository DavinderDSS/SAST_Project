Imports Microsoft.VisualBasic
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
'Imports System.Net
'Imports System.Data
'Imports System.Configuration
'Imports System.Text.ASCIIEncoding
'Imports System.net.Mime

''**************Code for Encrypt and decrypt added on 13-12-2019
Public Class Simple3Des
    Private tdDes As New TripleDESCryptoServiceProvider
    Sub New(ByVal strKey As String)

        tdDes.Key = Truncate(strKey, tdDes.KeySize \ 8)
        tdDes.IV = Truncate("", tdDes.BlockSize \ 8)

    End Sub
    Public Function EncryptDt(ByVal strInput As String) As String

        Dim btInputBytes() As Byte = Text.Encoding.Unicode.GetBytes(strInput)
        Dim msInput As New IO.MemoryStream
        Dim csEncrypt As New CryptoStream(msInput, tdDes.CreateEncryptor(), CryptoStreamMode.Write)

        csEncrypt.Write(btInputBytes, 0, btInputBytes.Length)
        csEncrypt.FlushFinalBlock()

        Return Convert.ToBase64String(msInput.ToArray)

    End Function
    Public Function DecryptDt(ByVal strOutput As String) As String

        Dim btOutputBytes() As Byte = Convert.FromBase64String(strOutput)
        Dim msOutput As New IO.MemoryStream
        Dim csDecrypt As New CryptoStream(msOutput, tdDes.CreateDecryptor(), CryptoStreamMode.Write)

        csDecrypt.Write(btOutputBytes, 0, btOutputBytes.Length)
        csDecrypt.FlushFinalBlock()

        Return Text.Encoding.Unicode.GetString(msOutput.ToArray)

    End Function
    Private Function Truncate(ByVal strKey As String, ByVal intLength As Integer) As Byte()

        Dim shaCrypto As New SHA1CryptoServiceProvider
        Dim btKeyBytes() As Byte = Encoding.Unicode.GetBytes(strKey)
        Dim btHash() As Byte = shaCrypto.ComputeHash(btKeyBytes)

        ReDim Preserve btHash(intLength - 1)
        Return btHash

    End Function
    
    ''**************Code for Encrypt and decrypt added on 13-12-2019 is ended here
    Public Shared Function ConvertStringToHex(ByVal input As String, ByVal encoding As Encoding) As String
        Dim stringBytes() As Byte
        Dim b As Byte

        stringBytes = encoding.GetBytes(input)
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
