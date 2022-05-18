Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Partial Class rptHamaliCount
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

                If Session("Role").ToString = "PlantOfficer"  Or Session("Role").ToString = "HeadOfficer" Then   ''Role wise page Autherity option Added this Code on 08-11-2019
                    HiddenFieldPC.Value = Session("plantCode")
                    HiddenFieldPlantName.Value = Session("plantName")

                    If Not IsPostBack Then



                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019
                        ModuleName = Page.GetType.Name.Split("_")("0")
                        RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019

                        If Session("Role").ToString = "PlantOfficer" Then
                            GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') and emp_staffid='" & Session("staffid").ToString & "' order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")
                        ElseIf Session("Role").ToString = "HeadOfficer" Then
                            GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")
                        ElseIf Session("Role").ToString = "Admin" Then
                            GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")
                        End If



                        ''GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master order by activity", cmbA, "activity", "activity_id")
                        ''''GetDataByQUERY("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='" & Session("plantCode") & "' order by Contractor_Code", cmbContractor, "Contractor_Name", "Contractor_code")

                    End If


                    cmbPlant.Attributes.Add("OnChange", "FillCombo(this)") ''This is for Contractors




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


                ''''This is for Supervisor combo "cmbSupervisor" filling on cmbP onchang event
                If Session("Role").ToString = "HeadOfficer" Or Session("Role").ToString = "Admin" Or Session("Role").ToString = "PlantOfficer" Then

                    If s(0).ToString.StartsWith("cmbPlant") Then


                        If s(1).ToString = "1156" Then
                            Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1156' order by Contractor_Code", "")
                            returnValue.Append("cmbContractor" + "|")

                        ElseIf s(1).ToString = "1179" Then
                            Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1179' order by Contractor_Code", "")
                            returnValue.Append("cmbContractor" + "|")

                        ElseIf s(1).ToString = "1181" Then
                            Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1181' order by Contractor_Code", "")
                            returnValue.Append("cmbContractor" + "|")

                        ElseIf s(1).ToString = "1107" Then
                            Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1107' order by Contractor_Code", "")
                            returnValue.Append("cmbContractor" + "|")

                        ElseIf s(1).ToString = "1108" Then
                            Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1108' order by Contractor_Code", "")
                            returnValue.Append("cmbContractor" + "|")

                        ElseIf s(1).ToString = "1180" Then
                            Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1180' order by Contractor_Code", "")
                            returnValue.Append("cmbContractor" + "|")

                        ElseIf s(1).ToString = "1157" Then
                            Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1157' order by Contractor_Code", "")
                            returnValue.Append("cmbContractor" + "|")

                        End If


                        If DataDr.HasRows Then
                            Dim cnt As Integer = 0
                            'returnValue.Append("0,Select,")

                            While DataDr.Read()
                                returnValue.Append((CType(DataDr(0), String) & "," & CType(DataDr(1), String)) & ",")
                            End While
                            returnValue.Remove((Len(returnValue.ToString()) - 1), 1)


                        End If

                        DataDr.Close()
                        con.Close()

                    End If
                End If

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


                        ''''qry = "Exec HamaliBilling " & v(0).ToString & "," & v(1).ToString & "," & v(2).ToString & "," & v(3).ToString & "," & v(4).ToString & ""


                        'qry = "select hc.hamal_date as CountDate,hcm.plant_name as PlantName,hcm.contractor_name as ContractorName,sum(hc.hamal_count) as Counts from Hamali_Contractor_Master as hcm inner join Hamal_Count as hc on hc.hamal_contractor=hcm.contractor_code and hc.hamal_plant=hcm.plant_code  where hc.hamal_date between convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and convert(smalldatetime,convert(varchar(10)," & Todate & ",102)) and hc.hamal_plant=" & AllStrDtl(2) & " and hc.hamal_contractor=" & AllStrDtl(3) & "  group by hc.hamal_date,hcm.plant_name,hcm.contractor_name order by hcm.plant_name"
                        qry = "select convert(varchar(10),hc.hamal_date,105) as CountDate,hcm.plant_name as PlantName,hcm.contractor_name as ContractorName,sum(hc.hamal_count) as Counts from Hamali_Contractor_Master as hcm inner join Hamal_Count as hc on hc.hamal_contractor=hcm.contractor_code and hc.hamal_plant=hcm.plant_code  where hc.hamal_date between " & FromDate & " and " & Todate & " and hc.hamal_plant=" & AllStrDtl(2) & " and hc.hamal_contractor=" & AllStrDtl(3) & "  group by hc.hamal_date,hcm.plant_name,hcm.contractor_name order by hcm.plant_name"

                        Call GetDataByDr(qry)
                        If DataDr.HasRows Then
                            GridView1.DataSource = DataDr
                            GridView1.DataBind()
                        End If

                        DataDr.Close()
                        DataDr = Nothing
                        con.Close()

                        'For Each r As GridViewRow In GridView1.Rows
                        '    If Val(r.Cells(4).Text) <> "" Then
                        '        r.Cells(4).BackColor = Drawing.Color.Pink
                        '    End If
                        'Next

                        For Each Title As GridViewRow In GridView1.Rows
                            Title.Cells(0).Attributes.Add("Title", "SrNo")
                            Title.Cells(1).Attributes.Add("Title", "CountDate")
                            Title.Cells(2).Attributes.Add("Title", "PlantName")
                            Title.Cells(3).Attributes.Add("Title", "ContractorName")
                            Title.Cells(4).Attributes.Add("Title", "Counts")

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

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' add the UnitPrice and QuantityTotal to the running total variables
            'BagWight += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bag_Weight"))
            'Number += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Number"))
            'Rate += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Rate"))
            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Counts"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Totals:"
            ' for the Footer, display the running totals
            'e.Row.Cells(4).Text = BagWight.ToString("")
            'e.Row.Cells(5).Text = Number.ToString("")
            'e.Row.Cells(6).Text = Rate.ToString("")
            e.Row.Cells(4).Text = Amount.ToString("")


            'e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            'e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
            'e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center

            e.Row.Font.Bold = True
        End If


    End Sub
End Class
