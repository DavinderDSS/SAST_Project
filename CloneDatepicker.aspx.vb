Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions


Partial Class CloneDatepicker
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    Protected returnValue As New StringBuilder

    '''' This Should Add on every Page and this is added for server banner point 25-12-2019
    Protected Sub Application_PreSendRequestHeaders()
        Response.Headers.Remove("Server") ''Commneted this on 23-12-2019 as error occure
        Response.Headers.Remove("X-AspNet-Version") ''ASP.NET Response Headers GIS Issue added on 03-01-2020
        Response.AddHeader("Sample1", "Value1")
    End Sub
    '''' This Should Add on every Page and this is added for server banner point 25-12-2019

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Application_PreSendRequestHeaders() '''' This Should Add on every Page and this is added for server banner point 25-12-2019
            Dim cbReference As String
            cbReference = Page.ClientScript.GetCallbackEventReference(Me, "arg", "ReceiveServerData", "context")
            Dim callbackScript As String = ""
            callbackScript &= "function CallServer(arg, context) { " & cbReference & "} ;"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", callbackScript, True)


            If Not IsPostBack Then



                GetDataByQUERY("select distinct pm.plant_code,pm.plant_name from plant_master as pm inner join user_rights as ur on pm.plant_code=ur.plant_code where ur.emp_staffid='" & Session("staffid") & "' and ur.plant_code='" & Session("plantCode") & "' order by plant_name ", cmbP, "plant_name", "plant_code")
                GetDataByQUERY("select activity_id,activity from Hamal_Activity_Master Order By activity", cmbA, "activity", "activity_id")
            End If


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

                Select Case s(0).ToString

                    Case getCaseString(s(0).ToString, "SaveData")

                        'Dim ClosingPara() = s(0).ToString.Split("|")

                        'Dim Queries As String = String.Empty
                        'Queries = "Insert Into hamal_rate_master(FromDateRang,ToDateRang,plantCode,activity,distance,weight,loading,unloading,sorting,stiching,stacking,restacking,bundle_preparation,opening_feeding,loading_unloading,noStack,stackDhToAnti,stackPlantToGodown,heightDiff) Values (" & s(1).ToString().Remove(s(1).ToString.Length - 2, 2) & ")"

                        'Queries = Queries & "~"

                        'If SaveDataWithTransaction(Queries, "~") = True Then
                        '    returnValue.Append("Record Saved Successfully")
                        'End If


                        ''new coe add for multiple rows addition work fine but lenghty

                        ''Dim ClosingPara() = s(0).ToString.Split("|")
                        Dim Result As String = String.Empty
                        Dim AllData() = s(1).ToString.Split("~")

                        Dim i As Integer
                        For i = 0 To AllData.Length - 2

                            Dim Queries As String = String.Empty
                            'Queries = "Insert Into hamal_rate_master(FromDateRang,ToDateRang,plantCode,activity,distance,weight,loading,unloading,sorting,stiching,stacking,restacking,bundle_preparation,opening_feeding,loading_unloading,noStack,stackDhToAnti,stackPlantToGodown,heightDiff) Values (" & s(1).ToString().Remove(s(1).ToString.Length - 2, 2) & ")"

                            Queries = "Insert Into hamal_rate_master(FromDateRang,ToDateRang,plantCode,activity,distance,weight,loading,unloading,sorting,stiching,stacking,restacking,weighing,bundle_preparation,opening_feeding,loading_unloading,UNP_RECD_UNL_WT_STK,UNP_RECD_UNL_SRT_WT_STK,DH_To_AC_STK,AC_To_DH_STK,LOADING_DH,UNLOADING_DH) Values (" & AllData(i).ToString().Remove(AllData(i).ToString.Length - 1, 1) & ")"

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

End Class
