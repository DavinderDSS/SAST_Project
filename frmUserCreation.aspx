<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUserCreation.aspx.vb" Inherits="frmUserCreation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>Create New User</title>
    <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
     <script language="javascript" type="text/javascript" src="js/AllJS.js"></script>
     <script language="javascript" type="text/javascript">
    document.onkeydown = checkKeycode  //for checking the single quote in form
    
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

    function SaveData()
         {         
     if(Page_ClientValidate())
          {
         
         //CurrentDate();
        
        if (confirm("Are you Sure ?")==true)
            {
         //document.getElementById("btnSave").disabled=true;
         var Qry;
         
         ////Qry="Update UserMaster SET empmailid='"+ReadTextValues('txtEmpMailId')+"',EmpRptId='"+ReadComboValues('cmbReportingTo')+"',EmpRMailId='"+ReadTextValues('txtRMailId')+"',EmpCmplntLevel="+ReadComboValues('cmbRole')+",EmpName='"+ReadTextValues('txtEmpName')+"' Where EmpStaffId='" + ReadComboValues('cmbEmpCode')+"'; Update CCFUserGroupName SET GroupName='" + ReadComboValues('cmbGroup') + "',EmpCmplntLevel="+ReadComboValues('cmbRole')+",TrDate='"+ HiddenField1 +"' Where EmpStaffId='" + ReadComboValues('cmbEmpCode')+"'"
         //Qry="insert into CCFUserGroupName select empstaffid,'' ,empcmplntlevel,empflag,getdate() from usermaster where empstaffid not in (select empstaffid from CCFUserGroupName) and empstaffid = '" + ReadComboValues('cmbEmpCode')+"'; Update UserMaster SET empmailid='"+ReadTextValues('txtEmpMailId')+"',EmpRptId='"+ReadComboValues('cmbReportingTo')+"',EmpRMailId='"+ReadTextValues('txtRMailId')+"',EmpCmplntLevel="+ReadComboValues('cmbRole')+",EmpName='"+ReadTextValues('txtEmpName')+"' Where EmpStaffId='" + ReadComboValues('cmbEmpCode')+"'; Update CCFUserGroupName SET GroupName='" + ReadComboValues('cmbGroup') + "',EmpCmplntLevel="+ReadComboValues('cmbRole')+",TrDate='"+ HiddenField1 +"' Where EmpStaffId='" + ReadComboValues('cmbEmpCode')+"'"
         //Qry="insert into usermaster (EmpstaffId,EmpPassword,EmpFName,EmpLName,EmpName,EmpSex,EmpMailId,EmpPMailId,EmpRptId,EmpRMailId,EmpUnit,EmpDOJ,EmpStatus,EmpDOL,EmpDesg,EmpLocn,CellNo,EmpCmplntLevel,EmpFlag) values ('"+ReadTextValues('txtEmpCode')+"','SUNGRO','"+ReadTextValues('txtEmpFName')+"','"+ReadTextValues('txtEmpLName')+"','"+ReadTextValues('txtEmpName')+"','" + ReadComboValues('cmbSex')+"','"+ReadTextValues('txtEmpMailId')+"','"+ReadTextValues('txtEmpPerMailId')+"','"+ReadTextValues('txtRepToId')+"','"+ReadTextValues('txtRMailId')+"','" + ReadComboValues('cmbUnit')+"','"+ReadDateValues('txtFromDate')+"','Active','','" + ReadComboValues('cmbDesg')+"','"+ReadTextValues('txtEmpLocn')+"','"+ReadTextValues('txtCellInfo')+"','0','0')" //its working one commented on 28-05-2012 due to error and below on is added
         Qry="insert into Hamali_user_master (Emp_staffId,EmpSAPID,user_initial,Emp_FName,Emp_LName,Emp_Pass,Emp_MailId,Emp_role,Emp_plantNm,Emp_PlantCode,empstatus,Flag) values ('"+ReadTextValues('txtEmpCode')+"','"+ReadTextValues('txtEmpSapID')+"','"+ReadTextValues('txtuserInitial')+"','"+ReadTextValues('txtEmpFName')+"','"+ReadTextValues('txtEmpLName')+"','MAHYCO','"+ReadTextValues('txtEmpMailId')+"','"+ReadTextValues('cmbEmprole')+"','"+ReadTextValues('cmbPlantNm')+"','"+ReadTextValues('cmbPlantCode')+"','Active','0')"
         
         //alert(Qry);
         CallServer("SaveData#"+Qry)
            }
         }
      } 

