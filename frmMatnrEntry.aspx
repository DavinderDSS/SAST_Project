<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMatnrEntry.aspx.vb" Inherits="frmMatnrEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>Create New Matnr Entry</title>
    <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
     <script language="javascript" type="text/javascript" src="js/AllJS.js"></script>
     <script language="javascript" type="text/javascript" src="Js/jquery.min.js"></script>
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
         
         /*   
         var Matnr=$("#txtMatnrCode").val();
         var MatDesc=$("#txtMatDesc").val();
         var Mtart=$("#cmbMtart").find("option:selected").text();
         var Matkl=$("#txtMatkl").val();
         var Pkgsize=$("#txtPkgSize").val();
         var Meins=$("#txtMeins").val();
         var Kind=$("#txtKind").val();
         var Varity=$("#txtVarity").val();
         //document.getElementById("btnSave").disabled=true;
         alert("Matnr---" + Matnr);
         alert("MatDesc---" + MatDesc);
         alert("Mtart---" + Mtart);
         alert("Matkl---" + Matkl);
         alert("Pkgsize---" + Pkgsize);
         alert("Meins---" + Meins);
         alert("Kind---" + Kind);
         alert("Varity---" + Varity);
         */
                 
         var InsertQry;
         
         ////////Qry="Update UserMaster SET empmailid='"+ReadTextValues('txtEmpMailId')+"',EmpRptId='"+ReadComboValues('cmbReportingTo')+"',EmpRMailId='"+ReadTextValues('txtRMailId')+"',EmpCmplntLevel="+ReadComboValues('cmbRole')+",EmpName='"+ReadTextValues('txtEmpName')+"' Where EmpStaffId='" + ReadComboValues('cmbEmpCode')+"'; Update CCFUserGroupName SET GroupName='" + ReadComboValues('cmbGroup') + "',EmpCmplntLevel="+ReadComboValues('cmbRole')+",TrDate='"+ HiddenField1 +"' Where EmpStaffId='" + ReadComboValues('cmbEmpCode')+"'"
         ////Qry="insert into CCFUserGroupName select empstaffid,'' ,empcmplntlevel,empflag,getdate() from usermaster where empstaffid not in (select empstaffid from CCFUserGroupName) and empstaffid = '" + ReadComboValues('cmbEmpCode')+"'; Update UserMaster SET empmailid='"+ReadTextValues('txtEmpMailId')+"',EmpRptId='"+ReadComboValues('cmbReportingTo')+"',EmpRMailId='"+ReadTextValues('txtRMailId')+"',EmpCmplntLevel="+ReadComboValues('cmbRole')+",EmpName='"+ReadTextValues('txtEmpName')+"' Where EmpStaffId='" + ReadComboValues('cmbEmpCode')+"'; Update CCFUserGroupName SET GroupName='" + ReadComboValues('cmbGroup') + "',EmpCmplntLevel="+ReadComboValues('cmbRole')+",TrDate='"+ HiddenField1 +"' Where EmpStaffId='" + ReadComboValues('cmbEmpCode')+"'"
         ////Qry="insert into usermaster (EmpstaffId,EmpPassword,EmpFName,EmpLName,EmpName,EmpSex,EmpMailId,EmpPMailId,EmpRptId,EmpRMailId,EmpUnit,EmpDOJ,EmpStatus,EmpDOL,EmpDesg,EmpLocn,CellNo,EmpCmplntLevel,EmpFlag) values ('"+ReadTextValues('txtEmpCode')+"','SUNGRO','"+ReadTextValues('txtEmpFName')+"','"+ReadTextValues('txtEmpLName')+"','"+ReadTextValues('txtEmpName')+"','" + ReadComboValues('cmbSex')+"','"+ReadTextValues('txtEmpMailId')+"','"+ReadTextValues('txtEmpPerMailId')+"','"+ReadTextValues('txtRepToId')+"','"+ReadTextValues('txtRMailId')+"','" + ReadComboValues('cmbUnit')+"','"+ReadDateValues('txtFromDate')+"','Active','','" + ReadComboValues('cmbDesg')+"','"+ReadTextValues('txtEmpLocn')+"','"+ReadTextValues('txtCellInfo')+"','0','0')" //its working one commented on 28-05-2012 due to error and below on is added
         
         ////InsertQry="insert into Matnr_Master (Matnr,Matdesc,Mtart,Matkl,Pkgsize,Meins,Kind,Varity) values ('"+ReadTextValues('txtMatnrCode')+"','"+ReadTextValues('txtMatDesc')+"','"+ReadComboValues('cmbMtart')+"','"+ReadTextValues('txtMatkl')+"','"+ReadTextValues('txtPkgsize')+"','"+ReadTextValues('txtMeins')+"','"+ReadTextValues('txtKind')+"','"+ReadTextValues('txtVarity')+"')"
         
         //InsertQry="insert into Matnr_Master (Matnr,Matdesc,Mtart,Matkl,Pkgsize,Meins,Kind,Varity) values ('"+ Matnr +"','"+ MatDesc +"','"+ Mtart +"','"+ Matkl +"','"+ Pkgsize +"','"+ Meins +"','"+ Kind +"','"+ Varity +"')"
         
         InsertQry="insert into Matnr_Master (Matnr,Matdesc,Mtart,Matkl,Pkgsize,Meins,Kind,Varity) values ('"+ $("#txtMatnrCode").val() +"','"+ $("#txtMatDesc").val() +"','"+ $("#cmbMtart").find("option:selected").text() +"','"+ $("#txtMatkl").val() +"','"+ $("#txtPkgSize").val() +"','"+ $("#txtMeins").val() +"','"+ $("#txtKind").val() +"','"+ $("#txtVarity").val() +"')"
         
         //alert(InsertQry);
         CallServer("SaveData#"+InsertQry)
            }
         }
      } 

