<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUserInActivation.aspx.vb" Inherits="frmUserInActivation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>User Active or InActive Status Form</title>
<link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="AllJS.js"></script>
     <script language="javascript" type="text/javascript">
document.onkeydown = checkKeycode  //for checking the single quote in form   
 

function CurrentDate()
        {
         var CheckDate = new Date()
			        var CheckYr = parseInt(CheckDate.getFullYear())
			        var CheckMon = parseInt((CheckDate.getMonth() + 1)) 
			        var CheckDay = parseInt(CheckDate.getDate())
        		        
			        CheckDate = (CheckDate.getFullYear()) + ("/") + (CheckDate.getMonth() + 1) + ("/") + (CheckDate.getDate());
			        HiddenField1=CheckDate;
			    
        }

function checkKeycode(e)
         {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            else if (e) keycode = e.which;
            if (keycode==222) return false;
            //alert("keycode: " + keycode);
         }
         
         function CheckEnter(e)
         {
           var keycode;
            if (window.event) keycode = window.event.keyCode;
            else if (e) keycode = e.which;
            //alert("keycode: " + keycode);
            if (keycode==13) FillSearchData(); 
         }
         
var myWindow;
     
      function FillComboData(data,obj)
 {
 var cnt;
 var j;		
 var i;
   		 for(Cnt=0;Cnt<data.length/2;Cnt++)
		     {
		        if(Cnt==0)
		            {
		                j=0;							
		            }
		        else
		            {
		                j=j+2;							
		            }
                option = new Option(data[j+1],data[j]);
	            document.getElementById(obj).options[document.getElementById(obj).length]=option;

	        }	
 }   
 
     
function DisplayEmpDetails()
    {
   
        if (ReadComboValues('cmbEmpCode')!='0')
        {
        //Loading();
        //document.getElementById("btnSave").disabled=false;
        //document.getElementById("lnkEditRTo").disabled=false;
        CallServer('cmbEmpCode#'+ReadTextValues('cmbEmpCode'),"");
        }
    }
    
 function DisplayMailID()
 {
    CallServer('DisplayMailID#'+ReadComboValues('cmbReportingTo'),"");
 } 
 
 function SaveData()
 {
     if(Page_ClientValidate())
     {
         
         CurrentDate();
        
        if (confirm("Are you Sure ?")==true)
            {
         //document.getElementById("btnSave").disabled=true;
         var Qry;
//         Qry="Update UserMaster SET empmailid='"+ReadTextValues('txtEmpMailId')+"',EmpRptId='"+ReadComboValues('cmbReportingTo')+"',EmpRMailId='"+ReadTextValues('txtRMailId')+"',EmpCmplntLevel="+ReadComboValues('cmbRole')+",EmpName='"+ReadTextValues('txtEmpName')+"' Where EmpStaffId='" + ReadComboValues('cmbEmpCode')+"'; Update CustComplaintAutoMail SET GroupName='" + ReadComboValues('cmbGroup') + "',EmpCmplntLevel="+ReadComboValues('cmbRole')+",TrDate='"+ HiddenField1 +"' Where EmpStaffId='" + ReadComboValues('cmbEmpCode')+"'"
         Qry="Update Hamali_User_Master SET Empstatus='"+ReadComboValues('cmbStatus')+"',Flag='1' Where Emp_StaffId='" + ReadComboValues('cmbEmpCode')+"'"
         CallServer("SaveData#"+Qry)
            }
     }
 } 
  
    
function FillSearchData()
{
//alert("i am in FillSearchData ");
            Loading();
           ClearCombo('cmbEmpCode');
           var para;
           para='SearchedData#'+ReadTextValues('txtSearch');
           //alert(para)	
           CallServer(para,"");
 }
 
     
function FillReportingTo()
 {

   Loading();
   document.getElementById("lnkEditRTo").disabled=true;
   ClearCombo('cmbReportingTo');	
   CallServer('cmbReportingTo#',"");
    
  } 
 
 
    
