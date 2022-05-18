<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptPurposeActivityWiseHamaliDtl.aspx.vb" Inherits="rptPurposeActivityWiseHamaliDtl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>Hamali Purpose Activity Wise Detail Report</title>
     <%--<link href="css/StyleSheet.css" rel="stylesheet" type="text/css"/>--%>
<style type="text/css">
        .Gridview
        {
        font-family:Verdana;
        font-size:10pt;
        font-weight:normal;
        color:black;
        }
</style>

<%--<style type="text/css">
table,th,td
{
border:1px solid black;
}
</style> --%>
     
     
<link href="StyleSheet.css" rel="stylesheet" type="text/css" />
     
<link type="text/css" href="CloneDatepicker/FrmToDateLimit/css/ui-lightness/1819jquery-ui.custom.css" rel="stylesheet" />
<script type="text/javascript" src="CloneDatepicker/FrmToDateLimit/js/172jquery.min.js"></script>
<script type="text/javascript" src="CloneDatepicker/FrmToDateLimit/js/1819jquery-ui.custom.min.js"></script>
<script type="text/javascript" src="AllJS.js"></script>     
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
      
  function FillCombo(obj)
    {
    
    
        //Loading();        
        var el=obj;//document.getElementById(obj);
        var code=el.options[el.selectedIndex].value;
        //alert(el);
        //alert(code);
        //alert(obj.outerHTML);
        var Para=$(obj).attr("id") + "#" + code;
        //alert(Para);
        CallServer(Para,"");    
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
 
 function FillComboActivity(obj)
    {
    
    
        //Loading();        
        var el=obj;//document.getElementById(obj);
        var code=el.options[el.selectedIndex].value;
        //alert(el);
        //alert(code);
        //alert(obj.outerHTML);
        var Para=$(obj).attr("id") + "#" + code;
        //alert(Para);
        CallServer(Para,"");    
    }
 
  
 function FillComboDataActivity(data,obj)
 {
//alert("i am in FillComboDataActivity");

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
 
 function FillComboDist(obj)
    {
    
    
        //Loading();        
        var el=obj;//document.getElementById(obj);
        var code=el.options[el.selectedIndex].value;
        //alert(el);
        //alert(code);
        //alert(obj.outerHTML);
        var Plant = document.getElementById("cmbPlant").value;
        var Para=$(obj).attr("id") + "#" + Plant;
        //alert(Para);
        CallServer(Para,"");    
    }
 
  
 function FillComboDataDist(data,obj)
 {
//alert("i am in FillComboDataActivity");

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
 
//function FillComboActivityType(obj)
//{
//var el=obj;
//var code=el.options[el.selectedIndex].value;
//var Plant = document.getElementById("cmbPlant").value;
//if (Plant=='1157')
//{
//$("#cmbActivityType").attr('value','Z');
//}

//}
      
</script>

<script language="javascript" type="text/javascript">
function ReceiveServerData(rValue)
                    {   
                     	 //alert("I am in ReceiveServerData");            			
             			var Diff = rValue.split("|")
             			//alert(Diff);
             			var cmbContractorVal= Diff[0]
             			             			
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
                                                               
                                                               var ActivityNm = document.getElementById("cmbActivity");
                                                               var str = ActivityNm.options[ActivityNm.selectedIndex].text;                                                              
                                                               var PlantNm = document.getElementById("cmbPlant");
                                                               var strPlant = PlantNm.options[PlantNm.selectedIndex].text;
                                                               var CropNm = document.getElementById("cmbT");
                                                               var strCrop = CropNm.options[CropNm.selectedIndex].text;
                                                               var ActivitytypeNm = document.getElementById("cmbActivityType");
                                                               var strActType = ActivitytypeNm.options[ActivitytypeNm.selectedIndex].text;
                                                               
                                                               var PurposeNm = document.getElementById("cmbPurpose");
                                                               var strPurpose = PurposeNm.options[PurposeNm.selectedIndex].text;
                                                               
                                                               
                                                               
                                                               ReportWindow.bgColor='steelblue';                                                               
                                                               ReportWindow.document.write('<h5>Hamali Purpose Activity Wise Detail : Period:Fm  '+ document.getElementById("txtFrom").value + ' To ' + document.getElementById("txtTo").value +' <br /> </h5>')
                                                               ReportWindow.document.write('<h5>Plant Detail:    '+ strPlant + '</h5>')
                                                               ReportWindow.document.write('<h5>Activity Detail:    '+ str + '</h5>')
                                                               ReportWindow.document.write('<h5>Crop Type Detail:    '+ strCrop + '</h5>')
                                                               ReportWindow.document.write('<h5>Activity Type Detail:    '+ strActType + '</h5>')
                                                               ReportWindow.document.write('<h5>Purpose Detail:    '+ strPurpose + '</h5>')                                                               
                                                               ReportWindow.document.write('<h5>Report Run On ' + currentDate + '</h5>')
                                                               ReportWindow.document.write(Diff[1]);                                                               
                                                               document.getElementById("Div1").innerHTML = ""; 
                                                               
                                                             
                                                             }
                                                             
                                                  
                                  } 
                                  
              
              
    var cmbActivityVal= Diff[0]
         //alert(cmbKNVVal);
        //alert(Diff[0]);
        var DiffVal=Diff[0].substring(0,13);
        //alert(DiffVal);
		
		// to clear all further combobox
        
        if(DiffVal=="cmbActivity")
        {      
        
        switch(DiffVal)
        	{
        	case "cmbActivity":
                                                                     //alert("i am in cmbcontraactor case");
                                                                     //alert(cmbContractorVal);
                                                                     ClearCombo(cmbActivityVal);				                    
				                                                     var ValueTextArray = Diff[1].split(",");
				                                                     //alert(ValueTextArray);
				                                                     //var ValueTextArray1  = ValueTextArray[1].split("~");
				                                                     //var ValueTextArray2 = ValueTextArray[0]+','+ValueTextArray1[0];                   
				                                                     //var a=ValueTextArray2.split(",");		                   
				                                                     FillComboData(ValueTextArray,cmbActivityVal);
				                                                     //FillComboData(ValueTextArray,cmbContractorVal);
				                                                     break
                                                             
                                                             case "Error":              
				                                                                  alert(Diff[1]);
        	
        	
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
                                                        
                              
                                    //var Plantcode = document.getElementById("HiddenFieldPC").value;
                                    
                                    //Para = Para + "|" + "'" + Plantcode + "'";
                                                            
                                    var Plant = document.getElementById("cmbPlant").value; 
                                    
                                    Para=Para + "|" + "'" + Plant + "'";   
                                         
                                    var Activity = document.getElementById("cmbActivity").value;     
                                    
                                    Para=Para + "|" + "'" + Activity + "'";                                    
                                    
                                    var Crop = document.getElementById("cmbT").value;     
                                    
                                    Para=Para + "|" + "'" + Crop + "'";
                                    
                                    var ActivityType = document.getElementById("cmbActivityType").value;     
                                    
                                    Para=Para + "|" + "'" + ActivityType + "'";
                                    
                                    var Purpose = document.getElementById("cmbPurpose").value; 
                                    
                                    Para=Para + "|" + "'" + Purpose + "'";
                                    
                                        
                                    //alert(Para);
                                                               
                                        
                                         CallServer("Fifth#" + Para, "");            
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
<tr><td align="center" style="height: 53px"><h3><u>HAMALI PURPOSE ACTIVITY WISE DETAIL REPORT</u></h3></td></tr>
</table>
    <table id="topTable1" style="width:100%; border:1;">
    <tr>    
    <td align="left" style="height: 53px"><h3><u>PLANT </u> : <% Response.Write(Session("plantName"))%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <u>PLANT USER NAME </u> : <% Response.Write(Session("initial"))%>    
    </h3></td>
    <td align="right" style="height: 53px">    
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>
        <br />    
    
   <center>           
            <div class="demo">
                            <table width="80%" id="MyTable" border="1">
                                    <tr><td align="left" style="width: 125px">
                                    <label for="from" style="font-size:larger">FromDate</label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                    <asp:TextBox ID="txtFrom" runat="server" Width="30%"/>
                                    <asp:RequiredFieldValidator ControlToValidate="txtFrom" SetFocusOnError="true" ErrorMessage="Enter From Date" ID="rfvtxtfrom" runat="server"></asp:RequiredFieldValidator>                                    
                                    </td>
                                    <td align="left" style="width: 125px">
                                    <label for="to" style="font-size:larger">ToDate</label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                    <asp:TextBox ID="txtTo" runat="server" Width="30%"/>
                                    <asp:RequiredFieldValidator ControlToValidate="txtTo" SetFocusOnError="true" ErrorMessage="Enter To Date" ID="rfvtxtto" runat="server"></asp:RequiredFieldValidator>                            
                                    </td>
                                    </tr>
                                    <tr>
                                    <td align="left" style="width: 125px">
                                    <label for="to" style="font-size:larger">Plant</label> &nbsp;
                                    </td>
                                    <td align="left">
                                    <asp:DropDownList ID="cmbPlant" runat="server">   <%--Width="50%"--%>
			                        <asp:ListItem Value="0">Select</asp:ListItem>
			                        </asp:DropDownList>
			                        <asp:RequiredFieldValidator ID="ReqcmbPlant" runat="server" SetFocusOnError="true" ErrorMessage="Select Plant " ControlToValidate="cmbPlant" InitialValue="0"></asp:RequiredFieldValidator>                                                            
                                    </td> 
                                    <td align="left" style="width: 125px">
                                    <label for="to" style="font-size:larger">Activity</label> &nbsp;
                                    </td>
                                    <td align="left">
                                    <asp:DropDownList ID="cmbActivity" runat="server"  >   <%--Width="60%"--%>
			                        <asp:ListItem Value="0">Select</asp:ListItem>			                        
			                        </asp:DropDownList>
			                        <asp:RequiredFieldValidator ID="ReqcmbActivity" runat="server" SetFocusOnError="true" ErrorMessage="Select Activity " ControlToValidate="cmbActivity" InitialValue="0"></asp:RequiredFieldValidator>                                                            
                                    </td>                                    
                                   </tr>
                                   <tr>
                                   <td align="left" style="width: 125px">
                                    <label for="to" style="font-size:larger">Crop Type</label> &nbsp;
                                    </td>
                                    <td align="left">
                                    <asp:DropDownList ID="cmbT" runat="server" Width="40%">
			                        <asp:ListItem Value="0">Select</asp:ListItem>
			                        <asp:ListItem Value="%">All</asp:ListItem>
			                        <asp:ListItem value="cotton">Cotton</asp:ListItem>
			                        <asp:ListItem value="field">Field Crop</asp:ListItem>
			                        <asp:ListItem value="vegetable">Vegetable</asp:ListItem>
			                        <asp:ListItem value="store">Store/other</asp:ListItem>
			                        </asp:DropDownList>
			                        <asp:RequiredFieldValidator ID="ReqcmbT" runat="server" SetFocusOnError="true" ErrorMessage="Select Crop Type " ControlToValidate="cmbT" InitialValue="0"></asp:RequiredFieldValidator>                                                            
                                    </td>
                                    <td align="left" style="width: 125px">
                                    <label for="to" style="font-size:larger">Activity Type</label> &nbsp;
                                    </td>
                                    <td align="left">
                                    <asp:DropDownList ID="cmbActivityType" runat="server"  >   <%--Width="60%"--%>
			                        <asp:ListItem Value="0">Select</asp:ListItem>
			                        <asp:ListItem Value="%">All</asp:ListItem>			                        
			                        <asp:ListItem Value="Loading">Loading</asp:ListItem>
			                        <asp:ListItem Value="UnLoading">UnLoading</asp:ListItem>
			                        <asp:ListItem Value="Sorting">Sorting</asp:ListItem>
			                        <asp:ListItem Value="Stiching">Stiching</asp:ListItem>
			                        <asp:ListItem Value="Stacking">Stacking</asp:ListItem>
			                        <asp:ListItem Value="Restacking">Restacking</asp:ListItem>
			                        <asp:ListItem Value="Weighing">Weighing</asp:ListItem>
			                        <asp:ListItem Value="Bundle_Preparation">Bundle_Preparation</asp:ListItem>
			                        <asp:ListItem Value="Opening_Feeding">Opening_Feeding</asp:ListItem>
			                        <asp:ListItem Value="Loading_Unloading">Loading_Unloading</asp:ListItem>
			                        <asp:ListItem Value="UnLoad_Stack">UnLoad_Stack</asp:ListItem>
			                        <asp:ListItem Value="Unp_Recd_Unl_Wt_Stk">Unp_Recd_Unl_Wt_Stk</asp:ListItem>
			                        <asp:ListItem Value="Unp_Recd_Unl_Srt_Wt_Stk">Unp_Recd_Unl_Srt_Wt_Stk</asp:ListItem>
			                        <asp:ListItem Value="Dh_To_Ac_Stk">Dh_To_Ac_Stk</asp:ListItem>
			                        <asp:ListItem Value="Ac_To_Dh_Stk">Ac_To_Dh_Stk</asp:ListItem>
			                        <asp:ListItem Value="Loading_Dh">Loading_Dh</asp:ListItem>
			                        <asp:ListItem Value="UnLoading_Dh">UnLoading_Dh</asp:ListItem>
			                        <asp:ListItem Value="Varai">Varai</asp:ListItem>
			                        <asp:ListItem Value="Stitch_Stack_Plant_OP_Spc_Dry">Stitch_Stack_Plant_OP_Spc_Dry</asp:ListItem>		                        
			                        
			                        </asp:DropDownList>
			                        <asp:RequiredFieldValidator ID="ReqcmbActivityType" runat="server" SetFocusOnError="true" ErrorMessage="Select Activity Type " ControlToValidate="cmbActivityType" InitialValue="0"></asp:RequiredFieldValidator>                                                            
                                    </td> 
                                   </tr>
                                   <tr>
                                    <td align="left" style="width: 125px">
                                    <label for="to" style="font-size:larger">Purpose</label> &nbsp;
                                    </td>
                                    <td align="left">
                                    <asp:DropDownList ID="cmbPurpose" runat="server">   <%--Width="50%"--%>
			                        <asp:ListItem Value="0">Select</asp:ListItem>
			                        </asp:DropDownList>
			                        <asp:RequiredFieldValidator ID="ReqcmbPurpose" runat="server" SetFocusOnError="true" ErrorMessage="Select Purpose " ControlToValidate="cmbPurpose" InitialValue="0"></asp:RequiredFieldValidator>                                                            
                                    </td>                                                                        
                                   </tr>
                                                                      
                                   <tr>
                                    <td colspan="4" align="center">
                                    
                                    <%--<asp:Button ID="btnshow" runat="server" value="show report" Text="show report" Width="95%" Font-Bold="true" OnClick="ExecuteServerCode();" />
                                    <input id="btnShowReport" type="button" style="width:20%" value="Show Report" onclick="ExecuteServerCode();" /> --%>
                                    <input id="btnShowReport" type="image" value="Show Report" onclick="ExecuteServerCode();return false"  src="img/Checklist/ShowReport.png"  />
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
            <asp:GridView id="grdPurpose" runat="server" Font-Size="Smaller" Font-Names="Courier New" ShowFooter="true"
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
    
 
    
    
    <table id="tbl" runat="server" border="1" cellpadding="6" cellspacing="0" style="border-collapse:collapse; height:100px" width="100px"></table>
       
</center>


        
        
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="False" />

        
        
        
<br /><br /><br />

    </form>
</body>
</html>
