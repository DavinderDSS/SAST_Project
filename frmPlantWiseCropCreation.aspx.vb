Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports system.Web.UI.ControlCollection
Imports System.Configuration
'Imports System.Linq
Imports System.Collections.Generic
Imports System.Collections
Partial Class frmPlantWiseCropCreation
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

                If Session("Role").ToString = "PlantOfficer" Or Session("Role").ToString = "Admin" Then   ''Role wise page Autherity option Added this Code on 08-11-2019
                    HiddenFieldPC.Value = Session("plantCode")
                    HiddenFieldEmpCode.Value = Session("staffid")
                    HiddenFieldEmpInitial.Value = Session("initial")
                    imgsubmit.Visible = False
                    imgUpdate.Visible = False
                    'imgShow.Visible = False


                    If Not IsPostBack Then

                        '' ''THis  code is used for Request Flooding but used client side method so commented added on 12-05-2020
                        ''Dim num = getRandomNumber()
                        ''hdnRndNo.Value = num
                        ''Session("RandomNumber") = num
                        '' ''Code is ended here added on 12-05-2020 for request Flooding

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


                        GetDataByQUERY("select distinct Mtart,MtartDesc from mattype_master ", cmbCropType, "MtartDesc", "Mtart")
                        GetDataByQUERY("select distinct Matkl,GrpName from matgrp_master ", cmbCropGrp, "MtartDesc", "Mtart")
                    End If



                    ''cmbPlant.Attributes.Add("OnChange", "FillCombo(this)") ''This is for Contractors            

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
    Protected Sub BindGridview()
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            ''con.Open()


            ''''''Dim cmd As New SqlCommand("select ht.rowid,convert(varchar,ht.tran_date,103) as tran_date,hcm.Contractor_Name,hum.user_initial as supervisor_code,ham.activity as activity_id,hpm.Purpose as purpose_id,mm.matdesc as matnr,ht.location,ht.dist_range,ht.bag_weight,ht.loading,ht.unloading,ht.sorting,ht.stiching,ht.stacking,ht.restacking,ht.weighing,ht.bundle_preparation,ht.opening_feeding,ht.loading_unloading,ht.Unp_Recd_Unl_Wt_Stk,ht.Unp_Recd_Unl_Srt_Wt_Stk,ht.Dh_To_Ac_Stk,ht.Ac_To_Dh_Stk,ht.Loading_Dh,ht.UnLoading_Dh,ht.Varai,ht.remark from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_purpose_master as hpm on ht.purpose_id=hpm.purpose_id inner join Matnr_master as mm on ht.matnr=mm.matnr inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code where  hum.emp_plantCode='" & Session("plantCode") & "' and ht.approval='unapprove' and ht.supervisor_code='" & cmbSupervisor.SelectedValue.ToString & "' and ht.status='save'  order by ht.tran_date", con)
            ''Dim cmd As New SqlCommand("select ht.rowid,convert(varchar,ht.tran_date,103) as tran_date,hcm.Contractor_Name,hum.user_initial as supervisor_code,ham.activity as activity_id,hpm.Purpose as purpose_id,mm.matdesc as matnr,ht.location,ht.dist_range,ht.bag_weight,ht.loading,ht.unloading,ht.sorting,ht.stiching,ht.stacking,ht.restacking,ht.weighing,ht.bundle_preparation,ht.opening_feeding,ht.loading_unloading,ht.Varai,ht.remark from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_purpose_master as hpm on ht.purpose_id=hpm.purpose_id inner join Matnr_master as mm on ht.matnr=mm.matnr inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code where  hum.emp_plantCode='" & Session("plantCode") & "' and ht.approval='unapprove' and ht.supervisor_code='" & cmbCategory.SelectedValue.ToString & "' and ht.status='save'  order by ht.tran_date", con)

            '' '' For testing below code is use uncomment upper code after testing and coment testing code 18-04-2013
            '' ''Dim cmd As New SqlCommand("select ht.rowid,convert(varchar,ht.tran_date,103) as tran_date,hcm.Contractor_Name,hum.user_initial as supervisor_code,ham.activity as activity_id,hpm.Purpose as purpose_id,mm.matdesc as matnr,ht.location,ht.dist_range,ht.bag_weight,ht.loading,ht.unloading,ht.sorting,ht.stiching,ht.stacking,ht.restacking,ht.weighing,ht.bundle_preparation,ht.opening_feeding,ht.loading_unloading,ht.Varai,ht.remark from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_purpose_master as hpm on ht.purpose_id=hpm.purpose_id inner join Matnr_master as mm on ht.matnr=mm.matnr inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code where  hum.emp_plantCode='" & Session("plantCode") & "' and ht.supervisor_code='" & cmbSupervisor.SelectedValue.ToString & "' and ht.status='save'  order by ht.tran_date", con)
            '' ''code is end here

            GrdPlantCurCrop.Visible = False
            GrdPlantCurCrop.DataSource = Nothing
            gvCrop.Visible = False
            gvCrop.DataSource = Nothing

            Dim qry As New SqlCommand


            If cmbType.SelectedValue.ToString = "NC" Then
                If con.State = ConnectionState.Closed Then con.Open()

                If cmbCropGrp.SelectedValue.ToString = "555" Then

                    Dim cmd As New SqlCommand("Select distinct mm.Matnr + '~' + mm.MatDesc as Matnr,mm.Mtart from matnr_master as mm inner join matgrp_master mgm on mm.matkl = mgm.matkl where mm.matnr not in (select matnr from plant_matnr_master where Plantcode='" & cmbPlant.SelectedValue.ToString & "') and (left(mm.matnr,1) in ('1','2','3') or mm.matnr = 'store') and mm.mtart='" & cmbCropType.SelectedValue.ToString & "' and left(mm.matkl,3)='" & cmbCropGrp.SelectedValue.ToString & "' ", con)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    da.Fill(ds)
                    con.Close()
                    gvCrop.DataSource = ds
                    gvCrop.DataBind()

                    gvCrop.Visible = True
                    GrdPlantCurCrop.Visible = False

                    imgsubmit.Visible = True
                    imgUpdate.Visible = False

                Else

                    Dim cmd As New SqlCommand("Select distinct mm.Matnr + '~' + mm.MatDesc as Matnr,mm.Mtart from matnr_master as mm inner join matgrp_master mgm on mm.matkl = mgm.matkl where mm.matnr not in (select matnr from plant_matnr_master where Plantcode='" & cmbPlant.SelectedValue.ToString & "') and (left(mm.matnr,1) in ('1','2','3') or mm.matnr = 'store') and mm.mtart='" & cmbCropType.SelectedValue.ToString & "' and left(mm.matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "' ", con)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    da.Fill(ds)
                    con.Close()
                    gvCrop.DataSource = ds
                    gvCrop.DataBind()

                    gvCrop.Visible = True
                    GrdPlantCurCrop.Visible = False

                    imgsubmit.Visible = True
                    imgUpdate.Visible = False

                End If
                'Dim da As New SqlDataAdapter(cmd)
                'Dim ds As New DataSet()
                'da.Fill(ds)
                'con.Close()
                'gvCrop.DataSource = ds
                'gvCrop.DataBind()

                'imgsubmit.Visible = True
                ''imgUpdate.Visible = False
                ''imgShow.Visible = False




            ElseIf cmbType.SelectedValue.ToString = "CC" Then
                If con.State = ConnectionState.Closed Then con.Open()

                If cmbCropGrp.SelectedValue.ToString = "555" Then
                    Dim cmd As New SqlCommand("select distinct Matnr + '~' + MatDesc as Matnr,Mtart from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and status='Active'  ", con)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    da.Fill(ds)
                    con.Close()
                    GrdPlantCurCrop.DataSource = ds
                    GrdPlantCurCrop.DataBind()


                    gvCrop.Visible = False
                    GrdPlantCurCrop.Visible = True

                    imgsubmit.Visible = False
                    imgUpdate.Visible = False
                    'imgShow.Visible = True
                Else

                    Dim cmd As New SqlCommand("select distinct Matnr + '~' + MatDesc as Matnr,Mtart from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and left(matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "' and status='Active' ", con)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    da.Fill(ds)
                    con.Close()
                    GrdPlantCurCrop.DataSource = ds
                    GrdPlantCurCrop.DataBind()

                    gvCrop.Visible = False
                    GrdPlantCurCrop.Visible = True

                    imgsubmit.Visible = False
                    imgUpdate.Visible = False
                    'imgShow.Visible = True
                End If
                'Dim cmd As New SqlCommand("select distinct Matnr + '-' + MatDesc as Matnr,Mtart from Crop_Master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and left(matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "'  ", con)
                'Dim da As New SqlDataAdapter(cmd)
                'Dim ds As New DataSet()
                'da.Fill(ds)
                'con.Close()
                'gvCrop.DataSource = ds
                'gvCrop.DataBind()

                'imgsubmit.Visible = False
                ''imgUpdate.Visible = False
                ''imgShow.Visible = True


            ElseIf cmbType.SelectedValue.ToString = "IC" Then
                If con.State = ConnectionState.Closed Then con.Open()
                If cmbCropGrp.SelectedValue.ToString = "555" Then
                    Dim cmd As New SqlCommand("select distinct Matnr + '~' + MatDesc as Matnr,Mtart from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and matnr='store' and status='Active'  ", con)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    da.Fill(ds)
                    con.Close()
                    gvCrop.DataSource = ds
                    gvCrop.DataBind()

                    gvCrop.Visible = True
                    GrdPlantCurCrop.Visible = False

                    imgsubmit.Visible = False
                    imgUpdate.Visible = True
                    'imgShow.Visible = False
                Else
                    Dim cmd As New SqlCommand("select distinct Matnr + '~' + MatDesc as Matnr,Mtart from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and left(matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "'  and status='Active'  ", con)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    da.Fill(ds)
                    con.Close()
                    gvCrop.DataSource = ds
                    gvCrop.DataBind()

                    gvCrop.Visible = True
                    GrdPlantCurCrop.Visible = False

                    imgsubmit.Visible = False
                    imgUpdate.Visible = True
                    'imgShow.Visible = False

                End If



            ElseIf cmbType.SelectedValue.ToString = "AIC" Then
                If con.State = ConnectionState.Closed Then con.Open()
                If cmbCropGrp.SelectedValue.ToString = "555" Then
                    Dim cmd As New SqlCommand("select distinct Matnr + '~' + MatDesc as Matnr,Mtart from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and matnr='store' and status='InActive'  ", con)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    da.Fill(ds)
                    con.Close()
                    gvCrop.DataSource = ds
                    gvCrop.DataBind()

                    gvCrop.Visible = True
                    GrdPlantCurCrop.Visible = False

                    imgsubmit.Visible = False
                    imgUpdate.Visible = True
                    'imgShow.Visible = False
                Else
                    Dim cmd As New SqlCommand("select distinct Matnr + '~' + MatDesc as Matnr,Mtart from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and left(matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "'  and status='InActive'  ", con)
                    Dim da As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    da.Fill(ds)
                    con.Close()
                    gvCrop.DataSource = ds
                    gvCrop.DataBind()

                    gvCrop.Visible = True
                    GrdPlantCurCrop.Visible = False

                    imgsubmit.Visible = False
                    imgUpdate.Visible = True
                    'imgShow.Visible = False

                End If

                'Dim cmd As New SqlCommand("select distinct Matnr + '-' + MatDesc as Matnr,Mtart from Crop_Master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and left(matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "'  ", con)
                'Dim da As New SqlDataAdapter(cmd)
                'Dim ds As New DataSet()
                'da.Fill(ds)
                'con.Close()
                'gvCrop.DataSource = ds
                'gvCrop.DataBind()

                'imgsubmit.Visible = False
                'imgUpdate.Visible = True
                ''imgShow.Visible = False

            End If



        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
            Response.Write("<script language='javascript'>window.alert('Invalid Values');window.location='MenuPage.aspx';</script>")
        Finally
            If con.State = Data.ConnectionState.Open Then con.Close()
        End Try



    End Sub

    Private Function ExecuteQuery(ByVal cmd As SqlCommand, ByVal action As String) As DataTable
        'Dim conString As String = ConfigurationManager.ConnectionStrings("constring").ConnectionString
        'Using con As New SqlConnection(conString)
        cmd.Connection = con
        Select Case action
            Case "SELECT"
                Using sda As New SqlDataAdapter()
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Return dt
                    End Using
                End Using
            Case "UPDATE"
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                Exit Select
        End Select
        Return Nothing
        'End Using
    End Function
    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Dim RAffected As Integer = 0
        If eventArgument Is Nothing Then
            returnValue.Append("-1")
        Else
            Try

                Dim s = eventArgument.Split("#")

                Dim Val As String = s(0).ToString
                Val = Val.Remove(0, 4)
                'Val = Val.Substring(4, Val.Length)




                Select Case s(0).ToString


                    Case getCaseString(s(0).ToString, "SaveData")

                        If hdnRndNo.Value = Session("RandomNumber") Then ''// code is added on 12-05-2020 For Request Flooding Issue Start here using existing code too

                            ''new coe add for multiple rows addition work fine but lenghty

                            ''Dim ClosingPara() = s(0).ToString.Split("|")
                            Dim Result As String = String.Empty
                            Dim J As Integer
                            Dim AllData() = s(1).ToString.Split("|")


                            For J = 0 To AllData.Length - 1

                                If AllData(J).ToString <> Nothing Then


                                    Dim ValDtl() = AllData(J).ToString.Split(",")


                                    Dim MatDtl() = ValDtl(0).ToString.Split("~")
                                    'Dim i As Integer
                                    'For i = 0 To J - 1

                                    Dim Queries As String = String.Empty

                                    'Queries = "insert into hamal_trans (tran_date,crop_type,Matnr,activity_id,location,bag_weight,dist_range,loading,opening_feeding,unloading,sorting,restacking,stiching,stacking,bundle_preparation,weighing,loading_unloading,UNP_RECD_UNL_WT_STK,UNP_RECD_UNL_SRT_WT_STK,heightDiff,remark,supervisor_code,plantCode) Values (" & AllData(i).ToString().Remove(AllData(i).ToString.Length - 1, 1) & ")"

                                    If ValDtl(2).ToString = "'NC'" Then
                                        Queries = "insert into plant_matnr_master (PlantCode,Matnr,MatDesc,Mtart,Emp_Staffid,Emp_Initial) values( " & ValDtl(3).ToString & "," & MatDtl(0).ToString & "','" & MatDtl(1).ToString & "," & ValDtl(5).ToString & "," & ValDtl(4).ToString & "," & ValDtl(7).ToString & ")"

                                        Queries = Queries & "~"

                                        If SaveDataWithTransaction(Queries, "~") = True Then
                                            Result = True
                                            ''returnValue.Append("Record Saved Successfully")
                                        Else
                                            Result = False
                                        End If
                                    End If

                                End If
                            Next
                            'Next
                            If Result.ToString = "True" Then
                                returnValue.Append("SaveData|Record Saved Successfully")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            Else
                                returnValue.Append("SaveData|Record Not Saved Successfully")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            End If

                            ''New Code End here

                        Else ''This else is for Request Flooding Revalidation point else added this on 12-05-2020
                            returnValue.Append("SaveData|Threr is some issue , Kindly Update again.")
                            Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                        End If ''This  is end of if added on 12-05-2020

                    Case getCaseString(s(0).ToString, "UpdateData")

                        If hdnRndNo.Value = Session("RandomNumber") Then ''// code is added on 12-05-2020 For Request Flooding Issue Start here using existing code too

                            ''new coe add for multiple rows addition work fine but lenghty

                            ''Dim ClosingPara() = s(0).ToString.Split("|")
                            Dim Result As String = String.Empty
                            Dim J As Integer
                            Dim AllData() = s(1).ToString.Split("|")


                            For J = 0 To AllData.Length - 1

                                If AllData(J).ToString <> Nothing Then


                                    Dim ValDtl() = AllData(J).ToString.Split(",")

                                    Dim MatDtl() = ValDtl(0).ToString.Split("~")

                                    Dim Queries As String = String.Empty

                                    'Queries = "insert into hamal_trans (tran_date,crop_type,Matnr,activity_id,location,bag_weight,dist_range,loading,opening_feeding,unloading,sorting,restacking,stiching,stacking,bundle_preparation,weighing,loading_unloading,UNP_RECD_UNL_WT_STK,UNP_RECD_UNL_SRT_WT_STK,heightDiff,remark,supervisor_code,plantCode) Values (" & AllData(i).ToString().Remove(AllData(i).ToString.Length - 1, 1) & ")"

                                    If ValDtl(2).ToString = "'IC'" Then
                                        Queries = "Update plant_matnr_master set Up_Emp_Staffid= " & ValDtl(4).ToString & ",Up_Emp_Initial= " & ValDtl(7).ToString & ",UpdationDt=getdate(),Status='InActive'  where PlantCode=" & ValDtl(3).ToString & " and  Matnr=" & MatDtl(0).ToString & "' and Matdesc='" & MatDtl(1).ToString & " and Mtart=" & ValDtl(5).ToString & " "


                                        Queries = Queries & "~"

                                        If SaveDataWithTransaction(Queries, "~") = True Then
                                            Result = True
                                            ''returnValue.Append("Record Saved Successfully")
                                        Else
                                            Result = False
                                        End If

                                    End If


                                    If ValDtl(2).ToString = "'AIC'" Then
                                        Queries = "Update plant_matnr_master set Emp_Staffid= " & ValDtl(4).ToString & ",Up_Emp_Initial= " & ValDtl(7).ToString & ",UpdationDt=getdate(),Status='Active'  where PlantCode=" & ValDtl(3).ToString & " and  Matnr=" & MatDtl(0).ToString & "' and Matdesc='" & MatDtl(1).ToString & " and Mtart=" & ValDtl(5).ToString & " "


                                        Queries = Queries & "~"

                                        If SaveDataWithTransaction(Queries, "~") = True Then
                                            Result = True
                                            ''returnValue.Append("Record Saved Successfully")
                                        Else
                                            Result = False
                                        End If

                                    End If

                                End If
                            Next
                            'Next
                            If Result.ToString = "True" Then
                                returnValue.Append("SaveData|Record Update Successfully")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            Else
                                returnValue.Append("SaveData|Record Not Saved Successfully")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            End If

                            ''New Code End here

                        Else ''This else is for Request Flooding Revalidation point else added this on 12-05-2020
                            returnValue.Append("SaveData|Threr is some issue , Kindly Update again.")
                            Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                        End If ''This  is end of if added on 12-05-2020

                End Select



            Catch ex As Exception
                ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
                Response.Write("<script language='javascript'>window.alert('Invalid Values');window.location='MenuPage.aspx';</script>")
            Finally
                If con.State = Data.ConnectionState.Open Then con.Close()
            End Try

        End If
    End Sub

    Public Function getCaseString(ByVal str As String, ByVal StartsWith As String) As String
        Dim s As String = String.Empty
        If str.StartsWith(StartsWith) Then
            s = str
        Else
            s = ""
        End If
        Return s
    End Function

    Protected Sub cmbType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbType.SelectedIndexChanged

        ''THis  code is used for Request Flooding but used client side method so commented added on 12-05-2020
        Dim num = getRandomNumber()
        hdnRndNo.Value = num
        Session("RandomNumber") = num
        ''Code is ended here added on 12-05-2020 for request Flooding

        BindGridview()
    End Sub
    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If cmbType.SelectedValue.ToString = "NC" Then

                If cmbCropGrp.SelectedValue.ToString = "555" Then
                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("Select distinct mm.Matnr + '~' + mm.MatDesc as Matnr from matnr_master as mm inner join matgrp_master mgm on mm.matkl = mgm.matkl where mm.matnr not in (select matnr from plant_matnr_master where Plantcode='" & cmbPlant.SelectedValue.ToString & "') and (left(mm.matnr,1) in ('1','2','3') or mm.matnr = 'store') and mm.mtart='" & cmbCropType.SelectedValue.ToString & "' and left(mm.matkl,3)='" & cmbCropGrp.SelectedValue.ToString & "' ")
                        Dim ddlMatnr As DropDownList = TryCast(e.Row.FindControl("ddlMatnr"), DropDownList)
                        ddlMatnr.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMatnr.DataTextField = "Matnr"
                        ddlMatnr.DataValueField = "Matnr"
                        ddlMatnr.DataBind()
                        Dim MatnrRn As String = TryCast(e.Row.FindControl("lblmatnr"), Label).Text
                        ddlMatnr.Items.FindByValue(MatnrRn).Selected = True
                    End If


                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("Select distinct mm.Mtart from matnr_master as mm inner join matgrp_master mgm on mm.matkl = mgm.matkl where mm.matnr not in (select matnr from plant_matnr_master where Plantcode='" & cmbPlant.SelectedValue.ToString & "') and  (left(mm.matnr,1) in ('1','2','3') or mm.matnr = 'store') and mm.mtart='" & cmbCropType.SelectedValue.ToString & "' and left(mm.matkl,3)='" & cmbCropGrp.SelectedValue.ToString & "' ")
                        Dim ddlMtart As DropDownList = TryCast(e.Row.FindControl("ddlMtart"), DropDownList)
                        ddlMtart.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMtart.DataTextField = "Mtart"
                        ddlMtart.DataValueField = "Mtart"
                        ddlMtart.DataBind()
                        Dim MtartRn As String = TryCast(e.Row.FindControl("lblMtart"), Label).Text
                        ddlMtart.Items.FindByValue(MtartRn).Selected = True
                    End If
                Else

                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("Select distinct mm.Matnr + '~' + mm.MatDesc as Matnr from matnr_master as mm inner join matgrp_master mgm on mm.matkl = mgm.matkl where mm.matnr not in (select matnr from plant_matnr_master where Plantcode='" & cmbPlant.SelectedValue.ToString & "') and  (left(mm.matnr,1) in ('1','2','3') or mm.matnr = 'store') and mm.mtart='" & cmbCropType.SelectedValue.ToString & "' and left(mm.matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "' ")
                        Dim ddlMatnr As DropDownList = TryCast(e.Row.FindControl("ddlMatnr"), DropDownList)
                        ddlMatnr.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMatnr.DataTextField = "Matnr"
                        ddlMatnr.DataValueField = "Matnr"
                        ddlMatnr.DataBind()
                        Dim MatnrRn As String = TryCast(e.Row.FindControl("lblmatnr"), Label).Text
                        ddlMatnr.Items.FindByValue(MatnrRn).Selected = True
                    End If


                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("Select distinct mm.Mtart from matnr_master as mm inner join matgrp_master mgm on mm.matkl = mgm.matkl where mm.matnr not in (select matnr from plant_matnr_master where Plantcode='" & cmbPlant.SelectedValue.ToString & "') and  (left(mm.matnr,1) in ('1','2','3') or mm.matnr = 'store') and mm.mtart='" & cmbCropType.SelectedValue.ToString & "' and left(mm.matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "' ")
                        Dim ddlMtart As DropDownList = TryCast(e.Row.FindControl("ddlMtart"), DropDownList)
                        ddlMtart.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMtart.DataTextField = "Mtart"
                        ddlMtart.DataValueField = "Mtart"
                        ddlMtart.DataBind()
                        Dim MtartRn As String = TryCast(e.Row.FindControl("lblMtart"), Label).Text
                        ddlMtart.Items.FindByValue(MtartRn).Selected = True
                    End If
                End If

            ElseIf cmbType.SelectedValue.ToString = "IC" Then

                If cmbCropGrp.SelectedValue.ToString = "555" Then
                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("select distinct Matnr + '~' + MatDesc as Matnr from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and matnr='store' and status='Active'  ")
                        Dim ddlMatnr As DropDownList = TryCast(e.Row.FindControl("ddlMatnr"), DropDownList)
                        ddlMatnr.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMatnr.DataTextField = "Matnr"
                        ddlMatnr.DataValueField = "Matnr"
                        ddlMatnr.DataBind()
                        Dim MatnrRn As String = TryCast(e.Row.FindControl("lblmatnr"), Label).Text
                        ddlMatnr.Items.FindByValue(MatnrRn).Selected = True
                    End If


                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("select distinct Mtart from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and matnr='store' and status='Active'   ")
                        Dim ddlMtart As DropDownList = TryCast(e.Row.FindControl("ddlMtart"), DropDownList)
                        ddlMtart.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMtart.DataTextField = "Mtart"
                        ddlMtart.DataValueField = "Mtart"
                        ddlMtart.DataBind()
                        Dim MtartRn As String = TryCast(e.Row.FindControl("lblMtart"), Label).Text
                        ddlMtart.Items.FindByValue(MtartRn).Selected = True
                    End If
                Else

                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("select distinct Matnr + '~' + MatDesc as Matnr from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and left(matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "'  and status='Active'")
                        Dim ddlMatnr As DropDownList = TryCast(e.Row.FindControl("ddlMatnr"), DropDownList)
                        ddlMatnr.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMatnr.DataTextField = "Matnr"
                        ddlMatnr.DataValueField = "Matnr"
                        ddlMatnr.DataBind()
                        Dim MatnrRn As String = TryCast(e.Row.FindControl("lblmatnr"), Label).Text
                        ddlMatnr.Items.FindByValue(MatnrRn).Selected = True
                    End If


                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("select distinct Mtart from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and left(matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "'  and status='Active' ")
                        Dim ddlMtart As DropDownList = TryCast(e.Row.FindControl("ddlMtart"), DropDownList)
                        ddlMtart.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMtart.DataTextField = "Mtart"
                        ddlMtart.DataValueField = "Mtart"
                        ddlMtart.DataBind()
                        Dim MtartRn As String = TryCast(e.Row.FindControl("lblMtart"), Label).Text
                        ddlMtart.Items.FindByValue(MtartRn).Selected = True
                    End If
                End If


            ElseIf cmbType.SelectedValue.ToString = "AIC" Then

                If cmbCropGrp.SelectedValue.ToString = "555" Then
                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("select distinct Matnr + '~' + MatDesc as Matnr from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and matnr='store' and status='InActive' ")
                        Dim ddlMatnr As DropDownList = TryCast(e.Row.FindControl("ddlMatnr"), DropDownList)
                        ddlMatnr.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMatnr.DataTextField = "Matnr"
                        ddlMatnr.DataValueField = "Matnr"
                        ddlMatnr.DataBind()
                        Dim MatnrRn As String = TryCast(e.Row.FindControl("lblmatnr"), Label).Text
                        ddlMatnr.Items.FindByValue(MatnrRn).Selected = True
                    End If


                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("select distinct Mtart from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and matnr='store' and status='InActive' ")
                        Dim ddlMtart As DropDownList = TryCast(e.Row.FindControl("ddlMtart"), DropDownList)
                        ddlMtart.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMtart.DataTextField = "Mtart"
                        ddlMtart.DataValueField = "Mtart"
                        ddlMtart.DataBind()
                        Dim MtartRn As String = TryCast(e.Row.FindControl("lblMtart"), Label).Text
                        ddlMtart.Items.FindByValue(MtartRn).Selected = True
                    End If
                Else

                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("select distinct Matnr + '~' + MatDesc as Matnr from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and left(matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "'  and status='InActive'")
                        Dim ddlMatnr As DropDownList = TryCast(e.Row.FindControl("ddlMatnr"), DropDownList)
                        ddlMatnr.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMatnr.DataTextField = "Matnr"
                        ddlMatnr.DataValueField = "Matnr"
                        ddlMatnr.DataBind()
                        Dim MatnrRn As String = TryCast(e.Row.FindControl("lblmatnr"), Label).Text
                        ddlMatnr.Items.FindByValue(MatnrRn).Selected = True
                    End If


                    If e.Row.RowType = DataControlRowType.DataRow Then
                        Dim cmd As New SqlCommand("select distinct Mtart from plant_matnr_master where plantCode ='" & cmbPlant.SelectedValue.ToString & "' and mtart='" & cmbCropType.SelectedValue.ToString & "' and left(matnr,3)='" & cmbCropGrp.SelectedValue.ToString & "'  and status='InActive'")
                        Dim ddlMtart As DropDownList = TryCast(e.Row.FindControl("ddlMtart"), DropDownList)
                        ddlMtart.DataSource = Me.ExecuteQuery(cmd, "SELECT")
                        ddlMtart.DataTextField = "Mtart"
                        ddlMtart.DataValueField = "Mtart"
                        ddlMtart.DataBind()
                        Dim MtartRn As String = TryCast(e.Row.FindControl("lblMtart"), Label).Text
                        ddlMtart.Items.FindByValue(MtartRn).Selected = True
                    End If
                End If


            End If

        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
            Response.Write("<script language='javascript'>window.alert('Invalid Values');window.location='MenuPage.aspx';</script>")
        Finally
            If con.State = Data.ConnectionState.Open Then con.Close()
        End Try

    End Sub

    
End Class