function ReceiveServerData(rValue)
    {   
          //alert("i am in ReceiveServerData ");     			
        var Diff = rValue.split("|")
        switch(Diff[0])
        	{
        		case "cmbEmpCode":
				                   var ValueTextArray = Diff[1].split("~");
				                   SetTextValues('txtEmpName',ValueTextArray[0]);
				                   SetTextValues('txtEmpMailId',ValueTextArray[1]);
                                   document.getElementById("txtEmpfName").value=ValueTextArray[2]
                                   document.getElementById("txtEmplName").value=ValueTextArray[3]
				                   document.getElementById("cmbStatus").value=ValueTextArray[4]
				                   //document.getElementById("txtEmpLocn").value=ValueTextArray[5]
				                   SetTextValues('txtEmpLocn',ValueTextArray[5]);
				                   LoadComplete();
				                   break
				                   
				case "cmbReportingTo":
				
				                    var ValueTextArray = Diff[1].split("~");
				                    FillComboData(ValueTextArray,'cmbReportingTo');	
				                    LoadComplete();			                                       
				                  	break
				                  	
				case "SearchedData":
				
				                    ClearCombo('cmbEmpCode');
				                    var ValueTextArray = Diff[1].split("~");
				                    FillComboData(ValueTextArray,'cmbEmpCode');	
				                    LoadComplete();			                                       
				                  	break                 	
				                  	
				case "DisplayMailID":
				                    SetTextValues('txtRMailId',Diff[1]);
				                    break
				case "SaveData":
				                   alert(Diff[1]);
				                   window.location="MenuPage.aspx";
				                   break 
				case "Error":
				                    alert(Diff[1]);
				                    
				                                      				                                                                    
			}
			
         
	}
	
	     
     </script>
     
</head>
<body>
    <form id="form1" runat="server">
    <div id="lodingDiv" style="position:absolute;" ></div>
<center>
<table id="topTable" style="width:100%; border:1;">
<tr><td align="center" style="height: 53px"><h3><u>HAMALI USER ACTIVE OR INACTIVE STATUS FORM</u></h3></td></tr>
</table>
    <table id="topTable1" style="width:100%; border:1;">
    <tr>    
    <td align="left" style="height: 53px"><h3><u>PLANT </u> : <% Response.Write(Session("plantName"))%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <u>HEAD OFFICER </u> : <% Response.Write(Session("initial"))%>    
    </h3></td>
    <td align="right" style="height: 53px">    
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>



