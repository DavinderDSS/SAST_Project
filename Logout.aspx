<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Logout.aspx.vb" Inherits="Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
<%--''This code is used to cover the Weak Cache management point (GIS suggested) added this on 01-12-2019--%>
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="-1" />  <%--Commened this on 11-12-2019--%>   <%--content="Sat, 01-jan-2000 00:00:00 GMT"--%>  

<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Cache-Control" content="no-store" />
 <%--''This code is used to cover the Weak Cache management point (GIS suggested) added this on 01-12-2019--%>

<%--This is added on 20-05-2020 for GIS validation point Improper session termination--%> 
<meta http-equiv="Cache-Control" content="no-cache"/>
<meta http-equiv="Pragma" content="no-cache"/>
<meta http-equiv="Expires" content="0"/>
<%--This is added on 20-05-2020 for GIS validation point Improper session termination--%> 
    <title>Logout Page</title>
         
     <%-- <link href="css/StyleSheet.css" rel="stylesheet" type="text/css"/>--%>
     <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
     
     <%--This is added on 20-05-2020 for GIS validation point Improper session termination--%> 
     <%--//clears browser history and redirects url--%>
<script language="javascript" type="text/javascript">
function ClearHistory()
{
     var backlen = history.length;
     history.go(-backlen);
     window.location.href = loggedOutPageUrl
}
</script>
<%--This is added on 20-05-2020 for GIS validation point Improper session termination--%> 
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <table style="height:400px">
    <tr>
    <td>
        <span style="font-size: 14pt; font-family: Bookman Old Style">You have been successfully logged out.<br /><br />   <%--color: #ff9900;--%>
            <asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl="~/Login.aspx" Font-Bold="True" ToolTip="Login Again">Login Again</asp:HyperLink>   
            <asp:HyperLink ID="HlnkClose" runat="server"  NavigateUrl="javascript:window.close()" Font-Bold="true" ToolTip="Close">Close</asp:HyperLink>
        </span>
        </td>
        </tr>
        </table>
        </center>
    </form>
</body>
</html>