//function DisplayEmpDetails()
//    {
//   
//        if (ReadComboValues('cmbPlantNm')!='0')
//        {
//        //Loading();
//        //document.getElementById("btnSave").disabled=false;
//        //document.getElementById("lnkEditRTo").disabled=false;
//        CallServer('cmbPlantNm#'+ReadTextValues('cmbPlantNm'),"");
//        }
//    }



   function ExistingMatnr()
   {
   
   var Para='';
   
   if(ReadTextValues('txtMatnrCode')!=null)
   {
   //Para='ExistingUser';
   var Qry;
   Qry="'"+ $("#txtMatnrCode").val() +"','"+ $("#txtMatDesc").val() +"','"+ $("#cmbMtart").find("option:selected").text() +"','"+ $("#txtMatkl").val() +"','"+ $("#txtPkgSize").val() +"'";  //,'"+ $("#txtPkgSize").val() +"'
   alert(Qry);
   CallServer("ExistingMatnr#"+Qry);
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
	        case "ExistingMatnr" :  
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
<tr><td align="center" style="height: 53px"><h3><u>HAMALI NEW MATNR ENTRY FORM</u></h3></td></tr>
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
     <tr><td align="right" style="width: 20%; font-size:larger" >Matnr :-</td><td align="left" style="width: 50%">
                   <asp:TextBox ID="txtMatnrCode" runat="server"  CssClass="MyText" ForeColor="Red"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqMatnrCode" runat="server" ControlToValidate="txtMatnrCode"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
                </td></tr>
                
                
     <tr><td align="right" style="width: 20%; font-size:larger" >MatDesc :-</td><td align="left" style="width: 50%">
                   <asp:TextBox ID="txtMatDesc" runat="server"  CssClass="MyText" ForeColor="Red"  Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqMatDesc" runat="server" ControlToValidate="txtMatDesc"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
                </td></tr>           
     
     
     <tr><td align="right" style="width: 20%; font-size:larger" >
        Mtart :-</td><td align="left" style="width: 50%" >
            <asp:DropDownList ID="cmbMtart" runat="server" CssClass="MyText" >
                <asp:ListItem Value="0">No-Mtart</asp:ListItem>
                <asp:ListItem Value="DIEN">DIEN</asp:ListItem>
                <asp:ListItem Value="FERT">FERT</asp:ListItem>
                <asp:ListItem Value="HALB">HALB</asp:ListItem>
                <asp:ListItem Value="HAWA">HAWA</asp:ListItem>
                <asp:ListItem Value="ROH">ROH</asp:ListItem>
                <asp:ListItem Value="STORE">STORE</asp:ListItem>
                <asp:ListItem Value="UNBW">UNBW</asp:ListItem>
                <asp:ListItem Value="VERP">VERP</asp:ListItem>                               
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ReqMtart" runat="server" ControlToValidate="cmbMtart"
            ErrorMessage="(Required)"  InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr>
     
     <tr><td align="right" style="width: 20%; font-size:larger" >
        Matkl :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtMatkl" runat="server" CssClass="MyText" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqMatkl" runat="server" ControlToValidate="txtMatkl"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr>
    
    
        <tr><td align="right" style="width: 20%; font-size:larger" >
        PkgSize :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtPkgSize" runat="server" CssClass="MyText">1</asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqPkgSize" runat="server" ControlToValidate="txtPkgSize"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr>
            
            <tr><td align="right" style="width: 20%; font-size:larger" >
             Meins :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtMeins" runat="server" CssClass="MyText">KG</asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqMeins" runat="server" ControlToValidate="txtMeins"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr>           
            
       <tr><td align="right" style="width: 20%; font-size:larger" >
        Kind :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtKind" runat="server" CssClass="MyText"  Width="30%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqKind" runat="server" ControlToValidate="txtKind"
                ErrorMessage="(Required)" Display="Dynamic"></asp:RequiredFieldValidator></td></tr>
                
              
 <tr><td align="right" style="width: 20%; font-size:larger" >
        Varity :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtVarity" runat="server" CssClass="MyText"  Width="50%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqVarity" runat="server" ControlToValidate="txtVarity"
                ErrorMessage="(Required)" Display="Dynamic"></asp:RequiredFieldValidator></td></tr>
            
            
       
            
                     
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
