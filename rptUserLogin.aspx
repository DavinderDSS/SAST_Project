<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptUserLogin.aspx.vb" Inherits="rptUserLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
<link rel="stylesheet" type="text/css" href="styleSheet.css" />
    <title>User Login Report</title>
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
                     alert("From date is greater than To date");
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
                                                                //document.getElementById("btnShowReport").disabled=false;
                                                                //LoadComplete();
                                                            }
                                                        else
                                                            {  
                                                               ReportWindow = window.open("", "subWind","toolbar=no,addressbar=no,resizable,scrollbars,modal=yes");
                                                               var currentDate = new Date();
                                                               
                                                               var PlantDetailNm = document.getElementById("cmbPlantDetail");
                                                               var strPlantDetail = PlantDetailNm.options[PlantDetailNm.selectedIndex].text;
                                                               
                                                               ReportWindow.bgColor='steelblue';                                                               
                                                               ReportWindow.document.write('<h5>Hamali :User Login Report      '+ document.getElementById("txtFromDate").value + ' To ' + document.getElementById("txtToDate").value +'</h5>')
                                                               ReportWindow.document.write('<h5>Plant Detail:    '+ strPlantDetail + '</h5>')
                                                               //ReportWindow.document.title='Hamali : User Login Report Run On '+ currentDate;
                                                               ReportWindow.document.write('<h5>Report Run On ' + currentDate + '</h5>')
                                                               ReportWindow.document.write(Diff[1]); 
                                                               
                                                               document.getElementById("TableDiv").innerHTML = ""; 
                                                               //document.getElementById("btnShowReport").disabled=false;
                                                             }
                                                     
                                  }                 
                    }   
    
    
         function ExecuteServerCode()
                {
                    //alert("i am in ExecuteServerCode");
                     if (Page_ClientValidate())
                        {
                     
                              document.getElementById("TableDiv").innerHTML = "";
                              //document.getElementById("TableDiv").innerHTML = "<center><img src='images/Loading.gif'/></center>";                              
                              //document.getElementById("btnShowReport").disabled=true;
                                                            
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
                              var cmbPlantDetailValue=document.getElementById('cmbPlantDetail').options[document.getElementById('cmbPlantDetail').selectedIndex].value;
                            
                              Para=Para + "|" + "'" + cmbPlantDetailValue + "'";
                                       
                        //alert(Para);
                                               
                         //Para=Para + "|";
                         CallServer("Fifth#" + Para, "");            
            }
            } 
     
     </script>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <table id="topTable" style="width:100%; border:1;">
<tr><td align="center" style="height: 53px"><h3><u>HAMALI USER LOGIN DETAIL REPORT</u></h3></td></tr>
</table>
    <table id="topTable1" style="width:100%; border:1;">
    <tr>    
    <td align="left" style="height: 53px"><h3><u>PLANT </u> : <% Response.Write(Session("plantName"))%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <u>HEAD-OFFICER </u> : <% Response.Write(Session("initial"))%>    
    </h3></td>
    <td align="right" style="height: 53px">    
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>
        <br />    
    <center>
    <table id="MyTable" border="1"  class="MyText" width="50%">        
                 <tr>
                 <td align="left">
                 <label for="from" style="font-size:larger">FromDate</label>
                 </td>                 
                <td align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="MyText" ReadOnly="True"></asp:TextBox><a href="javascript:NewCal('txtFromDate','ddmmyyyy')"><img src="images/cal.gif" width="16"  border = "0" height="16" alt="Pick a date"/></a>
                    <asp:RequiredFieldValidator ID="ReqFromDate" runat="server" ControlToValidate="TxtFromDate"
              Display="Dynamic" ErrorMessage="(Required)" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td></tr>
                    <tr>
                    <td align="left">
                    <label for="to" style="font-size:larger">ToDate</label>
                    </td>                   
                <td  align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="MyText" ReadOnly="True" onblur="compare();"></asp:TextBox><a href="javascript:NewCal('txtToDate','ddmmyyyy')"><img src="images/cal.gif" width="16"  border = "0" height="16" alt="Pick a date"/></a>
                     <asp:RequiredFieldValidator ID="ReqToDate" runat="server" ControlToValidate="TxtToDate"
                Display="Dynamic" ErrorMessage="(Required)" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td></tr>
                    <tr>
                    <td align="left" >
                <label for="to" style="font-size:larger">Plant Detail</label> &nbsp;
                </td>
                <td  align="left">
                    <asp:DropDownList ID="cmbPlantDetail" runat="server" CssClass="MyText" Width="168px">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <%--<asp:ListItem Value="FieldCrop">FieldCrop</asp:ListItem>
                        <asp:ListItem>Vegetable</asp:ListItem>
                        <asp:ListItem>Cotton</asp:ListItem>
                        <asp:ListItem>CommonAllGrp</asp:ListItem>--%>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="ReqPlantDetail" runat="server" ControlToValidate="cmbPlantDetail"
                        Display="Dynamic" ErrorMessage="(Required)" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td></tr>
                    <tr><td colspan="2" align="center" >
                    <%-- <input id="btnShowReport" type="button" value="Show Report" onclick="ExecuteServerCode();" /> --%>
                    <input id="btnShowReport" type="image" value="Show Report" onclick="ExecuteServerCode();return false"  src="img/Checklist/ShowReport.png"  />
                    </td></tr>
    </table>
        &nbsp;
    <asp:HiddenField ID="HiddenField1" runat="server" />
    </center>
    <div id="TableDiv" style="OVERFLOW: auto; WIDTH: 96%; HEIGHT: 250px">
            <asp:GridView id="GridView1" runat="server" Font-Size="Smaller" Font-Names="Courier New" ShowFooter="true"
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