function DisplayEmpDetails()
    {
   
        if (ReadComboValues('cmbPlantNm')!='0')
        {
        //Loading();
        //document.getElementById("btnSave").disabled=false;
        //document.getElementById("lnkEditRTo").disabled=false;
        CallServer('cmbPlantNm#'+ReadTextValues('cmbPlantNm'),"");
        }
    }



   function ExistingUser()
   {
   
   var Para='';
   
   if(ReadTextValues('txtEmpCode')!=null)
   {
   //Para='ExistingUser';
   var Qry;
   Qry="'"+ReadTextValues('txtEmpCode')+"'";
   CallServer("ExistingUser#"+Qry);
   }
   
   }

function OnlyNumbers(e,Type)
	{	
        if (Type=='Int')
	        {
	        if (!((e.keyCode >47) && (e.keyCode<58)))
		        {
			        e.keyCode=0;
        			
		        }	
	        }
        else if (!(((e.keyCode >47) && (e.keyCode<58)) || (e.keyCode ==46)))
		        {
		            e.keyCode=0;
		        }	
	}    
	
	
    function CheckMe(Type)
    {
        if (Page_ClientValidate())
            {
                if (Type=='btnSave')
                {
                    SaveData();
                }
            }
    
    }
 
 function ReceiveServerData(rValue)
    {   
          //alert("i am in ReceiveServerData ");     			
        var Diff = rValue.split("|")
        switch(Diff[0])
        	{
        	case "SaveData":
				                   alert(Diff[1]);
				                   window.location="MenuPage.aspx";
				                   break 
	        case "ExistingUser" :  
	                               alert(Diff[1]);
	                               window.location="MenuPage.aspx";
	                               break
	                               
	        case "cmbPlantNm":
				                   var ValueTextArray = Diff[1].split("~");
				                   //SetTextValues('txtEmpName',ValueTextArray[0]);
				                   document.getElementById("cmbPlantCode").value=ValueTextArray[0]
				                   //SetTextValues('txtEmpLocn',ValueTextArray[2]);				                   
				                   //LoadComplete();
				                   break                       
	                                 
				                   
				case "Error":
				                    alert(Diff[1]);
				                    
				                                      				                                                                    
			}
			
         
	}
 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
    </div>
    <center>
    <table id="topTable" style="width:100%; border:1;">
