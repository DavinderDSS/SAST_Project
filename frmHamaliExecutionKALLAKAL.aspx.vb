Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Partial Class frmHamaliExecutionKALLAKAL
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

                ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019
                ModuleName = Page.GetType.Name.Split("_")("0")
                RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019


                If Session("Role").ToString = "Supervisor" Then   ''Role wise page Autherity option Added this Code on 08-11-2019
                    HiddenFieldSVID.Value = Session("staffid")
                    HiddenFieldPC.Value = Session("plantCode")

                    cmbT.Attributes.Add("OnChange", "FillCombo(this)")
                    cmbMatArt.Attributes.Add("OnChange", "TypeComboActive(this)")
                    'cmbW.Attributes.Add("OnChange", "FillDataText(this)")

                    cmbContractor.Attributes.Add("OnChange", "DistComboActive(this)") ''This is for Distance
                    cmbA.Attributes.Add("OnChange", "WeightComboActive(this)") ''This is for Weight


                    If HiddenFieldPC.Value = "1157" Then ''1157 is Vejulpur
                        cmbDist.Enabled = True
                    Else
                        cmbDist.Enabled = False
                        cmbDist.SelectedValue = "Z"
                    End If

                    Dim cbReference As String
                    cbReference = Page.ClientScript.GetCallbackEventReference(Me, "arg", "ReceiveServerData", "context")
                    Dim callbackScript As String = ""
                    callbackScript &= "function CallServer(arg, context) { " & cbReference & "} ;"
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", callbackScript, True)

                    If Not IsPostBack Then  ''added on 12-05-2020
                        ''THis  code is used for Request Flooding but used client side method so commented added on 12-05-2020
                        Dim num = getRandomNumber()
                        hdnRndNo.Value = num
                        Session("RandomNumber") = num
                        ''Code is ended here added on 12-05-2020 for request Flooding
                    End If ''added on 12-05-2020


                    'If Not IsPostBack Then

                    If Session("plantCode") = "1156" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003','ha007','ha008') Order By activity", cmbA, "activity", "activity_id")


                    ElseIf Session("plantCode") = "1179" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003','ha006') Order By activity", cmbA, "activity", "activity_id")


                    ElseIf Session("plantCode") = "1181" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", cmbA, "activity", "activity_id")



                    ElseIf Session("plantCode") = "1107" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", cmbA, "activity", "activity_id")


                    ElseIf Session("plantCode") = "1108" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", cmbA, "activity", "activity_id")


                    ElseIf Session("plantCode") = "1180" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003') Order By activity", cmbA, "activity", "activity_id")



                    ElseIf Session("plantCode") = "1157" Then
                        'below is added on 16-1-2013 for adding plantwise activity
                        GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master where activity_id in ('ha001','ha002','ha003','ha004','ha005') Order By activity", cmbA, "activity", "activity_id")



                    End If

                    ''GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master Order By activity", cmbA, "activity", "activity_id")

                    GetDataByQUERY("select Purpose_ID,Purpose from Hamal_Purpose_Master  where Purpose_ID not in('hp010','hp011') Order By Purpose", cmbP, "Purpose", "Purpose_ID")
                    GetDataByQUERY("select Location_Code,Location_Code + ':' + Location_Name as Location from Location_master where plant_code='" & Session("plantCode") & "' order by Location_Code", cmbL, "Location", "Location_Code")

                    GetDataByQUERY("select Contractor_Code,Contractor_Name from Hamali_Contractor_master where plant_code='" & Session("plantCode") & "' order by Contractor_Code", cmbContractor, "Contractor_Name", "Contractor_code")
                    'GetDataByQUERY("select Location_Code,Location_Name from Location_master where plant_code='" & Session("plantCode") & "' order by Location_Code", cmbL, "Location_Name", "Location_Code")



                    'End If
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
        Dim RAffected As Integer = 0
        If eventArgument Is Nothing Then
            returnValue.Append("-1")
        Else
            Try

                Dim s = eventArgument.Split("#")

                Dim Val As String = s(0).ToString
                Val = Val.Remove(0, 4)
                'Val = Val.Substring(4, Val.Length)

                ''MsgBox(s(0).ToString)
                ''MsgBox(s(1).ToString)

                If s(0).ToString.StartsWith("cmbT") Then


                    ''''******Code is commented on 11-07-2020 as changed the display data in crop and verify is from 01-01-2017 as Sagde sir suggested so added this
                    'If s(1).ToString = "cotton" Then
                    '    If s(2).ToString = "HALB" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,3) in ('113','114','115','141','151') and mtart='" & s(2).ToString & "' and ( substring(matnr,8,1) in ('B','R','S','K','Z','J','V','D','U','M','P') or substring(matnr,10,1) in ('B','R','S','K','Z','J','V','D','U','M','P') ) order by matkl", "")
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    ElseIf s(2).ToString = "ROH" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,3) in ('113','114','115','141','151') and mtart='" & s(2).ToString & "' and substring(matnr,8,1) in ('A','E') order by matkl", "")
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    ElseIf s(2).ToString = "FERT" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,3) in ('113','114','115','141','151') and mtart='" & s(2).ToString & "' and substring(matnr,8,1) in ('C') order by matkl", "")
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")

                    '    ElseIf s(2).ToString = "VERP" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    End If

                    'ElseIf s(1).ToString = "field" Then
                    '    If s(2).ToString = "HALB" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where  matnr like '1%' and left(matnr,3) not in ('113','114','115','141','151') and mtart='" & s(2).ToString & "'  and ( substring(matnr,8,1) in ('B','R','S','K','Z','J','V','D','U','M','P') or substring(matnr,10,1) in ('B','R','S','K','Z','J','V','D','U','M','P') ) order by matkl", "")
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    ElseIf s(2).ToString = "ROH" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where  matnr like '1%' and left(matnr,3) not in ('113','114','115','141','151') and mtart='" & s(2).ToString & "'  and substring(matnr,8,1) in ('A','E') order by matkl", "")
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    ElseIf s(2).ToString = "FERT" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where  matnr like '1%' and left(matnr,3) not in ('113','114','115','141','151') and mtart='" & s(2).ToString & "'  and substring(matnr,8,1) in ('C') order by matkl", "")
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")

                    '    ElseIf s(2).ToString = "VERP" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    End If

                    'ElseIf s(1).ToString = "vegetable" Then
                    '    If s(2).ToString = "HALB" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,1) in ('2','3') and mtart='" & s(2).ToString & "'  and ( substring(matnr,8,1) in ('B','R','S','K','Z','J','V','D','U','M','P') or substring(matnr,10,1) in ('B','R','S','K','Z','J','V','D','U','M','P') ) order by matkl", "")
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    ElseIf s(2).ToString = "ROH" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,1) in ('2','3') and mtart='" & s(2).ToString & "'  and substring(matnr,8,1) in ('A','E') order by matkl", "")
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    ElseIf s(2).ToString = "FERT" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,1) in ('2','3') and mtart='" & s(2).ToString & "'  and substring(matnr,8,1) in ('C') order by matkl", "")
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")

                    '    ElseIf s(2).ToString = "VERP" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    End If


                    'ElseIf s(1).ToString = "store" Then
                    '    '''''Call ShowData("select prod_code,prod_name from prod_master where left(prod_code,3) = '" & s(1).ToString() & "' order by prod_name", "")
                    '    ''Call ShowData("Select distinct m.matnr,m.matdesc,m.matkl from matnr_master as m inner join norms_master n on m.matnr=n.matnr where m.matkl='555' and m.mtart='" & s(2).ToString & "' order by m.matkl", "")

                    '    If s(2).ToString = "HALB" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555' order by matkl", "")    ''''and mtart='" & s(2).ToString & "'
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    ElseIf s(2).ToString = "ROH" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    ElseIf s(2).ToString = "FERT" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    ElseIf s(2).ToString = "VERP" Then
                    '        Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'
                    '        returnValue.Append("cmbKNV" + Val.ToString + "|")
                    '    End If

                    ''''********Coe is ended here commneted on 11-07-2020 as sagade sir suggested it and added below code


                    If s(1).ToString = "cotton" Then
                        If s(2).ToString = "HALB" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,3) in ('113','114','115','141','151') and mtart='" & s(2).ToString & "' and ( substring(matnr,8,1) in ('B','R','S','K','Z','J','V','D','U','M','P') or substring(matnr,10,1) in ('B','R','S','K','Z','J','V','D','U','M','P') ) order by matkl", "") ''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm inner join Hamal_trans as ht on mm.matnr=ht.matnr and ht.tran_date >='2017-01-01' where left(mm.matnr,3) in ('113','114','115','141','151') and mm.mtart='" & s(2).ToString & "' and ( substring(mm.matnr,8,1) in ('B','R','S','K','Z','J','V','D','U','M','P') or substring(mm.matnr,10,1) in ('B','R','S','K','Z','J','V','D','U','M','P') ) order by mm.matkl", "") ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        ElseIf s(2).ToString = "ROH" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,3) in ('113','114','115','141','151') and mtart='" & s(2).ToString & "' and substring(matnr,8,1) in ('A','E') order by matkl", "")''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm inner join Hamal_trans as ht on mm.matnr=ht.matnr and ht.tran_date >='2017-01-01' where left(mm.matnr,3) in ('113','114','115','141','151') and mm.mtart='" & s(2).ToString & "' and substring(mm.matnr,8,1) in ('A','E') order by mm.matkl", "") ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        ElseIf s(2).ToString = "FERT" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,3) in ('113','114','115','141','151') and mtart='" & s(2).ToString & "' and substring(matnr,8,1) in ('C') order by matkl", "")''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm inner join Hamal_trans as ht on mm.matnr=ht.matnr and ht.tran_date >='2017-01-01' where left(mm.matnr,3) in ('113','114','115','141','151') and mm.mtart='" & s(2).ToString & "' and substring(mm.matnr,8,1) in ('C') order by mm.matkl", "") ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")

                        ElseIf s(2).ToString = "VERP" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'  ''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm where mm.matkl='555'  order by mm.matkl", "")    ''''and mtart='" & s(2).ToString & "'  ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        End If

                        '''''Call ShowData("select prod_code,prod_name from prod_master where left(prod_code,3) = '" & s(1).ToString() & "' order by prod_name", "")
                        ''Call ShowData("Select distinct m.matnr,m.matdesc,m.matkl from matnr_master as m inner join norms_master n on m.matnr=n.matnr where m.matkl<='115' and m.matkl>='113' and m.mtart='" & s(2).ToString & "' order by m.matkl", "")
                        ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl<='115' and matkl>='113' and mtart='" & s(2).ToString & "' order by matkl", "")
                        ''returnValue.Append("cmbKNV" + Val.ToString + "|")
                        'End If

                    ElseIf s(1).ToString = "field" Then
                        If s(2).ToString = "HALB" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where  matnr like '1%' and left(matnr,3) not in ('113','114','115','141','151') and mtart='" & s(2).ToString & "'  and ( substring(matnr,8,1) in ('B','R','S','K','Z','J','V','D','U','M','P') or substring(matnr,10,1) in ('B','R','S','K','Z','J','V','D','U','M','P') ) order by matkl", "")''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm inner join Hamal_trans as ht on mm.matnr=ht.matnr and ht.tran_date >='2017-01-01' where  mm.matnr like '1%' and left(mm.matnr,3) not in ('113','114','115','141','151') and mm.mtart='" & s(2).ToString & "'  and ( substring(mm.matnr,8,1) in ('B','R','S','K','Z','J','V','D','U','M','P') or substring(mm.matnr,10,1) in ('B','R','S','K','Z','J','V','D','U','M','P') ) order by mm.matkl", "") ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        ElseIf s(2).ToString = "ROH" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where  matnr like '1%' and left(matnr,3) not in ('113','114','115','141','151') and mtart='" & s(2).ToString & "'  and substring(matnr,8,1) in ('A','E') order by matkl", "")''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm inner join Hamal_trans as ht on mm.matnr=ht.matnr and ht.tran_date >='2017-01-01' where  mm.matnr like '1%' and left(mm.matnr,3) not in ('113','114','115','141','151') and mm.mtart='" & s(2).ToString & "'  and substring(mm.matnr,8,1) in ('A','E') order by mm.matkl", "") ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        ElseIf s(2).ToString = "FERT" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where  matnr like '1%' and left(matnr,3) not in ('113','114','115','141','151') and mtart='" & s(2).ToString & "'  and substring(matnr,8,1) in ('C') order by matkl", "")''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm inner join Hamal_trans as ht on mm.matnr=ht.matnr and ht.tran_date >='2017-01-01' where  mm.matnr like '1%' and left(mm.matnr,3) not in ('113','114','115','141','151') and mm.mtart='" & s(2).ToString & "'  and substring(mm.matnr,8,1) in ('C') order by mm.matkl", "") ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")

                        ElseIf s(2).ToString = "VERP" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'  ''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm where mm.matkl='555'  order by mm.matkl", "")    ''''and mtart='" & s(2).ToString & "'  ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        End If


                        '''''Call ShowData("select prod_code,prod_name from prod_master where left(prod_code,3) = '" & s(1).ToString() & "' order by prod_name", "")
                        ''Call ShowData("Select distinct m.matnr,m.matdesc,m.matkl from matnr_master as m inner join norms_master n on m.matnr=n.matnr where m.matkl<='112' or (m.matkl<='199' and matkl>='116') and m.mtart='" & s(2).ToString & "' order by m.matkl", "")
                        ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl<='112' or (matkl<='199' and matkl>='116') and mtart='" & s(2).ToString & "' order by matkl", "")
                        ''returnValue.Append("cmbKNV" + Val.ToString + "|")
                        'End If

                    ElseIf s(1).ToString = "vegetable" Then

                        If s(2).ToString = "HALB" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,1) in ('2','3') and mtart='" & s(2).ToString & "'  and ( substring(matnr,8,1) in ('B','R','S','K','Z','J','V','D','U','M','P') or substring(matnr,10,1) in ('B','R','S','K','Z','J','V','D','U','M','P') ) order by matkl", "")''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm inner join Hamal_trans as ht on mm.matnr=ht.matnr and ht.tran_date >='2017-01-01' where left(mm.matnr,1) in ('2','3') and mm.mtart='" & s(2).ToString & "'  and ( substring(mm.matnr,8,1) in ('B','R','S','K','Z','J','V','D','U','M','P') or substring(mm.matnr,10,1) in ('B','R','S','K','Z','J','V','D','U','M','P') ) order by mm.matkl", "") ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        ElseIf s(2).ToString = "ROH" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,1) in ('2','3') and mtart='" & s(2).ToString & "'  and substring(matnr,8,1) in ('A','E') order by matkl", "")''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm inner join Hamal_trans as ht on mm.matnr=ht.matnr and ht.tran_date >='2017-01-01' where left(mm.matnr,1) in ('2','3') and mm.mtart='" & s(2).ToString & "'  and substring(mm.matnr,8,1) in ('A','E') order by mm.matkl", "") ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        ElseIf s(2).ToString = "FERT" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where left(matnr,1) in ('2','3') and mtart='" & s(2).ToString & "'  and substring(matnr,8,1) in ('C') order by matkl", "")''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm inner join Hamal_trans as ht on mm.matnr=ht.matnr and ht.tran_date >='2017-01-01' where left(mm.matnr,1) in ('2','3') and mm.mtart='" & s(2).ToString & "'  and substring(mm.matnr,8,1) in ('C') order by mm.matkl", "") ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")

                        ElseIf s(2).ToString = "VERP" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm where mm.matkl='555'  order by mm.matkl", "")    ''''and mtart='" & s(2).ToString & "'  ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        End If

                        '''''Call ShowData("select prod_code,prod_name from prod_master where left(prod_code,3) = '" & s(1).ToString() & "' order by prod_name", "")
                        ''Call ShowData("Select distinct m.matnr,m.matdesc,m.matkl from matnr_master as m inner join norms_master n on m.matnr=n.matnr where m.matkl>='200' and m.mtart='" & s(2).ToString & "' order by m.matkl", "")
                        ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl>='200' and mtart='" & s(2).ToString & "' order by matkl", "")
                        ''returnValue.Append("cmbKNV" + Val.ToString + "|")
                        'End If


                    ElseIf s(1).ToString = "store" Then

                        If s(2).ToString = "HALB" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm where mm.matkl='555'  order by mm.matkl", "")    ''''and mtart='" & s(2).ToString & "'  ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        ElseIf s(2).ToString = "ROH" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm where mm.matkl='555'  order by mm.matkl", "")    ''''and mtart='" & s(2).ToString & "'  ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        ElseIf s(2).ToString = "FERT" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm where mm.matkl='555'  order by mm.matkl", "")    ''''and mtart='" & s(2).ToString & "'  ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        ElseIf s(2).ToString = "VERP" Then
                            ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'''Commented this on 11-07-2020 as SAgade sir suggested to hide the crop lsit before 01-01-2017 so added below code
                            Call ShowData("Select distinct mm.matnr,mm.matdesc,mm.matkl from matnr_master as mm where mm.matkl='555'  order by mm.matkl", "")    ''''and mtart='" & s(2).ToString & "'  ''Added this on 11-07-2020 as Sagade sir want this
                            returnValue.Append("cmbKNV" + Val.ToString + "|")
                        End If


                        '''''Call ShowData("select prod_code,prod_name from prod_master where left(prod_code,3) = '" & s(1).ToString() & "' order by prod_name", "")
                        ''Call ShowData("Select distinct m.matnr,m.matdesc,m.matkl from matnr_master as m inner join norms_master n on m.matnr=n.matnr where m.matkl='555' and m.mtart='" & s(2).ToString & "' order by m.matkl", "")
                        ''Call ShowData("Select distinct matnr,matdesc,matkl from matnr_master where matkl='555'  order by matkl", "")    ''''and mtart='" & s(2).ToString & "'
                        ''returnValue.Append("cmbKNV" + Val.ToString + "|")

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


                    If s(0).ToString.StartsWith("cmbW") Then

                        Dim AllDtl As String = s(1).ToString
                        AllDtl = Mid(AllDtl, 4)

                        Dim AllStrDtl() = AllDtl.Split(",")


                        If HiddenFieldPC.Value = "1157" Then ''1157 is Vejulpur
                            Call ShowData("select convert(varchar,loading,9) + '~' + convert(varchar,unloading,9) + '~' + convert(varchar,sorting,9) + '~' + convert(varchar,stiching,9) + '~' + convert(varchar,stacking,9) + '~' + convert(varchar,restacking,9) + '~' + convert(varchar,weighing,9) + '~' + convert(varchar,bundle_preparation,9) + '~' + convert(varchar,opening_feeding,9) + '~' + convert(varchar,loading_unloading,9) + '~' + convert(varchar,UNP_RECD_UNL_WT_STK,9) + '~' + convert(varchar,UNP_RECD_UNL_SRT_WT_STK,9) + '~' + convert(varchar,DH_To_AC_STK,9) + '~' + convert(varchar,AC_To_DH_STK,9) + '~' + convert(varchar,LOADING_DH,9) + '~' + convert(varchar,UNLOADING_DH,9) + '~' + convert(varchar,Varai,9) as AllStr from Hamal_Rate_Master where fromdaterang >=" & AllStrDtl(0).ToString & " and todaterang <=" & AllStrDtl(0).ToString & " and plantcode='" & HiddenFieldPC.Value & "' and activity=" & AllStrDtl(3).ToString & " and distance=" & AllStrDtl(5).ToString & " and weight=" & AllStrDtl(5).ToString & "", "")
                            'MsgBox("i am in cmbW")
                            returnValue.Append("cmbW" + Val.ToString + "|")

                        Else


                            Call ShowData("select convert(varchar,loading,9) + '~' + convert(varchar,unloading,9) + '~' + convert(varchar,sorting,9) + '~' + convert(varchar,stiching,9) + '~' + convert(varchar,stacking,9) + '~' + convert(varchar,restacking,9) + '~' + convert(varchar,weighing,9) + '~' + convert(varchar,bundle_preparation,9) + '~' + convert(varchar,opening_feeding,9) + '~' + convert(varchar,loading_unloading,9) + '~' + convert(varchar,UNP_RECD_UNL_WT_STK,9) + '~' + convert(varchar,UNP_RECD_UNL_SRT_WT_STK,9) + '~' + convert(varchar,DH_To_AC_STK,9) + '~' + convert(varchar,AC_To_DH_STK,9) + '~' + convert(varchar,LOADING_DH,9) + '~' + convert(varchar,UNLOADING_DH,9) + '~' + convert(varchar,Varai,9) as AllStr from Hamal_Rate_Master where fromdaterang >='2012-12-20' and todaterang <='2012-12-30' and plantcode='1107' and activity='ha012' and weight='15' ", "")
                            'MsgBox("i am in else of cmbW")
                            returnValue.Append("cmbW" + Val.ToString + "|")

                        End If

                        '' following code is needed to send the fetched data from above one of the case to the javascript
                        'If s(1).ToString <> "store" Then
                        'End If

                        If DataDr.HasRows Then
                            Dim cnt As Integer = 0
                            'returnValue.Append("0,Select,")

                            While DataDr.Read()
                                returnValue.Append((CType(DataDr(0), String)))
                            End While
                            returnValue.Remove((Len(returnValue.ToString()) - 1), 1)

                        End If

                        DataDr.Close()
                        con.Close()

                    End If


                    Select Case s(0).ToString


                        Case getCaseString(s(0).ToString, "SaveData")

                        If hdnRndNo.Value = Session("RandomNumber") Then ''// code is added on 12-05-2020 For Request Flooding Issue Start here using existing code too

                            ''new coe add for multiple rows addition work fine but lenghty

                            ''Dim ClosingPara() = s(0).ToString.Split("|")
                            Dim Result As String = String.Empty
                            Dim AllData() = s(1).ToString.Split("~")

                            Dim i As Integer
                            For i = 0 To AllData.Length - 2

                                Dim Queries As String = String.Empty

                                ''Queries = "insert into hamal_trans (tran_date,Contractor_code,crop_type,Matnr,activity_id,purpose_id,location,bag_weight,dist_range,loading,unloading,sorting,stiching,stacking,restacking,weighing,bundle_preparation,opening_feeding,loading_unloading,unload_stack,Unp_Recd_Unl_Wt_Stk,Unp_Recd_Unl_Srt_Wt_Stk,Dh_To_Ac_Stk,Ac_To_Dh_Stk,Loading_Dh,UnLoading_Dh,Varai,remark,supervisor_code,plantCode) Values (" & AllData(i).ToString().Remove(AllData(i).ToString.Length - 1, 1) & ")"

                                Queries = "insert into hamal_trans (tran_date,Contractor_code,crop_type,Matnr,activity_id,purpose_id,location,bag_weight,dist_range,loading,unloading,sorting,stiching,stacking,restacking,weighing,bundle_preparation,opening_feeding,unload_stack,Unp_Recd_Unl_Wt_Stk,Unp_Recd_Unl_Srt_Wt_Stk,Loading_Dh,UnLoading_Dh,Varai,remark,supervisor_code,plantCode) Values (" & AllData(i).ToString().Remove(AllData(i).ToString.Length - 1, 1) & ")"
                                Queries = Queries & "~"

                                If SaveDataWithTransaction(Queries, "~") = True Then
                                    Result = True
                                    'returnValue.Append("Record Saved Successfully")
                                Else
                                    Result = False
                                End If


                            Next
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


End Class
