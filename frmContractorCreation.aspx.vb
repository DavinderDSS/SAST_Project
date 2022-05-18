Imports System.IO
Imports System.Data
Imports System
Imports System.Data.SqlClient
Partial Class frmContractorCreation
    Inherits System.Web.UI.Page
    Implements ICallbackEventHandler
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

                If Session("Role").ToString = "HeadOfficer" Or Session("Role").ToString = "Admin" Then   ''Role wise page Autherity option Added this Code on 08-11-2019
                    cmbPlantNm.Attributes.Add("OnChange", "FillCombo('cmbPlantNm')")


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

                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019
                        ModuleName = Page.GetType.Name.Split("_")("0")
                        RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019

                        GetDataByQUERYPlantNdCode("select distinct emp_plantnm from hamali_user_master order by emp_plantnm", cmbPlantNm, "emp_plantnm", "emp_plantnm")

                        ''GetDataByQUERYPlantNdCode("select distinct pm.plant_name from plant_master as pm inner join user_rights as ur on pm.plant_code=ur.plant_code order by pm.plant_name", cmbPlantNm, "plant_name", "plant_name")

                        ''''GetDataByQUERYPlantNdCode("select distinct pm.plant_code from plant_master as pm inner join user_rights as ur on pm.plant_code=ur.plant_code order by pm.plant_code", cmbPlantCode, "plant_code", "plant_code")
                        ''''GetDataByQUERY("select Location_Code,Location_Name from Location_master where plant_code='" & Session("plantCode") & "' order by Location_Code", cmbL, "Location_Name", "Location_Code")



                    End If


                    'txtCellInfo.Attributes.Add("onkeypress", "OnlyNumbers(event,'Int')")
                    txtContractorNM.Attributes.Add("onblur", "ExistingUser()")
                    'txtEmpFName.Attributes.Add("onkeypress", "ExistingUser(event,'Char')")

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

        If eventArgument Is Nothing Then

            returnValue.Append("-1")

        Else
            Try
                Dim RAffected As Integer = 0
                Dim s() As String
                s = eventArgument.Split("#")

                If s(0).ToString.StartsWith("cmbPlantNm") Then
                    Call ShowData("select distinct emp_plantCode as PlantNm,emp_plantCode as PlantCD from hamali_user_master where emp_plantnm='" & s(1).ToString & "' order by emp_plantCode", "")
                    returnValue.Append("cmbPlantCode" + "|")

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




                Select Case s(0).ToString

                    Case "SaveData"

                        If hdnRndNo.Value = Session("RandomNumber") Then ''// code is added on 12-05-2020 For Request Flooding Issue Start here using existing code too

                            RAffected = SaveCmplntData(s(1).ToString())
                            'MsgBox(s(1).ToString())
                            If RAffected > 0 Then
                                returnValue.Append("SaveData|" & RAffected & " Record Inserted Successfully")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            Else
                                returnValue.Append("SaveData|Record Not Inserted")
                                Session("RandomNumber") = "" ''is used for Request Flooding Revalidation added this on 12-05-2020
                            End If

                            'Case "ExistingUser"

                            '    Dim EmpID As String
                            '    EmpID = s(1).ToString()



                            '    Call GetDataByDr("select Contractor_name from Hamali_Contractor_Master where Plant_Name=" & EmpID.ToString & " and Plant_Code="&&" and contractor_Name="&&" and Contractor_Code="&&" ")
                            '    If DataDr.HasRows Then
                            '        MsgBox("This user Is already present in User List")
                            '        'ClientScript.RegisterClientScriptBlock(Page.GetType, "AlertScript", "<script>alert('This user Is already present in User List');</script>")
                            '        'Dim Msg = InputBox("This user Is already present in User List", "Existing User")

                            '    End If
                            '    If con.State = Data.ConnectionState.Open Then
                            '        con.Close()
                            '        DataDr.Close()
                            '        DataDr = Nothing
                            '    End If

                            '    'Dim EmpCode As String
                            '    'EmpCode = txtEmpCode.Text
                            '    'Call ShowData("STP_ExistingUser", txtEmpCode.Text)
                            '    'If DataDr.HasRows Then
                            '    '    MsgBox("This user Is already present in User List")
                            '    '    txtEmpCode.Focus()
                            '    'End If

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

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If txtContractorCode.Text <> "" Then
                MsgBox("This user Is already present in User List")
            End If

        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
        End Try
    End Sub

    'Protected Sub cmbPlantNm_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPlantNm.SelectedIndexChanged
    '    GetDataByQUERYPlantNdCode("select distinct emp_plantCode,emp_plantnm from hamali_user_master where emp_plantnm='" & cmbPlantNm.SelectedItem.ToString & "' order by emp_plantCode", cmbPlantCode, "emp_plantCode", "emp_plantCode")

    'End Sub
End Class
