Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO
Imports System.IO.Stream
Public Module AESEncrytDecry
    Public Function DecryptStringAES(ByVal cipherText As String) As String
        ''Dim keybytes = Encoding.UTF8.GetBytes("8080808080808080")
        ''Dim iv = Encoding.UTF8.GetBytes("8080808080808080")

        Dim keybytes = Encoding.UTF8.GetBytes("1020604010208030")
        Dim iv = Encoding.UTF8.GetBytes("1020604010208030")

        Dim encrypted = Convert.FromBase64String(cipherText)
        Dim decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv)
        Return String.Format(decriptedFromJavascript)
    End Function
    Private Function DecryptStringFromBytes(ByVal cipherText As Byte(), ByVal key As Byte(), ByVal iv As Byte()) As String
        If cipherText Is Nothing OrElse cipherText.Length <= 0 Then
            Throw New ArgumentNullException("cipherText")
        End If

        If key Is Nothing OrElse key.Length <= 0 Then
            Throw New ArgumentNullException("key")
        End If

        If iv Is Nothing OrElse iv.Length <= 0 Then
            Throw New ArgumentNullException("key")
        End If

        Dim plaintext As String = Nothing

        Using rijAlg = New RijndaelManaged
            rijAlg.Mode = CipherMode.CBC
            rijAlg.Padding = PaddingMode.PKCS7
            rijAlg.FeedbackSize = 128
            rijAlg.Key = key
            rijAlg.IV = iv
            Dim decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV)

            Try

                'Using msDecrypt = New MemoryStream(cipherText)

                '    Using csDecrypt = New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)

                '        ''Using srDecrypt = New StreamReader(csDecrypt)
                '        Using srDecrypt = New StreamReader(csDecrypt)
                '            plaintext = srDecrypt.ReadToEnd
                '        End Using
                '    End Using
                'End Using

                ''New code done as suggested by Bhagyashri mam on 22-11-2019
                Dim msDecrypt As MemoryStream = New MemoryStream(cipherText)
                Dim csDecrypt As CryptoStream = New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
                Dim srDecrypt = New StreamReader(csDecrypt)
                plaintext = srDecrypt.ReadToEnd
                ''New code done as suggested by Bhagyashri mam on 22-11-2019

            Catch
                plaintext = "keyError"
            End Try
        End Using

        Return plaintext
    End Function

    Private Function EncryptStringToBytes(ByVal plainText As String, ByVal key As Byte(), ByVal iv As Byte()) As Byte()
        If plainText Is Nothing OrElse plainText.Length <= 0 Then
            Throw New ArgumentNullException("plainText")
        End If

        If key Is Nothing OrElse key.Length <= 0 Then
            Throw New ArgumentNullException("key")
        End If

        If iv Is Nothing OrElse iv.Length <= 0 Then
            Throw New ArgumentNullException("key")
        End If

        Dim encrypted As Byte()

        Using rijAlg = New RijndaelManaged
            rijAlg.Mode = CipherMode.CBC
            rijAlg.Padding = PaddingMode.PKCS7
            rijAlg.FeedbackSize = 128
            rijAlg.Key = key
            rijAlg.IV = iv
            Dim encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV)

            'Using msEncrypt = New MemoryStream

            '    Using csEncrypt = New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)

            '        Using swEncrypt = New StreamWriter(csEncrypt)
            '            swEncrypt.Write(plainText)
            '        End Using

            '        encrypted = msEncrypt.ToArray
            '    End Using
            'End Using

            ''New code done as suggested by Bhagyashri mam on 22-11-2019

            Dim msEncrypt As MemoryStream = New MemoryStream
            Dim csEncrypt As CryptoStream = New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
            Dim swEncrypt = New StreamWriter(csEncrypt)
            swEncrypt.Write(plainText)
            encrypted = msEncrypt.ToArray
            ''New code done as suggested by Bhagyashri mam on 22-11-2019

        End Using

        Return encrypted
    End Function
End Module


