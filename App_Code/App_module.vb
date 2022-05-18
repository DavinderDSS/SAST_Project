Imports Microsoft.VisualBasic
Imports system.Data
Imports System.Data.SqlClient
Imports System.Net.Mail

Imports System.IO
Imports System.Web

Imports System.Security.Cryptography
Imports System.Globalization
Imports System.Net.IPHostEntry
Imports System.Net.Dns
Imports System.Net

Public Module App_module

    Public con As New SqlConnection("Password=Mahdb@2022;Persist Security Info=True;User ID=sa;Initial Catalog=Hamali5010DB;Data Source=10.80.50.153;Connect Timeout=600")  '' This is for Live senario on 241 server DB 



    Public RecordsAffected As Integer
    Public DataDr As SqlClient.SqlDataReader
    Public Location As String = String.Empty    ''This is used to get the Location name
    Public ResultStatus As String = String.Empty  ''This is for Amend Result status detail

    Public CSRFUK As String = String.Empty '' Added this for CSRF value 19--11-2019

    Public LoginSessionSts As Integer ''= String.Empty  ''Added this value for Session hijacking 22-11-2019
    Public LoginCount As Integer ''= String.Empty ''Added this on 29-11-2019 for Captcha validation
    Public AccountLockCount As Integer ''= String.Empty ''Added this on 29-11-2019 for Captcha validation

    Public _encryptedString ''Added this on 23-11-2019 for Session Hijacking issue
    ' Public ImgFUpCID As Integer 'This is added on 30-05-2012 for getting CID of fileuploading time
    Public AuthTokennDtl As String ''= String.Empty '' Added this for CSRF value 19--11-2019

    Public Sub ForAJAX(ByVal frm As Page)
        Dim cbReference As String
        cbReference = frm.ClientScript.GetCallbackEventReference(frm, "arg", "ReceiveServerData", "context")
        Dim callbackScript As String = ""
        callbackScript &= "function CallServer(arg, context) { " & cbReference & "} ;"
        frm.ClientScript.RegisterClientScriptBlock(frm.GetType(), "CallServer", callbackScript, True)
    End Sub
    Public Sub GetDataByDr(ByVal TableQry As String)
        Dim cmd As New SqlCommand(TableQry, con)
        If con.State = ConnectionState.Closed Then con.Open()
        DataDr = cmd.ExecuteReader
    End Sub
    Public Function getHTML(ByVal ctrl As Control) As String
        Dim sw As New System.IO.StringWriter
        Dim htw As New HtmlTextWriter(sw)
        ctrl.RenderControl(htw)
        Return sw.ToString()
    End Function

    Public Sub GetSingleRow(ByVal TableQry As String)
        Dim cmd As New SqlCommand(TableQry, con)
        If con.State = ConnectionState.Closed Then con.Open()
        DataDr = cmd.ExecuteReader(CommandBehavior.SingleRow)
    End Sub

    Public Function SaveData(ByVal TableQry As String) As Integer
        Dim cmd As New SqlCommand(TableQry, con)
        If con.State = ConnectionState.Closed Then con.Open()
        RecordsAffected = cmd.ExecuteNonQuery()
        'cmd.ExecuteNonQuery()
        If con.State = ConnectionState.Open Then con.Close()
        Return RecordsAffected
    End Function
    Public Sub FillCombo(ByVal TableQry As String, ByVal TextField As String, ByVal ValueField As String, ByVal MyCmb As DropDownList, Optional ByVal ConState As String = "MustClose")
        Try
            Dim cmd As New SqlCommand(TableQry, con)
            If con.State = ConnectionState.Closed Then con.Open()
            DataDr = cmd.ExecuteReader()
            MyCmb.DataTextField = TextField
            MyCmb.DataValueField = ValueField
            If DataDr.HasRows Then
                MyCmb.DataSource = DataDr
                MyCmb.DataBind()
            End If
            If ConState = "MustClose" Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                    DataDr.Close()
                    DataDr = Nothing

                End If
            Else
                DataDr.Close()
                DataDr = Nothing
            End If

        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
                DataDr.Close()
                DataDr = Nothing
            End If
        End Try

    End Sub

    Public Sub ShowData(ByVal SPName As String, ByVal Parameters As String)
        Dim cmd As New SqlCommand()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        If Parameters = "" Then
            cmd.CommandText = SPName
        Else
            cmd.CommandText = "Exec " & SPName & " " & Parameters
        End If

        If con.State = ConnectionState.Closed Then con.Open()
        DataDr = cmd.ExecuteReader
    End Sub

    Public Sub GetDataByQUERY(ByVal Query As String, ByVal mycmb As DropDownList, ByVal TextField As String, ByVal ValueField As String)
        Dim cmd As New SqlCommand(Query, con)

        If con.State = ConnectionState.Closed Then con.Open()
        DataDr = cmd.ExecuteReader
        If DataDr.HasRows Then
            Dim itmSelect As New ListItem
            Dim itmAll As New ListItem

            itmSelect.Text = "Select"
            itmSelect.Value = "0"

            'mycmb.Items.Add(itmSelect)
            'itmAll.Text = "All"
            'itmAll.Value = "%"

            'mycmb.Items.Add(itmAll)
            ''mycmb.Items.Add("All")

            mycmb.DataTextField = TextField
            mycmb.DataValueField = ValueField
            While DataDr.Read
                Dim itm As New ListItem
                itm.Text = CStr(DataDr(1)).Trim(".")
                itm.Value = CStr(DataDr(0))
                mycmb.Items.Add(itm)
            End While
        End If

        DataDr.Close()
        con.Close()
    End Sub
    Public Sub GetDataByQUERYAppDisapp(ByVal Query As String, ByVal mycmb As DropDownList, ByVal TextField As String, ByVal ValueField As String)
        Dim cmd As New SqlCommand(Query, con)

        If con.State = ConnectionState.Closed Then con.Open()
        DataDr = cmd.ExecuteReader
        If DataDr.HasRows Then
            Dim itmSelect As New ListItem
            Dim itmAll As New ListItem

            'itmSelect.Text = "Select"
            'itmSelect.Value = "0"

            'mycmb.Items.Add(itmSelect)

            itmAll.Text = "All"
            itmAll.Value = "%"

            mycmb.Items.Add(itmAll)
            ''mycmb.Items.Add("All")

            mycmb.DataTextField = TextField
            mycmb.DataValueField = ValueField
            While DataDr.Read
                Dim itm As New ListItem
                'itm.Text = CStr(DataDr(1)).Trim(".")
                itm.Value = CStr(DataDr(0))
                mycmb.Items.Add(itm)
            End While
        End If

        DataDr.Close()
        con.Close()
    End Sub
    Public Sub GetDataByQUERYApp(ByVal Query As String, ByVal mycmb As DropDownList, ByVal TextField As String, ByVal ValueField As String)
        Dim cmd As New SqlCommand(Query, con)

        If con.State = ConnectionState.Closed Then con.Open()
        DataDr = cmd.ExecuteReader
        If DataDr.HasRows Then
            Dim itmSelect As New ListItem
            Dim itmAll As New ListItem

            'itmSelect.Text = "Select"
            'itmSelect.Value = "0"

            'mycmb.Items.Add(itmSelect)

            'itmAll.Text = "All"
            'itmAll.Value = "%"

            'mycmb.Items.Add(itmAll)
            ''mycmb.Items.Add("All")

            mycmb.DataTextField = TextField
            mycmb.DataValueField = ValueField
            While DataDr.Read
                Dim itm As New ListItem
                itm.Text = CStr(DataDr(1)).Trim(".")
                itm.Value = CStr(DataDr(0))
                mycmb.Items.Add(itm)
            End While
        End If

        DataDr.Close()
        con.Close()
    End Sub



    Public Sub GetDataByQUERYPlantNdCode(ByVal Query As String, ByVal mycmb As DropDownList, ByVal TextField As String, ByVal ValueField As String)
        Dim cmd As New SqlCommand(Query, con)

        If con.State = ConnectionState.Closed Then con.Open()
        DataDr = cmd.ExecuteReader
        If DataDr.HasRows Then
            Dim itmSelect As New ListItem
            Dim itmAll As New ListItem

            itmSelect.Text = "Select"
            itmSelect.Value = "0"

            'mycmb.Items.Add(itmSelect)
            'itmAll.Text = "All"
            'itmAll.Value = "%"

            'mycmb.Items.Add(itmAll)
            ''mycmb.Items.Add("All")

            mycmb.DataTextField = TextField
            mycmb.DataValueField = ValueField
            While DataDr.Read
                Dim itm As New ListItem
                itm.Text = CStr(DataDr(0)).Trim(".")
                'itm.Value = CStr(DataDr(0))
                mycmb.Items.Add(itm)
            End While
        End If

        DataDr.Close()
        con.Close()
    End Sub

    Public Sub GetDataByQUERYAll(ByVal Query As String, ByVal mycmb As DropDownList, ByVal TextField As String, ByVal ValueField As String)
        Dim cmd As New SqlCommand(Query, con)

        If con.State = ConnectionState.Closed Then con.Open()
        DataDr = cmd.ExecuteReader
        If DataDr.HasRows Then
            Dim itmSelect As New ListItem
            Dim itmAll As New ListItem

            'itmSelect.Text = "Select"
            'itmSelect.Value = "0"
            'mycmb.Items.Add(itmSelect)

            itmAll.Text = "All"
            itmAll.Value = "%"

            mycmb.Items.Add(itmAll)
            'mycmb.Items.Add("All")

            mycmb.DataTextField = TextField
            mycmb.DataValueField = ValueField
            While DataDr.Read
                Dim itm As New ListItem
                itm.Text = CStr(DataDr(1)).Trim(".")
                itm.Value = CStr(DataDr(0))
                mycmb.Items.Add(itm)
            End While
        End If

        DataDr.Close()
        con.Close()
    End Sub


    Public Function SaveDataWithTransaction(ByVal TableQry As String, ByVal Seperator As Char) As Boolean
        Dim commited As Boolean = False

        If con.State = Data.ConnectionState.Closed Then con.Open()
        Dim sqlTran As SqlTransaction = con.BeginTransaction ' begin the transaction

        Try

            Dim cmd As SqlCommand
            cmd = con.CreateCommand
            cmd.Transaction = sqlTran

            Dim Queries
            Queries = TableQry.Split(Seperator)

            ' ''New Code add for multiple row addition
            'Dim QryStr As String = "Insert Into hamal_rate_master(FromDateRang,ToDateRang,plantCode,activity,distance,weight,loading,unloading,sorting,stiching,stacking,restacking,bundle_preparation,opening_feeding,loading_unloading,noStack,stackDhToAnti,stackPlantToGodown,heightDiff) Values"
            ' ''code is end here

            Dim i As Int16 = 0
            For i = 0 To UBound(Queries)
                If Queries(i).ToString.Length > 5 Then
                    cmd.CommandText = Queries(i)
                    cmd.ExecuteNonQuery()
                End If
            Next

            sqlTran.Commit()
            commited = True

        Catch DataBaseEx As SqlException
            sqlTran.Rollback()
            commited = False

        Catch ex As Exception
            commited = False

        Finally
            If con.State = Data.ConnectionState.Open Then con.Close()
        End Try

        Return commited
    End Function

    Public Sub manipulation(ByVal tableqry As String)
        If con.State = ConnectionState.Open Then con.Close()
        Dim cmd As New SqlCommand(tableqry, con)
        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub
    Public Function SaveCmplntData(ByVal TableQry As String) As Integer
        Dim cmd As New SqlCommand(TableQry, con)
        If con.State = ConnectionState.Closed Then con.Open()
        RecordsAffected = cmd.ExecuteNonQuery()
        'cmd.ExecuteNonQuery()
        If con.State = ConnectionState.Open Then con.Close()
        Return RecordsAffected
    End Function

    ''**** working Fine for with out CSRF filed in DB table commented on 11-11-2019
    ''Public Function LogOut_Session(ByVal Session_ID As String) As Boolean
    ''Dim SID = Session_ID
    ''Dim RAffected As Integer = 0
    ''RAffected = SaveCmplntData("Update Hamali_User_Activity_Track set LogOutDateTime=getdate(),offline='Yes' where offline='No' and SessionID='" & SID & "'")
    ''End Function
    ''code is end here Added on 11-11-2019

    ''new code is added 12-11-2019 for maintaning login-logout datetime here Added this on 12-11-2019 with the value of CSRFUserKey
    Public Function LogOut_SessionWithCSRF(ByVal Session_ID As String, ByVal CSRFUKSIDNew As String) As Boolean
        ''Dim SID = Session_ID
        ''Dim RAffected As Integer = 0
        ''RAffected = SaveCmplntData("Update Hamali_User_Activity_Track set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL where offline='No' and SessionID='" & Session_ID & "' and CSRFUserKey='" & CSRFUKSIDNew & "'")
        ''RAffected = SaveCmplntData("Update Hamali_User_Activity_AuditTrail set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL where offline='No' and SessionID='" & Session_ID & "' and CSRFUserKey='" & CSRFUKSIDNew & "'")

        ''Added this code on 21-05-2020

        If SaveCmplntData("Update Hamali_User_Activity_Track set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL,AuthToken=NULL where offline='No' and SessionID='" & Session_ID & "' and CSRFUserKey='" & CSRFUKSIDNew & "'") = True Then
        End If
        If SaveCmplntData("Update Hamali_User_Activity_AuditTrail set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL where offline='No' and SessionID='" & Session_ID & "' and CSRFUserKey='" & CSRFUKSIDNew & "'") = True Then
        End If
        ''Code is ended here added on 21-05-2020


    End Function

    ''new code is added 12-11-2019 for maintaning login-logout datetime here Added this on 12-11-2019 with the value of CSRFUserKey
    Public Function LogOut_SessionWithAuth(ByVal Session_ID As String, ByVal CSRFUKSIDNew As String, ByVal AuthTokenDtl As String) As Boolean

        'RAffected = SaveData("Update CCF_User_Activity_Track set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL where offline='No' and SessionID='" & Session_ID & "' and CSRFUserKey='" & CSRFUKSIDNew & "'")
        'RAffected = SaveData("Update CCF_User_Activity_AuditTrail set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL where offline='No' and SessionID='" & Session_ID & "' and CSRFUserKey='" & CSRFUKSIDNew & "'")

        If SaveData("Update Hamali_User_Activity_Track set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL,AuthToken=NULL where offline='No' and SessionID='" & Session_ID & "' and CSRFUserKey='" & CSRFUKSIDNew & "' and AuthToken= '" & AuthTokenDtl & "' ") = True Then
        End If
        If SaveData("Update Hamali_User_Activity_AuditTrail set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL where offline='No' and SessionID='" & Session_ID & "' and CSRFUserKey='" & CSRFUKSIDNew & "'") = True Then
        End If

    End Function

    ''Added this on 09-01-2020 on the user code basis log out condition
    Public Function LogOut_Session(ByVal Session_ID As String) As Boolean
        ''Dim SID = Session_ID
        ''Dim RAffected As Integer = 0
        ''RAffected = SaveCmplntData("Update Hamali_User_Activity_Track set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL,AuthToken=NULL where offline='No' and User_StaffID='" & Session_ID & "'")
        ''RAffected = SaveCmplntData("Update Hamali_User_Activity_AuditTrail set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL where offline='No' and User_StaffID='" & Session_ID & "'")

        ''Added this code on 21-05-2020
        If SaveCmplntData("Update Hamali_User_Activity_Track set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL,AuthToken=NULL where offline='No' and User_StaffID='" & Session_ID & "'") = True Then
        End If
        If SaveCmplntData("Update Hamali_User_Activity_AuditTrail set LogOutDateTime=getdate(),offline='Yes',CSRFUserKey=NULL where offline='No' and User_StaffID='" & Session_ID & "'") = True Then
        End If
        ''Code is ended here added on 21-05-2020

    End Function

    Public Function Checking(ByVal tableqry As String) As Boolean
        Dim ReturnValue As Boolean = False
        Try
            Dim cmd As New SqlCommand(tableqry, con)
            If con.State = ConnectionState.Closed Then con.Open()
            DataDr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If DataDr.HasRows Then
                ReturnValue = True
            Else
                ReturnValue = False
            End If

        Catch ex As Exception
            ReturnValue = False
        Finally
            DataDr.Close()
            DataDr = Nothing
            If con.State = ConnectionState.Open Then con.Close()
        End Try
        Return ReturnValue
    End Function
    Public Sub GetDataBySPLQUERY(ByVal QUERY As String, ByVal LocationGroup As String)
        Dim cmd As New SqlCommand(QUERY, con)
        'cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@LocationGroup", SqlDbType.VarChar).Value = LocationGroup
        If con.State = ConnectionState.Closed Then con.Open()
        DataDr = cmd.ExecuteReader
    End Sub


    ''Code is ended here added on 13-11-2019
    ''Code to Encript the string for CSRF validation added this on 14-11-2019
    '' <summary>
    '' Encrypt text using AES Algorithm
    '' </summary>
    '' <param name="text">Text to encrypt</param>
    '' <param name="password">Password with which to encrypt</param>
    '' <returns>Returns encrypted text</returns>
    '' <remarks></remarks>
    'Public Function Encrypt(ByVal text As String, ByVal password As String) As String ''Commented this on 19-11-2019
    Public Function Encrypt(ByVal text As String) As String ''Added  this on 19-11-2019
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Dim hash(31) As Byte
        ''Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password)) ''Commented this on 19-11-2019
        Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(text)) ''Added  this on 19-11-2019
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = System.Security.Cryptography.CipherMode.ECB
        Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
        Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(text)
        encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        Return encrypted
    End Function
    ''Code is ended here added on 14-11-2019 for CSRF string Enription

    ''Code to Decrypt the string for CSRF validation added this on 14-11-2019
    '' <summary>
    '' Decrypt text using AES Algorithm
    '' </summary>
    '' <param name="text">Text to decrypt</param>
    '' <param name="password">Password with which to decrypt</param>
    '' <returns>Returns decrypted text</returns>
    '' <remarks></remarks>
    'Public Function Decrypt(ByVal text As String, ByVal password As String) As String ''Commented this on 19-11-2019
    Public Function Decrypt(ByVal text As String) As String ''Added  this on 19-11-2019
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Dim hash(31) As Byte
        'Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password)) ''Commented this on 19-11-2019
        Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(text)) ''Added  this on 19-11-2019
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = System.Security.Cryptography.CipherMode.ECB
        Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
        Dim Buffer As Byte() = Convert.FromBase64String(text)
        decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        Return decrypted
    End Function
    ''Code is ended here added on 14-11-2019 for CSRF string Decrypt

    Public Function GetMySQLAValue(ByVal sqlquery As String)

        Dim cmd As New SqlCommand(sqlquery, con)
        con.Open()
        Dim res = cmd.ExecuteScalar()
        con.Close()
        Return res

    End Function

    Public Function GetIPAddress() As String
        Dim ClientIPAddress = ""
        Dim Host As IPHostEntry = Nothing
        Dim Hostname As String = Nothing
        Hostname = Environment.MachineName
        Host = Dns.GetHostEntry(Hostname)

        For Each IP As IPAddress In Host.AddressList

            If IP.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                ClientIPAddress = Convert.ToString(IP)
            End If
        Next

        Return ClientIPAddress
    End Function
    ''Added this on 05-01-2020 for getting the client IP addres for CSRF GIS Point value
    Public Function GetClientIPAddress() As String
        Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
        Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If String.IsNullOrEmpty(sIPAddress) Then
            Return context.Request.ServerVariables("REMOTE_ADDR")
        Else
            Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
            Return ipArray(0)
        End If
    End Function
    ''Added this on 05-01-2020 for getting the client IP addres for CSRF GIS Point value

    '' Qry to Avoide 2.0 version Validation for SHA1 code for CSRF validation point added this on 26-11-2019
    Public Function getSHA1Hash(ByVal strToHash As String) As String

        Dim sha1Obj As New System.Security.Cryptography.SHA1CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = sha1Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult
    End Function

    ''**********Funcation to validate form fild server side code added on 02-12-2019 
    Public Function IsValidInputText(ByVal inputText As String) As Boolean
        Dim valid = True

        If inputText.Trim Is "" Then
            valid = False
        End If

        'Dim regex As Regex = New Regex("[~`!#$^*|\{}'<>/]") '' Commented this on 01-02-2020 and added below for record entry remove ' from list
        Dim regex As Regex = New Regex("[~`!#$^*|\{}<>/]")

        If regex.IsMatch(inputText) Then
            valid = False
        End If

        Return valid
    End Function

    Public Function IsPhoneNumber(ByVal number As String) As Boolean
        Dim valid = True

        Try

            If Not number.Length = 10 Then
                valid = False
            Else
                Dim temp = Convert.ToInt64(number)
            End If

        Catch h As Exception
            valid = False
        End Try

        Return valid
    End Function
    Public Function IsValidInputInteger(ByVal inputText As String) As Boolean
        Dim valid = True

        Try
            Dim temp = Convert.ToInt32(inputText)
        Catch h As Exception
            valid = False
        End Try

        Return valid
    End Function

    Public Function IsValidInputDouble(ByVal inputText As String) As Boolean
        Dim valid = True

        Try
            Dim temp = Convert.ToDouble(inputText)
        Catch h As Exception
            valid = False
        End Try

        Return valid
    End Function
    'Public Function IsValidDateTimeTest(ByVal dateTime As String) As Boolean
    '    Dim formats = {"dd/MM/yyyy"}
    '    Dim parsedDateTime As Date
    '    Return DateTime.TryParseExact(dateTime, formats, New CultureInfo("en-US"), DateTimeStyles.None, parsedDateTime)
    'End Function

    Public Function IsValidTimeFormat(ByVal input As String) As Boolean
        Dim dummyOutput As TimeSpan
        Return TimeSpan.TryParse(input, dummyOutput)
    End Function
    ''**********Funcation to validate form fild server side code added on 02-12-2019 ended here 

    ''Below function is dded to generate randum number for Request Flooding added on 12-05-2020
    Public Function getRandomNumber() As String
        Dim generator As Random = New Random()
        Dim r As String = generator.[Next](0, 999999).ToString("D6")
        Return r
    End Function
    ''Code is ended here added for randum number for Request Flooding added on 12-05-2020

End Module


