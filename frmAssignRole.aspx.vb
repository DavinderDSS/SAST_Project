Imports System.IO
Partial Class frmAssignRole
    Inherits System.Web.UI.Page
    Implements ICallbackEventHandler
    Protected returnValue As New StringBuilder
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
    '''' This Should Add on every Page and this is added for server banner point 25-12-2019

    ''---------------Befor Page Load
    '''' This is for getting the value of VSUK added this on 11-11-2019
    Protected Overrides Sub OnInit(ByVal e As EventArgs)

        MyBase.OnInit(e)
        ''''CSRFUserKey = ViewStateUserKey
        ''Call GetDataByDr("select top 1 CSRFUserKey from CCF_User_Activity_TrackBackUp111119 where offline='No' and SessionID='" & Session("SID") & "' and User_staffid='" & Session("staffid") & "' and CSRFUserKey IS NOT NULL and LogOutDateTime IS NULL order by AccessDateTime desc")
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
    '''' Qry is ended here added for VSUK added this on 11-11-2019
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Application_PreSendRequestHeaders() '''' This Should Add on every Page and this is added for server banner point 25-12-2019


            ''This is used to deactivate the browser back button on respective page added this on 13-05-2020
            ClientScript.RegisterClientScriptBlock(Page.GetType, "NOBACK", "<script>if(history.length>0)history.go(+1);</script>")
            ''Code is ended here added on 13-05-2020


            ''Qry to redirect the user if he not have the rights / CSRF validation page wise added this on 12-11-2019
            If CSRFUK = Session("CSRFUKSIDNew") And AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) Then   ''This is for else actule live usage   '' Added And obj.AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) in  if on 23-12-2019 ''''Session("AuthToken").ToString.Equals(Request.Cookies("AuthToken").Value)  
                ''If CSRFUK = Session("CSRFUKSID") Then ''This is for else testing usage

                If Session("Role").ToString = "HeadOfficer" Or Session("Role").ToString = "Admin" Then   ''Role wise page Autherity option Added this Code on 08-11-2019
                    Dim cbReference As String
                    cbReference = Page.ClientScript.GetCallbackEventReference(Me, "arg", "ReceiveServerData", "context")
                    Dim callbackScript As String = ""
                    callbackScript &= "function CallServer(arg, context) { " & cbReference & "} ;"
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", callbackScript, True)

                    If Not IsPostBack Then

                        ''THis  code is used for Request Flooding but used client side method so commented added on 12-05-2020
                        Dim num = getRandomNumber()
                        hdnRndNo.Value = num
                        Session("RandomNumber") = num
                        ''Code is ended here added on 12-05-2020 for request Flooding

                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019
                        ModuleName = Page.GetType.Name.Split("_")("0")
                        RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019

                        'Call FillCombo("select empstaffid+' | '+empname as Emp, empstaffid from usermaster where empstatus='active' order by empstaffid", "emp", "empstaffid", cmbEmpCode)
                    End If
                    cmbEmpCode.Attributes.Add("OnChange", "DisplayEmpDetails()")
                    'cmbReportingTo.Attributes.Add("OnChange", "DisplayMailID()")
                    txtSearch.Attributes.Add("onkeydown", "CheckEnter(event)")
                    'cmbGroup.Attributes.Add("OnChange", "CurrentDate()")
                Else ''Role wise page Autherity option Added this Code on 08-11-2019 and below line of code                    
                    ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", "window.onload = function(){ alert('You do not have the rights to access this Page / Form');window.location='MenuPage.aspx'; }", True)
                End If  ''Role wise page Autherity option Added this Code on 08-11-2019

            Else ''Role wise page Autherity option Added this Code on 12-11-2019 and below line of code

                Response.Write("<script language='javascript'>window.alert('Be careful.... You do not have the rights to access this Page / Form');window.location='MenuPage.aspx';</script>")

            End If ''This line of code is added on 12-11-2019 for CSRF validation

        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
            Response.Write("<script language='javascript'>window.alert('Invalid Values');window.location='MenuPage.aspx';</script>")
        Finally
            If con.State = Data.ConnectionState.Open Then con.Close()
        End Try

    End Sub
    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return returnValue.ToString
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent

        If eventArgument Is Nothing Then

            returnValue.Append("-1")

        Else
            Try

                Dim s() As String
                s = eventArgument.Split("#")

                Select Case s(0).ToString
                    Case "cmbEmpCode"

                        '''''''''Call GetDataByDr("select a.empname,a.empmailid,a.emprptid,b.empname,b.empmailid,a.EmpCmplntLevel,c.GroupName,a.Emplocn from usermaster a inner join usermaster b on a.emprptid = b.empstaffid inner join custcomplaintautomail c on a.empstaffid=c.empstaffid where a.empstaffid='" & s(1).ToString() & "'")
                        ''Call GetDataByDr("select a.empname,a.empmailid,a.emprptid,b.empname,b.empmailid,a.EmpCmplntLevel,c.GroupName,a.Emplocn from usermaster a inner join usermaster b on a.emprptid = b.empstaffid left outer join custcomplaintautomail c on a.empstaffid=c.empstaffid where a.empstaffid='" & s(1).ToString() & "'")

                        Call GetDataByDr("select user_initial,emp_mailid,emp_fname,emp_lname,emp_role,emp_plantnm from hamali_user_master where emp_staffid='" & s(1).ToString() & "'")

                        If DataDr.HasRows Then
                            Dim mailid As String = ""
                            Dim mailid1 As String = ""

                            'Dim L1MailId As String = ""
                            'Dim L1MailId1 As String = ""

                            returnValue.Append("cmbEmpCode|")
                            While DataDr.Read

                                mailid = DataDr(1)
                                If InStr(mailid, "/") = 0 Then
                                    mailid1 = mailid.ToString()
                                Else
                                    mailid1 = Left(mailid, mailid.ToString.IndexOf("/")).Replace(" ", ".") & "@mahyco.com"
                                End If

                                'L1MailId = DataDr(4)
                                'If InStr(L1MailId, "/") = 0 Then
                                '    L1MailId1 = L1MailId.ToString
                                'Else
                                '    L1MailId1 = Left(L1MailId, L1MailId.ToString.IndexOf("/")).Replace(" ", ".") & "@mahyco.com"
                                'End If

                                returnValue.Append(DataDr(0) & "~" & mailid1 & "~" & DataDr(2) & "~" & DataDr(3) & "~" & DataDr(4) & "~" & DataDr(5))
                            End While
                        End If
                        DataDr.Close()
                        DataDr = Nothing
                        con.Close()
                        Exit Sub
                    Case "cmbReportingTo"
                        Call GetDataByDr("select emp_staffid, emp_staffid+' - '+user_initial as Emp  from hamali_user_master where empstatus='active' order by empstaffid")
                        If DataDr.HasRows Then
                            returnValue.Append("cmbReportingTo|")
                            While DataDr.Read
                                returnValue.Append(DataDr(0) & "~" & DataDr(1) & "~")
                            End While
                            returnValue.Remove((Len(returnValue.ToString()) - 1), 1)
                        End If
                        DataDr.Close()
                        DataDr = Nothing
                        con.Close()
                        'Exit Sub
                    Case "DisplayMailID"

                        Call GetDataByDr("select empmailid from hamali_user_master where emp_staffid='" & s(1).ToString() & "'")
                        If DataDr.HasRows Then
                            Dim mailid As String = ""
                            Dim mailid1 As String = ""
                            returnValue.Append("DisplayMailID|")
                            While DataDr.Read
                                'returnValue.Append(IIf(InStr(DataDr(0), "/") > 0, Left(DataDr(0), DataDr(0).ToString.IndexOf("/")).Replace(" ", ".") & "@mahyco.com", DataDr(0).ToString))
                                mailid = DataDr(0)
                                If InStr(mailid, "/") = 0 Then
                                    mailid1 = mailid.ToString()
                                Else
                                    mailid1 = Left(mailid, mailid.ToString.IndexOf("/")).Replace(" ", ".") & "@mahyco.com"
                                End If
                                returnValue.Append(mailid1)
                            End While
                        End If
                        DataDr.Close()
                        DataDr = Nothing
                        con.Close()
                        Exit Sub
                    Case "SearchedData"
                        Call GetDataByDr("select emp_staffid,emp_staffid+' - '+User_initial as Emp  from Hamali_user_master where user_initial like '%" & s(1).ToString & "%' and empstatus='Active' order by emp_staffid")
                        returnValue.Append("SearchedData|")
                        If DataDr.HasRows Then
                            While DataDr.Read
                                returnValue.Append(DataDr(0) & "~" & DataDr(1) & "~")
                            End While
                            returnValue.Remove((Len(returnValue.ToString()) - 1), 1)
                        End If
                        DataDr.Close()
                        DataDr = Nothing
                        con.Close()
                        Exit Sub

                    Case "SaveData"

                        If hdnRndNo.Value = Session("RandomNumber") Then ''// code is added on 12-05-2020 For Request Flooding Issue Start here using existing code too
                            Dim RAffected As Integer = 0

                            RAffected = SaveCmplntData(s(1).ToString())
                            'MsgBox(s(1).ToString())
                            If RAffected > 0 Then
                                returnValue.Append("SaveData|" & RAffected & " Record Updated Successfully")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            Else
                                returnValue.Append("SaveData|Record Not Updated")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            End If
                        Else ''This else is for Request Flooding Revalidation point else added this on 12-05-2020
                            returnValue.Append("SaveData|Threr is some issue , Kindly Update again.")
                            Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                        End If ''This  is end of if added on 12-05-2020

                End Select
            Catch ex As Exception
                returnValue.Remove(0, returnValue.Length)
                returnValue.Append("Error|Runtime Error,please contact programmer." & Chr(13) & "Error Is :: " & ex.Message)
                If con.State = Data.ConnectionState.Open Then
                    If DataDr.IsClosed Then
                    Else
                        DataDr.Close()
                        DataDr = Nothing
                    End If

                    con.Close()
                End If

            End Try

        End If

    End Sub
End Class
