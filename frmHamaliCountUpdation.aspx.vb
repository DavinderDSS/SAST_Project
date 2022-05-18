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
Partial Class frmHamaliCountUpdation
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


            ''This is used to deactivate the browser back button on respective page added this on 13-05-2020
            ClientScript.RegisterClientScriptBlock(Page.GetType, "NOBACK", "<script>if(history.length>0)history.go(+1);</script>")
            ''Code is ended here added on 13-05-2020


            ''Qry to redirect the user if he not have the rights / CSRF validation page wise added this on 12-11-2019
            If CSRFUK = Session("CSRFUKSIDNew") And AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) Then   ''This is for else actule live usage   '' Added And obj.AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) in  if on 23-12-2019 ''''Session("AuthToken").ToString.Equals(Request.Cookies("AuthToken").Value)  
                ''If CSRFUK = Session("CSRFUKSID") Then ''This is for else testing usage

                If Session("Role").ToString = "PlantOfficer" Or Session("Role").ToString = "Admin" Then   ''Role wise page Autherity option Added this Code on 08-11-2019
                    HiddenFieldPC.Value = Session("plantCode")
                    HiddenFieldPlantName.Value = Session("plantName")

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
                        ElseIf Session("Role").ToString = "HeadOfficer" Then
                            GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")
                        ElseIf Session("Role").ToString = "Admin" Then
                            GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') order by emp_plantnm ", cmbPlant, "plant_name", "plant_code")
                        End If

                        If Session("Role").ToString = "PlantOfficer" Then
                            GetDataByQUERY("select Contractor_Code,Contractor_Name as Contractor_Name from Hamali_Contractor_master where Plant_Code=" & Session("plantCode") & " order by Contractor_Code ", cmbContractor, "Contractor_Name", "Contractor_Code")
                        ElseIf Session("Role").ToString = "HeadOfficer" Then
                            GetDataByQUERY("select Contractor_Code,Plant_Name +'-'+ Contractor_Name as Contractor_Name from Hamali_Contractor_master order by Contractor_Code ", cmbContractor, "Contractor_Name", "Contractor_Code")
                        ElseIf Session("Role").ToString = "Admin" Then
                            GetDataByQUERY("select Contractor_Code,Plant_Name +'-'+ Contractor_Name as Contractor_Name from Hamali_Contractor_master order by Contractor_Code ", cmbContractor, "Contractor_Name", "Contractor_Code")
                        End If

                        ''GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master order by activity", cmbA, "activity", "activity_id")
                        ''''GetDataByQUERY("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='" & Session("plantCode") & "' order by Contractor_Code", cmbContractor, "Contractor_Name", "Contractor_code")

                    End If


                    ''cmbPlant.Attributes.Add("OnChange", "FillCombo(this)") ''This is for Contractors




                    Dim cbReference As String
                    cbReference = Page.ClientScript.GetCallbackEventReference(Me, "arg", "ReceiveServerData", "context")
                    Dim callbackScript As String = ""
                    callbackScript &= "function CallServer(arg, context) { " & cbReference & "} ;"
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", callbackScript, True)

                    imgsubmit.Visible = False
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


        'If cmbContractor.SelectedValue.ToString = "Matnr" Then
        If con.State = ConnectionState.Closed Then con.Open()
        Dim cmd As New SqlCommand("select hc.RowId,convert(varchar(10),hc.hamal_date,120) as CountDate,hcm.plant_name as PlantName,hcm.contractor_name as ContractorName,sum(hc.hamal_count) as Counts from Hamali_Contractor_Master as hcm inner join Hamal_Count as hc on hc.hamal_contractor=hcm.contractor_code and hc.hamal_plant=hcm.plant_code  where hc.hamal_date between '" & txtFrom.Text & "' and '" & txtTo.Text & "' and hc.hamal_plant='" & cmbPlant.SelectedValue.ToString & "' and hc.hamal_contractor='" & cmbContractor.SelectedValue.ToString & "' and hc.flag not in ('1')  group by hc.RowId,hc.hamal_date,hcm.plant_name,hcm.contractor_name order by hcm.plant_name ", con)
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()
        da.Fill(ds)
        con.Close()
        gvCustomers.DataSource = ds
        gvCustomers.DataBind()

        imgsubmit.Visible = True




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
    Public Function getCaseString(ByVal str As String, ByVal StartsWith As String) As String
        Dim s As String = String.Empty
        If str.StartsWith(StartsWith) Then
            s = str
        Else
            s = ""
        End If
        Return s
    End Function
    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Try

            Dim RAffected As Integer = 0
            If eventArgument Is Nothing Then
                returnValue.Append("-1")
            Else
                Dim s = eventArgument.Split("#")

                Select Case s(0).ToString


                    Case getCaseString(s(0).ToString, "SaveData")

                        If hdnRndNo.Value = Session("RandomNumber") Then ''// code is added on 12-05-2020 For Request Flooding Issue Start here using existing code too

                            ''new coe add for multiple rows addition work fine but lenghty

                            ''Dim ClosingPara() = s(0).ToString.Split("|")
                            Dim Result As String = String.Empty
                            Dim InPutValResult As String = String.Empty ''Added this filed to validate Input Validation Function result values 06-01-2020
                            Dim J As Integer
                            Dim AllData() = s(1).ToString.Split("|")


                            For J = 0 To AllData.Length - 1

                                If AllData(J).ToString <> Nothing Then


                                    Dim ValDtl() = AllData(J).ToString.Split(",")

                                    'Dim i As Integer
                                    'For i = 0 To J - 1
                                    If IsValidInputText(ValDtl(2).ToString) = False Then ''Added this line on 06-01-2020 For GIS Input ValidationPoint  for Remark Filed

                                        returnValue.Append("Error|Invalid values enter please check and re-enter values")
                                    Else  ''Added this line on 06-01-2020 else

                                        If ValDtl(2).ToString.Length < 6 Then  ''Added this line on 07-01-2020 else


                                            Dim Queries As String = String.Empty
                                            Queries = ("Update Hamal_Count set Hamal_count=" & ValDtl(2).ToString & ",flag='1' where rowid=" & ValDtl(0).ToString & " and Hamal_date=" & ValDtl(1).ToString & " and hamal_contractor=" & ValDtl(3).ToString & " and Hamal_Plant=" & ValDtl(4).ToString & "")
                                            Queries = Queries & "~"

                                            If SaveDataWithTransaction(Queries, "~") = True Then
                                                Result = True
                                                ''returnValue.Append("Record Saved Successfully")
                                            Else
                                                Result = False
                                            End If

                                        Else  ''Added this line on 07-01-2020 else
                                            returnValue.Append("SaveData|Invalid values enter please check and re-enter values")
                                        End If  ''Added this line on 07-01-2020 else

                                    End If  ''Added this line on 06-01-2020 else


                                End If
                            Next
                            'Next


                            If Result.ToString = "True" Then
                                returnValue.Append("SaveData|Record Saved Successfully")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            Else
                                returnValue.Append("SaveData|Record Not Saved Successfully")
                                ''Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            End If

                            ''New Code End here

                        Else ''This else is for Request Flooding Revalidation point else added this on 12-05-2020
                            returnValue.Append("SaveData|Threr is some issue , Kindly Update again.")
                            Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                        End If ''This  is end of if added on 12-05-2020

                End Select
            End If

        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
            Response.Write("<script language='javascript'>window.alert('Invalid Values');window.location='MenuPage.aspx';</script>")
        Finally
            If con.State = Data.ConnectionState.Open Then con.Close()
        End Try

    End Sub

    Protected Sub cmbContractor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbContractor.SelectedIndexChanged
        BindGridview()
    End Sub
End Class
