<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MenuPage.aspx.vb" Inherits="MenuPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
	         <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

<%--''This code is used to cover the Weak Cache management point (GIS suggested) added this on 01-12-2019--%>
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="-1" />  <%--Commened this on 11-12-2019--%>   <%--content="Sat, 01-jan-2000 00:00:00 GMT"--%>  

<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Cache-Control" content="no-store" />
 <%--''This code is used to cover the Weak Cache management point (GIS suggested) added this on 01-12-2019--%>
    <title>Menu Page</title>    
     
   <link href="MenuPage/styles.css" rel="stylesheet" type="text/css" /> 
  <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
 </head>
<body  onunload='check(event)'>

<%--<table border="0"  cellpadding="0" cellspacing="0" width="100%"> 
    <tr>       
         <td width="25%" align="right">
            <a id="lnkusermanual" href="HAMALI.pps" target="_blank" style="font-weight: bold; font-size:larger; color:Black" >User's Manual</a>&nbsp;&nbsp;&nbsp;             
         </td>
    </tr>
</table>--%>

    <form id="form1" runat="server">
    
    
    <asp:Label ID="lblErrorMsg" runat="server" style="color:Purple; font-size:medium;" ></asp:Label>
    <div style="width:700px;margin:100px auto"> 		
	      <h2  title="AWS 50.26"><b>USER:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <% Response.Write(Session("initial"))%></b> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	      <b>Location:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <% Response.Write(Session("plantName"))%></b></h2>
		<div class="container4"> 
	<div class="menu4"> 
 
