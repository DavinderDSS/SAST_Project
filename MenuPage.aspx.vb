Imports System
Imports System.Guid
Imports System.IO
Imports System.Web.UI
Imports System.Web.Security  ''<!--Authenticated page accessible without authentication (GIS Bug) Added this line on 03-01-2020 -->
Partial Class MenuPage
    Inherits System.Web.UI.Page
    Public MenuList As Integer
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ''<!--Authenticated page accessible without authentication (GIS Bug) Added this line on 03-01-2020 -->
            If Not Me.Page.User.Identity.IsAuthenticated Then
                FormsAuthentication.RedirectToLoginPage()
            End If
            ''<!--Authenticated page accessible without authentication (GIS Bug) Added this line on 03-01-2020 -->

            Application_PreSendRequestHeaders() '''' This Should Add on every Page and this is added for server banner point 25-12-2019

            ''This is used to deactivate the browser back button on respective page added this on 13-05-2020
            ClientScript.RegisterClientScriptBlock(Page.GetType, "NOBACK", "<script>if(history.length>0)history.go(+1);</script>")
            ''Code is ended here added on 13-05-2020


            If CSRFUK = Nothing Or CSRFUK = "" Then

                Response.Write("<script language='javascript'>window.alert('Invalid Access of Page With Out Login');window.location='UserError.aspx';</script>")

            Else



                


                If Not IsPostBack Then

                    ''Qry to redirect the user if he not have the rights / CSRF validation page wise added this on 12-11-2019
                    If CSRFUK = Session("CSRFUKSIDNew") And AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) Then   ''This is for else actule live usage   '' Added And obj.AuthTokennDtl.ToString.Equals(Request.Cookies("AuthToken").Value) in  if on 23-12-2019 ''''Session("AuthToken").ToString.Equals(Request.Cookies("AuthToken").Value)  
                        ''If CSRFUK = Session("CSRFUKSID") Then ''This is for else testing usage

                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019
                        ModuleName = Page.GetType.Name.Split("_")("0")
                        RAffected = SaveCmplntData("Insert Into Hamali_User_Activity_AuditTrail(User_staffid,User_initial,emp_plantNm,emp_plantCode,emp_Role,AccessDateTime,SessionID,CSRFUserKey,CSRFUserKeyDtlCopy,UserIPAddressDtl,ModuleName) Values ('" & Session("staffid") & "', '" & Session("initial") & "','" & Session("plantName") & "','" & Session("plantCode") & "', '" & Session("Role") & "',getdate(),'" & Session("SID") & "','" & Session("CSRFUKSIDNew") & "','" & Session("CSRFUKSIDNew") & "', '" & Session("UserIPAddressDtl") & "', '" & ModuleName & "')")
                        ''To get the page details used this and this is for Audit trail issue detail point added this on 10-12-2019


                        'MenuList = Split(Session("Finalprog_id_Dtl"), "~")
                        'MenuList = Session("Role")
                        'Dim Role As String = String.Empty
                        'Dim Len As Integer = MenuList.Length

                        'If MenuList = "1" Then

                                                 Select Case CStr(Session("Role"))
                            Case ("HeadOfficer")

                                'lnkHamaliApproval.Visible = True
                                lnkHamaliBilling.Visible = False

                                lnkApproval.Visible = False
                                lnkBilling.Visible = False
                                'lnkHamaliExecution.Visible = True
                                lnkHoRptHamaliCount.Visible = True
                                lnkPoRptHamaliCount.Visible = False
                                lnkHoRptHamaliRate.Visible = True
                                lnkHoRptUserLogin.Visible = True
                                lnkHoRptHamaliApproveDisapprove.Visible = False
                                lnkHoRptSupervisorWiseHamaliDtl.Visible = True
                                lnkPoRptSupervisorWiseHamaliDtl.Visible = False
                                lnkSoRptSupervisorWiseHamaliDtl.Visible = False

                                lnkHoRptSectionActivityWiseHamaliDtl.Visible = True
                                lnkPoRptSectionActivityWiseHamaliDtl.Visible = False

                                lnkHoRptLocationActivityWiseHamaliDtl.Visible = True
                                lnkPoRptLocationActivityWiseHamaliDtl.Visible = False

                                lnkHoRptHamaliBilling.Visible = True
                                lnkHoRptHamaliMissingDataDetail.Visible = True
                                lnkApproval.Visible = False
                                lnkBilling.Visible = False
                                lnkExecution.Visible = False
                                lnkCount.Visible = True

                                lnkHOLevel.Visible = True
                                lnkPOLevel.Visible = False
                                lnkSOLevel.Visible = False

                                lnkNewUser.Visible = True
                                lnkAssignRole.Visible = True
                                lnkAssignLocation.Visible = True
                                lnkContractorCreation.Visible = True
                                lnkHamaliRate.Visible = True
                                lnkAmendedHamaliRate.Visible = True
                                ''lnkAmendedHamaliRateUsingGrid.Visible = False
                                lnkUser.Visible = True

                                ''This is true bydefault
                                'lnkChangePass.Visible = False
                                lnkContactUs.Visible = False
                                lnkLMBFAQs.Visible = False
                                lnkLMBflow.Visible = True


                                lnkHamaliApprovalDHANORA.Visible = False
                                lnkHamaliApprovalKALLAKAL.Visible = False

                                ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                'lnkHamaliApprovalKHANDWA.Visible = False
                                'lnkHamaliApprovalKOTA.Visible = False
                                'lnkHamaliApprovalVEJALPUR.Visible = False
                                'lnkHamaliApprovalKAMDOD.Visible = False
                                'lnkHamaliApprovalSARANGAPUR.Visible = False
                                ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here

                                lnkHamaliExecutionDHANORA.Visible = False
                                lnkHamaliExecutionKALLAKAL.Visible = False

                                ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                'lnkHamaliExecutionKHANDWA.Visible = False
                                'lnkHamaliExecutionKOTA.Visible = False
                                'lnkHamaliExecutionVEJALPUR.Visible = False
                                'lnkHamaliExecutionKAMDOD.Visible = False
                                'lnkHamaliExecutionSARANGAPUR.Visible = False
                                ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here

                                ''Added on 22-07-2014
                                lnkMatnr.Visible = True
                                lnkPlantWiseCropCreation.Visible = True
                                lnkMissingData.Visible = True
                                lnkMissingDataUpdation.Visible = True
                                ''ended here added on 22-07-2014

                                ''ADDED ON 29-07-2014
                                lnkHoRptAmendRateHamaliBilling.Visible = True
                                lnkAmendHamaliBilling.Visible = True
                                ''ENDED HERE ADDED ON 29-07-2014

                                ''Added on 18-09-2014
                                lnkPoRptPurposeActivityWiseHamaliDtl.Visible = False
                                lnkHoRptPurposeActivityWiseHamaliDtl.Visible = True
                                ''ENDED HERE ADDED ON 18-09-2014

                                ''Added on 19-02-2015
                                lnkHamaliEntryDeletion.Visible = True
                                ''Ended here added on 19-02-2015

                                ''Added on 8-12-2016
                                lnkUserInActivation.Visible = False
                                ''Ended here added on 8-12-2016


                            Case ("PlantOfficer")
                                Select Case CStr(Session("plantCode"))
                                    Case ("1107")  ''Khandwa
                                        lnkHamaliApprovalDHANORA.Visible = False
                                        lnkHamaliApprovalKALLAKAL.Visible = False

                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliApprovalKHANDWA.Visible = True
                                        'lnkHamaliApprovalKOTA.Visible = False
                                        'lnkHamaliApprovalVEJALPUR.Visible = False
                                        'lnkHamaliApprovalKAMDOD.Visible = False
                                        'lnkHamaliApprovalSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here

                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = False


                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = True
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here




                                    Case ("1108")  ''Kota
                                        lnkHamaliApprovalDHANORA.Visible = False
                                        lnkHamaliApprovalKALLAKAL.Visible = False

                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliApprovalKHANDWA.Visible = False
                                        'lnkHamaliApprovalKOTA.Visible = True
                                        'lnkHamaliApprovalVEJALPUR.Visible = False
                                        'lnkHamaliApprovalKAMDOD.Visible = False
                                        'lnkHamaliApprovalSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here

                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = False


                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = True
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here

                                    Case ("1156")  ''Dhanora
                                        lnkHamaliApprovalDHANORA.Visible = True
                                        lnkHamaliApprovalKALLAKAL.Visible = False

                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliApprovalKHANDWA.Visible = False
                                        'lnkHamaliApprovalKOTA.Visible = False
                                        'lnkHamaliApprovalVEJALPUR.Visible = False
                                        'lnkHamaliApprovalKAMDOD.Visible = False
                                        'lnkHamaliApprovalSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here

                                        lnkHamaliExecutionDHANORA.Visible = True
                                        lnkHamaliExecutionKALLAKAL.Visible = False


                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here

                                    Case ("1157")  ''Vejalpur
                                        lnkHamaliApprovalDHANORA.Visible = False
                                        lnkHamaliApprovalKALLAKAL.Visible = False

                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliApprovalKHANDWA.Visible = False
                                        'lnkHamaliApprovalKOTA.Visible = False
                                        'lnkHamaliApprovalVEJALPUR.Visible = True
                                        'lnkHamaliApprovalKAMDOD.Visible = False
                                        'lnkHamaliApprovalSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here

                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = False


                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = True
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here


                                    Case ("1179")  ''Kallakal
                                        lnkHamaliApprovalDHANORA.Visible = False
                                        lnkHamaliApprovalKALLAKAL.Visible = True

                                        'lnkHamaliApprovalKHANDWA.Visible = False
                                        'lnkHamaliApprovalKOTA.Visible = False
                                        'lnkHamaliApprovalVEJALPUR.Visible = False
                                        'lnkHamaliApprovalKAMDOD.Visible = False
                                        'lnkHamaliApprovalSARANGAPUR.Visible = False

                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = True

                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False

                                    Case ("1180")  ''Sarangapur
                                        lnkHamaliApprovalDHANORA.Visible = False
                                        lnkHamaliApprovalKALLAKAL.Visible = False

                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliApprovalKHANDWA.Visible = False
                                        'lnkHamaliApprovalKOTA.Visible = False
                                        'lnkHamaliApprovalVEJALPUR.Visible = False
                                        'lnkHamaliApprovalKAMDOD.Visible = False
                                        'lnkHamaliApprovalSARANGAPUR.Visible = True
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here

                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = False


                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = True
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here


                                    Case ("1181")  ''Kamdod
                                        lnkHamaliApprovalDHANORA.Visible = False
                                        lnkHamaliApprovalKALLAKAL.Visible = False

                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliApprovalKHANDWA.Visible = False
                                        'lnkHamaliApprovalKOTA.Visible = False
                                        'lnkHamaliApprovalVEJALPUR.Visible = False
                                        'lnkHamaliApprovalKAMDOD.Visible = True
                                        'lnkHamaliApprovalSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here

                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = False

                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = True
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here


                                End Select



                                'lnkHamaliApproval.Visible = True
                                lnkHamaliBilling.Visible = True
                                lnkApproval.Visible = True
                                lnkBilling.Visible = True

                                'lnkHamaliExecution.Visible = True
                                lnkExecution.Visible = True
                                lnkHoRptHamaliCount.Visible = False
                                lnkPoRptHamaliCount.Visible = True
                                lnkHoRptHamaliRate.Visible = False
                                lnkHoRptUserLogin.Visible = False
                                lnkHoRptHamaliApproveDisapprove.Visible = False
                                lnkHoRptSupervisorWiseHamaliDtl.Visible = False
                                lnkPoRptSupervisorWiseHamaliDtl.Visible = True
                                lnkSoRptSupervisorWiseHamaliDtl.Visible = False

                                lnkHoRptSectionActivityWiseHamaliDtl.Visible = False
                                lnkPoRptSectionActivityWiseHamaliDtl.Visible = True

                                lnkHoRptLocationActivityWiseHamaliDtl.Visible = False
                                lnkPoRptLocationActivityWiseHamaliDtl.Visible = False

                                lnkHoRptHamaliBilling.Visible = False
                                lnkHoRptHamaliMissingDataDetail.Visible = False
                                lnkHamaliRate.Visible = False
                                lnkAmendedHamaliRate.Visible = False
                                ''lnkAmendedHamaliRateUsingGrid.Visible = False
                                lnkUser.Visible = False
                                lnkCount.Visible = False
                                lnkHOLevel.Visible = False
                                lnkPOLevel.Visible = True
                                lnkSOLevel.Visible = False
                                lnkNewUser.Visible = False
                                lnkAssignRole.Visible = False
                                lnkAssignLocation.Visible = False
                                lnkContractorCreation.Visible = False
                                lnkCount.Visible = True




                                ''This is true bydefault
                                'lnkChangePass.Visible = False
                                lnkContactUs.Visible = False
                                lnkLMBFAQs.Visible = False
                                lnkLMBflow.Visible = True

                                ''Added on 22-07-2014
                                lnkMatnr.Visible = True
                                lnkPlantWiseCropCreation.Visible = True
                                lnkMissingData.Visible = True
                                lnkMissingDataUpdation.Visible = True
                                ''ended here added on 22-07-2014
                                ''ADDED ON 29-07-2014
                                lnkHoRptAmendRateHamaliBilling.Visible = True
                                lnkAmendHamaliBilling.Visible = True
                                ''ENDED HERE ADDED ON 29-07-2014
                                ''Added on 18-09-2014
                                lnkPoRptPurposeActivityWiseHamaliDtl.Visible = True
                                lnkHoRptPurposeActivityWiseHamaliDtl.Visible = False
                                ''ENDED HERE ADDED ON 18-09-2014

                                'lnkHamaliExecutionDHANORA.Visible = False
                                'lnkHamaliExecutionKHANDWA.Visible = False
                                'lnkHamaliExecutionKOTA.Visible = False
                                'lnkHamaliExecutionVEJALPUR.Visible = False
                                'lnkHamaliExecutionKAMDOD.Visible = False
                                'lnkHamaliExecutionKALLAKAL.Visible = False
                                'lnkHamaliExecutionSARANGAPUR.Visible = False

                                ''Added on 19-02-2015
                                lnkHamaliEntryDeletion.Visible = False
                                ''Ended here added on 19-02-2015

                                ''Added on 8-12-2016
                                lnkUserInActivation.Visible = True
                                ''Ended here added on 8-12-2016

                            Case ("Supervisor")

                                Select Case CStr(Session("plantCode"))
                                    Case ("1107")  ''Khandwa
                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = False


                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = True
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here


                                    Case ("1108")  ''Kota
                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = False


                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = True
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here


                                        'Case ("1152")  ''Dawalwadi
                                        '    lnkHamaliExecutionDHANORA.Visible = False
                                        '    lnkHamaliExecutionKHANDWA.Visible = False
                                        '    lnkHamaliExecutionKOTA.Visible = False
                                        '    lnkHamaliExecutionVEJALPUR.Visible = False
                                        '    lnkHamaliExecutionKAMDOD.Visible = False
                                        '    lnkHamaliExecutionKALLAKAL.Visible = False
                                        '    lnkHamaliExecutionSARANGAPUR.Visible = False
                                    Case ("1156")  ''Dhanora
                                        lnkHamaliExecutionDHANORA.Visible = True
                                        lnkHamaliExecutionKALLAKAL.Visible = False


                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here


                                    Case ("1157")  ''Vejalpur
                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = False


                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = True
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here


                                    Case ("1179")  ''Kallakal
                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = True

                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False

                                    Case ("1180")  ''Sarangapur
                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = False


                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = False
                                        'lnkHamaliExecutionSARANGAPUR.Visible = True
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here


                                    Case ("1181")  ''Kamdod
                                        lnkHamaliExecutionDHANORA.Visible = False
                                        lnkHamaliExecutionKALLAKAL.Visible = False


                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020
                                        'lnkHamaliExecutionKHANDWA.Visible = False
                                        'lnkHamaliExecutionKOTA.Visible = False
                                        'lnkHamaliExecutionVEJALPUR.Visible = False
                                        'lnkHamaliExecutionKAMDOD.Visible = True
                                        'lnkHamaliExecutionSARANGAPUR.Visible = False
                                        ''Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here


                                End Select


                                'lnkHamaliApproval.Visible = False
                                lnkHamaliBilling.Visible = True


                                lnkBilling.Visible = False
                                lnkBilling.Visible = False
                                'lnkHamaliExecution.Visible = True
                                lnkHoRptHamaliCount.Visible = False
                                lnkPoRptHamaliCount.Visible = False
                                lnkHoRptHamaliRate.Visible = False
                                lnkHoRptUserLogin.Visible = False
                                lnkHoRptHamaliApproveDisapprove.Visible = False
                                lnkHoRptSupervisorWiseHamaliDtl.Visible = False
                                lnkPoRptSupervisorWiseHamaliDtl.Visible = False
                                lnkSoRptSupervisorWiseHamaliDtl.Visible = True
                                lnkHoRptSectionActivityWiseHamaliDtl.Visible = False
                                lnkPoRptSectionActivityWiseHamaliDtl.Visible = False
                                lnkHoRptLocationActivityWiseHamaliDtl.Visible = False
                                lnkPoRptLocationActivityWiseHamaliDtl.Visible = False

                                lnkHoRptHamaliBilling.Visible = False
                                lnkHoRptHamaliMissingDataDetail.Visible = False
                                lnkHamaliRate.Visible = False
                                lnkAmendedHamaliRate.Visible = False
                                ''lnkAmendedHamaliRateUsingGrid.Visible = False
                                lnkUser.Visible = False
                                lnkApproval.Visible = False
                                lnkHOLevel.Visible = False
                                lnkPOLevel.Visible = False
                                lnkSOLevel.Visible = True
                                lnkNewUser.Visible = False
                                lnkAssignRole.Visible = False
                                lnkAssignLocation.Visible = False
                                lnkContractorCreation.Visible = False

                                lnkExecution.Visible = True
                                lnkCount.Visible = False

                                ''This is true bydefault
                                'lnkChangePass.Visible = False
                                lnkContactUs.Visible = False
                                lnkLMBFAQs.Visible = False
                                lnkLMBflow.Visible = True


                                ''Added on 22-07-2014
                                lnkMatnr.Visible = False
                                lnkPlantWiseCropCreation.Visible = False
                                lnkMissingData.Visible = False
                                lnkMissingDataUpdation.Visible = False
                                ''ended here added on 22-07-2014

                                ''ADDED ON 29-07-2014
                                lnkHoRptAmendRateHamaliBilling.Visible = False
                                lnkAmendHamaliBilling.Visible = True
                                ''ENDED HERE ADDED ON 29-07-2014
                                ''Added on 18-09-2014
                                lnkPoRptPurposeActivityWiseHamaliDtl.Visible = False
                                lnkHoRptPurposeActivityWiseHamaliDtl.Visible = False
                                ''ENDED HERE ADDED ON 18-09-2014

                                ''Added on 19-02-2015
                                lnkHamaliEntryDeletion.Visible = False
                                ''Ended here added on 19-02-2015

                                ''Added on 8-12-2016
                                lnkUserInActivation.Visible = False
                                ''Ended here added on 8-12-2016

                            Case ("Admin")

                                'lnkHamaliApproval.Visible = True
                                lnkHamaliBilling.Visible = True

                                lnkApproval.Visible = True
                                lnkBilling.Visible = True

                                'lnkHamaliExecution.Visible = True
                                lnkHoRptHamaliRate.Visible = True
                                lnkHoRptHamaliCount.Visible = True
                                lnkPoRptHamaliCount.Visible = False
                                lnkHoRptUserLogin.Visible = True
                                lnkHoRptHamaliApproveDisapprove.Visible = True
                                lnkHoRptSupervisorWiseHamaliDtl.Visible = True
                                lnkPoRptSupervisorWiseHamaliDtl.Visible = False
                                lnkSoRptSupervisorWiseHamaliDtl.Visible = False
                                lnkHoRptSectionActivityWiseHamaliDtl.Visible = True
                                lnkPoRptSectionActivityWiseHamaliDtl.Visible = False

                                lnkHoRptLocationActivityWiseHamaliDtl.Visible = True
                                lnkPoRptLocationActivityWiseHamaliDtl.Visible = False

                                lnkHoRptHamaliBilling.Visible = True
                                lnkHoRptHamaliMissingDataDetail.Visible = True
                                lnkHamaliRate.Visible = True
                                lnkAmendedHamaliRate.Visible = True
                                ''lnkAmendedHamaliRateUsingGrid.Visible = True
                                lnkUser.Visible = True
                                lnkApproval.Visible = True
                                lnkBilling.Visible = True
                                lnkExecution.Visible = True
                                lnkCount.Visible = True
                                lnkHOLevel.Visible = True
                                lnkPOLevel.Visible = False
                                lnkSOLevel.Visible = False
                                lnkNewUser.Visible = True
                                lnkAssignRole.Visible = True
                                lnkAssignLocation.Visible = True
                                lnkContractorCreation.Visible = True
                                ''This is true bydefault
                                'lnkChangePass.Visible = False
                                lnkContactUs.Visible = False
                                lnkLMBFAQs.Visible = False
                                lnkLMBflow.Visible = True

                                ''Added on 22-07-2014
                                lnkMatnr.Visible = True
                                lnkPlantWiseCropCreation.Visible = True
                                lnkMissingData.Visible = True
                                lnkMissingDataUpdation.Visible = True
                                ''ended here added on 22-07-2014

                                ''ADDED ON 29-07-2014
                                lnkHoRptAmendRateHamaliBilling.Visible = True
                                lnkAmendHamaliBilling.Visible = True
                                ''ENDED HERE ADDED ON 29-07-2014

                                ''Added on 18-09-2014
                                lnkPoRptPurposeActivityWiseHamaliDtl.Visible = True
                                lnkHoRptPurposeActivityWiseHamaliDtl.Visible = True
                                ''ENDED HERE ADDED ON 18-09-2014


                                ''Added on 19-02-2015
                                lnkHamaliEntryDeletion.Visible = True
                                ''Ended here added on 19-02-2015

                                ''Added on 8-12-2016
                                lnkUserInActivation.Visible = True
                                ''Ended here added on 8-12-2016

                            Case ("")

                                'lnkHamaliApproval.Visible = True
                                lnkHamaliBilling.Visible = False

                                lnkApproval.Visible = False
                                lnkBilling.Visible = False

                                'lnkHamaliExecution.Visible = True
                                lnkHoRptHamaliRate.Visible = False
                                lnkHoRptHamaliCount.Visible = False
                                lnkPoRptHamaliCount.Visible = False
                                lnkHoRptUserLogin.Visible = False
                                lnkHoRptHamaliApproveDisapprove.Visible = False
                                lnkHoRptSupervisorWiseHamaliDtl.Visible = False
                                lnkPoRptSupervisorWiseHamaliDtl.Visible = False
                                lnkSoRptSupervisorWiseHamaliDtl.Visible = False
                                lnkHoRptSectionActivityWiseHamaliDtl.Visible = False
                                lnkPoRptSectionActivityWiseHamaliDtl.Visible = False

                                lnkHoRptLocationActivityWiseHamaliDtl.Visible = False
                                lnkPoRptLocationActivityWiseHamaliDtl.Visible = False

                                lnkHoRptHamaliBilling.Visible = False
                                lnkHoRptHamaliMissingDataDetail.Visible = False
                                lnkHamaliRate.Visible = False
                                lnkAmendedHamaliRate.Visible = False
                                ''lnkAmendedHamaliRateUsingGrid.Visible = False
                                lnkUser.Visible = False
                                lnkApproval.Visible = False
                                lnkBilling.Visible = False
                                lnkExecution.Visible = False
                                lnkCount.Visible = False
                                lnkHOLevel.Visible = False
                                lnkPOLevel.Visible = False
                                lnkSOLevel.Visible = False
                                lnkNewUser.Visible = False
                                lnkAssignRole.Visible = False
                                lnkAssignLocation.Visible = False
                                lnkContractorCreation.Visible = False
                                ''This is False bydefault
                                'lnkChangePass.Visible = False
                                lnkContactUs.Visible = False
                                lnkLMBFAQs.Visible = False
                                lnkLMBflow.Visible = False

                                ''Added on 22-07-2014
                                lnkMatnr.Visible = False
                                lnkPlantWiseCropCreation.Visible = False
                                lnkMissingData.Visible = False
                                lnkMissingDataUpdation.Visible = False
                                ''ended here added on 22-07-2014

                                ''ADDED ON 29-07-2014
                                lnkHoRptAmendRateHamaliBilling.Visible = False
                                lnkAmendHamaliBilling.Visible = False
                                ''ENDED HERE ADDED ON 29-07-2014

                                ''Added on 18-09-2014
                                lnkPoRptPurposeActivityWiseHamaliDtl.Visible = False
                                lnkHoRptPurposeActivityWiseHamaliDtl.Visible = False
                                ''ENDED HERE ADDED ON 18-09-2014


                                ''Added on 19-02-2015
                                lnkHamaliEntryDeletion.Visible = False
                                ''Ended here added on 19-02-2015

                                ''Added on 8-12-2016
                                lnkUserInActivation.Visible = False
                                ''Ended here added on 8-12-2016



                        End Select

                    Else ''Role wise page Autherity option Added this Code on 12-11-2019 and below line of code

                        'Response.Write("<script language='javascript'>window.alert('Be careful.... You do not have the rights to access this Page / Form');window.location='Logout.aspx';</script>")

                        lblErrorMsg.Text = "Be careful.... You do not have the rights to access this Page / Form. <a href = 'Logout.aspx' target=_blank> Click Here </a>" ''Added this as anand sir suggested for alert on 09-12-2019



                        ''Page.ClientScript.RegisterStartupScript(Me.GetType(), "confirm", "<script type=text/javascript>confirm('You do not have the rights to access this Page / Form')</script>")
                        ''Response.Redirect("Logout.aspx")
                        ''Response.Write("<script>alert('You do not have the rights to access this Page / Form')</script>")
                        ''Response.Write("<script>window.location.href='Logout.aspx';</script>")
                        ''ClientScript.RegisterStartupScript(Page.GetType(), "Redirect", "window.onload = function(){ alert('You do not have the rights to access this Page / Form');window.location = 'Logout.aspx'; }", True)
                        ''ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", "window.onload = function(){ alert('You do not have the rights to access this Page / Form');window.location='MenuPage.aspx'; }", True)
                    End If ''This line of code is added on 12-11-2019 for CSRF validation

                End If
                


                End If ''Added this on 01-01-2020

        Catch ex As Exception
            ''MsgBox(ex.Message) ''Commented this on 09-12-2019 as this MsgBox will create the concer Showing a modal dialog box or form when the application is not running in UserInteractive mode is not a valid operation. Specify the ServiceNotification or DefaultDesktopOnly style to display a notification from a service application.
        End Try
    End Sub

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

