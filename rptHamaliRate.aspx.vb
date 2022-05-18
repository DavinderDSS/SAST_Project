Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Partial Class rptHamaliRate
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

                If Session("Role").ToString = "HeadOfficer" Or Session("Role").ToString = "Admin" Then   ''Role wise page Autherity option Added this Code on 08-11-2019
                    HiddenFieldPC.Value = Session("plantCode")
                    HiddenFieldPlantName.Value = Session("plantName")

                    If Not IsPostBack Then



                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019
                        ModuleName = Page.GetType.Name.Split("_")("0")
                        RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019

                        GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")

                        ''GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master order by activity", cmbA, "activity", "activity_id")

                        ''''GetDataByQUERY("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='" & Session("plantCode") & "' order by Contractor_Code", cmbContractor, "Contractor_Name", "Contractor_code")

                        cmbPlant.Attributes.Add("OnChange", "FillCombo(this);FillComboActivity(this)") ''This is for Contractors
                        cmbA.Attributes.Add("OnChange", "FillComboDist(this)")              ''This is for Distance


                        gvBilling.DataSource = Nothing
                        gvBilling.DataBind()
                        gvBilling.Visible = False


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

                ''''This is for contractor combo "cmbContractor" filling on cmbP onchang event

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



                    '' following code is needed to send the fetched data from above one of the case to the javascript
                    'If s(1).ToString <> "store" Then
                    'End If

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

                ''''This is for activity combo "cmbA" filling on cmbP onchang event add on 17-1-2013
                If s(0).ToString.StartsWith("cmbPlant") Then
                    ''MsgBox("i am in cmbA")

                    If s(1).ToString = "1156" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003','ha007','ha008') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + "|")

                    ElseIf s(1).ToString = "1179" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003','ha006') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + "|")

                    ElseIf s(1).ToString = "1181" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + "|")


                    ElseIf s(1).ToString = "1107" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + "|")

                    ElseIf s(1).ToString = "1108" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + "|")

                    ElseIf s(1).ToString = "1180" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + "|")


                    ElseIf s(1).ToString = "1157" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003','ha004','ha005') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + "|")


                    End If



                    '' following code is needed to send the fetched data from above one of the case to the javascript
                    'If s(1).ToString <> "store" Then
                    'End If

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
                ''''This is for activity combo "cmbA" filling on cmbP onchang event add on 17-1-2013 end here


                ''This is for Dist combo "cmbDist" filling on cmbA onchang event add on 25-3-2013
                If s(0).ToString.StartsWith("cmbA") Then
                    Call ShowData("select distinct hdln.range,hdln.description from Hamal_DL_Norms as hdln inner join Hamal_Rate_Master as hrm on hrm.distance=hdln.range where hrm.plantcode='" & s(1).ToString & "' ", "")
                    returnValue.Append("cmbDist" + "|")

                    '' following code is needed to send the fetched data from above one of the case to the javascript
                    'If s(1).ToString <> "store" Then
                    'End If

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

                ''This is for Dist combo "cmbDist" filling on cmbA onchang event add on 25-3-2013 end here

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


                        ''qry = "Exec HamaliBilling " & v(0).ToString & "," & v(1).ToString & "," & v(2).ToString & "," & v(3).ToString & "," & v(4).ToString & ""
                        'If AllStrDtl(2).ToString = "'1157'" Then

                        '    qry = "select hrm.FromDateRang,hrm.ToDateRang,hcm.Plant_Name,hcm.Contractor_Name,ham.Activity,hdln.Description as Distance,Weight,hrm.Loading,hrm.Unloading,hrm.Sorting,hrm.Stiching,hrm.Stacking,hrm.Restacking,hrm.Weighing,hrm.Bundle_Preparation,hrm.Opening_Feeding,hrm.Loading_Unloading,hrm.Unload_Stack,hrm.Unp_Recd_Unl_Wt_Stk,hrm.Unp_Recd_Unl_Srt_Wt_Stk,hrm.Dh_To_Ac_Stk,hrm.Ac_To_Dh_Stk,hrm.Loading_Dh,hrm.UnLoading_Dh,hrm.Varai from hamal_rate_master as hrm inner join Hamal_DL_Norms as hdln on hdln.range=hrm.distance inner join Hamal_Activity_Master as ham on hrm.activity=ham.activity_id inner join Hamali_Contractor_master as hcm on hrm.contractor_code=hcm.contractor_code and hrm.plantcode=hcm.plant_code where (convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) between hrm.FromDateRang and hrm.ToDateRang or convert(smalldatetime,convert(varchar(10)," & Todate & ",102)) between hrm.FromDateRang and hrm.ToDateRang or hrm.FromDateRang between convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and convert(smalldatetime,convert(varchar(10)," & Todate & ",102)) or hrm.ToDateRang between convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and convert(smalldatetime,convert(varchar(10)," & Todate & ",102))) and hrm.plantCode=" & AllStrDtl(2) & " and hrm.contractor_code=" & AllStrDtl(3) & " and hrm.activity=" & AllStrDtl(4) & " and hrm.distance=" & AllStrDtl(5) & " and hrm.weight=" & AllStrDtl(6) & " "

                        '    Call GetDataByDr(qry)
                        '    'If DataDr.HasRows Then
                        '    '    GridView1.DataSource = DataDr
                        '    '    GridView1.DataBind()
                        '    'End If

                        '    Dim tbl As New Table

                        '    If DataDr.HasRows Then
                        '        Dim r As Integer = 0
                        '        Dim headerrow As New TableHeaderRow
                        '        While DataDr.Read()

                        '            Dim i As Integer = 0
                        '            For i = 0 To DataDr.FieldCount - 1
                        '                If r = 0 Then ' this is for first iteration
                        '                    Dim td As New TableCell

                        '                    td.Text = DataDr(i)
                        '                    If td.Text = "0" Then
                        '                    Else
                        '                        Dim th As New TableHeaderCell
                        '                        th.Text = DataDr.GetName(i).ToString()

                        '                        headerrow.Cells.Add(th)
                        '                    End If

                        '                Else


                        '                End If

                        '            Next
                        '            r = r + 1
                        '        End While
                        '        tbl.Rows.Add(headerrow)
                        '    End If


                        '    DataDr.Close()
                        '    DataDr = Nothing
                        '    con.Close()


                        '    'For Each Title As GridViewRow In GridView1.Rows
                        '    '    Title.Cells(0).Attributes.Add("Title", "SrNo")
                        '    '    Title.Cells(1).Attributes.Add("Title", "Section")
                        '    '    Title.Cells(2).Attributes.Add("Title", "ActNo")
                        '    '    Title.Cells(3).Attributes.Add("Title", "Operation_Activities")
                        '    '    Title.Cells(4).Attributes.Add("Title", "Bag_Weight")
                        '    '    Title.Cells(5).Attributes.Add("Title", "Distance_Range")
                        '    '    Title.Cells(6).Attributes.Add("Title", "Number")
                        '    '    Title.Cells(7).Attributes.Add("Title", "Rate")
                        '    '    Title.Cells(8).Attributes.Add("Title", "Amount_Rs")
                        '    'Next

                        '    '*****code s end here

                        '    Dim sw1 As New StringWriter
                        '    Dim htw1 As New HtmlTextWriter(sw1)
                        '    tbl.RenderControl(htw1)
                        '    returnValue.Append("Fifth|")
                        '    returnValue.Append(sw1.ToString())

                        'Else

                        qry = "select hrm.FromDateRang,hrm.ToDateRang,hcm.Plant_Name,hcm.Contractor_Name,ham.Activity,hdln.Description as Distance,Weight,hrm.Loading,hrm.Unloading,hrm.Sorting,hrm.Stiching,hrm.Stacking,hrm.Restacking,hrm.Weighing,hrm.Bundle_Preparation,hrm.Opening_Feeding,hrm.Loading_Unloading,hrm.Unload_Stack,hrm.Unp_Recd_Unl_Wt_Stk,hrm.Unp_Recd_Unl_Srt_Wt_Stk,hrm.Dh_To_Ac_Stk,hrm.Ac_To_Dh_Stk,hrm.Loading_Dh,hrm.UnLoading_Dh,hrm.Varai,hrm.Stitch_Stack_Plant_OP_Spc_Dry from hamal_rate_master as hrm inner join Hamal_DL_Norms as hdln on hdln.range=hrm.distance inner join Hamal_Activity_Master as ham on hrm.activity=ham.activity_id inner join Hamali_Contractor_master as hcm on hrm.contractor_code=hcm.contractor_code and hrm.plantcode=hcm.plant_code where (convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) between hrm.FromDateRang and hrm.ToDateRang or convert(smalldatetime,convert(varchar(10)," & Todate & ",102)) between hrm.FromDateRang and hrm.ToDateRang or hrm.FromDateRang between convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and convert(smalldatetime,convert(varchar(10)," & Todate & ",102)) or hrm.ToDateRang between convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and convert(smalldatetime,convert(varchar(10)," & Todate & ",102))) and hrm.plantCode=" & AllStrDtl(2) & " and hrm.contractor_code=" & AllStrDtl(3) & " and hrm.activity=" & AllStrDtl(4) & " and hrm.distance=" & AllStrDtl(5) & " and hrm.weight in(" & AllStrDtl(6) & ")"

                        'qry = "SELECT ht.crop_type as Section,1 as ActNo,'Loading' as Activities,ht.Bag_Weight,ht.loading as Number,hrm.loading as Rate,SUM(ht.loading * hrm.loading) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.loading <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.loading,hrm.loading union all SELECT ht.crop_type as Section,2 as ActNo,'UnLoading' as Activities,ht.Bag_Weight,ht.unloading as Number,hrm.unloading as Rate,SUM(ht.unloading * hrm.unloading) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.unloading <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.unloading,hrm.unloading union all SELECT ht.crop_type as Section,3 as ActNo,'Sorting' as Activities,ht.Bag_Weight,ht.sorting as Number,hrm.sorting as Rate,SUM(ht.sorting * hrm.sorting) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.sorting <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.sorting,hrm.sorting union all SELECT ht.crop_type as Section,4 as ActNo,'Stiching' as Activities,ht.Bag_Weight,ht.stiching as Number,hrm.stiching as Rate,SUM(ht.stiching * hrm.stiching) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.stiching <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.stiching,hrm.stiching union all SELECT ht.crop_type as Section,5 as ActNo,'Stacking' as Activities,ht.Bag_Weight,ht.stacking as Number,hrm.stacking as Rate,SUM(ht.stacking * hrm.stacking) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.stacking <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.stacking,hrm.stacking union all SELECT ht.crop_type as Section,6 as ActNo,'Restacking' as Activities,ht.Bag_Weight,ht.restacking as Number,hrm.restacking as Rate,SUM(ht.restacking * hrm.restacking) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.restacking <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.restacking,hrm.restacking union all SELECT ht.crop_type as Section,7 as ActNo,'Weighing' as Activities,ht.Bag_Weight,ht.weighing as Number,hrm.weighing as Rate,SUM(ht.weighing * hrm.weighing) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.weighing <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.weighing,hrm.weighing union all SELECT ht.crop_type as Section,8 as ActNo,'Bundle_Preparation' as Activities,ht.Bag_Weight,ht.bundle_preparation as Number,hrm.bundle_preparation as Rate,SUM(ht.bundle_preparation * hrm.bundle_preparation) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.bundle_preparation <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.bundle_preparation,hrm.bundle_preparation union all SELECT ht.crop_type as Section,9 as ActNo,'Opening_Feeding' as Activities,ht.Bag_Weight,ht.opening_feeding as Number,hrm.opening_feeding as Rate,SUM(ht.opening_feeding * hrm.opening_feeding) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.opening_feeding <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.opening_feeding,hrm.opening_feeding union all SELECT ht.crop_type as Section,10 as ActNo,'Loading_Unloading' as Activities,ht.Bag_Weight,ht.loading_unloading as Number,hrm.loading_unloading as Rate,SUM(ht.loading_unloading * hrm.loading_unloading) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.loading_unloading <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.loading_unloading,hrm.loading_unloading union all SELECT ht.crop_type as Section,11 as ActNo,'Unp_Recd_Unl_Wt_Stk' as Activities,ht.Bag_Weight,ht.unp_recd_unl_wt_stk as Number,hrm.unp_recd_unl_wt_stk as Rate,SUM(ht.unp_recd_unl_wt_stk * hrm.unp_recd_unl_wt_stk) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.unp_recd_unl_wt_stk <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.unp_recd_unl_wt_stk,hrm.unp_recd_unl_wt_stk union all SELECT ht.crop_type as Section,12 as ActNo,'Unp_Recd_Unl_Srt_Wt_Stk' as Activities,ht.Bag_Weight,ht.unp_recd_unl_srt_wt_stk as Number,hrm.unp_recd_unl_srt_wt_stk as Rate,SUM(ht.unp_recd_unl_srt_wt_stk * hrm.unp_recd_unl_srt_wt_stk) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.unp_recd_unl_srt_wt_stk <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.unp_recd_unl_srt_wt_stk,hrm.unp_recd_unl_srt_wt_stk union all SELECT ht.crop_type as Section,13 as ActNo,'Dh_To_Ac_Stk' as Activities,ht.Bag_Weight,ht.dh_to_ac_stk as Number,hrm.dh_to_ac_stk as Rate,SUM(ht.dh_to_ac_stk * hrm.dh_to_ac_stk) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.dh_to_ac_stk <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.dh_to_ac_stk,hrm.dh_to_ac_stk union all SELECT ht.crop_type as Section,14 as ActNo,'Ac_To_Dh_Stk' as Activities,ht.Bag_Weight,ht.ac_to_dh_stk as Number,hrm.ac_to_dh_stk as Rate,SUM(ht.ac_to_dh_stk * hrm.ac_to_dh_stk) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.ac_to_dh_stk <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.ac_to_dh_stk,hrm.ac_to_dh_stk union all SELECT ht.crop_type as Section,15 as ActNo,'Loading_Dh' as Activities,ht.Bag_Weight,ht.loading_dh as Number,hrm.loading_dh as Rate,SUM(ht.loading_dh * hrm.loading_dh) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.loading_dh <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.loading_dh,hrm.loading_dh union all SELECT ht.crop_type as Section,16 as ActNo,'UnLoading_Dh' as Activities,ht.Bag_Weight,ht.unloading_dh as Number,hrm.unloading_dh as Rate,SUM(ht.unloading_dh * hrm.unloading_dh) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.unloading_dh <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.unloading_dh,hrm.unloading_dh union all SELECT ht.crop_type as Section,17 as ActNo,'Varai' as Activities,ht.Bag_Weight,ht.varai as Number,hrm.varai as Rate,SUM(ht.varai * hrm.varai) AS Amount FROM  Hamal_trans AS ht inner join Hamal_Rate_Master AS hrm on hrm.plantCode = ht.plantCode   and hrm.weight=ht.Bag_Weight and ht.tran_date between hrm.fromdaterang and hrm.todaterang inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code WHERE  ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " and ht.crop_type like(" & v(3).ToString & ") and ht.contractor_code=" & v(4).ToString & "  and ht.approval='approve' and ht.varai <> 0 GROUP BY ht.crop_type,ht.Bag_Weight,hdn.description,ht.varai,hrm.varai order by ht.crop_type"

                        Call GetDataByDr(qry)


                        'If DataDr.HasRows Then
                        '    GridView1.DataSource = DataDr
                        '    GridView1.DataBind()
                        'End If

                        Dim tbl As New Table
                        'tbl.Style.Add("border", "1")
                        'tbl.Style.Add("cellpadding", "6")
                        'tbl.Style.Add("cellspacing", "0")
                        'tbl.Style.Add("border-collapse", "collapse")

                        tbl.CellPadding = "6"
                        tbl.CellSpacing = "2"
                        tbl.BorderWidth = "5"
                        tbl.BackColor = Drawing.Color.Beige
                        'tbl.BorderColor = Drawing.Color.Black
                        'tbl.Attributes.Add("border-collapse", "collapse")
                        'tbl.Style.Add("BORDER-COLLAPSE", "COLLAPSE")
                        'tbl.Style("border-collapse") = "collapse"
                        'tbl.BorderStyle = BorderStyle.Groove

                        If DataDr.HasRows Then
                            Dim r As Integer = 0

                            While DataDr.Read()

                                Dim i As Integer = 0

                                Dim headerrow As New TableHeaderRow

                                Dim tr As New TableRow

                                tr.BorderWidth = "2"
                                tr.Style.Add("border-collapse", "collapse")

                                For i = 0 To DataDr.FieldCount - 1
                                    If r = 0 Then ' this is for first iteration

                                        Dim td As New TableCell

                                        td.BorderWidth = "2"
                                        td.Style.Add("border-collapse", "collapse")

                                        td.Text = DataDr(i)
                                        If td.Text = "0" Then
                                        Else
                                            Dim th As New TableHeaderCell
                                            th.Text = DataDr.GetName(i).ToString()

                                            Dim datatd As New TableCell
                                            datatd.Text = DataDr(i)

                                            tr.Cells.Add(datatd)
                                            headerrow.Cells.Add(th)

                                        End If

                                    Else
                                        Dim td As New TableCell

                                        td.BorderWidth = "2"
                                        td.Style.Add("border-collapse", "collapse")

                                        td.Text = DataDr(i)
                                        If td.Text = "0" Then
                                        Else

                                            Dim datatd As New TableCell
                                            datatd.Text = DataDr(i)

                                            tr.Cells.Add(datatd)

                                        End If


                                    End If


                                Next
                                If r = 0 Then
                                    tbl.Rows.Add(headerrow)
                                End If
                                r = r + 1

                                tbl.Rows.Add(tr)
                            End While

                        End If


                        DataDr.Close()
                        DataDr = Nothing
                        con.Close()


                        'For Each Title As GridViewRow In GridView1.Rows
                        '    Title.Cells(0).Attributes.Add("Title", "SrNo")
                        '    Title.Cells(1).Attributes.Add("Title", "Section")
                        '    Title.Cells(2).Attributes.Add("Title", "ActNo")
                        '    Title.Cells(3).Attributes.Add("Title", "Operation_Activities")
                        '    Title.Cells(4).Attributes.Add("Title", "Bag_Weight")
                        '    'Title.Cells(5).Attributes.Add("Title", "Distance_Range")
                        '    Title.Cells(5).Attributes.Add("Title", "Number")
                        '    Title.Cells(6).Attributes.Add("Title", "Rate")
                        '    Title.Cells(7).Attributes.Add("Title", "Amount_Rs")
                        'Next

                        '*****code s end here

                        Dim sw1 As New StringWriter
                        Dim htw1 As New HtmlTextWriter(sw1)
                        tbl.RenderControl(htw1)
                        returnValue.Append("Fifth|")
                        returnValue.Append(sw1.ToString())


                        ''End If


                End Select
            End If

        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
            Response.Write("<script language='javascript'>window.alert('Invalid Values');window.location='MenuPage.aspx';</script>")
        Finally
            If con.State = Data.ConnectionState.Open Then con.Close()
        End Try

    End Sub
    'Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
    '    If Session("plantCode") = "1157" Then

    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            ' add the UnitPrice and QuantityTotal to the running total variables
    '            'BagWight += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bag_Weight"))
    '            Number += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Number"))
    '            'Rate += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Rate"))
    '            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount_Rs"))

    '        ElseIf e.Row.RowType = DataControlRowType.Footer Then
    '            e.Row.Cells(0).Text = "Totals:"
    '            ' for the Footer, display the running totals
    '            'e.Row.Cells(4).Text = BagWight.ToString("")
    '            e.Row.Cells(6).Text = Number.ToString("")
    '            'e.Row.Cells(7).Text = Rate.ToString("")
    '            e.Row.Cells(8).Text = Amount.ToString("")


    '            'e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
    '            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
    '            'e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
    '            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center

    '            e.Row.Font.Bold = True
    '        End If

    '    Else

    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            ' add the UnitPrice and QuantityTotal to the running total variables
    '            'BagWight += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Bag_Weight"))
    '            Number += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Number"))
    '            'Rate += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Rate"))
    '            Amount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount_Rs"))

    '        ElseIf e.Row.RowType = DataControlRowType.Footer Then
    '            e.Row.Cells(0).Text = "Totals:"
    '            ' for the Footer, display the running totals
    '            'e.Row.Cells(4).Text = BagWight.ToString("")
    '            e.Row.Cells(5).Text = Number.ToString("")
    '            'e.Row.Cells(6).Text = Rate.ToString("")
    '            e.Row.Cells(7).Text = Amount.ToString("")


    '            'e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
    '            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
    '            'e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
    '            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center

    '            e.Row.Font.Bold = True
    '        End If

    '    End If

    'End Sub
End Class