<ul> 
                <li class="articles"><a href="#">MASTERS</a> 
	                <ul> 
		                <li class="add_article" id="lnkHamaliRate" runat="server"><a href="frmHamaliRate.aspx">HAMAL RATE</a></li>
		                <li class="add_article" id="lnkAmendedHamaliRate" runat="server"><a href="frmAmendedHamaliRate.aspx">AMEND HAMAL RATE</a></li>
		                <%--<li class="add_article" id="lnkAmendedHamaliRateUsingGrid" runat="server"><a href="frmAmendedHamaliRateUsingGrid.aspx">AMEND HAMAL RATE II</a></li>--%>
		                <li class="add_article" id="lnkContractorCreation" runat="server"><a href="frmContractorCreation.aspx">CONTRACTOR CREATION</a></li> 
		                <li class="categories" id="lnkUser" runat="server"><a href="#">USER</a>
		                     <ul> 
		                         <li class="last" id="lnkNewUser" runat="server"><a href="frmusercreation.aspx">NEW USER ENTRY</a></li> 
		                         <li class="last" id="lnkAssignRole" runat="server"><a href="frmAssignRole.aspx">ASSIGN ROLE</a></li>
		                         <li class="last" id="lnkAssignLocation" runat="server"><a href="frmAssignLocation.aspx">ASSIGN LOCATION</a></li>
		                         <li class="last" id="lnkUserInActivation" runat="server"><a href="frmUserInActivation.aspx">ASSIGN STATUS</a></li>
		                         
	                         </ul> 		                
		                </li>
		                <!-- Below Code added on 15-02-2019 -->
		                <li class="categories" id="lnkMatnrEntry" runat="server"><a href="#">MATNR ENTRY</a>
		                     <ul> 
		                         <li class="last" id="Li2" runat="server"><a href="frmMatnrEntry.aspx">NEW MATNR ENTRY</a></li>                       
		                         
	                         </ul> 		                
		                </li>
		                <!-- Code ended here added on 15-02-2019 -->
		                
		                <li class="add_article" id="lnkHamaliEntryDeletion" runat="server"><a href="frmHamaliEntryDeletion.aspx">HAMALI ENTRY DELETION</a></li>
	                </ul> 
                </li> 
                <li class="users"><a href="#">PROGRAMS</a> 
	                <ul>		
		                <li class="show subsubl" id="lnkExecution" runat="server"><a href="#">EXECUTION</a> 
	                <ul> 
		                <%--<li class="last" id="lnkHamaliExecution" runat="server"><a href="frmHamaliExecution.aspx">Hamali Execution</a></li>--%> 
		                 <li class="last" id="lnkHamaliExecutionDHANORA" runat="server"><a href="frmHamaliExecutionDHANORA.aspx">HAMALI EXECUTION</a></li> 
		                <li class="last" id="lnkHamaliExecutionKALLAKAL" runat="server"><a href="frmHamaliExecutionKALLAKAL.aspx">HAMALI EXECUTION</a></li> 
		                
		                <%--Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020--%>
		                <%--<li class="last" id="lnkHamaliExecutionKHANDWA" runat="server"><a href="frmHamaliExecutionKHANDWA.aspx">HAMALI EXECUTION</a></li> 
		                <li class="last" id="lnkHamaliExecutionKOTA" runat="server"><a href="frmHamaliExecutionKOTA.aspx">HAMALI EXECUTION</a></li> 
		                <li class="last" id="lnkHamaliExecutionVEJALPUR" runat="server"><a href="frmHamaliExecutionVEJALPUR.aspx">HAMALI EXECUTION</a></li> 
		                <li class="last" id="lnkHamaliExecutionKAMDOD" runat="server"><a href="frmHamaliExecutionKAMDOD.aspx">HAMALI EXECUTION</a></li> 
		                <li class="last" id="lnkHamaliExecutionSARANGAPUR" runat="server"><a href="frmHamaliExecutionSARANGAPUR.aspx">HAMALI EXECUTION</a></li>--%>
		                <%--Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here--%>

	                </ul> 
                </li> 
                <li class="show subsubl" id="lnkApproval" runat="server" ><a href="#">APPROVAL</a> 
	                <ul> 
		                <%--<li class="last" id="lnkHamaliApproval" runat="server"><a href="frmHamaliApproval.aspx">Approve Hamali Exec</a></li>--%>
		                <li class="last" id="lnkHamaliApprovalDHANORA" runat="server"><a href="frmHamaliApprovalDHANORA.aspx">APPROVE HAMALI EXEC</a></li>
		                <li class="last" id="lnkHamaliApprovalKALLAKAL" runat="server"><a href="frmHamaliApprovalKALLAKAL.aspx">APPROVE HAMALI EXEC</a></li>
		                <%--Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020--%>
		                <%--<li class="last" id="lnkHamaliApprovalKHANDWA" runat="server"><a href="frmHamaliApprovalKHANDWA.aspx">APPROVE HAMALI EXEC</a></li>
		                <li class="last" id="lnkHamaliApprovalKOTA" runat="server"><a href="frmHamaliApprovalKOTA.aspx">APPROVE HAMALI EXEC</a></li>
		                <li class="last" id="lnkHamaliApprovalVEJALPUR" runat="server"><a href="frmHamaliApprovalVEJALPUR.aspx">APPROVE HAMALI EXEC</a></li>
		                <li class="last" id="lnkHamaliApprovalKAMDOD" runat="server"><a href="frmHamaliApprovalKAMDOD.aspx">APPROVE HAMALI EXEC</a></li>		               
		                <li class="last" id="lnkHamaliApprovalSARANGAPUR" runat="server"><a href="frmHamaliApprovalSARANGAPUR.aspx">APPROVE HAMALI EXEC</a></li>--%>
		                <%--Below links are hide as Kalyan Sagade sir suggest only dosplay active plant list in Application commented on 11-07-2020 is ended here--%>
		                <%--<li class="last" id="lnkMissingDataUpdation" runat="server"><a href="frmMissingDataUpdation.aspx">MISSING DATA UPDATION</a></li>--%>
	                </ul> 
                </li> 
                <li class="show subsubl" id="lnkBilling" runat="server" ><a href="#">BILLING</a> 
	                <ul> 
		                <li class="last" id="lnkHamaliBilling" runat="server"><a href="HamaliBilling.aspx">HAMALI BILLING</a></li> 
		                <li class="last" id="lnkAmendHamaliBilling" runat="server"><a href="frmAmendHamaliBilling.aspx">AMEND HAMALI BILLING</a></li> 
	                </ul> 
                </li>
                
                <li class="show subsubl" id="lnkCount" runat="server" ><a href="#">COUNT</a> 
	                <ul> 
		                <li class="last" id="lnkHamaliCount" runat="server"><a href="frmHamaliCount.aspx">HAMALI COUNT</a></li> 
		                <li class="last" id="lnkHamaliCountUpdation" runat="server"><a href="frmHamaliCountUpdation.aspx">HAMALI COUNT UPDATION</a></li> 
	                </ul> 
                </li>
                
                <li class="show subsubl" id="lnkMatnr" runat="server" ><a href="#">MATNR</a> 
	                <ul> 
		                <li class="last" id="lnkPlantWiseCropCreation" runat="server"><a href="frmPlantWiseCropCreation.aspx">PLANT-WISE MATNR ENTRY</a></li> 
	                </ul> 
                </li>
                
                <li class="show subsubl" id="lnkMissingData" runat="server" ><a href="#">MISSING DATA</a> 
	                <ul> 
		                <li class="last" id="lnkMissingDataUpdation" runat="server"><a href="frmMissingDataUpdation.aspx">MISSING DATA DETAIL ENTRY</a></li> 
	                </ul> 
                </li>
                
                
 	                </ul> 
                </li> 

                
                <li class="users"><a href="#">REPORT</a> 
	                <ul>		
		                <li class="show subsubl" id="lnkHOLevel" runat="server"><a href="#">HO LEVEL</a> 
	                <ul> 
		                <%--<li class="last" id="lnkHamaliExecution"><a href="rptHamaliRate.aspx">Hamali Rate</a></li>--%>
		                <li class="last" id="lnkHoRptHamaliRate" runat="server"><a href="rptHamaliRate.aspx">HAMALI RATE</a></li> 
		                <li class="last" id="lnkHoRptHamaliCount" runat="server"><a href="rptHamaliCount.aspx">HAMALI COUNT</a></li>
		                <li class="last" id="lnkHoRptUserLogin" runat="server"><a href="rptUserLogin.aspx">HAMALI USER LOGIN</a></li>
		                <li class="last" id="lnkHoRptHamaliApproveDisapprove" runat="server"><a href="rptHamaliApproveDisapprove.aspx">HAMALI APPROVE DISAPPROVE DETAIL</a></li> 
	                    <li class="last" id="lnkHoRptSupervisorWiseHamaliDtl" runat="server"><a href="rptSupervisorWiseHamaliDtl.aspx">SUPERVISOR WISE HAMALI DETAIL</a></li>
	                    <li class="last" id="lnkHoRptSectionActivityWiseHamaliDtl" runat="server"><a href="rptSectionActivityWiseHamaliDtl.aspx">SECTION ACTIVITY WISE HAMALI DETAIL</a></li>
	                    <li class="last" id="lnkHoRptLocationActivityWiseHamaliDtl" runat="server"><a href="rptLocationActivityWiseHamaliDtl.aspx">LOCATION ACTIVITY WISE HAMALI DETAIL</a></li>
	                    <li class="last" id="lnkHoRptHamaliBilling" runat="server"><a href="rptHamaliBilling.aspx">HAMALI BILLING DETAIL</a></li>
	                    <li class="last" id="lnkHoRptAmendRateHamaliBilling" runat="server"><a href="rptAmendRateHamaliBilling.aspx">AMEND RATE HAMALI BILLING DETAIL</a></li>
	                    <li class="last" id="lnkHoRptHamaliMissingDataDetail" runat="server"><a href="rptHamaliMissingDataDetail.aspx">HAMALI MISSING ENTRY DATA DETAIL</a></li>
	                    <li class="last" id="lnkHoRptPurposeActivityWiseHamaliDtl" runat="server"><a href="rptPurposeActivityWiseHamaliDtl.aspx">PURPOSE ACTIVITY WISE HAMALI DETAIL</a></li>
	                </ul> 
                      </li> 
                      
                      <li class="show subsubl" id="lnkPOLevel" runat="server"><a href="#">PO LEVEL</a> 
	                <ul> 
		                <%--<li class="last" id="lnkHamaliExecution"><a href="rptHamaliRate.aspx">Hamali Rate</a></li>--%>
		                <li class="last" id="lnkPoRptHamaliCount" runat="server"><a href="rptHamaliCount.aspx">HAMALI COUNT</a></li>
		                <li class="last" id="lnkPoRptSupervisorWiseHamaliDtl" runat="server"><a href="rptSupervisorWiseHamaliDtl.aspx">SUPERVISOR WISE HAMALI DETAIL</a></li>  
		                <li class="last" id="lnkPoRptSectionActivityWiseHamaliDtl" runat="server"><a href="rptSectionActivityWiseHamaliDtl.aspx">SECTION ACTIVITY WISE HAMALI DETAIL</a></li>  
	                    <li class="last" id="lnkPoRptLocationActivityWiseHamaliDtl" runat="server"><a href="rptLocationActivityWiseHamaliDtl.aspx">LOCATION ACTIVITY WISE HAMALI DETAIL</a></li>
	                    <li class="last" id="lnkPoRptPurposeActivityWiseHamaliDtl" runat="server"><a href="rptPurposeActivityWiseHamaliDtl.aspx">PURPOSE ACTIVITY WISE HAMALI DETAIL</a></li>
	                </ul> 
                      </li>   
                             
                     <li class="show subsubl" id="lnkSOLevel" runat="server"><a href="#">SO LEVEL</a> 
	                <ul> 
		                <%--<li class="last" id="lnkHamaliExecution"><a href="rptHamaliRate.aspx">Hamali Rate</a></li>--%>
		                <li class="last" id="lnkSoRptSupervisorWiseHamaliDtl" runat="server"><a href="rptSupervisorWiseHamaliDtl.aspx">SUPERVISOR WISE HAMALI DETAIL</a></li>  
	                </ul> 
                      </li>
                      
                               
 	                </ul> 
                </li> 
                
                <li class="articles"><a href="#">PERSONAL</a> 
	                <ul> 
		               <%-- <li class="add_article" id="lnkChangePass" runat="server"><a href="ChangePass.aspx">CHANGE PASSWORD</a></li>--%> 
		                <li class="categories" id="lnkContactUs" runat="server"><a href="#">CONTACT US</a></li>		
	                </ul> 
                </li> 
                
                <li class="articles"><a href="#">HELP</a> 
	                <ul> 
		                <%--<li  id="lnkLMBflow" runat="server"><a href="LMB-flow.htm" target="_blank">Work-Flow</a></li>--%>		                
		                <li  id="lnkLMBflow" runat="server"><a href="HAMALI-USER-MANNUAL-PLANT.pps" target="_blank">WORK-FLOW</a></li>
		                <li class="categories" id="lnkLMBFAQs" runat="server"><a href="LMBFAQs.htm">FAQs</a></li>		
		                
	                </ul> 
                </li> 
                <li class="add_article"><a href="Logout.aspx">SIGN OUT</a></li>
                
</ul> 
 
	</div> 
	</div> 
	
	<table style="height:80px">
	<tr>    
    <td style="width:25%; height:25px" align="center">    
    <img src="images/older_workers_752.JPG" alt="" />
    </td>    
    </tr>
    </table>
	
	</div> 
    
   </form>
    
    
    
</body>
</html>
