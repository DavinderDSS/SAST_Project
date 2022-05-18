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


Partial Class frmHamaliApproval
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

                If Session("Role").ToString = "PlantOfficer" Then   ''Role wise page Autherity option Added this Code on 08-11-2019
                    HiddenFieldSVID.Value = Session("staffid")
                    HiddenFieldPC.Value = Session("plantCode")

                    Dim cbReference As String
                    cbReference = Page.ClientScript.GetCallbackEventReference(Me, "arg", "ReceiveServerData", "context")
                    Dim callbackScript As String = ""
                    callbackScript &= "function CallServer(arg, context) { " & cbReference & "} ;"
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", callbackScript, True)



                    If Not IsPostBack Then



                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019
                        ModuleName = Page.GetType.Name.Split("_")("0")
                        RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019

                        ''BindGridview() ''First gridview

                        GetDataByQUERY("select emp_staffid,user_initial from hamali_user_master where emp_plantNm='" & Session("plantName") & "' and emp_role='3' and emp_staffid in( select supervisor_code from hamal_trans where plantcode='" & Session("plantCode") & "' and approval='unapprove')", cmbSupervisor, "Location", "Location_Code")

                        ''''Me.BindGrid()  ''second gridview

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
        con.Open()
        Dim cmd As New SqlCommand("select ht.rowid,convert(varchar,ht.tran_date,103) as tran_date,hcm.Contractor_Name,hum.user_initial as supervisor_code,ham.activity as activity_id,hpm.Purpose as purpose_id,mm.matdesc as matnr,ht.location,ht.dist_range,ht.bag_weight,ht.loading,ht.unloading,ht.sorting,ht.stiching,ht.stacking,ht.restacking,ht.weighing,ht.bundle_preparation,ht.opening_feeding,ht.loading_unloading,ht.Unp_Recd_Unl_Wt_Stk,ht.Unp_Recd_Unl_Srt_Wt_Stk,ht.Dh_To_Ac_Stk,ht.Ac_To_Dh_Stk,ht.Loading_Dh,ht.UnLoading_Dh,ht.Varai,ht.remark from hamal_trans as ht inner join hamali_user_master as hum on ht.supervisor_code=hum.emp_staffid inner join Hamal_Activity_Master as ham on ht.activity_id=ham.activity_id inner join Hamal_purpose_master as hpm on ht.purpose_id=hpm.purpose_id inner join Matnr_master as mm on ht.matnr=mm.matnr inner join Hamali_Contractor_Master as hcm on ht.contractor_code=hcm.contractor_code and ht.plantcode=hcm.plant_code where  hum.emp_plantCode='" & Session("plantCode") & "' and ht.approval='unapprove' and ht.supervisor_code='" & cmbSupervisor.SelectedValue.ToString & "' and ht.status='save'  order by ht.tran_date", con)
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()
        da.Fill(ds)
        con.Close()
        'gvUserInfo.DataSource = ds
        'gvUserInfo.DataBind()

        gvCustomers.DataSource = ds
        gvCustomers.DataBind()

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

    
    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cmd As New SqlCommand("select Location_Code,Location_Code + ':' + Location_Name as Location from Location_master where plant_code='" & Session("plantCode") & "' order by Location_Code")
            Dim ddlLocationes As DropDownList = TryCast(e.Row.FindControl("ddlLocationes"), DropDownList)
            ddlLocationes.DataSource = Me.ExecuteQuery(cmd, "SELECT")
            ddlLocationes.DataTextField = "Location"
            ddlLocationes.DataValueField = "Location_Code"
            ddlLocationes.DataBind()
            Dim Location As String = TryCast(e.Row.FindControl("lblLocation"), Label).Text
            ddlLocationes.Items.FindByValue(Location).Selected = True
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cmd As New SqlCommand("select distinct hdn.range,hdn.description from Hamal_DL_Norms as hdn inner join Hamal_trans as ht on hdn.range=ht.dist_range where ht.plantcode='" & Session("plantCode") & "'")
            Dim ddldist_range As DropDownList = TryCast(e.Row.FindControl("ddldist_range"), DropDownList)
            ddldist_range.DataSource = Me.ExecuteQuery(cmd, "SELECT")
            ddldist_range.DataTextField = "description"
            ddldist_range.DataValueField = "range"
            ddldist_range.DataBind()
            Dim DistRn As String = TryCast(e.Row.FindControl("lbldist_range"), Label).Text
            'ddldist_range.Items.FindByValue(DistRn).Selected = True
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cmd As New SqlCommand("select distinct bag_weight from Hamal_trans where plantcode='" & Session("plantCode") & "'")
            Dim ddlBGWait As DropDownList = TryCast(e.Row.FindControl("ddlBGWait"), DropDownList)
            ddlBGWait.DataSource = Me.ExecuteQuery(cmd, "SELECT")
            ddlBGWait.DataTextField = "bag_weight"
            ddlBGWait.DataValueField = "bag_weight"
            ddlBGWait.DataBind()
            Dim BGWaitRn As String = TryCast(e.Row.FindControl("lblBGWait"), Label).Text
            ddlBGWait.Items.FindByValue(BGWaitRn).Selected = True
        End If


    End Sub
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

                                'Queries = "insert into hamal_trans (tran_date,crop_type,Matnr,activity_id,location,bag_weight,dist_range,loading,opening_feeding,unloading,sorting,restacking,stiching,stacking,bundle_preparation,weighing,loading_unloading,UNP_RECD_UNL_WT_STK,UNP_RECD_UNL_SRT_WT_STK,heightDiff,remark,supervisor_code,plantCode) Values (" & AllData(i).ToString().Remove(AllData(i).ToString.Length - 1, 1) & ")"


                                If HiddenFieldPC.Value = "1157" Then ''1157 is Vejulpur
                                    Queries = ("Update hamal_trans set location=" & ValDtl(7).ToString & ",dist_range=" & ValDtl(8).ToString & ",bag_weight=" & ValDtl(9).ToString & ",loading=" & ValDtl(10).ToString & ",unloading=" & ValDtl(11).ToString & ",sorting=" & ValDtl(12).ToString & ",stiching=" & ValDtl(13).ToString & ",stacking=" & ValDtl(14).ToString & ",restacking=" & ValDtl(15).ToString & ",weighing=" & ValDtl(16).ToString & ",bundle_preparation=" & ValDtl(17).ToString & ",opening_feeding=" & ValDtl(18).ToString & ",loading_unloading=" & ValDtl(19).ToString & ",unload_stack=" & ValDtl(20).ToString & ",UNP_RECD_UNL_WT_STK=" & ValDtl(21).ToString & ",UNP_RECD_UNL_SRT_WT_STK=" & ValDtl(22).ToString & ",DH_To_AC_STK=" & ValDtl(23).ToString & ",AC_To_DH_STK=" & ValDtl(24).ToString & ",LOADING_DH=" & ValDtl(25).ToString & ",UNLOADING_DH=" & ValDtl(26).ToString & ",Varai=" & ValDtl(27).ToString & ",remark=" & ValDtl(28).ToString & ",approval='approve' where rowid=" & ValDtl(0).ToString & " and plantCode='" & HiddenFieldPC.Value & "' and supervisor_code='" & cmbSupervisor.SelectedValue.ToString & "'")
                                Else
                                    Queries = ("Update hamal_trans set location=" & ValDtl(7).ToString & ",bag_weight=" & ValDtl(9).ToString & ",loading=" & ValDtl(10).ToString & ",unloading=" & ValDtl(11).ToString & ",sorting=" & ValDtl(12).ToString & ",stiching=" & ValDtl(13).ToString & ",stacking=" & ValDtl(14).ToString & ",restacking=" & ValDtl(15).ToString & ",weighing=" & ValDtl(16).ToString & ",bundle_preparation=" & ValDtl(17).ToString & ",opening_feeding=" & ValDtl(18).ToString & ",loading_unloading=" & ValDtl(19).ToString & ",unload_stack=" & ValDtl(20).ToString & ",UNP_RECD_UNL_WT_STK=" & ValDtl(21).ToString & ",UNP_RECD_UNL_SRT_WT_STK=" & ValDtl(22).ToString & ",DH_To_AC_STK=" & ValDtl(23).ToString & ",AC_To_DH_STK=" & ValDtl(24).ToString & ",LOADING_DH=" & ValDtl(25).ToString & ",UNLOADING_DH=" & ValDtl(26).ToString & ",Varai=" & ValDtl(27).ToString & ",remark=" & ValDtl(28).ToString & ",approval='approve' where rowid=" & ValDtl(0).ToString & " and plantCode='" & HiddenFieldPC.Value & "' and supervisor_code='" & cmbSupervisor.SelectedValue.ToString & "'")
                                End If

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
                            returnValue.Append("SaveData|Record Saved Successfully")
                        Else
                            returnValue.Append("SaveData|Record Not Saved Successfully")
                        End If

                        ''New Code End here

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

    Protected Sub gvCustomers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCustomers.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            con.Open()
            Dim ddl = DirectCast(e.Row.FindControl("ddlLocationes"), DropDownList)
            'Dim CountryId As Integer = Convert.ToInt32(e.Row.Cells(0).Text)
            Dim cmd As New SqlCommand("select Location_Code,Location_Code + ':' + Location_Name as Location from Location_master where plant_code='" & Session("plantCode") & "' order by Location_Code", con)
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()
            da.Fill(ds)
            con.Close()
            ddl.DataSource = ds
            ddl.DataTextField = "Location"
            ddl.DataValueField = "Location_Code"
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("--Select--", "0"))
        End If
    End Sub

    Protected Sub cmbSupervisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupervisor.SelectedIndexChanged
        BindGridview()
    End Sub
End Class
'Protected Sub btnGetData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetData.Click
'    'retrieves the selected rows and prints details on top of page.
'    'For Each row As GridViewRow In gvCustomers.Rows
'    '    Dim cb As CheckBox = CType(row.FindControl("CheckBox1"), CheckBox)
'    '    If cb IsNot Nothing AndAlso cb.Checked Then
'    '        Response.Write("Employee Id: " & gvCustomers.DataKeys(row.RowIndex).Value & " Name: " & row.Cells(2).Text & " " & row.Cells(3).Text & "<br/>")
'    '    End If
'    'Next row

'    'For Each row As GridViewRow In gvCustomers.Rows
'    '    Dim chk As CheckBox = DirectCast(row.FindControl("Yourcheckboxname"), CheckBox)
'    '    If chk.Checked Then
'    '        Dim id As String = gvCustomers.Rows(row.RowIndex).Cells(0).Text
'    '        'emp id    
'    '        'FIRSTNAME
'    '        Dim frstName As String = gvCustomers.Rows(row.RowIndex).Cells(1).Text
'    '    End If
'    'Next


'    For i As Integer = 0 To gvCustomers.Rows.Count - 1
'        Dim chkb As CheckBox = DirectCast(gvCustomers.Rows(i).Cells(0).FindControl("CheckBox1"), CheckBox)
'        If chkb.Checked Then
'            Dim name As String = gvCustomers.Rows(i).Cells(2).Text
'            Response.Write("<br>" + name)
'        End If
'    Next


'End Sub


'Protected Sub Update(ByVal sender As Object, ByVal e As EventArgs)
'    For Each row As GridViewRow In gvCustomers.Rows
'        If row.RowType = DataControlRowType.DataRow Then
'            Dim isChecked As Boolean = row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked
'            If isChecked Then
'                Dim cmd As New SqlCommand("UPDATE Customers SET ContactName = @ContactName, Country = @Country WHERE CustomerId = @CustomerId")
'                cmd.Parameters.AddWithValue("@ContactName", row.Cells(1).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
'                cmd.Parameters.AddWithValue("@Country", row.Cells(2).Controls.OfType(Of DropDownList)().FirstOrDefault().SelectedItem.Value)
'                cmd.Parameters.AddWithValue("@CustomerId", gvCustomers.DataKeys(row.RowIndex).Value)
'                Me.ExecuteQuery(cmd, "SELECT")
'            End If
'        End If
'    Next
'    btnUpdate.Visible = False
'    Me.BindGrid()
'End Sub

'Protected Sub OnCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
'Dim isUpdateVisible As Boolean = False
'Dim chk As CheckBox = TryCast(sender, CheckBox)
'If chk.ID = "chkAll" Then
'    For Each row As GridViewRow In gvCustomers.Rows
'        If row.RowType = DataControlRowType.DataRow Then
'            row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked = chk.Checked
'        End If
'    Next
'End If
'    Dim chkAll As CheckBox = TryCast(gvCustomers.HeaderRow.FindControl("chkAll"), CheckBox)
'    chkAll.Checked = True
'    For Each row As GridViewRow In gvCustomers.Rows
'        If row.RowType = DataControlRowType.DataRow Then
'            Dim isChecked As Boolean = row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked
'            For i As Integer = 1 To row.Cells.Count - 1
'                row.Cells(i).Controls.OfType(Of Label)().FirstOrDefault().Visible = Not isChecked
'                If row.Cells(i).Controls.OfType(Of TextBox)().ToList().Count > 0 Then
'                    row.Cells(i).Controls.OfType(Of TextBox)().FirstOrDefault().Visible = isChecked
'                End If
'                If row.Cells(i).Controls.OfType(Of DropDownList)().ToList().Count > 0 Then
'                    row.Cells(i).Controls.OfType(Of DropDownList)().FirstOrDefault().Visible = isChecked
'                End If
'                If isChecked AndAlso Not isUpdateVisible Then
'                    isUpdateVisible = True
'                End If
'                If Not isChecked Then
'                    chkAll.Checked = False
'                End If
'            Next
'        End If
'    Next
'    btnUpdate.Visible = isUpdateVisible
'End Sub



'Protected Sub btnSubmitDt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitDt.Click
'    'Dim i As Integer
'    'Dim valueInCell1 As String
'    'For i = 0 To gvUserInfo.Rows.Count - 1
'    '    If DirectCast(gvUserInfo.Rows(i).FindControl("chkSelect"), CheckBox).Checked() Then
'    '        Dim row As GridViewRow = gvUserInfo.Rows(i)
'    '        'call function to save the data in database...
'    '        valueInCell1 = row.Cells(0).Text
'    '    End If
'    'Next

'    Dim uPrimaryid() As Integer
'    Dim iCount As Integer = 0
'    For Each gvr As GridViewRow In gvUserInfo.Rows
'        If (CType(gvr.FindControl("CheckBox1"), CheckBox)).Checked = True Then
'            ReDim uPrimaryid(iCount)
'            uPrimaryid(iCount) = New Integer
'            uPrimaryid(iCount) = gvr.Cells("SN").Text
'            iCount += 1
'        End If
'    Next gvr



'End Sub

'Protected Sub gvUserInfo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvUserInfo.RowDataBound
'    If e.Row.RowType = DataControlRowType.DataRow Then
'        con.Open()
'        Dim ddl = DirectCast(e.Row.FindControl("ddllocation"), DropDownList)
'        'Dim CountryId As Integer = Convert.ToInt32(e.Row.Cells(0).Text)
'        Dim cmd As New SqlCommand("select Location_Code,Location_Code + ':' + Location_Name as Location from Location_master where plant_code='" & Session("plantCode") & "' order by Location_Code", con)
'        Dim da As New SqlDataAdapter(cmd)
'        Dim ds As New DataSet()
'        da.Fill(ds)
'        con.Close()
'        ddl.DataSource = ds
'        ddl.DataTextField = "Location"
'        ddl.DataValueField = "Location_Code"
'        ddl.DataBind()
'        ddl.Items.Insert(0, New ListItem("--Select--", "0"))
'    End If
'End Sub


'Private Sub BindGrid()
'    Dim cmd As New SqlCommand("select convert(varchar,tran_date,103) as tran_date,supervisor_code,activity_id,matnr,location,dist_range,bag_weight,loading,unloading,weighing,sorting,stiching,stacking,restacking,loading_unloading,UNP_RECD_UNL_WT_STK,UNP_RECD_UNL_SRT_WT_STK,bundle_preparation,DH_To_AC_STK,AC_To_DH_STK,LOADING_DH,UNLOADING_DH,opening_feeding,remark from hamal_trans where plantCode='" & Session("plantCode") & "' and approval='unapprove' and supervisor_code='" & Session("staffid") & "' and status='save' and tran_date between '2012-12-05' and '2012-12-10' order by tran_date")
'    gvCustomers.DataSource = Me.ExecuteQuery(cmd, "SELECT")
'    gvCustomers.DataBind()
'End Sub
