<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAmendHamaliBilling.aspx.vb" Inherits="frmAmendHamaliBilling" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>Amend Hamali Billing</title>
<style type="text/css">
        .Gridview
        {
        font-family:Verdana;
        font-size:10pt;
        font-weight:normal;
        color:black;
        }
</style>
     
     
     
<link href="StyleSheet.css" rel="stylesheet" type="text/css" />
     
<link type="text/css" href="CloneDatepicker/FrmToDateLimit/css/ui-lightness/1819jquery-ui.custom.css" rel="stylesheet" />
<script type="text/javascript" src="CloneDatepicker/FrmToDateLimit/js/172jquery.min.js"></script>
<script type="text/javascript" src="CloneDatepicker/FrmToDateLimit/js/1819jquery-ui.custom.min.js"></script>
     
<script type="text/javascript">
            $(function() 
            {
                var dates = $("#txtFrom, #txtTo").datepicker(
                {
                    //minDate: '0',
                    //maxDate: '+1M',
                    onSelect: function(selectedDate) 
                    {
                    var option = this.id == "txtFrom" ? "minDate" : "maxDate",
                    instance = $(this).data("datepicker"),
                    date = $.datepicker.parseDate(
                    instance.settings.dateFormat ||
                    $.datepicker._defaults.dateFormat,
                    selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                    }
                });
            });
</script>

<script language="javascript" type="text/javascript">
function Validation()
      {
                            var validate=true;
							$("#MyTable").find("input[type=text]").each(function()
							{
							            if ($(this).is("text"))
							             {
							                if (validate==true)
							                {
							                    if($(this).text().length > 0)
							                    {	
								                    btnSubmit_onclick(1);					
							                    }
							                    else
							                    {
							                    alert("Enter value in text box");
							                    //alert(validate);
							                    return false;
							                    }
							                }							
							            }
							            return false;						
							});
      }
</script>

<script language="javascript" type="text/javascript">
function ReceiveServerData(rValue)
                    {   
                     	 //alert("I am in ReceiveServerData");            			
             			var Diff = rValue.split("|")
             			             			
             			switch(Diff[0])
                                    {
                                     case "Fifth":
                                                    
                                                        //GetRadioButtonFlagType();
                                                         if (Diff[1]=='')
                                                            {
                                                                document.getElementById("Div1").innerTEXT = "No Record Found";
                                                                
                                                            }
                                                        else
                                                            {
                                                            
                                                             ReportWindow = window.open("", "subWind","toolbar=no,addressbar=no,resizable,scrollbars,modal=yes");
                                                               var currentDate = new Date();
                                                               var ContractorNm = document.getElementById("cmbContractor");
                                                               var str = ContractorNm.options[ContractorNm.selectedIndex].text;
                                                               ReportWindow.bgColor='steelblue';                                                               
                                                               ReportWindow.document.write('<h5>Amended Rate Hamali Billing Details : Billing Period:Fm  '+ document.getElementById("txtFrom").value + ' To ' + document.getElementById("txtTo").value +' <br /> </h5>')
                                                               ReportWindow.document.write('<h5>Plant Detail:    '+ document.getElementById("HiddenFieldPlantName").value + '</h5>')
                                                               ReportWindow.document.write('<h5>Contractor Detail:    '+ str + '</h5>')
                                                               ReportWindow.document.write('<h5>Report Run On ' + currentDate + '</h5>')
                                                               ReportWindow.document.write(Diff[1]);                                                               
                                                               document.getElementById("Div1").innerHTML = ""; 
                                                               
                                                             
                                                             }
                                                     }
                          switch(Diff[0])
                                    {
                                                     case "Sixth":
                                                    
                                                        //GetRadioButtonFlagType();
                                                         if (Diff[1]=='')
                                                            {
                                                                document.getElementById("Div1").innerTEXT = "No Record Found";
                                                                
                                                            }
                                                        else
                                                            {
                                                            
                                                             ReportWindow = window.open("", "subWind","toolbar=no,addressbar=no,resizable,scrollbars,modal=yes");
                                                               var currentDate = new Date();
                                                               var ContractorNm = document.getElementById("cmbContractor");
                                                               var str = ContractorNm.options[ContractorNm.selectedIndex].text;
                                                               ReportWindow.bgColor='steelblue';                                                               
                                                               ReportWindow.document.write('<h5>Amended Rate Hamali Billing Details : Billing Period:Fm  '+ document.getElementById("txtFrom").value + ' To ' + document.getElementById("txtTo").value +' <br /> </h5>')
                                                               ReportWindow.document.write('<h5>Plant Detail:    '+ document.getElementById("HiddenFieldPlantName").value + '</h5>')
                                                               ReportWindow.document.write('<h5>Contractor Detail:    '+ str + '</h5>')
                                                               ReportWindow.document.write('<h5>Report Run On ' + currentDate + '</h5>')
                                                               ReportWindow.document.write(Diff[1]);                                                               
                                                               document.getElementById("Div1").innerHTML = ""; 
                                                               
                                                             
                                                             }
                                                     
                                                     
                                  }                 
                    }
                    
 function ExecuteServerCode()
                {
                    //alert("i am in ExecuteServerCode");
                     if (Page_ClientValidate())
                        {
                              //GetRadioButtonFlagType();                     
                     
                              //document.getElementById("TableDiv").innerHTML = "";
                              //document.getElementById("TableDiv").innerHTML = "<center><img src='images/Loading.gif'/></center>";                              
                              //document.getElementById("btnShowReport").disabled=true;
                                                            
                              var Para="";
                              
                              var FromDate = document.getElementById("txtFrom").value;
                                        var fromDateArray = FromDate.split("-");
                                        var FrMon;
                                        FrMon=fromDateArray[1];
                                        if (String(FrMon).length > 1)
                                            {}
                                               else
                                                   {
                                                        FrMon='0' + FrMon;
                                                   }
                                            
                                                var ActualFDt = fromDateArray[0] + "-" + FrMon + "-" + fromDateArray[2];
                                        
                                                Para = Para + "'" + ActualFDt + "'";
                                        
                                         var ToDate = document.getElementById("txtTo").value;
                                         var toDateArray = ToDate.split("-");
                                         var ToMon;
                                                    ToMon=toDateArray[1];
                                                    if (String(ToMon).length > 1)
                                                        {}
                                                        else
                                                        {
                                                        ToMon='0' + ToMon;
                                                       }
                                                                                   
                                                            var ActulaTDt = toDateArray[0] + "-" + ToMon + "-" + toDateArray[2];
                                                            
                                                            Para = Para + "|" + "'" + ActulaTDt + "'";   
                                                        
                              
                                    var Plantcode = document.getElementById("HiddenFieldPC").value;
                                    
                                    Para = Para + "|" + "'" + Plantcode + "'";
                                   
                                    var Crop = document.getElementById("cmbT").value; 
                                    
                                    Para=Para + "|" + "'" + Crop + "'";     
                                         
                                    var Contractor = document.getElementById("cmbContractor").value;     
                                    Para=Para + "|" + "'" + Contractor + "'";
                                        
                                    //alert(Para);
                                                               
                                        
                                         CallServer("Fifth#" + Para, "");            
            }
            }                   



function ExecuteServerCodeSummary()
                {
                    //alert("i am in ExecuteServerCode");
                     if (Page_ClientValidate())
                        {
                              //GetRadioButtonFlagType();                     
                     
                              //document.getElementById("TableDiv").innerHTML = "";
                              //document.getElementById("TableDiv").innerHTML = "<center><img src='images/Loading.gif'/></center>";                              
                              //document.getElementById("btnShowReport").disabled=true;
                                                            
                              var Para="";
                              
                              var FromDate = document.getElementById("txtFrom").value;
                                        var fromDateArray = FromDate.split("-");
                                        var FrMon;
                                        FrMon=fromDateArray[1];
                                        if (String(FrMon).length > 1)
                                            {}
                                               else
                                                   {
                                                        FrMon='0' + FrMon;
                                                   }
                                            
                                                var ActualFDt = fromDateArray[0] + "-" + FrMon + "-" + fromDateArray[2];
                                        
                                                Para = Para + "'" + ActualFDt + "'";
                                        
                                         var ToDate = document.getElementById("txtTo").value;
                                         var toDateArray = ToDate.split("-");
                                         var ToMon;
                                                    ToMon=toDateArray[1];
                                                    if (String(ToMon).length > 1)
                                                        {}
                                                        else
                                                        {
                                                        ToMon='0' + ToMon;
                                                       }
                                                                                   
                                                            var ActulaTDt = toDateArray[0] + "-" + ToMon + "-" + toDateArray[2];
                                                            
                                                            Para = Para + "|" + "'" + ActulaTDt + "'";   
                                                        
                              
                                    var Plantcode = document.getElementById("HiddenFieldPC").value;
                                    
                                    Para = Para + "|" + "'" + Plantcode + "'";
                                   
                                    var Crop = document.getElementById("cmbT").value; 
                                    
                                    Para=Para + "|" + "'" + Crop + "'";     
                                         
                                    var Contractor = document.getElementById("cmbContractor").value;     
                                    Para=Para + "|" + "'" + Contractor + "'";
                                        
                                    //alert(Para);
                                                               
                                        
                                         CallServer("Sixth#" + Para, "");            
            }
            }  
</script>





<style type="text/css">
.ui-datepicker { font-size:8pt !important}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <table id="topTable" style="width:100%; border:1;">
<tr><td align="center" style="height: 53px"><h3><u>AMEND HAMALI BILLING FORM</u></h3></td></tr>
</table>
    <table id="topTable1" style="width:100%; border:1;">
    <tr>    
    <td align="left" style="height: 53px"><h3><u>PLANT </u> : <% Response.Write(Session("plantName"))%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <u>PLANT INCHARGE </u> : <% Response.Write(Session("initial"))%>    
    </h3></td>
    <td align="right" style="height: 53px">    
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>
<%--<table id="topTable" style="width:100%; border:1;">
<tr><td align="center" style="width:100%"><h3><u>HAMALI BILLING</u></h3></td></tr>
</table>
    <table id="topTable1" style="width:100%; border:1;">
    <tr>    
    <td align="left" style="height: 53px"><h3><u>PLANT </u> : <% Response.Write(Session("plantName"))%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <u>PLANT INCHARGE </u> : <% Response.Write(Session("initial"))%>    
    </h3></td>
    <td align="right"  style="width:100%">    
        <asp:HyperLink ID="HylHome" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HylLogOt" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>--%>
        <br />    
    
   <center>           
            <div class="demo">
                            <table width="80%" id="MyTable" border="1">
                                    <tr><td align="left">
                                    <label for="from" style="font-size:larger">FromDate</label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                    <asp:TextBox ID="txtFrom" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtFrom" SetFocusOnError="true" ErrorMessage="Enter From Date" ID="rfvtxtfrom" runat="server"></asp:RequiredFieldValidator>                                    
                                    </td>
                                    <td align="left">
                                    <label for="to" style="font-size:larger">ToDate</label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                    <asp:TextBox ID="txtTo" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtTo" SetFocusOnError="true" ErrorMessage="Enter To Date" ID="rfvtxtto" runat="server"></asp:RequiredFieldValidator>                            
                                    </td>
                                    </tr>
                                    <tr>
                                    <td align="left">
                                    <label for="from" style="font-size:larger">Crop Type</label> &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                    <asp:DropDownList ID="cmbT" runat="server" >
			                        <asp:ListItem Value="0">Select</asp:ListItem>
			                        <asp:ListItem Value="%">All</asp:ListItem>
			                        <asp:ListItem value="cotton">Cotton</asp:ListItem>
			                        <asp:ListItem value="field">Field Crop</asp:ListItem>
			                        <asp:ListItem value="vegetable">Vegetable</asp:ListItem>
			                        <asp:ListItem value="store">Store/other</asp:ListItem>
			                        </asp:DropDownList>
			                        <asp:RequiredFieldValidator ID="rfvcmbT" runat="server" SetFocusOnError="true" ErrorMessage="Select Crop Type" ControlToValidate="cmbT" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="left">
                                    <label for="to" style="font-size:larger">Contractor</label> &nbsp;
                                    </td>
                                    <td align="left">
                                    <asp:DropDownList ID="cmbContractor" runat="server" >
			                        <asp:ListItem Value="0">Select</asp:ListItem>
			                        </asp:DropDownList>
			                        <asp:RequiredFieldValidator ID="ReqcmbContractor" runat="server" SetFocusOnError="true" ErrorMessage="Select Contractor " ControlToValidate="cmbContractor" InitialValue="0"></asp:RequiredFieldValidator>                                                            
                                    </td>                                    
                                   </tr>
                                   <tr>
                                    <td colspan="4" align="center">
                                    
                                    <%--<asp:Button ID="btnshow" runat="server" value="show report" Text="show report" Width="95%" Font-Bold="true" OnClick="ExecuteServerCode();" />
                                    <input id="btnShowReport" type="button" style="width:20%" value="Show Detail" onclick="ExecuteServerCode();" />
                                    <input id="btnShowSummaryReport" type="button" style="width:20%" value="Show Summary Detail" onclick="ExecuteServerCodeSummary();" />
                                    --%>
                                    <input id="btnShowReport" type="image" value="Show Report" onclick="ExecuteServerCode();return false"  src="img/Checklist/ShowReport.png"  />
                                    <input id="btnShowSummaryReport" type="image" value="Show Summary Detail" onclick="ExecuteServerCodeSummary();return false" src="img/Checklist/ShowSummaryDetail.png" />
                                    </td>
                                    </tr>
                            </table>
            </div>
   </center>    
   <br />
        <asp:HiddenField ID="HiddenFieldPC" runat="server" />
        <asp:HiddenField ID="HiddenFieldPlantName" runat="server" />
   <br />
   <%--<br /> --%>  
<center>        
    
 <div id="Div1" style="OVERFLOW: auto; WIDTH: 96%; HEIGHT: 150px"> 
            <asp:GridView id="GridView1" runat="server" Font-Size="Smaller" Font-Names="Courier New" ShowFooter="true"
                    Font-Bold="True" CellPadding="4" EnableViewState="False" ForeColor="#333333">
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"  />
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="center" Wrap="False"  />
                    <EditRowStyle BackColor="#7C6F57" BorderColor="#004000"  />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"  />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center"  />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"  />
                    <AlternatingRowStyle BackColor="White"  />
                    <Columns>
                                <asp:TemplateField HeaderText="SrNo">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                </asp:TemplateField>
                    </Columns>
                            
          </asp:GridView> 
             
 </div>
    
 
</center>


        
        
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="False" />

        
        
        
<br /><br /><br />

    </form>
</body>
</html>