End Class
'If MenuList(1).ToString = "Approve_Hamali_Exec" And MenuList(3).ToString = "Hamali_Billing" And MenuList(2).ToString = "Hamali_Execution" And MenuList(0).ToString = "R_HamaliRate_HL" Then

'                            lnkHamaliApproval.Visible = True
'                            lnkHamaliBilling.Visible = True
'                            lnkHamaliExecution.Visible = True
'                            lnkRptHamaliRate.Visible = True
'                            lnkChangePass.Visible = True
'                            lnkContactUs.Visible = True
'                            lnkLMBFAQs.Visible = True
'                            lnkLMBflow.Visible = True
'                            lnkCount.Visible = True
'                            lnkHOLevel.Visible = True
'                            lnkPOLevel.Visible = False

'                        ElseIf Len = "6" And MenuList(2).ToString = "Approve_Hamali_Exec" And MenuList(1).ToString = "Hamali_Billing" And MenuList(3).ToString = "Hamali_Execution" And MenuList(0).ToString = "R_Hamali_Exec_PL" And MenuList(4).ToString = "R_HamaliRate_HL" Then

'                            lnkHamaliApproval.Visible = True
'                            lnkHamaliBilling.Visible = True
'                            lnkHamaliExecution.Visible = True
'                            lnkRptHamaliRate.Visible = True

