<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<%--Below is added on 28-11-2019 for implemention of captcha--%>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <%--Ended here added on 28-11-2019 for implemention of captcha--%>
    <%--''This code is used to cover the Weak Cache management point (GIS suggested) added this on 01-12-2019--%>
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="-1" />   <%--content="Sat, 01-jan-2000 00:00:00 GMT"--%>  
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Cache-Control" content="no-store" />
 <%--''This code is used to cover the Weak Cache management point (GIS suggested) added this on 01-12-2019--%>
    <title>Hamali Login Page</title>
   <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript" src="js/360jquery.js"></script>
   <script language="javascript" type="text/javascript" src="js/aes.js"></script> <%--THis is added for CSRF point--%>
   <script language="javascript" type="text/javascript">
////   $(document).ready(function(){
////   if($("#Login1_UserName").val()=='')
////   {}
////       else
////       {
////       $("#Login1_LoginLinkButton").click(function(){ WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("Login1$LoginLinkButton", "", true, "Login1", "", false, true)); });
////       $("#Login1_LoginLinkButton").click();
////       }
////   });
   
   $(document).ready(function(){
    
    /////Need to check the counts on client side if they are 3 or morethat that display the below details and need to take hidden field values for captcha text and count anand sir shared the code on 05-12-2019
    //$("#Login1_LoginLinkButton").parent().parent().parent().append("<tr><td><img src='Captcha.aspx' /></td></tr>");
    if (($("#hfCount").val() >= 3 || $("#hfCount").val() == 5) && $("#hfAccLockCout").val() == 0) //!= 1 replace with
    {
    $("#Login1_LoginLinkButton").closest('table').find('tr:last').prev().after("<tr><td></td><td><center><img src='Captcha.aspx' /></td></center></tr><tr><td>Captcha</td><td><input type='text' style='width:70px;' id='txtmycaptcha' autofocus/></td></tr>");
//    <body OnLoad="document.myform.txtmycaptcha.focus();">
//    var myCap = $("#txtmycaptcha").val();
//    $("#hfCaptchaVal").val(myCap);
//    alert(myCap);
    }
    
    //Added this on 06-12-2019 for captcha wrong massage text
//    if ($("#hflblCaptcha").val() !== "")
//                {
//                var myCaplblval = $("#lblcaptcha").val();
//                $("#hflblCaptcha").val(myCaplblval);
//                }
    
    //ended here added code on 06-12-2019 for captcha wrong massage text
    
    //Added this on 06-12-2019 for captcha wrong massage text  //log out button is populate in wrong instance thats why commetented on 11-12-2019
    //if (($("#hfCount").val() >= 3 || $("#hfCount").val() == 5) && $("#hfSignOut").val() == 1 && $("#hfAccLockCout").val() == 0)    
    //{        
    //$("#Login1_LoginLinkButton").closest('table').find('tr:last').prev().after("<tr><td></td><td align='right'><a href='Logout.aspx' style='display:inline-block;color:#8b0000;background-color:#FFFBFF;border-color:#CCCCCC;border-width:2px;border-style:Ridge;font-family:Verdana;font-size:0.8em;font-weight:bold;' id='atab'>Logout</a></td></tr>");
    //}
    //Added this on 06-12-2019 for captcha wrong massage text
        
    
//    if ($("#hfSignOut").val() == 1)
//    {    
//    $("#Login1_LoginLinkButton").closest('table').find('tr:last').prev().after("<tr><td></td><td align='right'><a href='Logout.aspx' style='display:inline-block;color:#8b0000;background-color:#FFFBFF;border-color:#CCCCCC;border-width:2px;border-style:Ridge;font-family:Verdana;font-size:1.1em;font-weight:bold;' id='atab'>Logout</a></td></tr>");
//    }
    
    $("#Login1_LoginLinkButton").click(function(){ 
    return showLoadingLGC();
     WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("Login1$LoginLinkButton", "", true, "Login1", "", false, true)); });  //showLoadingLGC();
    
    });
   
   function showLoadingLGC() {
//debugger;
//alert("I am in showLoadingLGC");
            var valid = false;
            var errorMsg = "";
            $("#lblErrorMsg").empty();                     
                               
           
           //Below code is added on 13-05-2020 for login validaton massage display
                if ($("#Login1_UserName").val().trim() == "" && $("#Login1_Password").val().trim() == "")
                {
                valid = false;
                $("#lblErrorMsg").empty();
                 $("#lblErrorMsg").append("Either Login ID Or Password is Blank");  //Invalid !!! Try Again...
                }
                //Cod is ended here added on 12-05-2020
           
                //*********Added this is on 05-12-2019 for captcha value using client side
                if ($("#hfCount").val() >= 3 || $("#hfCount").val() == 5)
                {
                var myCap = $("#txtmycaptcha").val();
                $("#hfCaptchaVal").val(myCap);
                
                //Below code is added on 12-05-2020 for login valodaton and captcha display
                if (myCap == "")
                {
                valid = false;
                $("#lblErrorMsg").empty();
                 $("#lblErrorMsg").append("Please enter the Captcha and Login Details first");
                }
                //Cod is ended here added on 12-05-2020
                
                //Below Code is added on 26-05-2020 as if  user not enter password 
                
                if (myCap != "" && $("#Login1_Password").val().trim() == "")
                {
                valid = false;
                $("#lblErrorMsg").empty();
                 $("#lblErrorMsg").append("Either Captcha Or Password is Blank");  //Invalid !!! Try Again...
                }
                
                //Code is ended here added on 26-05-2020 
                
                if ($("#Login1_UserName").val().trim() != "" && $("#Login1_Password").val().trim() != "" && $("#txtmycaptcha").val() != "") 
                    { // This is for login control BUtton click option
                    
                        var key = CryptoJS.enc.Utf8.parse('1020604010208030');
                        var iv = CryptoJS.enc.Utf8.parse('1020604010208030');

                        var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#Login1_Password").val()), key,
                        {
                            keySize: 128 / 8,
                            iv: iv,
                            mode: CryptoJS.mode.CBC,
                            padding: CryptoJS.pad.Pkcs7
                        });
                          
                        
                        $("#Login1_Password").val(encryptedpassword);
                            
                        valid = true;
                    }

                }
                else
                {
                    if ($("#Login1_UserName").val().trim() != "" && $("#Login1_Password").val().trim() != "") 
                    { 
                    
                        var key = CryptoJS.enc.Utf8.parse('1020604010208030');
                        var iv = CryptoJS.enc.Utf8.parse('1020604010208030');

                        var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#Login1_Password").val()), key,
                        {
                            keySize: 128 / 8,
                            iv: iv,
                            mode: CryptoJS.mode.CBC,
                            padding: CryptoJS.pad.Pkcs7
                        });                  
                        
                        $("#Login1_Password").val(encryptedpassword);
                    
                        valid = true;
                    }
                    
                    else
                        {
                        $("#lblErrorMsg").empty();
                        $("#lblErrorMsg").append("Either Login ID Or Password is Blank");
                        //var IDOrPassBlnk= "Either Login ID Or Password is Blank"
                        //$("#lblErrorMsg").text("Either Login ID Or Password is Blank");
                        valid = false;
                        }
                    
                }
            //alert(valid);
            
            
            return valid; //Added this on 09-12-2019 for checking the function retun value for sending user to server side suggested by anand Sir
        }
   
   
   </script>
   
   
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    
 <%--       <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/MenuPage.aspx">
        </asp:Login>--%>

