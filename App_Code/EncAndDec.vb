Imports Microsoft.VisualBasic
Imports System.Security.Cryptography.RijndaelManaged
Imports System.Web
Imports System.Security.Cryptography

Public Class EncAndDec

    'Public Function Encrypt(ByVal clearText As String) As String
    '    Dim EncryptionKey = "MAH2019SSGRAPESEXPORT"
    '    Dim clearBytes = Encoding.Unicode.GetBytes(clearText)

    '    Using encryptor As AES = Aes.Create
    '        Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
    '        encryptor.Key = pdb.GetBytes(32)
    '        encryptor.IV = pdb.GetBytes(16)

    '        Using ms = New MemoryStream

    '            Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateEncryptor, CryptoStreamMode.Write)
    '                cs.Write(clearBytes, 0, clearBytes.Length)
    '                cs.Close()
    '            End Using

    '            clearText = Convert.ToBase64String(ms.ToArray)
    '        End Using
    '    End Using

    '    Return clearText
    'End Function

    'Public Function Decrypt(ByVal cipherText As String) As String
    '    Dim EncryptionKey = "MAH2019SSGRAPESEXPORT"
    '    cipherText = cipherText.Replace(" ", "+")
    '    Dim cipherBytes = Convert.FromBase64String(cipherText)

    '    Using encryptor As System.Security.Cryptography.Aes = Aes.Create
    '        Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
    '        encryptor.Key = pdb.GetBytes(32)
    '        encryptor.IV = pdb.GetBytes(16)

    '        Using ms = New MemoryStream

    '            Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateDecryptor, CryptoStreamMode.Write)
    '                cs.Write(cipherBytes, 0, cipherBytes.Length)
    '                cs.Close()
    '            End Using

    '            cipherText = Encoding.Unicode.GetString(ms.ToArray)
    '        End Using
    '    End Using

    '    Return cipherText
    'End Function


    Public Function AES_Encrypt(ByVal input As String) As String   '', ByVal pass As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim EncryptionKey = "MAH2019SSGRAPESEXPORT"
            Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})

            'Dim hash(31) As Byte
            'Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            'Array.Copy(temp, 0, hash, 0, 16)
            'Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = pdb.GetBytes(32)
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
            Return input 'If encryption fails, return the unaltered input.
        End Try
    End Function
    'Decrypt a string with AES
    Public Function AES_Decrypt(ByVal input As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim EncryptionKey = "MAH2019SSGRAPESEXPORT"
            Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})

            'Dim hash(31) As Byte
            'Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            'Array.Copy(temp, 0, hash, 0, 16)
            'Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = pdb.GetBytes(32)
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Return input 'If decryption fails, return the unaltered input.
        End Try
    End Function

End Class
