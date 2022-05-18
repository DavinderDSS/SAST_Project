Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Partial Class rptHamaliApproveDisapprove
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    Protected returnValue As New StringBuilder
    Dim BagWight As Decimal = 0
    Dim Number As Decimal = 0
    Dim Rate As Decimal = 0
    Dim Amount As Decimal = 0
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
                    HiddenFieldPC.Value = Session("plantCode")
                    HiddenFieldPlantName.Value = Session("plantName")

                    If Not IsPostBack Then



                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019
                        ModuleName = Page.GetType.Name.Split("_")("0")
                        RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019

                        GetDataByQUERYApp("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")

                        GetDataByQUERYAppDisapp("select distinct Approval from Hamal_trans order by Approval ", cmbStatus, "Approval", "Approval")

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
            Dim RAffected As Integer = 0
            If eventArgument Is Nothing Then
                returnValue.Append("-1")
            Else
                Dim s = eventArgument.Split("#")

                Dim Val As String = s(0).ToString
                Val = Val.Remove(0, 4)

                Select Case s(0).ToString

                    Case "Fifth"
                        Dim v = s(1).ToString.Split("|")
                        Dim qry As String = String.Empty

                        Dim AllDtl As String = s(1).ToString
                        ''AllDtl = Mid(AllDtl, 4)

                        Dim AllStrDtl() = AllDtl.Split("|")

                        Dim FromDate = AllStrDtl(0).ToString
                        Dim Todate = AllStrDtl(1).ToString

                        Dim Result As String = String.Empty


                        qry = "select distinct ht.rowid as EntryId,hum.emp_PlantNm as PlantName,hum.user_initial as SupervisorName,hcm.contractor_Name as ContractorName,convert(varchar(10),ht.tran_date,105) as Tran_Date,ham.activity as Activity,hpm.purpose as Purpose,ht.Approval as Approval from hamali_user_master as hum inner join Hamal_trans as ht on hum.emp_staffid=ht.supervisor_code inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_Purpose_Master as hpm on ht.purpose_id=hpm.purpose_id where ht.tran_date between " & FromDate & " and " & Todate & " and ht.plantcode=" & v(2).ToString & " and ht.approval like(" & v(3).ToString & ") order by ht.approval "

                        Call GetDataByDr(qry)
                        If DataDr.HasRows Then
                            GridView1.DataSource = DataDr
                            GridView1.DataBind()
                        End If

                        DataDr.Close()
                        DataDr = Nothing
                        con.Close()

                        For Each Title As GridViewRow In GridView1.Rows
                            Title.Cells(0).Attributes.Add("Title", "SrNo")
                            Title.Cells(1).Attributes.Add("Title", "EntryId")
                            Title.Cells(2).Attributes.Add("Title", "PlantName")
                            Title.Cells(3).Attributes.Add("Title", "SupervisorName")
                            Title.Cells(4).Attributes.Add("Title", "ContractorName")
                            Title.Cells(5).Attributes.Add("Title", "Date")
                            Title.Cells(6).Attributes.Add("Title", "Activity")
                            Title.Cells(7).Attributes.Add("Title", "Purpose")
                            Title.Cells(8).Attributes.Add("Title", "Approval")
                        Next

                        '*****code s end here

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
End Class
