<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserError.aspx.vb" Inherits="UserError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>Untitled Page</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div>
    <%--<h1><font color="blue">Please Use Only Internet Explorer (IE)</font></h1>
    <h1><font color="blue">In Compatibility Mode</font></h1>--%>
    <h1><font color="brown">Something went Wrong, Please co-ordinate with administrator</font></h1>
    <asp:HyperLink id="HyperLink1" runat="server" Text="Click Here To LogOut" NavigateUrl="Logout.aspx"></asp:HyperLink>
    </div>
    </center>
    </form>
</body>
</html>
