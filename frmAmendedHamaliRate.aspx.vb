Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Partial Class frmAmendedHamaliRate
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    Protected returnValue As New StringBuilder
    Dim PlantSelected As String = String.Empty
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


            ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019
            ModuleName = Page.GetType.Name.Split("_")("0")
            RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
            ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019



            ''Qry to redirect the user if he not have the rights / CSRF validation page wise added this on 12-11-2019
            If CSRFUK = Session("CSRFUKSIDNew") And AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) Then   ''This is for else actule live usage   '' Added And obj.AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) in  if on 23-12-2019 ''''Session("AuthToken").ToString.Equals(Request.Cookies("AuthToken").Value)  
                ''If CSRFUK = Session("CSRFUKSID") Then ''This is for else testing usage

                If Session("Role").ToString = "HeadOfficer" Or Session("Role").ToString = "Admin" Then   ''Role wise page Autherity option Added this Code on 08-11-2019


                    HiddenFieldPC.Value = Session("plantCode")

                    cmbP.Attributes.Add("OnChange", "FillCombo(this);FillComboActivity(this)")   ''This is for Contractors
                    'cmbContractor.Attributes.Add("OnChange", "FillComboActivity(this)")         ''This is for Activity
                    cmbContractor.Attributes.Add("OnChange", "DistComboActive(this)")            ''This is for Distance
                    cmbA.Attributes.Add("OnChange", "WeightComboActive(this)")                   ''This is for Weight
                    'cmbW.Attributes.Add("OnChange", "FillDataText(this)")
                    'cmbW.Attributes.Add("OnChange", "Existingdata(this)")



                    'If HiddenFieldPC.Value = "1157" Then ''1157 is Vejulpur
                    'cmbDist.Enabled = True
                    'Else
                    cmbDist.Enabled = False
                    '    cmbDist.SelectedValue = "Z"
                    'End If

                    Dim cbReference As String
                    cbReference = Page.ClientScript.GetCallbackEventReference(Me, "arg", "ReceiveServerData", "context")
                    Dim callbackScript As String = ""
                    callbackScript &= "function CallServer(arg, context) { " & cbReference & "} ;"
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", callbackScript, True)


                    If Not IsPostBack Then

                        ''THis  code is used for Request Flooding but used client side method so commented added on 12-05-2020
                        Dim num = getRandomNumber()
                        hdnRndNo.Value = num
                        Session("RandomNumber") = num
                        ''Code is ended here added on 12-05-2020 for request Flooding

                        '''''GetDataByQUERY("select distinct pm.plant_code,pm.plant_name from plant_master as pm inner join user_rights as ur on pm.plant_code=ur.plant_code where ur.emp_staffid='" & Session("staffid") & "' and ur.plant_code='" & Session("plantCode") & "' order by plant_name ", cmbP, "plant_name", "plant_code")
                        ''''GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_staffid='" & Session("staffid") & "' and emp_plantcode='" & Session("plantCode") & "' order by emp_plantnm ", cmbP, "plant_name", "plant_code")
                        GetDataByQUERY("select distinct emp_plantcode,emp_plantnm from hamali_user_master where emp_plantcode not in ('1152') order by emp_plantnm ", cmbP, "plant_name", "plant_code")
                        'GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master Order By activity", cmbA, "activity", "activity_id")
                        ''''GetDataByQUERY("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='" & Session("plantCode") & "' order by Contractor_Code", cmbContractor, "Contractor_Name", "Contractor_code")
                    End If



                Else ''Role wise page Autherity option Added this Code on 08-11-2019 and below line of code
                    ''MsgBox("You do not have the rights to access this Page / Form")
                    ''Response.Redirect("MenuForm.aspx")
                    ''Server.Transfer("MenuForm.aspx")
                    ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", "window.onload = function(){ alert('You do not have the rights to access this Page / Form');window.location='MenuPage.aspx'; }", True)
                End If  ''Role wise page Autherity option Added this Code on 08-11-2019

            Else ''Role wise page Autherity option Added this Code on 12-11-2019 and below line of code

                Response.Write("<script language='javascript'>window.alert('Be careful.... You do not have the rights to access this Page / Form');window.location='MenuPage.aspx';</script>")

                ''Page.ClientScript.RegisterStartupScript(Me.GetType(), "confirm", "<script type=text/javascript>confirm('You do not have the rights to access this Page / Form')</script>")
                ''Response.Redirect("Logout.aspx")
                ''Response.Write("<script>alert('You do not have the rights to access this Page / Form')</script>")
                ''Response.Write("<script>window.location.href='Logout.aspx';</script>")
                ''ClientScript.RegisterStartupScript(Page.GetType(), "Redirect", "window.onload = function(){ alert('You do not have the rights to access this Page / Form');window.location = 'Logout.aspx'; }", True)
                ''ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", "window.onload = function(){ alert('You do not have the rights to access this Page / Form');window.location='MenuPage.aspx'; }", True)
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
        Dim RAffected As Integer = 0
        If eventArgument Is Nothing Then
            returnValue.Append("-1")
        Else
            Try

                Dim s = eventArgument.Split("#")

                Dim Val As String = s(0).ToString
                Val = Val.Remove(0, 4)

                ''MsgBox(s(0).ToString)
                ''MsgBox(s(1).ToString)

                ''''This is for contractor combo "cmbContractor" filling on cmbP onchang event

                If s(0).ToString.StartsWith("cmbP") Then


                    If s(1).ToString = "1156" Then
                        Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1156' order by Contractor_Code", "")
                        returnValue.Append("cmbContractor" + Val.ToString + "|")

                    ElseIf s(1).ToString = "1179" Then
                        Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1179' order by Contractor_Code", "")
                        returnValue.Append("cmbContractor" + Val.ToString + "|")

                    ElseIf s(1).ToString = "1181" Then
                        Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1181' order by Contractor_Code", "")
                        returnValue.Append("cmbContractor" + Val.ToString + "|")

                    ElseIf s(1).ToString = "1107" Then
                        Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1107' order by Contractor_Code", "")
                        returnValue.Append("cmbContractor" + Val.ToString + "|")

                    ElseIf s(1).ToString = "1108" Then
                        Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1108' order by Contractor_Code", "")
                        returnValue.Append("cmbContractor" + Val.ToString + "|")

                    ElseIf s(1).ToString = "1180" Then
                        Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1180' order by Contractor_Code", "")
                        returnValue.Append("cmbContractor" + Val.ToString + "|")

                    ElseIf s(1).ToString = "1157" Then
                        Call ShowData("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='1157' order by Contractor_Code", "")
                        returnValue.Append("cmbContractor" + Val.ToString + "|")

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
                If s(0).ToString.StartsWith("cmbP") Then
                    ''MsgBox("i am in cmbA")

                    If s(1).ToString = "1156" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003','ha007','ha008') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + Val.ToString + "|")

                    ElseIf s(1).ToString = "1179" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003','ha006') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + Val.ToString + "|")

                    ElseIf s(1).ToString = "1181" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + Val.ToString + "|")


                    ElseIf s(1).ToString = "1107" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + Val.ToString + "|")

                    ElseIf s(1).ToString = "1108" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + Val.ToString + "|")

                    ElseIf s(1).ToString = "1180" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + Val.ToString + "|")


                    ElseIf s(1).ToString = "1157" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        Call ShowData("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003','ha004','ha005') Order By activity", "")
                        returnValue.Append("~" + "cmbA" + Val.ToString + "|")


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




                Select Case s(0).ToString


                    Case getCaseString(s(0).ToString, "Existingdata")
                        'If s(0).ToString.StartsWith("cmbW") Then

                        Dim AllDtl As String = s(1).ToString
                        ''AllDtl = Mid(AllDtl, 4)

                        Dim AllStrDtl() = AllDtl.Split(",")

                        Dim FromDate = AllStrDtl(0).ToString
                        Dim Todate = AllStrDtl(1).ToString

                        Dim Result As String = String.Empty
                        Dim FDt As String = String.Empty
                        Dim TDt As String = String.Empty
                        Dim RowID As String = String.Empty
                        Dim RowIdDtl As String = String.Empty

                        If FromDate.ToString <> "' '" And Todate.ToString <> "' '" And AllStrDtl(2).ToString <> "'0'" And AllStrDtl(3).ToString <> "'0'" And AllStrDtl(4).ToString <> "'0'" And AllStrDtl(5).ToString <> "' '" And AllStrDtl(6).ToString <> "'0'" Then

                            If AllStrDtl(2).ToString = "'1157'" Then     ''If Session("plantCode")AllStrDtl(2).ToString = "1157" Then

                                If Checking("select a.* from hamal_rate_master a where a.FromDateRang = convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and a.ToDateRang =convert(smalldatetime,convert(varchar(10)," & Todate & ",102)) and a.plantCode=" & AllStrDtl(2) & " and a.contractor_code=" & AllStrDtl(3) & " and a.activity=" & AllStrDtl(4) & " and a.distance=" & AllStrDtl(5) & " and a.weight=" & AllStrDtl(6) & "and a.rowid = (select max(b.rowid)from hamal_rate_master b where b.FromDateRang = convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and   b.ToDateRang =convert(smalldatetime,convert(varchar(10)," & Todate & ",102))  and b.plantCode=" & AllStrDtl(2) & " and b.contractor_code=" & AllStrDtl(3) & " and b.activity=" & AllStrDtl(4) & " and b.distance=" & AllStrDtl(5) & " and b.weight=" & AllStrDtl(6) & " )") = True Then

                                    ''Below code is add on 29-3-2013 for getting from date and to date of existing data recode
                                    Call ShowData("select a.* from hamal_rate_master a where a.FromDateRang = convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and  a.ToDateRang =convert(smalldatetime,convert(varchar(10)," & Todate & ",102))  and  a.plantCode=" & AllStrDtl(2) & " and a.contractor_code=" & AllStrDtl(3) & " and a.activity=" & AllStrDtl(4) & " and a.distance=" & AllStrDtl(5) & " and a.weight=" & AllStrDtl(6) & " and a.rowid = (select max(b.rowid)from hamal_rate_master b where b.FromDateRang = convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and   b.ToDateRang =convert(smalldatetime,convert(varchar(10)," & Todate & ",102))  and b.plantCode=" & AllStrDtl(2) & " and b.contractor_code=" & AllStrDtl(3) & " and b.activity=" & AllStrDtl(4) & " and b.distance=" & AllStrDtl(5) & " and b.weight=" & AllStrDtl(6) & " )", "")

                                    If DataDr.HasRows Then
                                        While DataDr.Read()
                                            FDt = DataDr(1)
                                            TDt = DataDr(2)
                                            RowID = DataDr(0)
                                            RowIdDtl = RowIdDtl + "-" + RowID
                                        End While
                                    End If

                                    DataDr.Close()
                                    DataDr = Nothing
                                    ''Below code is add on 29-3-2013 for getting from date and to date of existing data recode end here

                                    Result = True
                                    ClientScript.RegisterClientScriptBlock(Page.GetType, "AlertScript", "<script>alert('Rate is already Define for the selected date');</script>")
                                End If

                            Else

                                If Checking("select a.* from hamal_rate_master a where a.FromDateRang = convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and   a.ToDateRang =convert(smalldatetime,convert(varchar(10)," & Todate & ",102))  and  a.plantCode=" & AllStrDtl(2) & " and a.contractor_code=" & AllStrDtl(3) & " and a.activity=" & AllStrDtl(4) & " and a.weight=" & AllStrDtl(6) & " and a.rowid = (select max(b.rowid)from hamal_rate_master b where b.FromDateRang = convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and   b.ToDateRang =convert(smalldatetime,convert(varchar(10)," & Todate & ",102))  and b.plantCode=" & AllStrDtl(2) & " and b.contractor_code=" & AllStrDtl(3) & " and b.activity=" & AllStrDtl(4) & " and b.weight=" & AllStrDtl(6) & " ) ") = True Then

                                    ''Below code is add on 29-3-2013 for getting from date and to date of existing data recode
                                    Call ShowData("select a.* from hamal_rate_master a where a.FromDateRang = convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and a.ToDateRang =convert(smalldatetime,convert(varchar(10)," & Todate & ",102))  and  a.plantCode=" & AllStrDtl(2) & " and a.contractor_code=" & AllStrDtl(3) & " and a.activity=" & AllStrDtl(4) & " and a.weight=" & AllStrDtl(6) & " and a.rowid = (select max(b.rowid)from hamal_rate_master b where b.FromDateRang = convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and   b.ToDateRang =convert(smalldatetime,convert(varchar(10)," & Todate & ",102))  and b.plantCode=" & AllStrDtl(2) & " and b.contractor_code=" & AllStrDtl(3) & " and b.activity=" & AllStrDtl(4) & " and b.weight=" & AllStrDtl(6) & " ) ", "")

                                    If DataDr.HasRows Then
                                        While DataDr.Read()
                                            FDt = DataDr(1)
                                            TDt = DataDr(2)
                                            RowID = DataDr(0)
                                            RowIdDtl = RowIdDtl + "-" + RowID
                                        End While
                                    End If

                                    DataDr.Close()
                                    DataDr = Nothing
                                    ''Below code is add on 29-3-2013 for getting from date and to date of existing data recode end here

                                    Result = True
                                    ''ClientScript.RegisterClientScriptBlock(Page.GetType, "AlertScript", "<script>alert('Rate is already Define for the selected date');</script>")
                                End If

                            End If



                            If Result.ToString = "True" Then
                                ResultStatus = True
                                returnValue.Append("Existingdata|Rate is already Define for the period Start " + FDt + " To End " + TDt + "")
                            Else
                                ResultStatus = False
                                returnValue.Append("Existingdata|Need to Enter Rate, Rate is Not Define, For New Rate Entry Use Hamali Rate Form")
                            End If

                        End If
                        'End If
                        ''New Code End here

                        

                    Case getCaseString(s(0).ToString, "SaveData")


                        If hdnRndNo.Value = Session("RandomNumber") Then ''// code is added on 12-05-2020 For Request Flooding Issue Start here using existing code too

                            ''new coe add for multiple rows addition work fine but lenghty

                            ''Dim ClosingPara() = s(0).ToString.Split("|")
                            Dim Result As String = String.Empty
                            Dim ResultUpdate As String = String.Empty

                            Dim i As Integer
                            Dim AllData() = s(1).ToString.Split("~")


                            For i = 0 To AllData.Length - 2

                                ''New code to update data asdd on 16-8-2013
                                If AllData(i).ToString <> Nothing Then


                                    Dim ValDtl() = AllData(i).ToString.Split(",")

                                    If ValDtl(7).ToString <> "" Then

                                        Dim Queries As String = String.Empty
                                        Dim QueriesUpdate As String = String.Empty

                                        Queries = ("insert into Hamal_Rate_Master_Amend select *,getdate(),'" & Session("staffid") & "' from Hamal_Rate_Master where rowid in(" & ValDtl(7).ToString & ") ")

                                        Queries += ("Update Hamal_Rate_Master set FromDateRang=" & ValDtl(0).ToString & ",ToDateRang=" & ValDtl(1).ToString & ",plantCode=" & ValDtl(2).ToString & ",contractor_code=" & ValDtl(3).ToString & ",activity=" & ValDtl(4).ToString & ",distance=" & ValDtl(5).ToString & ",weight=" & ValDtl(6).ToString & ",loading=" & ValDtl(8).ToString & ",unloading=" & ValDtl(9).ToString & ",sorting=" & ValDtl(10).ToString & ",stiching=" & ValDtl(11).ToString & ",stacking=" & ValDtl(12).ToString & ",restacking=" & ValDtl(13).ToString & ",weighing=" & ValDtl(14).ToString & ",bundle_preparation=" & ValDtl(15).ToString & ",opening_feeding=" & ValDtl(16).ToString & ",loading_unloading=" & ValDtl(17).ToString & ",unload_stack=" & ValDtl(18).ToString & ",Unp_Recd_Unl_Wt_Stk=" & ValDtl(19).ToString & ",Unp_Recd_Unl_Srt_Wt_Stk=" & ValDtl(20).ToString & ",Dh_To_Ac_Stk=" & ValDtl(21).ToString & ",Ac_To_Dh_Stk=" & ValDtl(22).ToString & ",Loading_Dh=" & ValDtl(23).ToString & ",UnLoading_Dh=" & ValDtl(24).ToString & ",Varai=" & ValDtl(25).ToString & ",Stitch_Stack_Plant_OP_Spc_Dry=" & ValDtl(26).ToString & ",creationdate=getdate(),flag= flag + 1 where rowid=" & ValDtl(7).ToString & "")  ''and plantCode='" & HiddenFieldPC.Value & "'

                                        Queries = Queries & "~"

                                        If SaveDataWithTransaction(Queries, "~") = True Then
                                            Result = True
                                        Else
                                            Result = False
                                        End If

                                        'If Result = True Then

                                        '    QueriesUpdate = ("Update Hamal_Rate_Master set FromDateRang=" & ValDtl(0).ToString & ",ToDateRang=" & ValDtl(1).ToString & ",plantCode=" & ValDtl(2).ToString & ",contractor_code=" & ValDtl(3).ToString & ",activity=" & ValDtl(4).ToString & ",distance=" & ValDtl(5).ToString & ",weight=" & ValDtl(6).ToString & ",loading=" & ValDtl(8).ToString & ",unloading=" & ValDtl(9).ToString & ",sorting=" & ValDtl(10).ToString & ",stiching=" & ValDtl(11).ToString & ",stacking=" & ValDtl(12).ToString & ",restacking=" & ValDtl(13).ToString & ",weighing=" & ValDtl(14).ToString & ",bundle_preparation=" & ValDtl(15).ToString & ",opening_feeding=" & ValDtl(16).ToString & ",loading_unloading=" & ValDtl(17).ToString & ",unload_stack=" & ValDtl(18).ToString & ",Unp_Recd_Unl_Wt_Stk=" & ValDtl(19).ToString & ",Unp_Recd_Unl_Srt_Wt_Stk=" & ValDtl(20).ToString & ",Dh_To_Ac_Stk=" & ValDtl(21).ToString & ",Ac_To_Dh_Stk=" & ValDtl(22).ToString & ",Loading_Dh=" & ValDtl(23).ToString & ",UnLoading_Dh=" & ValDtl(24).ToString & ",Varai=" & ValDtl(25).ToString & ",creationdate=getdate(),flag= flag + 1 where rowid=" & ValDtl(7).ToString & "")  ''and plantCode='" & HiddenFieldPC.Value & "'

                                        '    QueriesUpdate = QueriesUpdate & "~"

                                        '    If SaveDataWithTransaction(QueriesUpdate, "~") = True Then
                                        '        ResultUpdate = True

                                        '    Else
                                        '        ResultUpdate = False
                                        '    End If
                                        'End If

                                        'End If

                                    Else
                                        ClientScript.RegisterClientScriptBlock(Page.GetType, "AlertScript", "<script>alert('There is now Record to update check and Verify Entry');</script>")
                                    End If
                                End If
                                ''End here 16-8-2013 code here

                            Next

                            If Result.ToString = "True" Then  ''And ResultUpdate.ToString = "True"
                                returnValue.Append("SaveData|Record Save / Update Successfully")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            Else
                                returnValue.Append("SaveData|Record Not Saved / Update Successfully")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            End If

                        Else ''This else is for Request Flooding Revalidation point else added this on 12-05-2020
                            returnValue.Append("SaveData|Threr is some issue , Kindly Update again.")
                            Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                        End If ''This  is end of if added on 12-05-2020


                End Select



                If s(0).ToString.StartsWith("FillDataText") Then

                    Dim ValFill As String = s(0).ToString
                    ValFill = ValFill.Remove(0, 12)

                    Dim AllDtl As String = s(1).ToString
                    'AllDtl = Mid(AllDtl, 4)

                    Dim AllStrDtl() = AllDtl.Split(",")

                    Dim FromDate = AllStrDtl(0).ToString
                    Dim Todate = AllStrDtl(1).ToString

                    PlantSelected = AllStrDtl(2).ToString

                    If ResultStatus.ToString = "True" Then

                        ''If PlantSelected.ToString = "'1157'" Then ''1157 is Vejulpur
                        ''    Call ShowData("select '0' + '~' + '0' + '~' + convert(varchar,Rowid,9) + '~' + convert(varchar,loading,9) + '~' + convert(varchar,unloading,9) + '~' + convert(varchar,sorting,9) + '~' + convert(varchar,stiching,9) + '~' + convert(varchar,stacking,9) + '~' + convert(varchar,restacking,9) + '~' + convert(varchar,weighing,9) + '~' + convert(varchar,bundle_preparation,9) + '~' + convert(varchar,opening_feeding,9) + '~' + convert(varchar,loading_unloading,9) + '~' + convert(varchar,unload_stack,9) + '~' + convert(varchar,UNP_RECD_UNL_WT_STK,9) + '~' + convert(varchar,UNP_RECD_UNL_SRT_WT_STK,9) + '~' + convert(varchar,DH_To_AC_STK,9) + '~' + convert(varchar,AC_To_DH_STK,9) + '~' + convert(varchar,LOADING_DH,9) + '~' + convert(varchar,UNLOADING_DH,9) + '~' + convert(varchar,Varai,9)  as AllStr from Hamal_Rate_Master where fromdaterang >=convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and todaterang <=convert(smalldatetime,convert(varchar(10)," & Todate & ",102)) and plantcode=" & AllStrDtl(2).ToString & " and activity=" & AllStrDtl(4).ToString & " and distance=" & AllStrDtl(5).ToString & " and weight=" & AllStrDtl(6).ToString & " and contractor_code=" & AllStrDtl(3).ToString & " ", "")
                        ''    'MsgBox("i am in cmbW")
                        ''    returnValue.Append("FillDataText" + ValFill.ToString + "|")

                        ''Else

                        Call ShowData("select '0' + '~' + '0' + '~' + convert(varchar,Rowid,9) + '~' + convert(varchar,loading,9) + '~' + convert(varchar,unloading,9) + '~' + convert(varchar,sorting,9) + '~' + convert(varchar,stiching,9) + '~' + convert(varchar,stacking,9) + '~' + convert(varchar,restacking,9) + '~' + convert(varchar,weighing,9) + '~' + convert(varchar,bundle_preparation,9) + '~' + convert(varchar,opening_feeding,9) + '~' + convert(varchar,loading_unloading,9) + '~' + convert(varchar,unload_stack,9) + '~' + convert(varchar,UNP_RECD_UNL_WT_STK,9) + '~' + convert(varchar,UNP_RECD_UNL_SRT_WT_STK,9) + '~' + convert(varchar,DH_To_AC_STK,9) + '~' + convert(varchar,AC_To_DH_STK,9) + '~' + convert(varchar,LOADING_DH,9) + '~' + convert(varchar,UNLOADING_DH,9) + '~' + convert(varchar,Varai,9) + '~' + convert(varchar,Stitch_Stack_Plant_OP_Spc_Dry,9)  as AllStr from Hamal_Rate_Master where (convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) between FromDateRang and ToDateRang or convert(smalldatetime,convert(varchar(10)," & Todate & ",102)) between FromDateRang and ToDateRang or FromDateRang between convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and convert(smalldatetime,convert(varchar(10)," & Todate & ",102)) or ToDateRang between convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and convert(smalldatetime,convert(varchar(10)," & Todate & ",102))) and plantcode=" & AllStrDtl(2).ToString & " and activity=" & AllStrDtl(4).ToString & " and distance=" & AllStrDtl(5).ToString & " and weight=" & AllStrDtl(6).ToString & " and contractor_code=" & AllStrDtl(3).ToString & " ", "")

                        ''''Call ShowData("select convert(varchar,loading,9) + '~' + convert(varchar,unloading,9) + '~' + convert(varchar,sorting,9) + '~' + convert(varchar,stiching,9) + '~' + convert(varchar,stacking,9) + '~' + convert(varchar,restacking,9) + '~' + convert(varchar,weighing,9) + '~' + convert(varchar,bundle_preparation,9) + '~' + convert(varchar,opening_feeding,9) + '~' + convert(varchar,loading_unloading,9) + '~' + convert(varchar,UNP_RECD_UNL_WT_STK,9) + '~' + convert(varchar,UNP_RECD_UNL_SRT_WT_STK,9) + '~' + convert(varchar,DH_To_AC_STK,9) + '~' + convert(varchar,AC_To_DH_STK,9) + '~' + convert(varchar,LOADING_DH,9) + '~' + convert(varchar,UNLOADING_DH,9) + '~' + convert(varchar,Varai,9) as AllStr from Hamal_Rate_Master where fromdaterang >=convert(smalldatetime,convert(varchar(10)," & FromDate & ",102)) and todaterang <=convert(smalldatetime,convert(varchar(10)," & Todate & ",102)) and plantcode=" & AllStrDtl(2).ToString & " and activity=" & AllStrDtl(4).ToString & " and distance=" & AllStrDtl(5).ToString & " and weight=" & AllStrDtl(6).ToString & " ", "")
                        ''''MsgBox("i am in else of cmbW")
                        returnValue.Append("FillDataText" + ValFill.ToString + "|")

                        ''End If

                        ''''' following code is needed to send the fetched data from above one of the case to the javascript
                        ''''If s(1).ToString <> "store" Then
                        ''''End If

                        If DataDr.HasRows Then
                            Dim cnt As Integer = 0
                            'returnValue.Append("0,Select,")

                            While DataDr.Read()
                                returnValue.Append((CType(DataDr(0), String)))
                            End While
                            ''returnValue.Remove((Len(returnValue.ToString()) - 0), 1)

                        End If

                        DataDr.Close()
                        con.Close()
                        ResultStatus = False
                    End If



                End If


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
End Class
