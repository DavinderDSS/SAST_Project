Imports System.Data
Imports System.IO
Imports System.Data.SqlClient

''New Added on 08-12-2019
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports System.Configuration
Imports System.Text
Imports System.Data.Sql
Imports System.Net
Imports System.Guid
Imports System.Security.Cryptography
Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
'Imports System.Web.HttpApplicationState
Imports System.Web.SessionState
Imports System.Collections.Generic
Imports System.Data.OleDb
Imports System.Diagnostics.Debug
Imports System.Web.UI.Page
''Imports System.Web.Script.Serialization
''Imports System.Linq
''Imports System.Net.Http



Partial Class Login
    Inherits System.Web.UI.Page
    ''To get the page details used this and this is for Audit trail issue detail point added this on 01-12-2019
    Dim ModuleName As String = String.Empty
    ''To get the page details used this and this is for Audit trail issue detail point added this on 01-12-2019

    '''' This Should Add on every Page and this is added for server banner point 25-12-2019
    Protected Sub Application_PreSendRequestHeaders()
        Response.Headers.Remove("Server") ''Commneted this on 23-12-2019 as error occure
        Response.Headers.Remove("X-AspNet-Version") ''ASP.NET Response Headers GIS Issue added on 03-01-2020
        Response.AddHeader("Sample1", "Value1")
    End Sub
    '''' This Should Add on every Page and this is added for server banner point 25-12-2019

    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        MyBase.OnInit(e)
        If User.Identity.IsAuthenticated Then ViewStateUserKey = User.Identity.Name
        ViewStateUserKey = Session.SessionID
        Session("CSRFUKSID") = ViewStateUserKey '''' This is for getting the value of VSUK added this on 11-11-2019        
        antiforgery.Value = Encrypt(getSHA1Hash(Session.SessionID)) ''Added this on 26-11-2019 as SHA256 and above not support in 2.0 and 3.5 version

    End Sub

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''If Request.Browser.Browser.ToString.ToUpper = "IE" Then
        ''Else
        ''    Response.Redirect("UserError.aspx")
        ''End If
        Try

            ''This is used to deactivate the browser back button on respective page added this on 13-05-2020
            ClientScript.RegisterClientScriptBlock(Page.GetType, "NOBACK", "<script>if(history.length>0)history.go(+1);</script>")
            ''Code is ended here added on 13-05-2020

            Application_PreSendRequestHeaders() '''' This Should Add on every Page and this is added for server banner point 25-12-2019

            If Not Page.IsPostBack Then

                ''Login1.Attributes.Add("onclick", "showLoadingLGC();")

                'Session("UserIPAddressDtl") = GetIPAddress() '' This is used for getting IP Address of system.

                Session("UserIPAddressDtl") = GetClientIPAddress() '' This is used for getting Client Machine IP Address of system. added on 05-01-2020

                Session("AppName") = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName()
                ''Dim AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name

                ''Dim filename = Path.GetFileName(Request.Path)
                ''Dim fnm = Path.GetFileName(Request.CurrentExecutionFilePath.ToLower())

                'Login1.Focus()

                Login1.Focus()
                If IsNothing(Request.QueryString("nsf")) Then

                Else
                    Login1.UserName = CommonFunctions.Crypto.Decrypt(Request.QueryString("nsf").Replace(" ", "+"), "")
                    CType(Login1.FindControl("Password"), TextBox).Attributes.Add("value", "abcd")
                End If

            End If


        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
            Response.Write("<script language='javascript'>window.alert('Invalid Values');window.location='Login.aspx';</script>")
        Finally
            If con.State = Data.ConnectionState.Open Then con.Close()
        End Try

    End Sub

    Protected Sub Login1_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate
        Try

            Session("EmpLoginStatusID") = Login1.UserName '' Added this on 06-12-2019 for getting the detail of user while his account is locked and if he presed the logout button then user must be logout proprely 
            hfCount.Value = GetMySQLAValue("Exec STP_GISGetLoginCount '" & Login1.UserName & "'")

            
            If Login1.UserName = "" Or Login1.Password = "" Then
            Else ''Added this on 03-11-2019 for varifying the blank input and avoid the exceptions



                Dim RedirectPage As Boolean = False '' This is for redirecting user to CellInfo Page added this on 27-11-2019
                Dim obj As New CommonFunctions.Helper
                ''Dim Authenticated As Boolean = False ''= obj.IsAuthenticated(Login1.UserName, Login1.Password)

                ''Below variable is used to get the value details of mentioned variable added on 04-12-2019
                Dim AccountLockCountDtl = ""
                Dim LoginCountDtl = ""
                Dim LoginSessionStsDtl = ""

                Dim RAffected As Integer = 0

                Dim password = AESEncrytDecry.DecryptStringAES(Login1.Password)

                AccountLockCount = GetMySQLAValue("Exec STP_GISGetAccountLockCount '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code

                AccountLockCountDtl = AccountLockCount

                If AccountLockCount = 0 Then ''This code is added on 29-11-2019 for captch code

                    LoginCount = GetMySQLAValue("Exec STP_GISGetLoginCount '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code
                    LoginSessionSts = GetMySQLAValue("Exec STP_GISEmpLoginStatusCount '" & Login1.UserName & "' ")

                    LoginCountDtl = LoginCount
                    LoginSessionStsDtl = LoginSessionSts


                    If LoginSessionSts = 0 And LoginCount < 6 Then   ''And LoginCount < 5 Added this condition on 06-12-2019 for locking the account 

                        '' ''New code added on 05-12-2019 if captcha is display then need to check captcha first then other validation

                        'If txtCaptcha.Text.ToLower <> "" And (LoginCount > 3 Or LoginCount = 5) Then ''Commented this on 05-12-2019
                        ''*********As server side control commented and client side control added on 05-12-2019
                        If hfCaptchaVal.Value.ToLower <> "" And (LoginCount > 3 Or LoginCount = 5) Then
                            If hfCaptchaVal.Value.ToLower = Session("CaptchaVerify") Then

                                e.Authenticated = obj.IsAuthenticated(Login1.UserName, password)
                                If e.Authenticated = True Then ''\\ For live with out encrept / decrept logic old one  ''obj.IsAuthenticated(Login1.UserName, password) = True

                                    If ValidateLogin(Login1.UserName) = True Then

                                        ''Below code is for Session Hijacking point as discussed with Bhagyashri Mam added this on 22-11-2019
                                        RAffected = SaveCmplntData("Exec STP_GISEmpLoginStatusUpdate '" & Login1.UserName & "'  ")
                                        ''Below code is for Session Hijacking point as discussed with Bhagyashri Mam added this on 22-11-2019

                                        RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                        Session("EmpLoginStatusID") = Login1.UserName

                                        ''Code is added  on 12-05-2020 as User login successfully with correct data then Count must be 0 as suggested by Anand sir and guide by Bhagyashri mam
                                        RAffected = SaveCmplntData("Update hamali_user_master set LoginCount=0 where (Emp_staffid='" & Session("EmpLoginStatusID") & "' or EMpSapId='" & Session("EmpLoginStatusID") & "')")
                                        ''Code is ended here added on 12-05-2020

                                    Else ''  '''' This is added on 29-11-2019 for Captche code
                                        RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                    End If
                                Else
                                    RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                    ''Below code is added on 05-12-2019 for captcha field display and hide
                                    If LoginCount = 5 Or LoginCount > 5 Then
                                        RAffected = SaveCmplntData("Exec STP_GISLoginAccountLock '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 
                                        
                                    End If
                                    ''Code is ended here added on 05-12-2019 for captcha field display and hide

                                End If

                            Else

                                lblErrorMsg.Text = "Please enter correct captcha !" ''Added this as anand sir suggested for alert on 09-12-2019
                                
                                RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 
                                ''ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Captcha enterd is wrong. \nPlease enter the correct Captcha and re-try');", True)
                            End If

                            
                        ElseIf IsNothing(Request.QueryString("nsf")) And hfCaptchaVal.Value.ToLower = "" Then
                            
                            e.Authenticated = obj.IsAuthenticated(Login1.UserName, password)

                            If e.Authenticated = True Then  ''\\For live and increpted password \\Remove obj.IsAuthenticated(Login1.UserName, password) = True and added e.Authenticated on 05-12-2019
                                ''If obj.IsAuthenticated(Login1.UserName, Login1.Password) = True Then ''\\ For live with out encrept / decrept logic old one

                                If ValidateLogin(Login1.UserName) = True Then

                                    ''Below code is for Session Hijacking point as discussed with Bhagyashri Mam added this on 22-11-2019
                                    RAffected = SaveCmplntData("Exec STP_GISEmpLoginStatusUpdate '" & Login1.UserName & "' ")
                                    ''Below code is for Session Hijacking point as discussed with Bhagyashri Mam added this on 22-11-2019

                                    RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 
                                    ''RedirectPage = True '' This is for redirecting user to CellInfo Page added this on 27-11-2019

                                    Session("EmpLoginStatusID") = Login1.UserName

                                    ''Code is added  on 12-05-2020 as User login successfully with correct data then Count must be 0 as suggested by Anand sir and guide by Bhagyashri mam
                                    RAffected = SaveCmplntData("Update hamali_user_master set LoginCount=0 where (Emp_staffid='" & Session("EmpLoginStatusID") & "' or EMpSapId='" & Session("EmpLoginStatusID") & "')")
                                    ''Code is ended here added on 12-05-2020


                                Else ''  '''' This is added on 29-11-2019 for Captche code
                                    RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                End If
                            Else ''This is added on 29-11-2019 for captche
                                If LoginCount > 3 And LoginCount < 5 Then

                                    If hfCaptchaVal.Value = Session("CaptchaVerify") Then
                                        ''*********As server side control commented and client side control added on 05-12-2019

                                        If obj.IsAuthenticated(Login1.UserName, password) = True Then ''\\ For live with out encrept / decrept logic old one

                                            If ValidateLogin(Login1.UserName) = True Then

                                                ''Below code is for Session Hijacking point as discussed with Bhagyashri Mam added this on 22-11-2019
                                                RAffected = SaveCmplntData("Exec STP_GISEmpLoginStatusUpdate '" & Login1.UserName & "'  ")
                                                ''Below code is for Session Hijacking point as discussed with Bhagyashri Mam added this on 22-11-2019

                                                RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 
                                                RedirectPage = True '' This is for redirecting user to CellInfo Page added this on 27-11-2019

                                                ''Code is added  on 12-05-2020 as User login successfully with correct data then Count must be 0 as suggested by Anand sir and guide by Bhagyashri mam
                                                RAffected = SaveCmplntData("Update hamali_user_master set LoginCount=0 where (Emp_staffid='" & Session("EmpLoginStatusID") & "' or EMpSapId='" & Session("EmpLoginStatusID") & "')")
                                                ''Code is ended here added on 12-05-2020

                                            Else ''  '''' This is added on 29-11-2019 for Captche code
                                                RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                            End If
                                        Else
                                            RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                            ''Below code is added on 05-12-2019 for captcha field display and hide
                                            If LoginCount = 5 Or LoginCount > 5 Then
                                                RAffected = SaveCmplntData("Exec STP_GISLoginAccountLock '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                               
                                            End If
                                            ''Code is ended here added on 05-12-2019 for captcha field display and hide
                                        End If

                                    Else

                                        lblErrorMsg.Text = "Please enter correct captcha !" ''Added this as anand sir suggested for alert on 09-12-2019
                                        
                                    End If
                                ElseIf LoginCount < 3 Then
                                    RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 
                                    
                                ElseIf LoginCount = 3 Then
                                    RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 
                                    
                                ElseIf LoginCount = 5 Then
                                    RAffected = SaveCmplntData("Exec STP_GISLoginAccountLock '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 
                                    
                                End If
                            End If
                        Else
                            
                            If LoginCount = 5 Or LoginCount > 5 Then
                                RAffected = SaveCmplntData("Exec STP_GISLoginAccountLock '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 
                                
                            End If
                            ''Code is ended here added on 05-12-2019 for captcha field display and hide
                        End If

                        ''Below code is for Session Hijacking point as discussed with Bhagyashri Mam added this on 22-11-2019
                    ElseIf LoginSessionSts = 1 And obj.IsAuthenticated(Login1.UserName, password) = False Then
                        '' ''ClientScript.RegisterClientScriptBlock(Page.GetType, "AlertScript", "<script>alert('CCFMailRe-Scheduling is set Successfully');window.location='MenuPage.aspx'</script>")
                        ''Session("EmpLoginStatusID") = Login1.UserName
                        ''RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                        '' ''Commneted on 12-05-2020 as bhagyashri mam suggest after testing so commnented and added new alert
                        '' ''lblErrorMsg.Text = "Be careful.... You enter Wrong Details <br />You do not have the rights to access this Page / Form, <br />user is already login with this EmpCode Please check once" ''Added this as anand sir suggested for alert on 09-12-2019
                        ''lblErrorMsg.Text = "Wrong Credentials<br />" ''Added this as anand sir suggested for alert on 09-12-2019
                        ''''''Code is ended here added on 12-05-2020


                        If LoginCount > 3 And LoginCount < 5 Then
                            If hfCaptchaVal.Value.ToLower = Session("CaptchaVerify") Then  ''This Code is added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account

                                Session("EmpLoginStatusID") = Login1.UserName
                                RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                ''Commneted on 12-05-2020 as bhagyashri mam suggest after testing so commnented and added new alert
                                ''lblErrorMsg.Text = "Be careful.... You enter Wrong Details <br />You do not have the rights to access this Page / Form, <br />user is already login with this EmpCode Please check once" ''Added this as anand sir suggested for alert on 09-12-2019
                                lblErrorMsg.Text = "Wrong Credentials<br />" ''Added this as anand sir suggested for alert on 09-12-2019
                                ''''Code is ended here added on 12-05-2020

                                ''Below Code is added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account
                                If (LoginCount = 6 Or LoginCount > 6) Then
                                    RAffected = SaveCmplntData("Exec STP_GISLoginAccountLock '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                    lblErrorMsg.Text = "Account is locked because of too many unsuccessful Login trials. <br />Please contact Admin to unlock your account!" ''Added this as anand sir suggested for alert on 09-12-2019

                                End If
                                ''Code is ended here added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account



                            Else  ''This Code is added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account
                                lblErrorMsg.Text = "Please enter correct captcha !" ''Added this as anand sir suggested for alert on 09-12-2019

                            End If ''This Code is added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account


                        Else  ''Code is ended here added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account

                            Session("EmpLoginStatusID") = Login1.UserName
                            RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                            ''Commneted on 12-05-2020 as bhagyashri mam suggest after testing so commnented and added new alert
                            ''lblErrorMsg.Text = "Be careful.... You enter Wrong Details <br />You do not have the rights to access this Page / Form, <br />user is already login with this EmpCode Please check once" ''Added this as anand sir suggested for alert on 09-12-2019
                            lblErrorMsg.Text = "Wrong Credentials<br />" ''Added this as anand sir suggested for alert on 09-12-2019
                            ''''Code is ended here added on 12-05-2020

                            ''Below Code is added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account
                            If (LoginCount = 6 Or LoginCount > 6) Then
                                RAffected = SaveCmplntData("Exec STP_GISLoginAccountLock '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                lblErrorMsg.Text = "Account is locked because of too many unsuccessful Login trials. <br />Please contact Admin to unlock your account!" ''Added this as anand sir suggested for alert on 09-12-2019

                            End If
                            ''Code is ended here added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account

                        End If   ''Code is ended here added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account


                    ElseIf LoginSessionSts.ToString >= 1 And LoginSessionSts.ToString < 3 And obj.IsAuthenticated(Login1.UserName, password) = True Then
                        '' ''ClientScript.RegisterClientScriptBlock(Page.GetType, "AlertScript", "<script>alert('CCFMailRe-Scheduling is set Successfully');window.location='MenuPage.aspx'</script>")
                        ''Session("EmpLoginStatusID") = Login1.UserName
                        ''RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                        ''lblErrorMsg.Text = "User have already loggedin in another system. Please Logout first!<a href='Logout.aspx' style='display:inline-block;color:#8b0000;background-color:#FFFBFF;border-color:#CCCCCC;border-width:2px;border-style:Ridge;font-family:Verdana;font-size:1.0em;font-weight:bold;' id='atab'>LogOut</a>" ''Added this as anand sir suggested for alert on 09-12-2019

                        ''hfSignOut.Value = 1 ''Added on 06-12-2019 for anchore tage for log out with authenticated user 
                        ''hfAccLockCout.Value = 0


                        If LoginCount > 3 And LoginCount < 5 Then
                            If hfCaptchaVal.Value.ToLower = Session("CaptchaVerify") Then  ''This Code is added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account


                                ''ClientScript.RegisterClientScriptBlock(Page.GetType, "AlertScript", "<script>alert('CCFMailRe-Scheduling is set Successfully');window.location='MenuForm.aspx'</script>")
                                Session("EmpLoginStatusID") = Login1.UserName
                                RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                lblErrorMsg.Text = "User have already loggedin in another system. Please Logout first!  <a href='Logout.aspx' style='color:#8b0000;font-family:Verdana;font-size:0.8em;font-weight:bold;' id='atab'>LogOut</a><br />" ''style='color:#8b0000;background-color:#FFFBFF;border-color:#CCCCCC;border-width:2px;border-style:Ridge;font-family:Verdana;font-size:0.8em;font-weight:bold;' ''Added this as anand sir suggested for alert on 09-12-2019

                                hfSignOut.Value = 1 ''Added on 06-12-2019 for anchore tage for log out with authenticated user 
                                hfAccLockCout.Value = 0

                                ''Below Code is added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account
                                If (LoginCount = 6 Or LoginCount > 6) Then
                                    RAffected = SaveCmplntData("Exec STP_GISLoginAccountLock '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                    lblErrorMsg.Text = "User have already loggedin in another system. Please Logout first!  <a href='Logout.aspx' style='color:#8b0000;font-family:Verdana;font-size:0.8em;font-weight:bold;' id='atab'>LogOut</a><br /><br />Account is locked because of too many unsuccessful Login trials. <br />Please contact Admin to unlock your account!" ''Added this as anand sir suggested for alert on 09-12-2019

                                End If
                                ''Code is ended here added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account


                            Else  ''This Code is added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account
                                lblErrorMsg.Text = "Please enter correct captcha !" ''Added this as anand sir suggested for alert on 09-12-2019

                            End If ''This Code is added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account

                        Else


                            ''ClientScript.RegisterClientScriptBlock(Page.GetType, "AlertScript", "<script>alert('CCFMailRe-Scheduling is set Successfully');window.location='MenuForm.aspx'</script>")
                            Session("EmpLoginStatusID") = Login1.UserName
                            RAffected = SaveCmplntData("Exec STP_GISLoginCountUpdate '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                            lblErrorMsg.Text = "User have already loggedin in another system. Please Logout first!  <a href='Logout.aspx' style='color:#8b0000;font-family:Verdana;font-size:0.8em;font-weight:bold;' id='atab'>LogOut</a><br />" ''style='color:#8b0000;background-color:#FFFBFF;border-color:#CCCCCC;border-width:2px;border-style:Ridge;font-family:Verdana;font-size:0.8em;font-weight:bold;' ''Added this as anand sir suggested for alert on 09-12-2019

                            hfSignOut.Value = 1 ''Added on 06-12-2019 for anchore tage for log out with authenticated user 
                            hfAccLockCout.Value = 0

                            ''Below Code is added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account
                            If (LoginCount = 6 Or LoginCount > 6) Then
                                RAffected = SaveCmplntData("Exec STP_GISLoginAccountLock '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                                lblErrorMsg.Text = "User have already loggedin in another system. Please Logout first!  <a href='Logout.aspx' style='color:#8b0000;font-family:Verdana;font-size:0.8em;font-weight:bold;' id='atab'>LogOut</a><br /><br />Account is locked because of too many unsuccessful Login trials. <br />Please contact Admin to unlock your account!" ''Added this as anand sir suggested for alert on 09-12-2019

                            End If
                            ''Code is ended here added on 17-05-2020 if user is already loged in and NOt log out then need to lock the account

                        End If

                        ''Below code is added on 06-12-2019 for captcha field display and hide
                    ElseIf LoginSessionSts.ToString = 0 And (LoginCount = 5 Or LoginCount > 5) Then
                        RAffected = SaveCmplntData("Exec STP_GISLoginAccountLock '" & Login1.UserName & "' ") '''' This is added on 29-11-2019 for Captche code 

                        lblErrorMsg.Text = "Account is locked because of too many unsuccessful Login trials. <br />Please contact Admin to unlock your account!" ''Added this as anand sir suggested for alert on 09-12-2019
                        

                    End If  ''''LoginSessionSts = 0 if end here

                    ''This is used for Session Hijacking point as discussed with Bhagyashri Mam added this on 22-11-2019

                ElseIf AccountLockCount >= 1 Then
                    
                    lblErrorMsg.Text = "Account is locked because of too many unsuccessful Login trials. <br />Please contact Admin to unlock your account!" ''Added this as anand sir suggested for alert on 09-12-2019
                    
                    hfAccLockCout.Value = 1
                End If ''This end if used for Login username and password field added this code on 23-11-2019 ''AccountLockCount = 0 if end here

            End If ''This code is added on 29-11-2019 for captch code  ''Login ID and Password = "" if end here


        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
            Response.Write("<script language='javascript'>window.alert('Invalid Values');window.location='Login.aspx';</script>")
        Finally
            If con.State = Data.ConnectionState.Open Then con.Close()
        End Try


    End Sub
    ''On 115 server got error so added this on 07-12-2019 "Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application."
    Public Sub MesgBox(ByVal sMessage As String)
        Dim msg As String
        msg = "<script language='javascript'>"
        msg += "alert('" & sMessage & "');"
        msg += "<" & "/script>"
        Response.Write(msg)
    End Sub

    Public Sub Client_Alert(ByVal sMessage As String, Optional ByVal sURL As String = "")
        On Error Resume Next
        Dim str As String
        Dim P As System.Web.UI.Page = CType(System.Web.HttpContext.Current.Handler, System.Web.UI.Page)
        Dim sb As New StringBuilder(Len(sMessage) * 5)
        sMessage = sMessage.Replace(Chr(0), "")
        For Each c As String In sMessage : sb.Append("\x" & Right("0" & Hex(Asc(c)), 2)) : Next
        str = vbCrLf & "<script language=javascript>" & vbCrLf
        str = str & "    alert('" & sb.ToString & "');" & vbCrLf
        If Len(sURL) > 0 Then str = str & "    window.location='" & sURL & "';" & vbCrLf
        str = str & "</script>" & vbCrLf
        P.ClientScript.RegisterStartupScript(P.GetType, "", str)
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub
    '' This for On 115 server got error so added this on 07-12-2019 ended here
    Public Function ValidateLogin(ByVal UserName As String) As Boolean
        '', ByVal Password As String
        Dim ans As Boolean = False ''Added this on 21-05-2020 as it was under Try so remove from Try and place it here 
        Try


            ''''''''''Call GetDataByDr("select * from user_master where emp_staffid ='" & UserName.ToString & "' and emp_pass='" & Password.ToString & "' and flag='Y'")

            ''''Call GetDataByDr("select distinct um.emp_staffid as staffid,um.user_initial as initial,um.emp_mailid as mailid,um.emp_pass as pass, ur.plant_code as plantCode,pm.plant_name as plantName,pm.labour_type as labourType,pm.OT_flag as otFlag from user_master as um inner join user_rights as ur on um.emp_staffid=ur.emp_staffid inner join plant_master as pm on pm.plant_code=ur.plant_code where um.emp_staffid='" & UserName.ToString & "' and um.emp_pass='" & Password.ToString & "' and um.flag='Y'")

            ''Call GetDataByDr("select distinct emp_staffid as staffid,user_initial as initial,emp_mailid as mailid,emp_pass as pass,emp_plantcode as plantCode,emp_plantnm as plantName,emp_role as Role from hamali_user_master where emp_staffid='" & UserName.ToString & "' and emp_pass='" & Password.ToString & "' and empstatus='Active'")

            Call GetDataByDr("select distinct emp_staffid as staffid,user_initial as initial,emp_mailid as mailid,emp_pass as pass,emp_plantcode as plantCode,emp_plantnm as plantName,emp_role as Role from hamali_user_master where ( emp_staffid='" & UserName.ToString & "' or EmpSAPID='" & UserName.ToString & "' ) and empstatus='Active'")

            ''Dim response As Boolean ''Commented this on 27-12-2019 and add below code
            ''Dim ans As Boolean ''Commented on 21-05-2020
            Dim Role As String = ""
            If DataDr.HasRows Then

                ''response = True ''Commented this on 27-12-2019 and add below code 
                ans = True
                While DataDr.Read
                    Session("staffid") = DataDr(0)
                    Session("initial") = DataDr(1)
                    Session("mailid") = DataDr(2)
                    Session("pass") = DataDr(3)
                    Session("plantCode") = DataDr(4)
                    Session("plantName") = DataDr(5)
                    Session("RoleNo") = DataDr(6)
                    'Session("otFlag") = DataDr(7)

                    'Session("Password") = DataDr(3)
                    'Session("Role") = DataDr(4)
                    'Session("Location") = DataDr(5)

                    Select Case (DataDr("Role"))
                        Case 1
                            Role = "HeadOfficer"
                        Case 2
                            Role = "PlantOfficer"
                        Case 3
                            Role = "Supervisor"
                        Case 4
                            Role = "Admin"
                    End Select

                    Session("Role") = Role
                End While

            Else

                'response = False ''Commented this on 27-12-2019 and add below code
                ans = False
            End If
            DataDr.Close()
            DataDr = Nothing
            con.Close()

            'If response = True Then ''Commented this on 27-12-2019 and add below code
            If ans = True Then


                '    DataDr.Close()
                '    ''''select um.emp_staffid,um.user_initial,um.emp_mailid,um.flag,mu2.menu2_code,mu2.flag,mm2.prog_id,mm2.menu2_name from user_master as um inner join menu2_user as mu2 on um.emp_staffid=mu2.emp_staffid inner join  menu2_master as mm2 on mu2.menu2_code=mm2.menu2_code where um.emp_staffid='97A0416' and mu2.flag='1' and mm2.prog_id like('%Hamal%') 

                '    Call GetDataByDr("select mm2.prog_id from user_master as um inner join menu2_user as mu2 on um.emp_staffid=mu2.emp_staffid inner join  menu2_master as mm2 on mu2.menu2_code=mm2.menu2_code where um.emp_staffid='" & UserName.ToString & "' and mu2.flag='1' and mm2.prog_id like('%Hamal%')")

                '    If DataDr.HasRows Then

                '        Dim prog_id_Dtl As String = ""
                '        Dim Finalprog_id_Dtl As String = ""

                '        While DataDr.Read
                '            prog_id_Dtl = DataDr("prog_id")
                '            Finalprog_id_Dtl = prog_id_Dtl + "~" + Finalprog_id_Dtl
                '        End While
                '        Session("Finalprog_id_Dtl") = Finalprog_id_Dtl

                '    End If



                ''_______new code is added 22-10-2010 for maintaning login-logout datetime here
                ''DataDr.Close()
                ''Dim RAffected As Integer = 0
                ''RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_Track(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session.SessionID & "')")
                ''_________code is end here 22-10-2010


                ''_______new code is added 11-11-2019 for maintaning login-logout datetime here Added this on 11-11-2019 with the value of CSRFUserKey
                ''DataDr.Close()
                Dim RAffected As Integer = 0
                ''Field Add to Get the CSRF validation State Session Code added this on 11-11-2019
                Dim SID As String = Session.SessionID
                Session("SID") = SID

                ''RAffected = SaveCmplntData("Insert Into CCF_User_Activity_TrackBackUp111119(UserName,UserLocn,UserRole,UserRptId,UserRptToName,AccessDateTime,SessionID,CSRFUserKey) Values ('" & Session("EmpDetails") & "','" & Session("Locn") & "','" & Session("Role") & "','" & Session("UserRptId") & "','" & Session("RptToName") & "' ,getdate(),'" & SID & "','" & Session("CSRFUKSID") + "" + captchaText & "')")

                '' Session Fixation / Improper session termination Issue related data added in code as suggested by Mam on 23-12-2019
                Dim guid As String = System.Guid.NewGuid.ToString ''guid.NewGuid.ToString
                Session("AuthToken") = guid
                Response.Cookies.Add(New HttpCookie("AuthToken", guid))
                '' Session Fixation Issue related data added in code as suggested by Mam on 23-12-2019

                ''To get the page details used this and this is for Audit trail issue detail point added this on 01-12-2019
                ModuleName = Page.GetType.Name.Split("_")("0")
                RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & SID & "','" & antiforgery.Value & "','" & antiforgery.Value & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                ''To get the page details used this and this is for Audit trail issue detail point added this on 01-12-2019

                ''RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_Track(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & SID & "','" & antiforgery.Value & "','" & antiforgery.Value & "')")
                RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_Track(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,AuthToken,AuthTokenDtl) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & SID & "','" & antiforgery.Value & "','" & antiforgery.Value & "','" & Session("AuthToken") & "','" & Session("AuthToken") & "')")


                ''_________code is end here 11-11-2019
                ''---Working fine but for testing i will commented it 03-12-2019
                '' ''New code is added on 12-11-2019 for making Session value secured

                Session("CSRFUKSIDNew") = antiforgery.Value

                '' ''Session("CSRFUKSIDNew") = Decrypt(antiforgery.Value)
                '' ''New code is ended here added on 12-11-2019
                ''---Working fine but for testing i will commented it 03-12-2019

            End If



            'Return response ''Commented this on 27-12-2019 and add below code
            ''Return ans  ''COmmented this on 21-05-2020 and added this in before end function

        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
            Response.Write("<script language='javascript'>window.alert('Invalid Values');window.location='Login.aspx';</script>")
            ans = False  ''THis is added on 21-05-2020 as compaire code with Helpdesk application in that Improper Session termination Isuse is not occured
        Finally
            If con.State = Data.ConnectionState.Open Then con.Close()
        End Try

        Return ans  ''THis is added on 21-05-2020 as compaire code with Helpdesk application in that Improper Session termination Isuse is not occured

    End Function

    
End Class
