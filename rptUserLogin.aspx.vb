Imports System.IO
Imports System.Text.RegularExpressions
Partial Class rptUserLogin
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    Protected returnValue As New StringBuilder
    Dim NoLog As Decimal = 0
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

            ''Qry to redirect the user if he not have the rights / CSRF validation page wise added this on 12-11-2019
            If CSRFUK = Session("CSRFUKSIDNew") And AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) Then   ''This is for else actule live usage   '' Added And obj.AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) in  if on 23-12-2019 ''''Session("AuthToken").ToString.Equals(Request.Cookies("AuthToken").Value) 
                ''If CSRFUK = Session("CSRFUKSID") Then ''This is for else testing usage

                If Session("Role").ToString = "HeadOfficer" Then   ''Role wise page Autherity option Added this Code on 08-11-2019

                    If Not IsPostBack Then




                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019
                        ModuleName = Page.GetType.Name.Split("_")("0")
                        RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019

                        GetDataByQUERYAll("select distinct emp_plantcode,emp_plantnm from hamali_user_master order by emp_plantnm ", cmbPlantDetail, "plant_name", "plant_code")
                    End If


                    Dim cbReference As String
                    cbReference = Page.ClientScript.GetCallbackEventReference(Me, "arg", "ReceiveServerData", "context")
                    Dim callbackScript As String = ""
                    callbackScript &= "function CallServer(arg, context) { " & cbReference & "} ;"
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", callbackScript, True)

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
                        qry = "select User_Initial,Emp_PlantNm,Emp_Role,count(user_staffid) as NoOfLogin from Hamali_User_Activity_Track  where accessdatetime>= convert(datetime," & v(0).ToString & ",102) and  accessdatetime<=convert(datetime," & v(1).ToString & ",102)+1  and emp_plantCode like(" & v(2).ToString & ") group by User_Initial,Emp_PlantNm,Emp_Role order by User_Initial"
                        Call GetDataByDr(qry)
                        If DataDr.HasRows Then
                            GridView1.DataSource = DataDr
                            GridView1.DataBind()
                        End If
                        DataDr.Close()
                        DataDr = Nothing
                        con.Close()

                        For Each r As GridViewRow In GridView1.Rows
                            If Val(r.Cells(4).Text) <> 0 Then
                                r.Cells(4).BackColor = Drawing.Color.Pink
                            End If
                        Next


                        '***This code is add on date 07-06-2010 for give the tooltip for gridview header column name to field
                        For Each Title As GridViewRow In GridView1.Rows
                            Title.Cells(0).Attributes.Add("Title", "SrNo")
                            Title.Cells(1).Attributes.Add("Title", "User_Initial")
                            Title.Cells(2).Attributes.Add("Title", "Emp_PlantNm")
                            Title.Cells(3).Attributes.Add("Title", "Emp_Role")
                            Title.Cells(4).Attributes.Add("Title", "NoOfLogin")

                        Next
                        ''*****code s end here

                        Dim sw1 As New StringWriter
                        Dim htw1 As New HtmlTextWriter(sw1)
                        GridView1.RenderControl(htw1)
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

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' add the UnitPrice and QuantityTotal to the running total variables
            NoLog += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfLogin"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Totals:"

            e.Row.Cells(4).Text = NoLog.ToString("")

            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Left

            e.Row.Font.Bold = True
        End If
    End Sub
End Class