<tr><td align="center" style="height: 53px"><h3><u>HAMALI NEW USER CREATION FORM</u></h3></td></tr>
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
    <br />
    <%--<table id="Table1"  width="95%" >
    <tr><td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <h3>HAMALI NEW USER CREATION FORM</h3>
    </td>
    <td align="right">
    <div align="right">         
            <asp:HyperLink ID="HylHome" runat="server" NavigateUrl="~/Menupage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png">Home</asp:HyperLink>
            <asp:HyperLink ID="HylLogOt" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png">Sign Out</asp:HyperLink>
    </div>
    
        </td></tr>
    </table>--%>
    </center>
    <center>
    <div id="mydiv">
    <table border="1" cellpadding="2" cellspacing="0" width="95%" class="mytable">
     <tr><td align="right" style="width: 20%; font-size:larger" >Emp. Code :-</td><td align="left" style="width: 50%">
                   <asp:TextBox ID="txtEmpCode" runat="server"  CssClass="MyText" ForeColor="Red"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqEmpCode" runat="server" ControlToValidate="txtEmpCode"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
                </td></tr>
                
                
     <tr><td align="right" style="width: 20%; font-size:larger" >Emp. SapID :-</td><td align="left" style="width: 50%">
                   <asp:TextBox ID="txtEmpSapID" runat="server"  CssClass="MyText" ForeColor="Red"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqEmpSapID" runat="server" ControlToValidate="txtEmpSapID"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
                </td></tr>           
     
     <tr><td align="right" style="width: 20%; font-size:larger" >
        Emp. Name :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtuserInitial" runat="server" CssClass="MyText" Width="50%" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequserInitial" runat="server" ControlToValidate="txtuserInitial"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr>
    
    
        <tr><td align="right" style="width: 20%; font-size:larger" >
        Emp First Name :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtEmpFName" runat="server" CssClass="MyText" Width="50%" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqEmpFNM" runat="server" ControlToValidate="txtEmpFName"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr>
            
            <tr><td align="right" style="width: 20%; font-size:larger" >
             Emp Last Name :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtEmpLName" runat="server" CssClass="MyText" Width="50%" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqEmpLNM" runat="server" ControlToValidate="txtEmpLName"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr>           
            
       <tr><td align="right" style="width: 20%; font-size:larger" >
        Off.&nbsp; Mail-ID :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtEmpMailId" runat="server" CssClass="MyText"  Width="50%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqOffMailID" runat="server" ControlToValidate="txtEmpMailId"
                ErrorMessage="(Required)" Display="Dynamic"></asp:RequiredFieldValidator></td></tr>
                
                
                    
    <tr><td align="right" style="width: 20%; font-size:larger" >
        Emp Unit :-</td><td align="left" style="width: 50%" >
            <asp:DropDownList ID="cmbEmprole" runat="server" CssClass="MyText" >
                <asp:ListItem Value="0">No-Unit</asp:ListItem>
                <asp:ListItem Value="1">HO</asp:ListItem>
                <asp:ListItem Value="2">PI</asp:ListItem>
                <asp:ListItem Value="3">Supervisor</asp:ListItem>
                <asp:ListItem Value="4">Admin</asp:ListItem>                                
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ReqEmprole" runat="server" ControlToValidate="cmbEmprole"
            ErrorMessage="(Required)"  InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr> 
            
           <tr><td align="right" style="width: 20%; font-size:larger" >
        Emp. Location :-</td><td align="left" style="width: 50%">
            <%--<asp:TextBox ID="txtPlantNm" runat="server" CssClass="MyText" Width="80%" BackColor="MistyRose"></asp:TextBox>--%>
            <asp:DropDownList ID="cmbPlantNm" runat="server" CssClass="MyText"  AppendDataBoundItems="True">
                <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ReqPlantNm" runat="server" ControlToValidate="cmbPlantNm"
            ErrorMessage="(Required)" Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr> 
            
            
            <tr><td align="right" style="width: 20%; font-size:larger" >
        Emp. Location Code :-</td><td align="left" style="width: 50%">
            <%--<asp:TextBox ID="txtPlantCode" runat="server" CssClass="MyText" Width="80%" ></asp:TextBox> AppendDataBoundItems="True" --%>
            <asp:DropDownList ID="cmbPlantCode" runat="server" CssClass="MyText" Enabled="false">
                <asp:ListItem Value="0">Select</asp:ListItem>
                <asp:ListItem Value="1152">1152</asp:ListItem>
                <asp:ListItem Value="1156">1156</asp:ListItem>
                <asp:ListItem Value="1179">1179</asp:ListItem>
                <asp:ListItem Value="1181">1181</asp:ListItem>
                <asp:ListItem Value="1107">1107</asp:ListItem>
                <asp:ListItem Value="1108">1108</asp:ListItem>
                <asp:ListItem Value="1180">1180</asp:ListItem>
                <asp:ListItem Value="1157">1157</asp:ListItem>
                </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ReqPlantCode" runat="server" ControlToValidate="cmbPlantCode"
            ErrorMessage="(Required)" Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr> 
            
                     
    <tr><td colspan="2" align="center">
        <%--<asp:Button ID="btnSave" runat="server" CssClass="FormButton" Text="Save" OnClientClick="SaveData();return false;" Width="96px"/>--%>
        <asp:Button ID="btnSave" runat="server" CssClass="FormButton" Text="Save" OnClientClick="CheckMe('btnSave');return false;" Width="150px" Font-Bold="true"/>
        
       
        </td></tr>    
    </table>
    </div>
    
    </center>
        <asp:HiddenField ID="hdnRndNo" runat="server" /><%--used for Request Flooding Revalidation added this on 12-05-2020--%>
 <asp:HiddenField ID="ClnthdnRndNo" runat="server" /> <%--used for Request Flooding Revalidation added this on 12-05-2020--%>
    </form>
</body>
</html>
