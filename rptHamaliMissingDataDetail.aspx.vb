Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Partial Class rptHamaliMissingDataDetail
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    Protected returnValue As New StringBuilder
    Dim BagWight As Decimal = 0
    Dim Number As Decimal = 0
    Dim Rate As Decimal = 0
    Dim Amount As Decimal = 0
    Dim PlantSelected As String = String.Empty
    Dim MissingTypeDtl As String = String.Empty
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

                        If Session("Role").ToString = "PlantOfficer" Then
                            GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') and emp_staffid='" & Session("staffid").ToString & "' order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")

                            'GetDataByQUERYAll("select distinct ham.activity_id,ham.activity from Hamal_Activity_Master as ham inner join Hamal_trans as ht on ham.activity_id=ht.activity_id where ht.plantcode='" & Session("plantCode").ToString & "'  ", cmbActivity, "activity", "activity_id")

                        ElseIf Session("Role").ToString = "HeadOfficer" Then
                            GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")

                        ElseIf Session("Role").ToString = "Admin" Then
                            GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")
                        End If

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

                        PlantSelected = v(2).ToString
                        'ActivitySelected = v(5).ToString
                        MissingTypeDtl = v(3).ToString


                        Dim qry As String = String.Empty

                        Dim AllDtl As String = s(1).ToString
                        ''AllDtl = Mid(AllDtl, 4)

                        Dim AllStrDtl() = AllDtl.Split("|")

                        Dim FromDate = AllStrDtl(0).ToString
                        Dim Todate = AllStrDtl(1).ToString

                        Dim Result As String = String.Empty

                        If PlantSelected.ToString = "'%'" Then

                            If MissingTypeDtl.ToString = "'Matnr'" Then

                                qry = "select distinct ht.RowId,hum.Emp_Plantnm,convert(varchar,ht.Tran_Date,103) as Tran_Date,hcm.Contractor_Name,hum.User_Initial,ham.Activity,hpm.Purpose,lm.Location_Name,ht.Bag_Weight,ht.Matnr,hdn.Description,ht.Loading,ht.Unloading,ht.Sorting,ht.Stiching,ht.Stacking,ht.Restacking,ht.Weighing,ht.Bundle_Preparation,ht.Opening_Feeding,ht.Loading_Unloading,ht.Unload_Stack,ht.Unp_Recd_Unl_Wt_Stk,ht.Unp_Recd_Unl_Srt_Wt_Stk,ht.Dh_To_Ac_Stk,ht.Ac_To_Dh_Stk,ht.Loading_Dh,ht.UnLoading_Dh,ht.Varai,ht.Stitch_Stack_Plant_OP_Spc_Dry from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_Purpose_Master as hpm on ht.purpose_id=hpm.purpose_id inner join location_master as lm on ht.location=lm.location_code and ht.plantcode=lm.plant_code inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range where ht.matnr='' and ht.plantCode like(" & v(2).ToString & ") AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " "  ''order by ht.tran_date,ht.supervisor_code
                                'Call GetDataByDr(qry)
                            ElseIf MissingTypeDtl.ToString = "'Location'" Then

                                qry = "select distinct ht.RowId,hum.Emp_Plantnm,convert(varchar,ht.Tran_Date,103) as Tran_Date,hcm.Contractor_Name,hum.User_Initial,ham.Activity,hpm.Purpose,ht.location as Location_Name,ht.Bag_Weight,ht.Matnr,hdn.Description,ht.Loading,ht.Unloading,ht.Sorting,ht.Stiching,ht.Stacking,ht.Restacking,ht.Weighing,ht.Bundle_Preparation,ht.Opening_Feeding,ht.Loading_Unloading,ht.Unload_Stack,ht.Unp_Recd_Unl_Wt_Stk,ht.Unp_Recd_Unl_Srt_Wt_Stk,ht.Dh_To_Ac_Stk,ht.Ac_To_Dh_Stk,ht.Loading_Dh,ht.UnLoading_Dh,ht.Varai,ht.Stitch_Stack_Plant_OP_Spc_Dry from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_Purpose_Master as hpm on ht.purpose_id=hpm.purpose_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range where ht.location='' and ht.plantCode like(" & v(2).ToString & ") AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " "  ''order by ht.tran_date,ht.supervisor_code
                                'Call GetDataByDr(qry)
                            ElseIf MissingTypeDtl.ToString = "'Distance_Range'" Then

                                qry = "select distinct ht.RowId,hum.Emp_Plantnm,convert(varchar,ht.Tran_Date,103) as Tran_Date,hcm.Contractor_Name,hum.User_Initial,ham.Activity,hpm.Purpose,lm.Location_Name,ht.Bag_Weight,ht.Matnr,ht.Dist_Range as Description,ht.Loading,ht.Unloading,ht.Sorting,ht.Stiching,ht.Stacking,ht.Restacking,ht.Weighing,ht.Bundle_Preparation,ht.Opening_Feeding,ht.Loading_Unloading,ht.Unload_Stack,ht.Unp_Recd_Unl_Wt_Stk,ht.Unp_Recd_Unl_Srt_Wt_Stk,ht.Dh_To_Ac_Stk,ht.Ac_To_Dh_Stk,ht.Loading_Dh,ht.UnLoading_Dh,ht.Varai,ht.Stitch_Stack_Plant_OP_Spc_Dry from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_Purpose_Master as hpm on ht.purpose_id=hpm.purpose_id inner join location_master as lm on ht.location=lm.location_code  and ht.plantcode=lm.plant_code where ht.dist_range='' and ht.plantCode like(" & v(2).ToString & ") AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " "  ''order by ht.tran_date,ht.supervisor_code
                                'Call GetDataByDr(qry)
                            End If

                            Call GetDataByDr(qry)

                            If DataDr.HasRows Then
                                GridView1.DataSource = DataDr
                                GridView1.DataBind()

                                DataDr.Close()
                                DataDr = Nothing
                                con.Close()

                            Else

                                DataDr.Close()
                                DataDr = Nothing
                                con.Close()

                            End If


                        Else

                            If MissingTypeDtl.ToString = "'Matnr'" Then

                                qry = "select distinct ht.RowId,hum.Emp_Plantnm,convert(varchar,ht.Tran_Date,103) as Tran_Date,hcm.Contractor_Name,hum.User_Initial,ham.Activity,hpm.Purpose,lm.Location_Name,ht.Bag_Weight,ht.Matnr,hdn.Description,ht.Loading,ht.Unloading,ht.Sorting,ht.Stiching,ht.Stacking,ht.Restacking,ht.Weighing,ht.Bundle_Preparation,ht.Opening_Feeding,ht.Loading_Unloading,ht.Unload_Stack,ht.Unp_Recd_Unl_Wt_Stk,ht.Unp_Recd_Unl_Srt_Wt_Stk,ht.Dh_To_Ac_Stk,ht.Ac_To_Dh_Stk,ht.Loading_Dh,ht.UnLoading_Dh,ht.Varai,ht.Stitch_Stack_Plant_OP_Spc_Dry from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_Purpose_Master as hpm on ht.purpose_id=hpm.purpose_id inner join location_master as lm on ht.location=lm.location_code and ht.plantcode=lm.plant_code inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range where ht.matnr='' and ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " "  ''order by ht.tran_date,ht.supervisor_code
                                'Call GetDataByDr(qry)
                            ElseIf MissingTypeDtl.ToString = "'Location'" Then

                                qry = "select distinct ht.RowId,hum.Emp_Plantnm,convert(varchar,ht.Tran_Date,103) as Tran_Date,hcm.Contractor_Name,hum.User_Initial,ham.Activity,hpm.Purpose,ht.location as Location_Name,ht.Bag_Weight,ht.Matnr,hdn.Description,ht.Loading,ht.Unloading,ht.Sorting,ht.Stiching,ht.Stacking,ht.Restacking,ht.Weighing,ht.Bundle_Preparation,ht.Opening_Feeding,ht.Loading_Unloading,ht.Unload_Stack,ht.Unp_Recd_Unl_Wt_Stk,ht.Unp_Recd_Unl_Srt_Wt_Stk,ht.Dh_To_Ac_Stk,ht.Ac_To_Dh_Stk,ht.Loading_Dh,ht.UnLoading_Dh,ht.Varai,ht.Stitch_Stack_Plant_OP_Spc_Dry from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_Purpose_Master as hpm on ht.purpose_id=hpm.purpose_id inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range where ht.location='' and ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " "  ''order by ht.tran_date,ht.supervisor_code
                                'Call GetDataByDr(qry)
                            ElseIf MissingTypeDtl.ToString = "'Distance_Range'" Then

                                qry = "select distinct ht.RowId,hum.Emp_Plantnm,convert(varchar,ht.Tran_Date,103) as Tran_Date,hcm.Contractor_Name,hum.User_Initial,ham.Activity,hpm.Purpose,lm.Location_Name,ht.Bag_Weight,ht.Matnr,ht.Dist_Range as Description,ht.Loading,ht.Unloading,ht.Sorting,ht.Stiching,ht.Stacking,ht.Restacking,ht.Weighing,ht.Bundle_Preparation,ht.Opening_Feeding,ht.Loading_Unloading,ht.Unload_Stack,ht.Unp_Recd_Unl_Wt_Stk,ht.Unp_Recd_Unl_Srt_Wt_Stk,ht.Dh_To_Ac_Stk,ht.Ac_To_Dh_Stk,ht.Loading_Dh,ht.UnLoading_Dh,ht.Varai,ht.Stitch_Stack_Plant_OP_Spc_Dry from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_Purpose_Master as hpm on ht.purpose_id=hpm.purpose_id inner join location_master as lm on ht.location=lm.location_code  and ht.plantcode=lm.plant_code where ht.dist_range='' and ht.plantCode = " & v(2).ToString & " AND ht.tran_date BETWEEN " & v(0).ToString & " AND " & v(1).ToString & " "  ''order by ht.tran_date,ht.supervisor_code
                                'Call GetDataByDr(qry)
                            End If


                            Call GetDataByDr(qry)

                            If DataDr.HasRows Then
                                GridView1.DataSource = DataDr
                                GridView1.DataBind()

                                DataDr.Close()
                                DataDr = Nothing
                                con.Close()

                            Else

                                DataDr.Close()
                                DataDr = Nothing
                                con.Close()

                            End If
                            'DataDr.Close()
                            'DataDr = Nothing
                            'con.Close()

                            ''If MissingTypeDtl.ToString = "'Matnr'" Then
                        End If


                        For Each Title As GridViewRow In GridView1.Rows
                            Title.Cells(0).Attributes.Add("Title", "SrNo")
                            Title.Cells(1).Attributes.Add("Title", "RowId")
                            Title.Cells(2).Attributes.Add("Title", "Emp_Plantnm")
                            Title.Cells(3).Attributes.Add("Title", "Tran_Date")
                            Title.Cells(4).Attributes.Add("Title", "Contractor_Name")
                            Title.Cells(5).Attributes.Add("Title", "User_Initial")
                            Title.Cells(6).Attributes.Add("Title", "Activity")
                            Title.Cells(7).Attributes.Add("Title", "Purpose")
                            Title.Cells(8).Attributes.Add("Title", "Location_Name")
                            Title.Cells(9).Attributes.Add("Title", "Bag_Weight")
                            Title.Cells(10).Attributes.Add("Title", "MatDesc")
                            Title.Cells(11).Attributes.Add("Title", "Description")
                            Title.Cells(12).Attributes.Add("Title", "Loading")
                            Title.Cells(13).Attributes.Add("Title", "Unloading")
                            Title.Cells(14).Attributes.Add("Title", "Sorting")
                            Title.Cells(15).Attributes.Add("Title", "Stiching")
                            Title.Cells(16).Attributes.Add("Title", "Stacking")
                            Title.Cells(17).Attributes.Add("Title", "Restacking")
                            Title.Cells(18).Attributes.Add("Title", "Weighing")
                            Title.Cells(19).Attributes.Add("Title", "Bundle_Preparation")
                            Title.Cells(20).Attributes.Add("Title", "Opening_Feeding")
                            Title.Cells(21).Attributes.Add("Title", "Loading_Unloading")
                            Title.Cells(22).Attributes.Add("Title", "Unload_Stack")
                            Title.Cells(23).Attributes.Add("Title", "Unp_Recd_Unl_Wt_Stk")
                            Title.Cells(24).Attributes.Add("Title", "Unp_Recd_Unl_Srt_Wt_Stk")
                            Title.Cells(25).Attributes.Add("Title", "Dh_To_Ac_Stk")
                            Title.Cells(26).Attributes.Add("Title", "Ac_To_Dh_Stk")
                            Title.Cells(27).Attributes.Add("Title", "Loading_Dh")
                            Title.Cells(28).Attributes.Add("Title", "UnLoading_Dh")
                            Title.Cells(29).Attributes.Add("Title", "Varai")
                            Title.Cells(30).Attributes.Add("Title", "Stitch_Stack_Plant_OP_Spc_Dry")
                        Next

                        ''ElseIf MissingTypeDtl.ToString = "'Location'" Then
                        ''ElseIf MissingTypeDtl.ToString = "'Distance_Range'" Then
                        ''End If



                        '*****code s end here

                        Dim sw1 As New StringWriter
                        Dim htw1 As New HtmlTextWriter(sw1)
                        GridView1.RenderControl(htw1)
                        returnValue.Append("Fifth|")
                        returnValue.Append(sw1.ToString())
                        'End If


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
