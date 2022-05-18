<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptAuditTrail.aspx.vb" Inherits="rptAuditTrail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>Audit Trail</title> 
        <%--''This code is used to cover the Weak Cache management point (GIS suggested) added this on 01-12-2019--%>
        <meta http-equiv="Pragma" content="no-cache" />
        <meta http-equiv="Expires" content="-1" />
        <%--''This code is used to cover the Weak Cache management point (GIS suggested) added this on 01-12-2019--%>
     <link rel="stylesheet" type="text/css" href="styleSheet.css" />
<script language="javascript" type="text/javascript" src="AllJS.js"></script>    
     <script language="javascript"  type="text/javascript">
     
     function compare()
    {
    //if(Page_ClientValidate())
    //{
    
    var From=document.getElementById("txtFromDate").value;
    var To=document.getElementById("txtToDate").value;
    
    if (From=="" && To!="")
     {
        
        return true;
     }
     if (From!="" && To=="")
     {
                return true;
     }
     if (From==To)
     {
     return 0
     }
       
    //==============new code from 18-04-09   
    
                dt1=getDateObject(From,"-");
                dt2=getDateObject(To,"-");

                if(dt1>dt2)
                {
                     alert("Complaint From date is greater than To date");
                    document.form1.txtFromDate.focus();
                }

            function getDateObject(dateString,dateSeperator)
            {
                 //This function return a date object after accepting 
                 //a date string ans dateseparator as arguments
                 var curValue=dateString;
                 var sepChar=dateSeperator;
                 var curPos=0;
                 var cDate,cMonth,cYear;

                 //extract day portion
                 curPos=dateString.indexOf(sepChar);
                 cDate=dateString.substring(0,curPos);
                 
                 //extract month portion                    
                 endPos=dateString.indexOf(sepChar,curPos+1);          
                 cMonth=dateString.substring(curPos+1,endPos);

                 //extract year portion                    
                 curPos=endPos;
                 endPos=curPos+5;               
                 cYear=curValue.substring(curPos+1,endPos);
                 
                 //Create Date Object
                 dtObject=new Date(cYear,cMonth,cDate);     
                 return dtObject;
            }
//===================================
   }
     
    function ReceiveServerData(rValue)
                    {   
                     	 //alert("I am in ReceiveServerData");            			
             			var Diff = rValue.split("|")
             			             			
             			switch(Diff[0])
                                    {
                                                     
                                      case "Fifth":
                
                                                         if (Diff[1]=='')
                                                            {
                                                                document.getElementById("TableDiv").innerTEXT = "No Record Found";
                                                                document.getElementById("btnShowReport").disabled=false;
                                                                //LoadComplete();
                                                            }
                                                        else
                                                            {  
                                                               ReportWindow = window.open("", "subWind","toolbar=no,addressbar=no,resizable,scrollbars,modal=yes");
                                                               var currentDate = new Date();
                                                               //ReportWindow.bgColor='steelblue';                                                               
                                                               ReportWindow.document.write('<h5>HAMALI :Audit Trail Report'+ document.getElementById("txtFromDate").value + ' To ' + document.getElementById("txtToDate").value +'</h5>')
                                                               ReportWindow.document.title='HAMALI : Audit Trail Report Run On '+ currentDate;
                                                               ReportWindow.document.write(Diff[1]); 
                                                               
                                                               document.getElementById("TableDiv").innerHTML = ""; 
                                                               document.getElementById("btnShowReport").disabled=false;
                                                             }
                                                     
                                  }                 
                    }   
    
    
         function ExecuteServerCode()
                {
                    //alert("i am in ExecuteServerCode");
                     if (Page_ClientValidate())
                        {
                     
                              document.getElementById("TableDiv").innerHTML = "";
                              document.getElementById("TableDiv").innerHTML = "<center><img src='images/Loading.gif'/></center>";                              
                              document.getElementById("btnShowReport").disabled=true;
                                                            
                              var Para="";
                              
                              var FromDate = document.getElementById("txtFromDate").value;
                                        var fromDateArray = FromDate.split("-");
                                
                                        var FrMon;
                                        FrMon=fromDateArray[1];
                                        if (String(FrMon).length > 1)
                                            {}
                                               else
                                                   {
                                                        FrMon='0' + FrMon;
                                                   }
                                            
                                                var ActualFDt = fromDateArray[2] + "-" + FrMon + "-" + fromDateArray[0];
                                        
                                                Para = Para + "'" + ActualFDt + "'";
                                        
                                                var ToDate = document.getElementById("txtToDate").value;
                                                var toDateArray = ToDate.split("-");
                                        
                                                    var ToMon;
                                                    ToMon=toDateArray[1];
                                                    if (String(ToMon).length > 1)
                                                        {}
                                                        else
                                                        {
                                                        ToMon='0' + ToMon;
                                                       }
                                                                                   
                                                            var ActulaTDt = toDateArray[2] + "-" + ToMon + "-" + toDateArray[0];
                                                            
                                                            Para = Para + "|" + "'" + ActulaTDt + "'";   
                              
//                              
                              //var cmbBusinessunitValue=document.getElementById('cmbBusinessunit').options[document.getElementById('cmbBusinessunit').selectedIndex].value;
                              //Para=Para + "|" + "'" + cmbBusinessunitValue + "'";
                                       
                        //alert(Para);
                                               
                         //Para=Para + "|";
                         CallServer("Fifth#" + Para, "");            
            }
            } 
     
     </script>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" width="98%" >
    <tr><td align="right">
    
    <div align="right">         
             <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
    </div>
    <%--<asp:HyperLink id="lnkHome" runat="server" NavigateUrl="~/MenuForm.aspx" ForeColor="Blue" Font-Bold="True">Home</asp:HyperLink>
        &nbsp;<asp:LinkButton id="lnkSignout" runat="server" CausesValidation="False" Font-Bold="True">SignOut</asp:LinkButton>--%>
        </td></tr>
    </table>
    
     <center>       
        <h5 style="color: #0000cc">HAMALI : User Login Report</h5>
    </center>
    <center>
    <table id="MyTable" border="1"  class="MyText" width="50%">        
                 <tr><td style="width: 9%" align="left">
                    Complaint Recd. From Date :</td>
                <td style="width: 20%" align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="MyText" ReadOnly="True"></asp:TextBox><a href="javascript:NewCal('txtFromDate','ddmmyyyy')"><img src="images/cal.gif" width="16"  border = "0" height="16" alt="Pick a date"/></a>
                    <asp:RequiredFieldValidator ID="ReqFromDate" runat="server" ControlToValidate="TxtFromDate"
              Display="Dynamic" ErrorMessage="(Required)" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td></tr>
                    <tr><td style="width: 4%" align="left">
                    To Date  :</td>
                <td style="width: 12%" align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="MyText" ReadOnly="True" onblur="compare();"></asp:TextBox><a href="javascript:NewCal('txtToDate','ddmmyyyy')"><img src="images/cal.gif" width="16"  border = "0" height="16" alt="Pick a date"/></a>
                     <asp:RequiredFieldValidator ID="ReqToDate" runat="server" ControlToValidate="TxtToDate"
                Display="Dynamic" ErrorMessage="(Required)" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td></tr>
                    <%--<tr><td style="width: 9%" align="left">
                    Business Unit :</td>
                <td style="width: 20%" align="left">
                    <asp:DropDownList ID="cmbBusinessunit" runat="server" CssClass="MyText" Width="168px">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="FieldCrop">FieldCrop</asp:ListItem>
                        <asp:ListItem Value="Vegetable">Vegetable</asp:ListItem>
                        <asp:ListItem Value="Cotton">Cotton</asp:ListItem>
                        <asp:ListItem Value="CommonAllGrp">CommonAllGrp</asp:ListItem>
                        <asp:ListItem Value="RowCropsGrp">RowCropsGrp</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="ReqBusinessunit" runat="server" ControlToValidate="cmbBusinessunit"
                        Display="Dynamic" ErrorMessage="(Required)" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td></tr>--%>
                    <tr><td colspan="2" align="center" >
                    <input id="btnShowReport" type="button" value="Show Report" onclick="ExecuteServerCode();" />
                    </td></tr>
    </table>
        &nbsp;
    <asp:HiddenField ID="HiddenField1" runat="server" />
    </center>
    <div id="TableDiv" style="OVERFLOW: auto; WIDTH: 96%; HEIGHT: 250px">
            <asp:GridView id="grdAuditTrail" runat="server" Font-Size="Smaller" Font-Names="Courier New" 
            Font-Bold="True" CellPadding="4" EnableViewState="False" ForeColor="#333333">
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"  />
            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" Wrap="False"  />
            <EditRowStyle BackColor="#7C6F57" BorderColor="#004000"  />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"  />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center"  />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"  />
            <AlternatingRowStyle BackColor="White"  />
            
             <Columns>
                    <asp:TemplateField HeaderText="SN">
                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    
        </asp:GridView>        
        </div>
    </form>
</body>
</html>
