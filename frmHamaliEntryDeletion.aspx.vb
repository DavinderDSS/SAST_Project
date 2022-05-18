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
Partial Class frmHamaliEntryDeletion
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

                If Session("Role").ToString = "HeadOfficer" Then   ''Role wise page Autherity option Added this Code on 08-11-2019

                    ''HiddenFieldCID.Value = Session("staffid")
                    HiddenFieldPC.Value = Session("plantCode")

                    Dim cbReference As String
                    cbReference = Page.ClientScript.GetCallbackEventReference(Me, "arg", "ReceiveServerData", "context")
                    Dim callbackScript As String = ""
                    callbackScript &= "function CallServer(arg, context) { " & cbReference & "} ;"
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", callbackScript, True)

                    imgsubmit.Visible = False



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

                        If Session("Role").ToString = "PlantOfficer" Then
                            GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') and emp_staffid='" & Session("staffid").ToString & "' order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")

                            'GetDataByQUERYAll("select distinct ham.activity_id,ham.activity from Hamal_Activity_Master as ham inner join Hamal_trans as ht on ham.activity_id=ht.activity_id where ht.plantcode='" & Session("plantCode").ToString & "'  ", cmbActivity, "activity", "activity_id")

                        ElseIf Session("Role").ToString = "HeadOfficer" Then
                            GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")

                        ElseIf Session("Role").ToString = "Admin" Then
                            GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")
                        End If


                    End If

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
        If con.State = ConnectionState.Closed Then con.Open()
        ''''''Dim cmd As New SqlCommand("select ht.rowid,convert(varchar,ht.tran_date,103) as tran_date,hcm.Contractor_Name,hum.user_initial as supervisor_code,ham.activity as activity_id,hpm.Purpose as purpose_id,mm.matdesc as matnr,ht.location,ht.dist_range,ht.bag_weight,ht.loading,ht.unloading,ht.sorting,ht.stiching,ht.stacking,ht.restacking,ht.weighing,ht.bundle_preparation,ht.opening_feeding,ht.loading_unloading,ht.Unp_Recd_Unl_Wt_Stk,ht.Unp_Recd_Unl_Srt_Wt_Stk,ht.Dh_To_Ac_Stk,ht.Ac_To_Dh_Stk,ht.Loading_Dh,ht.UnLoading_Dh,ht.Varai,ht.remark from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_purpose_master as hpm on ht.purpose_id=hpm.purpose_id inner join Matnr_master as mm on ht.matnr=mm.matnr inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code where  hum.emp_plantCode='" & Session("plantCode") & "' and ht.approval='unapprove' and ht.supervisor_code='" & cmbSupervisor.SelectedValue.ToString & "' and ht.status='save'  order by ht.tran_date", con)
        ''Dim cmd As New SqlCommand("select ht.rowid,convert(varchar,ht.tran_date,103) as tran_date,hcm.Contractor_Name,hum.user_initial as supervisor_code,ham.activity as activity_id,hpm.Purpose as purpose_id,mm.matdesc as matnr,ht.location,ht.dist_range,ht.bag_weight,ht.loading,ht.unloading,ht.sorting,ht.stiching,ht.stacking,ht.restacking,ht.weighing,ht.bundle_preparation,ht.opening_feeding,ht.loading_unloading,ht.Varai,ht.remark from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_purpose_master as hpm on ht.purpose_id=hpm.purpose_id inner join Matnr_master as mm on ht.matnr=mm.matnr inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code where  hum.emp_plantCode='" & Session("plantCode") & "' and ht.approval='unapprove' and ht.supervisor_code='" & cmbCategory.SelectedValue.ToString & "' and ht.status='save'  order by ht.tran_date", con)

        '' '' For testing below code is use uncomment upper code after testing and coment testing code 18-04-2013
        '' ''Dim cmd As New SqlCommand("select ht.rowid,convert(varchar,ht.tran_date,103) as tran_date,hcm.Contractor_Name,hum.user_initial as supervisor_code,ham.activity as activity_id,hpm.Purpose as purpose_id,mm.matdesc as matnr,ht.location,ht.dist_range,ht.bag_weight,ht.loading,ht.unloading,ht.sorting,ht.stiching,ht.stacking,ht.restacking,ht.weighing,ht.bundle_preparation,ht.opening_feeding,ht.loading_unloading,ht.Varai,ht.remark from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_purpose_master as hpm on ht.purpose_id=hpm.purpose_id inner join Matnr_master as mm on ht.matnr=mm.matnr inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code where  hum.emp_plantCode='" & Session("plantCode") & "' and ht.supervisor_code='" & cmbSupervisor.SelectedValue.ToString & "' and ht.status='save'  order by ht.tran_date", con)
        '' ''code is end here

        Dim qry As New SqlCommand


        'If cmbSupervisor.SelectedValue.ToString <> "Supervisor" Then
        If con.State = ConnectionState.Closed Then con.Open()
        Dim cmd As New SqlCommand("select distinct ht.RowId,hum.Emp_Plantnm,convert(varchar(10),ht.Tran_Date,121) as Tran_Date,hcm.Contractor_Name,hum.User_Initial,ham.Activity,hpm.Purpose,ht.location as Location,ht.Bag_Weight,ht.Matnr,ht.dist_range as Dist_Range,ht.Loading,ht.Unloading,ht.Sorting,ht.Stiching,ht.Stacking,ht.Restacking,ht.Weighing,ht.Bundle_Preparation,ht.Opening_Feeding,ht.Loading_Unloading,ht.Unload_Stack,ht.Unp_Recd_Unl_Wt_Stk,ht.Unp_Recd_Unl_Srt_Wt_Stk,ht.Dh_To_Ac_Stk,ht.Ac_To_Dh_Stk,ht.Loading_Dh,ht.UnLoading_Dh,ht.Varai from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_Purpose_Master as hpm on ht.purpose_id=hpm.purpose_id inner join location_master as lm on ht.location=lm.location_code and ht.plantcode=lm.plant_code inner join Hamal_DL_Norms as hdn on ht.dist_range=hdn.range where ht.plantCode ='" & cmbPlant.SelectedValue.ToString & "' and ht.tran_date between '" & txtFrom.Text & "' and '" & txtTo.Text & "' and ht.supervisor_code='" & cmbSupervisor.SelectedValue & "' ", con)
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()
        da.Fill(ds)
        con.Close()
        gvCustomers.DataSource = ds
        gvCustomers.DataBind()

        imgsubmit.Visible = True






        'End If





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

                                    'Dim i As Integer
                                    'For i = 0 To J - 1

                                    Dim Queries As String = String.Empty


                                    Queries = ("delete from hamal_trans  where rowid=" & ValDtl(0).ToString & " and tran_date=" & ValDtl(1).ToString & " and supervisor_code=" & ValDtl(2).ToString & " and Plantcode=" & ValDtl(3).ToString & "")

                                    Queries = Queries & "~"

                                    If SaveDataWithTransaction(Queries, "~") = True Then
                                        Result = True
                                        ''returnValue.Append("Record Saved Successfully")
                                    Else
                                        Result = False
                                    End If

                                End If
                            Next
                            'Next
                            If Result.ToString = "True" Then
                                returnValue.Append("SaveData|Record Deleted Successfully")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            Else
                                returnValue.Append("SaveData|Record Not Deleted Successfully")
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
    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        ''If cmbSupervisor.SelectedValue.ToString = "Matnr" Then

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cmd As New SqlCommand("select Location_Code,Location_Code + ':' + Location_Name as Location from Location_master where plant_code='" & cmbPlant.SelectedValue.ToString & "' order by Location_Code")
            Dim ddlLocationes As DropDownList = TryCast(e.Row.FindControl("ddlLocationes"), DropDownList)
            ddlLocationes.DataSource = Me.ExecuteQuery(cmd, "SELECT")
            ddlLocationes.DataTextField = "Location"
            ddlLocationes.DataValueField = "Location_Code"
            ddlLocationes.DataBind()
            Dim Location As String = TryCast(e.Row.FindControl("lblLocation"), Label).Text
            ddlLocationes.Items.FindByValue(Location).Selected = True
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cmd As New SqlCommand("select distinct rtrim(hdn.range) as range,hdn.description from Hamal_DL_Norms as hdn inner join Hamal_trans as ht on hdn.range=ht.dist_range where ht.plantcode='" & cmbPlant.SelectedValue.ToString & "'")
            'Dim cmd As New SqlCommand("select distinct hdn.range,hdn.description from Hamal_DL_Norms as hdn inner join Hamal_trans as ht on hdn.range=ht.dist_range where ht.plantcode='" & Session("plantCode") & "'")
            Dim ddldist_range As DropDownList = TryCast(e.Row.FindControl("ddlDist_Range"), DropDownList)
            ddldist_range.DataSource = Me.ExecuteQuery(cmd, "SELECT")
            ddldist_range.DataTextField = "description"
            ddldist_range.DataValueField = "range"
            ddldist_range.DataBind()
            Dim DistRn As String = TryCast(e.Row.FindControl("lblDist_Range"), Label).Text
            ddldist_range.Items.FindByValue(DistRn).Selected = True
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cmd As New SqlCommand("select distinct bag_weight from Hamal_trans where plantcode='" & cmbPlant.SelectedValue.ToString & "'")
            Dim ddlBGWait As DropDownList = TryCast(e.Row.FindControl("ddlBGWait"), DropDownList)
            ddlBGWait.DataSource = Me.ExecuteQuery(cmd, "SELECT")
            ddlBGWait.DataTextField = "bag_weight"
            ddlBGWait.DataValueField = "bag_weight"
            ddlBGWait.DataBind()
            Dim BGWaitRn As String = TryCast(e.Row.FindControl("lblBGWait"), Label).Text
            ddlBGWait.Items.FindByValue(BGWaitRn).Selected = True
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cmd As New SqlCommand("Select distinct ht.Matnr,mm.matdesc from matnr_master as mm inner join hamal_trans as ht on mm.matnr = ht.matnr where ht.plantcode='" & cmbPlant.SelectedValue.ToString & "' and (left(ht.matnr,1) in ('1','2','3') or ht.matnr = 'store')")
            Dim ddlMatnr As DropDownList = TryCast(e.Row.FindControl("ddlMatnr"), DropDownList)
            ddlMatnr.DataSource = Me.ExecuteQuery(cmd, "SELECT")
            ddlMatnr.DataTextField = "matdesc"
            ddlMatnr.DataValueField = "Matnr"
            ddlMatnr.DataBind()
            Dim MatnrRn As String = TryCast(e.Row.FindControl("lblmatnr"), Label).Text
            ddlMatnr.Items.FindByValue(MatnrRn).Selected = True
        End If

        ''End If

        


    End Sub

    Protected Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        BindGridview()
    End Sub

    Protected Sub cmbPlant_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPlant.SelectedIndexChanged
        'Dim Qry As String = String.Empty
        'Qry = "select hum.emp_staffid,hum.user_initial from hamali_user_master as hum inner join Hamal_trans as ht on hum.emp_staffid=ht.supervisor_code where hum.emp_plantcode='" & cmbPlant.SelectedValue & "' and ht.tran_date between '" & txtFrom.Text & "' and '" & txtTo.Text & "' and  hum.empstatus='Active' and hum.emp_role='3' group by hum.emp_staffid,hum.user_initial order by hum.emp_staffid"
        'Call GetDataByDr(Qry)
        'If DataDr.HasRows Then
        '    cmbSupervisor.DataSource = DataDr
        '    cmbSupervisor.DataBind()

        '    DataDr.Close()
        '    DataDr = Nothing
        '    con.Close()

        'Else

        '    DataDr.Close()
        '    DataDr = Nothing
        '    con.Close()

        'End If
        GetDataByQUERY("select hum.emp_staffid as Staffid,hum.user_initial as Initial from hamali_user_master as hum inner join Hamal_trans as ht on hum.emp_staffid=ht.supervisor_code where hum.emp_plantcode='" & cmbPlant.SelectedValue & "' and ht.tran_date between '" & txtFrom.Text & "' and '" & txtTo.Text & "' and  hum.empstatus='Active' and hum.emp_role='3' group by hum.emp_staffid,hum.user_initial order by hum.emp_staffid", cmbSupervisor, "Initial", "Staffid")

    End Sub
End Class