'                            lnkChangePass.Visible = True
'                            lnkContactUs.Visible = True
'                            lnkLMBFAQs.Visible = True
'                            lnkLMBflow.Visible = True
'                            lnkCount.Visible = True
'                            lnkHOLevel.Visible = True
'                            lnkPOLevel.Visible = True

'                        ElseIf Len = "1" And MenuList(0).ToString = "Hamali_Billing" Then
'                            lnkHamaliApproval.Visible = False
'                            lnkHamaliBilling.Visible = True
'                            lnkHamaliExecution.Visible = False
'                            lnkRptHamaliRate.Visible = False

'                            lnkCount.Visible = False
'                            lnkHOLevel.Visible = False
'                            lnkPOLevel.Visible = False
'                            lnkChangePass.Visible = True
'                            lnkContactUs.Visible = True
'                            lnkLMBFAQs.Visible = True
'                            lnkLMBflow.Visible = True

''ElseIf MenuList(3).ToString = "R_Hamali_Exec_PL" Then
''    lnkHamaliRate.Visible = True
''ElseIf MenuList(4).ToString = "R_HamaliRate_HL" Then
''    lnkRptHamaliRate.Visible = True

''End If

'                        End If
'''---------------------------------------------

'    Case 2
'        If MenuList(0).ToString = "Hamali_Execution" Then

'            lnkHamaliRate.Visible = False
'            lnkUser.Visible = False

'            lnkApproval.Visible = False
'            lnkBilling.Visible = False

'            lnkHamaliApproval.Visible = False
'            lnkHamaliBilling.Visible = False
'            lnkHamaliExecution.Visible = True
'            lnkRptHamaliRate.Visible = False

'            lnkCount.Visible = False
'            lnkHOLevel.Visible = False
'            lnkPOLevel.Visible = False
'            lnkChangePass.Visible = True
'            lnkContactUs.Visible = True
'            lnkLMBFAQs.Visible = True
'            lnkLMBflow.Visible = True

'        ElseIf MenuList(0).ToString = "Approve_Hamali_Exec" Then
'            lnkHamaliRate.Visible = False
'            lnkUser.Visible = False

'            lnkApproval.Visible = True
'            lnkExecution.Visible = False
'            lnkBilling.Visible = False

'            lnkHamaliApproval.Visible = True
'            lnkHamaliBilling.Visible = False
'            lnkHamaliExecution.Visible = False
'            lnkRptHamaliRate.Visible = False

'            lnkCount.Visible = False
'            lnkHOLevel.Visible = False
'            lnkPOLevel.Visible = False
'            lnkChangePass.Visible = True
'            lnkContactUs.Visible = True
'            lnkLMBFAQs.Visible = True
'            lnkLMBflow.Visible = True
'        End If

'    Case 3
'        If MenuList(0).ToString = "Hamali_Billing" And MenuList(1).ToString = "Hamali_Execution" Then
'            lnkHamaliRate.Visible = False
'            lnkUser.Visible = False

'            lnkApproval.Visible = False
'            lnkBilling.Visible = True

'            lnkHamaliApproval.Visible = False
'            lnkHamaliBilling.Visible = True
'            lnkHamaliExecution.Visible = True
'            lnkRptHamaliRate.Visible = False
'            lnkChangePass.Visible = True
'            lnkContactUs.Visible = True
'            lnkLMBFAQs.Visible = True
'            lnkLMBflow.Visible = True
'            lnkCount.Visible = False
'            lnkHOLevel.Visible = False
'            lnkPOLevel.Visible = False
'        End If
'    Case 4
'        If MenuList(0).ToString = "Approve_Hamali_Exec" And MenuList(2).ToString = "Hamali_Billing" And MenuList(1).ToString = "Hamali_Execution" Then
'            lnkHamaliRate.Visible = False
'            lnkUser.Visible = False

'            lnkApproval.Visible = True
'            lnkBilling.Visible = True

'            lnkHamaliApproval.Visible = True
'            lnkHamaliBilling.Visible = True
'            lnkHamaliExecution.Visible = True
'            lnkRptHamaliRate.Visible = False
'            lnkChangePass.Visible = True
'            lnkContactUs.Visible = True
'            lnkLMBFAQs.Visible = True
'            lnkLMBflow.Visible = True
'            lnkCount.Visible = False
'            lnkHOLevel.Visible = False
'            lnkPOLevel.Visible = False
'        End If


'    Case 5
'        If MenuList(1).ToString = "Approve_Hamali_Exec" And MenuList(3).ToString = "Hamali_Billing" And MenuList(2).ToString = "Hamali_Execution" And MenuList(0).ToString = "R_HamaliRate_HL" Then
'            lnkHamaliRate.Visible = True
'            lnkUser.Visible = True

'            lnkApproval.Visible = True
'            lnkBilling.Visible = True

'            lnkHamaliApproval.Visible = True
'            lnkHamaliBilling.Visible = True
'            lnkHamaliExecution.Visible = True
'            lnkRptHamaliRate.Visible = True
'            lnkChangePass.Visible = True
'            lnkContactUs.Visible = True
'            lnkLMBFAQs.Visible = True
'            lnkLMBflow.Visible = True
'            lnkCount.Visible = True
'            lnkHOLevel.Visible = True
'            lnkPOLevel.Visible = False

'        ElseIf MenuList(2).ToString = "Approve_Hamali_Exec" And MenuList(1).ToString = "Hamali_Billing" And MenuList(3).ToString = "Hamali_Execution" And MenuList(0).ToString = "R_Hamali_Exec_PL" Then
'            lnkHamaliRate.Visible = False
'            lnkUser.Visible = False

'            lnkApproval.Visible = True
'            lnkBilling.Visible = True

'            lnkHamaliApproval.Visible = True
'            lnkHamaliBilling.Visible = True
'            lnkHamaliExecution.Visible = True
'            lnkRptHamaliRate.Visible = False
'            lnkChangePass.Visible = True
'            lnkContactUs.Visible = True
'            lnkLMBFAQs.Visible = True
'            lnkLMBflow.Visible = True
'            lnkCount.Visible = True
'            lnkHOLevel.Visible = False
'            lnkPOLevel.Visible = False


'        End If

'    Case 6
'        If MenuList(2).ToString = "Approve_Hamali_Exec" And MenuList(1).ToString = "Hamali_Billing" And MenuList(3).ToString = "Hamali_Execution" And MenuList(0).ToString = "R_Hamali_Exec_PL" And MenuList(4).ToString = "R_HamaliRate_HL" Then
'            lnkHamaliRate.Visible = True
'            lnkUser.Visible = True

'            lnkApproval.Visible = True
'            lnkBilling.Visible = True

'            lnkHamaliApproval.Visible = True
'            lnkHamaliBilling.Visible = True
'            lnkHamaliExecution.Visible = True
'            lnkRptHamaliRate.Visible = True

'            lnkChangePass.Visible = True
'            lnkContactUs.Visible = True
'            lnkLMBFAQs.Visible = True
'            lnkLMBflow.Visible = True
'            lnkCount.Visible = True
'            lnkHOLevel.Visible = True
'            lnkPOLevel.Visible = True
'        End If

''------------------------
'Session("RoleDetail") = Role
'Else
'    ''For testing a normal employye also this links make visible true after testing make them false all
'    lnkHamaliApproval.Visible = True
'    lnkHamaliBilling.Visible = True

'    lnkApproval.Visible = True
'    lnkBilling.Visible = True

'    lnkHamaliExecution.Visible = True
'    lnkRptHamaliRate.Visible = True
'    lnkHamaliRate.Visible = True
'    lnkUser.Visible = True
'    lnkApproval.Visible = True
'    lnkBilling.Visible = True
'    lnkExecution.Visible = True
'    lnkCount.Visible = True
'    lnkHOLevel.Visible = True
'    lnkPOLevel.Visible = True
'    lnkNewUser.Visible = True
'    lnkAssignRole.Visible = True

'    ''This is true bydefault
'    lnkChangePass.Visible = True
'    lnkContactUs.Visible = True
'    lnkLMBFAQs.Visible = True
'    lnkLMBflow.Visible = True