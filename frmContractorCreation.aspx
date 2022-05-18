<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmContractorCreation.aspx.vb" Inherits="frmContractorCreation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>Create New Contactor</title>
    <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
     <script language="javascript" type="text/javascript" src="AllJS.js"></script>
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
         
         Qry="insert into Hamali_Contractor_Master (Plant_Name,Plant_Code,Contractor_Code,Contractor_Name,Contractor_Address,Contractor_PhoneDtl,Flag) values ('"+ReadComboValues('cmbPlantNm')+"','"+ReadComboValues('cmbPlantCode')+"','"+ReadTextValues('txtContractorCode')+"','"+ReadTextValues('txtContractorNM')+"','"+ReadTextValues('txtContractorADD')+"','"+ReadTextValues('txtContractorPhDtl')+"','0')"
         
         //alert(Qry);
         CallServer("SaveData#"+Qry)
            }
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
        //alert(Diff);
        //alert(Diff[1]);
        switch(Diff[0])
        	{
        	case "SaveData":
				                   alert(Diff[1]);
				                   window.location="MenuPage.aspx";
				                   break 
	        case "ExistingUser" :  
	                               alert(Diff[1]);
	                               break 
	                                
			case "cmbPlantCode":
                     //alert("i am in cmbPlantCode");
				                    ClearCombo('cmbPlantCode');
				                    
				                    var ValueTextArray = Diff[1].split(",");
				                    //alert(ValueTextArray);				                   
				                    FillComboData(ValueTextArray,'cmbPlantCode');
				                    //LoadComplete();
				                    break
				                    	                   
			case "Error":
				                    alert(Diff[1]);
				                    
				                                      				                                                                    
			}
			
         
	}
 
 function FillComboData(data,obj)
 {

    if (data.length>1)
    {
 var cnt;
 var j;					
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
	            //alert(option);
	            
            }	
    }
 }   
 
 function FillCombo(obj)
    {
    
    
        //Loading();        
        var el=document.getElementById(obj);
        var code=el.options[el.selectedIndex].value;
        var Para=obj + "#" + code;
        //alert(Para);
        CallServer(Para,"");    
    }
 
 
    </script>
    
    
    
</head>
<body>
    <form id="form1" runat="server">
    <%-- This div is add on 18-03-2011 to loading the combobox error free and fast--%>
<div id="lodingDiv" style="position:absolute; top:50%; width:350px; left:35%"></div>
<%-- 18-03-2011 code is ended here --%>
    <div>    
    </div>
    <center>
    <table id="topTable" style="width:100%; border:1;">
<tr><td align="center" style="height: 53px"><h3><u>HAMALI NEW CONTRACTOR CREATION / ADDITION FORM</u></h3></td></tr>
</table>
    <table id="topTable1" style="width:100%; border:1;">
    <tr>    
    <td align="left" style="height: 53px"><h3><u>PLANT </u> : <% Response.Write(Session("plantName"))%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <u>HEAD OFFICER </u> : <% Response.Write(Session("initial"))%>    
    </h3></td>
    <td align="right" style="height: 53px">    
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>
    <br />
    
    <%--<table id="Table1"  width="95%" >
    <tr>
    <td align="LEFT">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <h3></h3></td>
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
     
     
     <tr><td align="right" style="width: 20%; font-size:larger">
        Plant Name :-</td><td align="left" style="width: 50%">
            <%--<asp:TextBox ID="txtPlantNm" runat="server" CssClass="MyText" Width="80%" ></asp:TextBox>--%>
            <asp:DropDownList ID="cmbPlantNm" runat="server" CssClass="MyText"  AppendDataBoundItems="True">
                <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ReqPlantNm" runat="server" ControlToValidate="cmbPlantNm"
            ErrorMessage="(Required)" Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr> 
            
            
            <tr><td align="right" style="width: 20%; font-size:larger" >
        Plant Code :-</td><td align="left" style="width: 50%">
            <%--<asp:TextBox ID="txtPlantCode" runat="server" CssClass="MyText" Width="80%" BackColor="MistyRose"></asp:TextBox>--%>
            <asp:DropDownList ID="cmbPlantCode" runat="server" CssClass="MyText" >
                <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ReqPlantCode" runat="server" ControlToValidate="cmbPlantCode"
            ErrorMessage="(Required)" Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr> 
     
     
     
     <tr><td align="right" style="width: 20%; font-size:larger" >Contractor Code :-</td><td align="left" style="width: 50%">
                   <asp:TextBox ID="txtContractorCode" runat="server"  CssClass="MyText" ForeColor="Black" Font-Bold="true"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqContractorCode" runat="server" ControlToValidate="txtContractorCode"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
                </td></tr>
     
     <tr><td align="right" style="width: 20%; font-size:larger" >
        Contractor Name :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtContractorNM" runat="server" CssClass="MyText" Width="50%"  ForeColor="Black" Font-Bold="true"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqContractorNM" runat="server" ControlToValidate="txtContractorNM"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr>
    
    
        <tr><td align="right" style="width: 20%; font-size:larger" >
        Contractor Address :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtContractorADD" runat="server" CssClass="MyText" Width="50%"  ForeColor="Black" Font-Bold="true"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqContractorADD" runat="server" ControlToValidate="txtContractorADD"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr>
            
            <tr><td align="right" style="width: 20%; font-size:larger" >
             Contractor Phone Detail :-</td><td align="left" style="width: 50%">
            <asp:TextBox ID="txtContractorPhDtl" runat="server" CssClass="MyText" Width="50%"  ForeColor="Black" Font-Bold="true"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ReqContractorPhDtl" runat="server" ControlToValidate="txtContractorPhDtl"
            ErrorMessage="(Required)"  Display="Dynamic"></asp:RequiredFieldValidator>
            </td></tr>    
            
                 
           
            
                     
    <tr><td colspan="2" align="center">
        <%--<asp:Button ID="btnSave" runat="server" CssClass="FormButton" Text="Save" OnClientClick="SaveData();return false;" Width="96px"/>--%>
        <asp:Button ID="btnSave" runat="server" CssClass="FormButton" Text="Save" OnClientClick="CheckMe('btnSave');" Width="150px" Font-Bold="true" />
        
       
        </td></tr>    
    </table>
    </div>
    
    </center>
    <asp:HiddenField ID="hdnRndNo" runat="server" /><%--used for Request Flooding Revalidation added this on 12-05-2020--%>
 <asp:HiddenField ID="ClnthdnRndNo" runat="server" /> <%--used for Request Flooding Revalidation added this on 12-05-2020--%>
    </form>
</body>
</html>
