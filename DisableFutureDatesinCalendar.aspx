<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DisableFutureDatesinCalendar.aspx.vb" Inherits="DisableFutureDatesinCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
<meta charset="utf-8" content=""  />
<title>jQuery Datepicker: Disable Future Dates</title>
<link rel="stylesheet" href="CloneDatepicker/DisableFutureDatesin Calendar/jquery-ui.css" />
<script src="CloneDatepicker/DisableFutureDatesin Calendar/182jquery.js" type="text/javascript"></script>
<script src="CloneDatepicker/DisableFutureDatesin Calendar/jquery-ui.js" type="text/javascript"></script>
<script type="text/javascript">
$(function() {
var date = new Date();
var currentMonth = date.getMonth();
var currentDate = date.getDate();
var currentYear = date.getFullYear();
$('#datepicker').datepicker({
maxDate: new Date(currentYear, currentMonth, currentDate)
});
});
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <p>Date: <input type="text" id="datepicker" /></p>
    </div>
    </form>
</body>
</html>
