Imports System.IO
Imports System.Text.RegularExpressions
Partial Class rptAuditTrail
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Application_PreSendRequestHeaders() '''' This Should Add on every Page and this is added for server banner point 25-12-2019

            ''Qry to GIS Report Input For CSRF / Request Flooding point validation page wise added this on 27-11-2019
            If CSRFUK = Session("CSRFUKSIDNew") And AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) Then   ''This is for else actule live usage   '' Added And obj.AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) in  if on 23-12-2019 ''''Session("AuthToken").ToString.Equals(Request.Cookies("AuthToken").Value) 


                ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019
                ModuleName = Page.GetType.Name.Split("_")("0")
                RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019

                If Session("Role").ToString = "Admin" Then   ''Role wise page Autherity option Added this Code on 08-11-2019


                    Dim cbReference As String
                    cbReference = Page.ClientScript.GetCallbackEventReference(Me, "arg", "ReceiveServerData", "context")
                    Dim callbackScript As String = ""
                    callbackScript &= "function CallServer(arg, context) { " & cbReference & "} ;"
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", callbackScript, True)


                    ''To get the page details used this and this is for Audit trail issue detail point added this on 01-12-2019
                    ModuleName = Page.GetType.Name.Split("_")("0")
                    RAffected = SaveCmplntData("Insert Into CCF_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("EmpDetails") & "','" & Session("Locn") & "','" & Session("Role") & "','" & Session("UserRptId") & "','" & Session("RptToName") & "' ,getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                    ''To get the page details used this and this is for Audit trail issue detail point added this on 01-12-2019


                    'If Not IsPostBack Then
                    '    Dim RptId() As String
                    '    Dim Data = Right(Session("EmpDetails"), 8)
                    '    RptId = Split(Data, ")")
                    '    'MsgBox(RptId(0))
                    '    GetDataByDr("select distinct (cam.Groupname) from custcomplaintautomail as cam inner join ccfmailschedule as cms on cam.groupname=cms.crop_unit where cam.empstaffid='" & RptId(0) & "'")
                    '    Dim Groupname As String = Nothing
                    '    If DataDr.HasRows Then
                    '        While DataDr.Read
                    '            Groupname = DataDr(0)
                    '        End While
                    '        DataDr.Close()
                    '        DataDr = Nothing
                    '        con.Close()

                    '        HiddenField1.Value = Groupname
                    '    End If
                    'End If

                Else ''Role wise page Autherity option Added this Code on 08-11-2019 and below line of code
                    ''MsgBox("You do not have the rights to access this Page / Form")
                    ''Response.Redirect("MenuForm.aspx")
                    ''Server.Transfer("MenuForm.aspx")
                    ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", "window.onload = function(){ alert('You do not have the rights to access this Page / Form');window.location='MenuPage.aspx'; }", True)
                End If  ''Role wise page Autherity option Added this Code on 08-11-2019


            Else ''Role wise page Autherity option Added this Code on 25-11-2019 and below line of code            
                Response.Write("<script language='javascript'>window.alert('Be careful.... You do not have the rights to access this Page / Form');window.location='UserError.aspx';</script>")
            End If ''This line of code is added on 27-11-2019 for CSRF / Request Flooding point validation

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
        Try

            If eventArgument Is Nothing Then
                returnValue.Append("-1")
            Else
                Dim s = eventArgument.Split("#")

                Select Case s(0).ToString


                    Case "Fifth"
                        Dim v = s(1).ToString.Split("|")
                        Dim qry As String = String.Empty
                        'qry = "select b.user_code,upper(a.user_name) as User_Name,upper(isnull(c.divn_name,'Not Entered')) as Depo,sum(case when datediff(a.ss,access_date_time,a.logout_date_time)>0 then 1 else 1 end) as TotalLogins,sum(case when datediff(ss,a.access_date_time,a.logout_date_time)>0 then 1 else 0 end) as ProperLogout, sum(case when datediff(ss,a.access_date_time,a.logout_date_time)=0 then 1 else 0 end) as NonProperLogout,convert(varchar(8),dateadd(ss,sum(datediff(ss,a.access_date_time,a.logout_date_time)),0),14) as TotalTime, count(distinct( convert(varchar,access_date_time,102))) as NoOfDays from user_activity a inner join user_master b on a.user_name=b.user_name left outer join divn_master c on b.div_code=c.divn_code where access_date_time>=" & s(0).ToString & " and access_date_time<=dateadd(day,1,convert(datetime," & s(1).ToString & ",102)) group by a.user_name,b.user_code,c.divn_name order by c.divn_name"
                        qry = "Exec STP_GISHamaliUserActivityAuditTrail " & v(0).ToString & "," & v(1).ToString & "" ''," & v(2).ToString & "
                        Call GetDataByDr(qry)
                        If DataDr.HasRows Then
                            grdAuditTrail.DataSource = DataDr
                            grdAuditTrail.DataBind()
                        End If
                        DataDr.Close()
                        DataDr = Nothing
                        con.Close()

                        For Each r As GridViewRow In grdAuditTrail.Rows
                            If Val(r.Cells(5).Text) <> 0 Then
                                r.Cells(5).BackColor = Drawing.Color.Pink
                            End If
                        Next


                        '***This code is add on date 07-06-2010 for give the tooltip for gridview header column name to field
                        For Each Title As GridViewRow In grdAuditTrail.Rows
                            Title.Cells(0).Attributes.Add("Title", "SrNo")
                            Title.Cells(1).Attributes.Add("Title", "User ID")
                            Title.Cells(2).Attributes.Add("Title", "IP Address")
                            Title.Cells(3).Attributes.Add("Title", "Login Date and Time")
                            Title.Cells(4).Attributes.Add("Title", "Login Status")
                            Title.Cells(5).Attributes.Add("Title", "Log Out Date and Time using logout option")
                            Title.Cells(6).Attributes.Add("Title", "Module Name")
                            Title.Cells(7).Attributes.Add("Title", "Action Date")

                        Next
                        ''*****code s end here

                        Dim sw1 As New StringWriter
                        Dim htw1 As New HtmlTextWriter(sw1)
                        grdAuditTrail.RenderControl(htw1)
                        returnValue.Append("Fifth|")
                        returnValue.Append(sw1.ToString())

                End Select
            End If

        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
            Response.Write("<script language='javascript'>window.alert('Invalid Values');window.location='MenuPage.aspx';</script>")
        Finally
            If con.State = Data.ConnectionState.Open Then con.Close()
        End Try

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
        CSRFUK = GetMySQLAValue("select top 1 CSRFUserKey from CCF_User_Activity_Track where offline='No' and SessionID='" & Session("SID") & "' and UserName='" & Session("EmpDetails") & "' and CSRFUserKey IS NOT NULL and LogOutDateTime IS NULL order by AccessDateTime desc")
        ''Session Fixation issue code added on 26-12-2019 is as below 
        AuthTokennDtl = GetMySQLAValue("select top 1 AuthToken from Hamali_User_Activity_Track where offline='No' and SessionID='" & Session("SID") & "' and User_staffid='" & Session("staffid") & "' and CSRFUserKey IS NOT NULL and LogOutDateTime IS NULL order by AccessDateTime desc")
        ''Session Fixation issue code added on 26-12-2019 is as below 
    End Sub
    '''' Qry is ended here added for VSUK added this on 11-11-2019
End Class
