
Partial Class Logout
    Inherits System.Web.UI.Page
    ''To get the page details used this and this is for Audit trail issue detail point added this on 01-12-2019
    Dim RAffected As Integer = 0
    Dim ModuleName As String = String.Empty
    ''To get the page details used this and this is for Audit trail issue detail point added this on 01-12-2019

    '''' This Should Add on every Page and this is added for server banner point 25-12-2019
    Protected Sub Application_PreSendRequestHeaders()
        Response.Headers.Remove("Server") ''Commneted this on 23-12-2019 as error occure
        Response.Headers.Remove("X-AspNet-Version") ''ASP.NET Response Headers GIS Issue added on 03-01-2020
        Response.AddHeader("Sample1", "Value1")
    End Sub
    '''' This is for getting the value of VSUK added this on 11-11-2019
    Protected Overrides Sub OnInit(ByVal e As EventArgs)

        MyBase.OnInit(e)
        '' ''CSRFUserKey = ViewStateUserKey
        ''Call GetDataByDr("select top 1 CSRFUserKey from CCF_User_Activity_TrackBackUp111119 where offline='No' and SessionID='" & Session("SID") & "' and UserName='" & Session("EmpDetails") & "' and CSRFUserKey IS NOT NULL and LogOutDateTime IS NULL order by AccessDateTime desc")
        ''If DataDr.HasRows Then
        ''    While DataDr.Read
        ''        CSRFUK = DataDr("CSRFUserKey")
        ''    End While
        ''End If
        ''DataDr.Close()
        CSRFUK = GetMySQLAValue("select top 1 CSRFUserKey from Hamali_User_Activity_Track where offline='No' and SessionID='" & Session("SID") & "' and User_staffid='" & Session("staffid") & "' and CSRFUserKey IS NOT NULL and LogOutDateTime IS NULL order by AccessDateTime desc")
        ''Session Fixation issue code added on 26-12-2019 is as below 
        AuthTokennDtl = GetMySQLAValue("select top 1 AuthToken from Hamali_User_Activity_Track where offline='No' and SessionID='" & Session("SID") & "' and User_staffid='" & Session("staffid") & "' and CSRFUserKey IS NOT NULL and LogOutDateTime IS NULL order by AccessDateTime desc")
        ''Session Fixation issue code added on 26-12-2019 is as below 
    End Sub
    '''' This Should Add on every Page and this is added for server banner point 25-12-2019

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Application_PreSendRequestHeaders() '''' This Should Add on every Page and this is added for server banner point 25-12-2019


            ''This is used to deactivate the browser back button on respective page added this on 13-05-2020
            ClientScript.RegisterClientScriptBlock(Page.GetType, "NOBACK", "<script>if(history.length>0)history.go(+1);</script>")
            ''Code is ended here added on 13-05-2020



            ''New code for Improper session termination  / Session Fixation added on 19-05-2020
            ''If Session("CSRFUKSIDNew") IsNot Nothing And Session("AuthToken") IsNot Nothing And Request.Cookies("AuthToken") IsNot Nothing Then  ''New code for Improper session termination  / Session Fixation added on 19-05-2020

            ''If Session("AuthToken").ToString.Equals(Request.Cookies("AuthToken").Value) Then  ''New code for Improper session termination  / Session Fixation added on 19-05-2020





            ''If CSRFUK <> Nothing Or CSRFUK <> "" Then    '' If is here Added this on 01-01-2020
            ''Qry to GIS Report Input For CSRF / Request Flooding point validation page wise added this on 27-11-2019
            If CSRFUK = Session("CSRFUKSIDNew") Then   ''This is for else actule live usage   '' Added And obj.AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) in  if on 23-12-2019 ''''Session("AuthToken").ToString.Equals(Request.Cookies("AuthToken").Value) And AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value)

                If Not Page.IsPostBack Then
                    ''To get the page details used this and this is for Audit trail issue detail point added this on 01-12-2019

                    If Session("staffid") = "" And Session("initial") = "" And Session("plantName") = "" And Session("Role") = "" Then
                    Else
                        ModuleName = Page.GetType.Name.Split("_")("0")
                        RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                    End If


                    ''To get the page details used this and this is for Audit trail issue detail point added this on 01-12-2019

                    If Not IsNothing(Request.QueryString("msg")) Then
                        Response.Write(Request.QueryString("msg").ToString)
                    End If

                    'Dim RAffected As Integer = 0 ''Added this line of code is for Session Hijacking point as discussed with Bhagyashri Mam added this on 22-11-2019

                    ''Below code is for Session Hijacking point as discussed with Bhagyashri Mam added this on 22-11-2019
                    'RAffected = SaveCmplntData("Update UserMaster set EmpLoginStatus='0' where (EmpStaffID='" & Session("EmpLoginStatusID") & "' or EMpSapId='" & Session("EmpLoginStatusID") & "') and EmpLoginStatus='1' ")

                    RAffected = SaveCmplntData("Update hamali_user_master set EmpLoginStatus='0',LoginCount=0,AccountLock='0' where (Emp_staffid='" & Session("EmpLoginStatusID") & "' or EMpSapId='" & Session("EmpLoginStatusID") & "')")  '' and EmpLoginStatus='1' 

                    ''RAffected = SaveCmplntData("Update UserMaster set EmpLoginStatus='0',LoginCount=0,AccountLock=0  where (EmpStaffID='" & Session("EmpLoginStatusID") & "' or EMpSapId='" & Session("EmpLoginStatusID") & "') and EmpLoginStatus='1' ")
                    ''Below code is for Session Hijacking point as discussed with Bhagyashri Mam added this on 22-11-2019


                    'This code is add on 23-12-2010 for get sessionout time for track
                    ''''Call LogOut_Session(Session.SessionID.ToString) ''Commented this on 12-11-2019 and add below code

                    ''Call LogOut_Session(Session("CSRFUKSID"), Session("CSRFUKSIDNew")) '' Added this code on 12-11-2019 for validating


                    ''Added this on 24-12-2019 for validating the AuthToken details by Me
                    If AuthTokennDtl <> Nothing Or AuthTokennDtl <> "" Then
                        Call LogOut_SessionWithAuth(Session("CSRFUKSID"), Session("CSRFUKSIDNew"), AuthTokennDtl.ToString) '' Added this code on 23-12-2019 for validating of AuthToken value implementation as Mam suggested or used and commmneted upper code
                    ElseIf Session("CSRFUKSIDNew") <> Nothing Or Session("CSRFUKSIDNew") <> "" Then   ''Added this line of code on 08-01-2020
                        Call LogOut_SessionWithCSRF(Session("CSRFUKSID"), Session("CSRFUKSIDNew")) '' Added this code on 23-12-2019 for validating of AuthToken value implementation as Mam suggested or used and commmneted upper code
                    Else ''Added this line of code on 08-01-2020
                        Call LogOut_Session(Session("staffid"))

                    End If
                    ''End If
                    ''COde is ended here added on 24-12-2019

                    Session.Clear() '' Added  this on 23-11-2019 For Session replay riske and Session not terminate after password change

                End If

            Else ''Role wise page Autherity option Added this Code on 25-11-2019 and below line of code            
                Response.Write("<script language='javascript'>window.alert('Be careful.... You do not have the rights to access this Page / Form');window.location='Login.aspx';</script>")
            End If ''This line of code is added on 27-11-2019 for CSRF / Request Flooding point validation
            Session.Abandon()
            FormsAuthentication.SignOut()

            ''---------Start Session replay GIS issue code added on 23-12-2019 as Mam suggested this

            ''on the reference of below link change the sequesnce of values https://dzone.com/articles/broken-authentication-and-session-management-part-1
            Session.Clear()
            Session.Abandon()
            Session.RemoveAll()
            'code is end here added on 13-05-2020

            If Request.Cookies("ASP.NET_SessionId") IsNot Nothing Then
                Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                Response.Cookies("ASP.NET_SessionId").Expires = Date.Now.AddMonths(-20)
            End If

            If Request.Cookies("AuthToken") IsNot Nothing Then
                Response.Cookies("AuthToken").Value = String.Empty
                Response.Cookies("AuthToken").Expires = Date.Now.AddMonths(-20)
            End If
            ''---------End Session replay GIS issue code added on 23-12-2019 as Mam suggested this


            ''New Code added on 13-05-2020 as below
            AuthTokennDtl = Nothing
            Session("CSRFUKSID") = Nothing
            Session("CSRFUKSIDNew") = Nothing
            Session("staffid") = Nothing
            ''End New Code added on 13-05-2020 as below

            ''THis code is added on 20-05-2020 FOr Improper session termination GIS Vul.
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1))
            Response.Cache.SetNoStore()
            Response.Expires = -1500
            FormsAuthentication.SignOut()
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "clearHistory", "ClearHistory();", True)
            Response.Cookies.Clear()
            HttpContext.Current.Session.Clear()
            HttpContext.Current.Session.Abandon()
            HttpContext.Current.Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
            ''This code is added on 20-05-2020 is ended here

            Session("AuthToken") = String.Empty ''Aded this on 21-05-2020
            Session.Remove("AuthToken") ''Aded this on 21-05-2020
            Session("AuthToken") = String.Empty ''Aded this on 21-05-2020


            ''End If      '' If end is here Added this on 01-01-2020

            ''End If ''New code for Improper session termination  / Session Fixation added on 19-05-2020

            ''End If ''New code for Improper session termination  / Session Fixation added on 19-05-2020
            ''New code is ended here added on 19-05-2020

           


        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
            Response.Write("<script language='javascript'>window.alert('Invalid Values');</script>")
        Finally
            If con.State = Data.ConnectionState.Open Then con.Close()
        End Try

    End Sub

    '''' Qry is ended here added for VSUK added this on 11-11-2019

    ''**** working Fine for with out CSRF filed in DB table commented on 11-11-2019
    ''Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    ''    Try
    ''        'This code is add on 23-12-2010 for get sessionout time for track
    ''        Call LogOut_Session(Session.SessionID.ToString)
    ''        FormsAuthentication.SignOut()
    ''        'code is end here

    ''        If Not Page.IsPostBack Then
    ''            If Not IsNothing(Request.QueryString("msg")) Then
    ''                Response.Write(Request.QueryString("msg").ToString)
    ''            End If
    ''        End If

    ''    Catch ex As Exception
    ''        MsgBox(ex.Message)
    ''    End Try
    ''End Sub
    ''code is end here Added on 11-11-2019

    

End Class