<br />
<br />
<br />
<br />
<br />
<br />
<div align="center">
  <center>
  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;height:55%" width="70%" id="AutoNumber1" >
    <tr>
      <td width="46%"  height="70%" colspan="3">
                      
                      <p align="center"><b style="font-size:larger"><font face="Arial" >Welcome to HAMALI Management and Billing System</font></b></p>
                        <p align="center"><b style="font-size:medium; color:Purple"><font face="Arial" >Use IE6 and above Explorer for best performance</font></b></p>
                        <br />
                        <center>
                                    <table id="LoginTable" width="70%" style="height: 200px">
                                            <tr>                                            
                                            <td style="width:55%; background-image:url(images/hamaliGI.jpeg)"></td>
                                                    <td align="center" style="width:90%; height:100px"  >
                                                    
                                                                <asp:Login ID="Login1" runat="server"  BorderColor="#F7F6F3" BorderPadding="8"
                                                                    BorderStyle="None" BorderWidth="0px" DestinationPageUrl="~/MenuPage.aspx"
                                                                    DisplayRememberMe="False" Font-Names="Verdana" Font-Size="10pt" ForeColor="#333333" 
                                                                    TitleText="Log-In to Hamali-Module" Font-Bold="True" UserNameLabelText="EMP Code:" FailureText="Invalid !!! Try Again" Width="100%" LoginButtonType="Link"> 
                                                                    <TitleTextStyle  Font-Bold="True" Font-Size="0.9em" ForeColor="#FF66CC" BorderStyle="Outset" /> 
                                                                    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                                                                    <TextBoxStyle Font-Size="0.8em" BorderStyle="Ridge" />
                                                                    <LoginButtonStyle  BorderColor="#CCCCCC" BorderStyle="Ridge" BorderWidth="2px"
                                                                        Font-Names="Verdana" Font-Size="1.1em" ForeColor="#284775" Font-Bold="True" />
                                                                </asp:Login>
                                                    <asp:Label ID="lblErrorMsg" runat="server" style="color:Purple; font-size:medium;" ></asp:Label>
                                                     </td>
                                             </tr>
                                             
                                   </table>
                          </center>
 
        </td>
      </tr>
      
    <%--<tr>
      
      <td align="right" style="width:60%"><p align="center"><b><font face="Arial" color="gray" size="7">HAMALI</font></b></p></td>      
      <td align="left" style="width:40%"><img border="0" src="img/gear.GIF"  style="height:70px; width:30%" alt="" /></td>      
      </tr>   --%> 
      
      <tr>
                        <td  align="left" style="height: 20px">
                            <marquee behavior="scroll" scrollamount="3" direction="left" style="color: #FF0066;
                                font-weight: bold;">Please use your Helpdesk Staff ID and Password for Login..</marquee>
                        </td>
                    </tr>  
            </table>
            </center>
    </div>
    <asp:HiddenField ID="antiforgery" runat="server"/>
 <asp:HiddenField runat="server" ID="ClientKey"/>
        
        <asp:HiddenField ID="hfCount" runat="server"/>
 <asp:HiddenField runat="server" ID="hfCaptchaVal"/>
 <asp:HiddenField runat="server" ID="hflblCaptcha"/>
 <asp:HiddenField runat="server" ID="hfSignOut"/>
 <asp:HiddenField runat="server" ID="hfAccLockCout"/>
    </form>
</body>
</html>