<%--<table id="topTable" style="width:90%; border:1;">
    <tr>    
    <td align="LEFT">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <h3>HAMALI ROLE ASSIGN DETAILS / UPDATION FORM</h3></td>
    <td align="right">    
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>--%>
  </center>      
        
    <center>
    <div id="mydiv">
    <table border="1" cellpadding="2" cellspacing="0" width="90%" class="mytable">
     <tr><td align="right" style="width: 20%" >
     <img  src="images/search1.gif" alt="Search"  /></td><td align="left" style="width: 50%">
            <span id="divSearch" style="background-color:Silver; width:45%; height:30px;  border-left-color: #0000ff; border-bottom-color: #0000ff; border-top-style: groove; border-top-color: #0000ff; border-right-style: groove; border-left-style: groove; border-right-color: #0000ff; border-bottom-style: groove;" >
                <asp:TextBox ID="txtSearch" runat="server" BackColor="#C0FFFF" CssClass="MyText" ForeColor="Red"></asp:TextBox>
                <asp:LinkButton ID="lnkGo" runat="server" OnClientClick="FillSearchData();return false;" CausesValidation="False">Go</asp:LinkButton></span></td></tr>
    <tr>
    <td align="right" style="width: 20%; font-size:larger" >
        Emp. Code :-</td><td align="left" style="width: 50%">
            <asp:DropDownList ID="cmbEmpCode" runat="server" AppendDataBoundItems="True" CssClass="MyText">
                <asp:ListItem Value="0">Select</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ReqEmpCode" runat="server" ControlToValidate="cmbEmpCode"
            ErrorMessage="(Required)" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
            
            </td></tr>
    <tr><td align="right" style="width: 20%; font-size:larger" >
        Emp. Initial :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtEmpName" runat="server" CssClass="MyText" Width="50%" ReadOnly="true" ></asp:TextBox></td></tr>
          
          
     
    <tr><td align="right" style="width: 20%; font-size:larger" >
        Emp. FName :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtEmpfName" runat="server" CssClass="MyText" Width="50%"  ReadOnly="true"></asp:TextBox></td></tr>

    <tr><td align="right" style="width: 20%; font-size:larger" >
        Emp. LName :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtEmplName" runat="server" CssClass="MyText" Width="50%"  ReadOnly="true"></asp:TextBox></td></tr>
     
          
    <tr><td align="right" style="width: 20%; font-size:larger" >
        Off.&nbsp; Mail-ID :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtEmpMailId" runat="server" CssClass="MyText" Width="50%" ReadOnly="true"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqOffMailID" runat="server" ControlToValidate="txtEmpMailId"
                ErrorMessage="(Required)" Display="Dynamic"></asp:RequiredFieldValidator></td></tr>
       <%--<tr><td align="right" style="width: 50%" id="mytd" >
        Reporting To :-</td><td align="left" style="width: 50%"><asp:DropDownList ID="cmbReportingTo" runat="server" AppendDataBoundItems="True" CssClass="MyText">
        </asp:DropDownList>
            <asp:LinkButton ID="lnkEditRTo" runat="server" CausesValidation="False"  OnClientClick="FillReportingTo();return false;">Change</asp:LinkButton>
            <asp:RequiredFieldValidator ID="ReqRepTo" runat="server" ControlToValidate="cmbReportingTo"
                ErrorMessage="(Required)" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator></td></tr>
    <tr><td align="right" style="width: 50%" >
        ReportingTo off. mail-ID :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtRMailId" runat="server" CssClass="MyText" Width="80%" ReadOnly="True"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqRepMailID" runat="server" ControlToValidate="txtRMailId"
                ErrorMessage="(Required)" Display="Dynamic"></asp:RequiredFieldValidator></td></tr>--%>     
    <tr><td align="right" style="width: 20%; font-size:larger" >
        Emp Status :-</td><td align="left" style="width: 50%">
            <asp:DropDownList ID="cmbStatus" runat="server" CssClass="MyText">
                <asp:ListItem Value="0">Select</asp:ListItem>
                <asp:ListItem Value="Active">Active</asp:ListItem>
                <asp:ListItem Value="InActive">InActive</asp:ListItem>
            </asp:DropDownList></td></tr> 
            
            
            <tr><td align="right" style="width: 20%; font-size:larger" >
        Emp. Location :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtEmpLocn" runat="server" CssClass="MyText" Width="50%"  ReadOnly="true" ></asp:TextBox></td></tr>            
    <tr><td colspan="2" align="center">
        <%--<asp:Button ID="btnSave" runat="server" CssClass="FormButton" Text="Save" OnClientClick="SaveData();return false;" Width="150px" Font-Bold="true"/>--%>
        <input id="btnSave" type="image" value="Save Data" onclick="SaveData();return false;"  src="img/Checklist/Save.png"  />
        <%--&nbsp; <a href="default.aspx" >Close</a>--%>
        </td></tr>    
    </table>
    </div>
    
    </center>
        <asp:HiddenField ID="HiddenField1" runat="server" />
       <asp:HiddenField ID="hdnRndNo" runat="server" /><%--used for Request Flooding Revalidation added this on 12-05-2020--%>
 <asp:HiddenField ID="ClnthdnRndNo" runat="server" /> <%--used for Request Flooding Revalidation added this on 12-05-2020--%>
    </form>
</body>
</html>
