<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmHamaliApprovalKALLAKAL.aspx.vb" Inherits="frmHamaliApprovalKALLAKAL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>APPROVE HAMALI EXECUTION</title>
    
        <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
        <link type="text/css" href="CloneDatepicker/172jquery-ui.custom.css" rel="stylesheet" /> 
        <script type="text/javascript" src="Js/AllJS.js"></script> <%--/*PMS ALLJS*/--%>
        <script type="text/javascript" src="AllJS.js"></script>
		<script type="text/javascript" src="CloneDatepicker/360jquery.min.js"></script> 
		<script type="text/javascript" src="CloneDatepicker/172jquery-ui.custom.min.js"></script> 
    
            
    
     <%--<link href="css/StyleSheet.css" rel="stylesheet" type="text/css"/>--%>
     <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
     <style type = "text/css">
    input[type=text], select{background-color:; border:1px solid #ccc}
    </style>
    
    
    <%--<link href="StyleSheet.css" rel="stylesheet" type="text/css" />--%>
     
<link type="text/css" href="CloneDatepicker/FrmToDateLimit/css/ui-lightness/1819jquery-ui.custom.css" rel="stylesheet" />
<script type="text/javascript" src="CloneDatepicker/FrmToDateLimit/js/172jquery.min.js"></script>
<script type="text/javascript" src="CloneDatepicker/FrmToDateLimit/js/1819jquery-ui.custom.min.js"></script>
<script type="text/javascript" src="AllJS.js"></script>

 <!-- TODO: cut,copy & paste options are disabled Added this  on 06-01-2020 as for password value issue -->    
        <script type="text/javascript">
        $(document).ready(function() {
         $('input:text').bind('copy paste cut',function(e) { 
         e.preventDefault(); //disable cut,copy,paste
         alert('cut,copy & paste options are disabled !!');
         });
        });
        </script>

     <script type="text/JavaScript" language="javascript">
function validSpecialChar() 
{
//!(/^[A-z&#209;&#241;0-9]*$/i).test(f.value)?f.value = f.value.replace(/[^A-z&#209;&#241;0-9]/ig,''):null;
//!(/^[A-z&#209;&#241;0-9-\s]*$/i).test(f.value)?f.value = f.value.replace(/[^A-z&#209;&#241;0-9-\s]/ig,''):null;

var AsciiValue = event.keyCode 
if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || (AsciiValue == 8 || AsciiValue == 32  || AsciiValue == 37 || AsciiValue == 38 || AsciiValue == 127 || AsciiValue == 46)) 
event.returnValue = true; 
else 
event.returnValue = false;

}

</script>
<!-- TODO: cut,copy & paste options are disabled Added this  on 06-01-2020 as for password value issue -->     
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
    
    
    		<%--<script type="text/javascript" language="javascript"> 
					
			
			$(document).ready(function(){				
								
				//	-- Datepicker
				$(".my_date").datepicker({
					dateFormat: 'yy-mm-dd',
					showButtonPanel: true
				});					
				
				// -- Clone table rows
				$(".cloneTableRows").live('click', function(){
 
					// this tables id
					var thisTableId = $(this).parents("table").attr("id");
				
					// lastRow
					var lastRow = $('#'+thisTableId + " tr:last");
					
					// clone last row
					var newRow = lastRow.clone(true);
                    
					// append row to this table
					$('#'+thisTableId).append(newRow);
					
					// make the delete image visible
					$('#'+thisTableId + " tr:last td:first img").css("visibility", "visible");
					
					// clear the inputs (Optional)
					//$('#'+thisTableId + " tr:last td :input").val('');  //This Line is commented on 22-11-2012 for copeing textbox values 
					
					// new rows datepicker need to be re-initialized
					$(newRow).find("input").each(function(){
						if($(this).hasClass("hasDatepicker")){ // if the current input has the hasDatpicker class
							var this_id = $(this).attr("id"); // current inputs id
							var new_id = this_id +1; // a new id
							$(this).attr("id", new_id); // change to new id
							$(this).removeClass('hasDatepicker'); // remove hasDatepicker class
							 // re-init datepicker
							$(this).datepicker({
								dateFormat: 'yy-mm-dd',
								showButtonPanel: true
							});
						}
					});					
					
					return false;
				});
				
				// Delete a table row
				$("img.delRow").click(function(){
					$(this).parents("tr").remove();
					return false;
				});
			
			});
		</script> --%>
    

<%--<script type="text/javascript">
$(document).ready(function() {
$('#btnGet').click(function() {
var hdntxt = '';
$("input[name$=CheckBox1]:checked").each(function() {
hdntxt += "," + $(this).next("input[name$=hdID]").val()
});
$('#lbltxt').text(hdntxt.substring(1,hdntxt.length))
});
});
</script> --%>
    
    
   
    
<script language="javascript" type="text/javascript">


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
        var el=obj;//document.getElementById(obj);
        var code=el.options[el.selectedIndex].value;
        //alert(el);
        //alert(code);
        //alert(obj.outerHTML);
        var Para=$(obj).attr("id") + "#" + code;
        //alert(Para);
        CallServer(Para,"");    
    }


function ReceiveServerData(rValue)
    {   
        //alert(rValue);  			
        var Diff = rValue.split("|")
        switch(Diff[0])
        	{
        			
				                   
				     case "SaveData":
				                        //LoadComplete();
				                        //alert("i am in SaveData");
				                        alert(Diff[1]);
				                        //window.location="default.aspx";
				                        window.location="MenuPage.aspx";
				                        break				                        

     
				     case "Error":              
				                    //LoadComplete();
				                    alert(Diff[1]);
				               		                    
				                                                                    
			}
           
	}


		 
//////////////////////////////////////////////////////////////////////    
function btnSubmit_onclick(flag)
 {
 
  
                    
                var insert_data = new Array();
                var para='';
                var Supervisor=document.getElementById('HiddenFieldSVID').value.toUpperCase();
                var PlantCd=document.getElementById('HiddenFieldPC').value;
                //alert(Supervisor);
                var total=1;
                para='SaveData#'
               
               //New line added code start here on 14-12-2012 if have any prob comment
                
               $("#gvCustomers").find("tr").not(":first").each(
                function()
                    {
                        if($(this).find(":checkbox").is(":checked"))
                            {
                                $(this).find("input[type=text],select").each(function()
                                       {
                                        var ans=$(this).is("select");
                                        
                                        if(ans=='true')
                                        { 
                                        para=para +"'" + $("#"+ $(this).attr("id")+"option:selected").val()+ "'," ;  
                                        }
                                        else
                                        {
                                        para=para +"'" + $(this).attr('value')+ "'," ;  
                                        } 
                                      }
                                      );
                            }
                            //);//I if end tag is here
                            //para=para +"'"+ Supervisor +"'," +"'"+ PlantCd +"',"+'~';
                            para=para +'|';
                    }        
               );
               //alert(para);
            //New line added code end here on 14-12-2012 if have any prob comment
                  
                     
                    if (para=='')
                    {
                     jAlert("No Data to Submit", 'Alert Box');
                    return false;
                    }

                    if (confirm('Are You Sure ?')==true)
                        {    
                             alert(para);
                             // Loading();
                             CallServer(para,""); 
                        }
                        

                       
                        
  }
		
	
</script>
 
 
 <script type="text/javascript">
// Select/Deselect checkboxes based on header checkbox
function SelectheaderCheckboxes(headerchk) {
debugger
var gvcheck = document.getElementById('gvCustomers');
var i;
//Condition to check header checkbox selected or not if that is true checked all checkboxes
if (headerchk.checked) {
for (i = 0; i < gvcheck.rows.length; i++) {
var inputs = gvcheck.rows[i].getElementsByTagName('input');
inputs[0].checked = true;
}
}
//if condition fails uncheck all checkboxes in gridview
else {
for (i = 0; i < gvcheck.rows.length; i++) {
var inputs = gvcheck.rows[i].getElementsByTagName('input');
inputs[0].checked = false;
}
}
}
//function to check header checkbox based on child checkboxes condition
function Selectchildcheckboxes(header) {
var ck = header;
var count = 0;
var gvcheck = document.getElementById('gvCustomers');
var headerchk = document.getElementById(header);
var rowcount = gvcheck.rows.length;
//By using this for loop we will count how many checkboxes has checked
for (i = 1; i < gvcheck.rows.length; i++) {
var inputs = gvcheck.rows[i].getElementsByTagName('input');
if (inputs[0].checked) {
count++;
}
}
//Condition to check all the checkboxes selected or not
if (count == rowcount-1) {
headerchk.checked = true;
}
else {
headerchk.checked = false;
}
}
</script>

<script type="text/javascript" language="javascript">
                function validateCheckBoxes() 
                {
                //alert("i am in validateCheckBoxes");
                    var isValid = false;
                    var gridView = document.getElementById('<%= gvCustomers.ClientID %>');
                    for (var i = 1; i < gridView.rows.length; i++) 
                    {
                    var inputs = gridView.rows[i].getElementsByTagName('input');
                    if (inputs != null) 
                    {
                    if (inputs[0].type == "checkbox") 
                    {
                    if (inputs[0].checked) 
                    {         
                    isValid = true;
                    btnSubmit_onclick(1);
                    return true;                                        
                    }
                    }
                    }
                    }
                    alert("Please select atleast one checkbox");
                    return false;
               }
          </script> 
  
  <script language="javascript" type="text/javascript">
  function check()
  {
  alert("i am in check");
  var PlantCode =document.getElementById("HiddenFieldPC").value;
  if (PlantCode=='1157')
  {
  alert(PlantCode);
//  document.getElementById("ddldist_range").style.display = 'block';
//  document.getElementById("ddldist_range").disabled = false;
//  //document.getElementById('ddldist_range').visible=true;
  }
  else
  {
  alert(PlantCode);
//  //document.getElementById('ddldist_range').display=none;
//  document.getElementById("ddldist_range").disabled = true;
//  document.getElementById("ddldist_range").style.display = 'none';

        var ri = 2; // I suppose that you know the Index of Row Which you want to hide
        var grd = document.getElementById('<%= gvCustomers.ClientID %>');
        grd.rows[ri].style.display = 'none';
        //grd.cells[ri].style.display = 'none';
        grd.Columns[ri].style.display = 'none';          
  }
  
  }
   //onload="check()
   
   function hide_column(){
    grid = $('#gvCustomers');
    $('tr', grid).each(function() {
        $('td:eq(5), th:eq(5), td:eq(6), th:eq(6), td:eq(7), th:eq(7), td:eq(8), th:eq(8)',this).hide();
    });
}
$(hide_column());
   
  </script>
<script language="javascript" type="text/javascript">
                function NumberOnly()
                 { 
                        var AsciiValue = event.keyCode 
                        if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue == 46)) 
                        event.returnValue = true; 
                        else 
                        event.returnValue = false; 
                 } 

</script>
    
    
</head>
<body style = "font-family:Arial;font-size:10pt" >
<table id="topTable" style="width:100%; border:1;">
<tr><td align="center" style="height: 53px"><h3><u>HAMALI DIARY APPROVAL FORM</u></h3></td></tr>
</table>
    <table id="topTable1" style="width:100%; border:1;">
    <tr>    
    <td align="left" style="height: 53px"><h3><u>PLANT </u> : <% Response.Write(Session("plantName"))%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <u>PLANT INCHARGE </u> : <% Response.Write(Session("initial"))%>    
    </h3></td>
    <td align="right" style="height: 53px">    
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>
    <form id="form1" runat="server">
         
    <%--<center>
    <table id="MyTable" border="1"> 
		<caption>SELECT DATE FOR APPROVE HAMALI EXECUTION</caption> 
		<tr> 
		<td></td>--%>
		<%--<img src="CloneDatepicker/add.png" class="cloneTableRows" alt=""   title="Click here to add New Entry"  />--%>
		  <%--onclick="BlankRow();"--%>
            
            <%--<th class=".MyTH">Plant</th>
			<th class=".MyTH">FromDate</th>
			<th class=".MyTH">ToDate</th>  --%>
			
			
            			 
		<%--</tr> 
		
		<tr> 
			<td></td>
			<td><asp:DropDownList ID="cmbSVDtl" runat="server">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>
			<td><input type="text" name="in_dateF[]" id="txtFDt"  class="my_date"  /></td> 
			<td><input type="text" name="in_dateT[]" id="txtTDt"   class="my_date" /></td>  
								 
			 
		</tr> 
 
	<tr>
	<td align="center" colspan="4">
	<img id="imgsubmit" style="cursor:pointer;" class="vtip" title="Submit the data to Database.<br />You can not edit the data afterwards." alt="Submit" src="img/submit.png" runat="server" />&nbsp;
	</td>
	</tr>
</table> 
</center>--%>
<table width="96%" border="1">

<tr>
                                    <td align="left" style="width:10%">
                                    <h3><label>From Date</label></h3>
                                    </td>
                                    <td align="left" style="width:10%">
                                    <asp:TextBox ID="txtFrom" runat="server" Width="50%"/>
                                                                      
                                    </td>
                                    <td align="left" style="width:10%">
                                    <h3><label>To Date</label></h3> 
                                    </td>
                                    <td align="left" style="width:10%">
                                    <asp:TextBox ID="txtTo" runat="server" Width="50%"/>
                                                              
                                    </td>
                                    
<td style="width:10%"><h3><label>Supervisors List</label></h3></td>
<td  style="width:30%">
<asp:DropDownList ID="cmbSupervisor" runat="server" Width="50%" AppendDataBoundItems="True" AutoPostBack="True">
<asp:ListItem Value="0">Select</asp:ListItem>
        </asp:DropDownList>
</td>
</tr>
</table>
        
 <br />
 <br />
 <div style="width:99%; overflow:auto">
  <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" OnRowDataBound = "OnRowDataBound" DataKeyNames = "supervisor_code" EmptyDataText="No Record Found">
        <HeaderStyle BackColor="#DF5015" Font-Bold="True" ForeColor="White" Wrap="true"   />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID = "chkAll" runat="server" AutoPostBack="false" onclick="javascript:SelectheaderCheckboxes(this)"  /> <%--OnCheckedChanged="OnCheckedChanged"--%>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="false"  /> <%--OnCheckedChanged="OnCheckedChanged"--%>
                    <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("rowid") %>' />
                
                </ItemTemplate>
            </asp:TemplateField>
            
            <%--<asp:TemplateField HeaderText="SN">
                              <ItemTemplate><%# Container.DataItemIndex + 1 %>                              
                              </ItemTemplate>
            </asp:TemplateField>--%>
            
            
            <asp:TemplateField HeaderText="SN" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="lblrowid" runat="server" Text='<%# Eval("rowid") %>' Visible="false"></asp:Label>
                    <asp:TextBox ID="txtrowid" runat="server" Text='<%# Eval("rowid") %>' ReadOnly="true" Visible="true"  ></asp:TextBox>
                </ItemTemplate>
               <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="TranDate">
                <ItemTemplate>
                    <%--<asp:Label ID="lbltranDt" runat="server" Text='<%# Eval("tran_date") %>'></asp:Label>--%>
                    <asp:TextBox ID="txttranDt" runat="server" Text='<%# Eval("tran_date") %>' ReadOnly="true" Visible="true"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="ContractorName">
                <ItemTemplate>
                    <%--<asp:Label ID="lblSupCode" runat="server" Text='<%# Eval("supervisor_code") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtContractor_Name" runat="server" Text='<%# Eval("Contractor_Name") %>' ReadOnly="true" Visible="true"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="SupervisorCode">
                <ItemTemplate>
                    <%--<asp:Label ID="lblSupCode" runat="server" Text='<%# Eval("supervisor_code") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtSupCode" runat="server" Text='<%# Eval("supervisor_code") %>' ReadOnly="true" Visible="true"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Activity">
                <ItemTemplate>
                    <%--<asp:Label ID="lblActivityId" runat="server" Text='<%# Eval("activity_id") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtActivityId" runat="server" Text='<%# Eval("activity_id") %>' ReadOnly="true" Visible="true"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Purpose">
                <ItemTemplate>
                    <%--<asp:Label ID="lblActivityId" runat="server" Text='<%# Eval("activity_id") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtPurposeId" runat="server" Text='<%# Eval("purpose_id") %>' ReadOnly="true" Visible="true"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Matnr">
                <ItemTemplate>
                    <%--<asp:Label ID="lblMatnr" runat="server" Text='<%# Eval("Matnr") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtMatnr" runat="server" Text='<%# Eval("Matnr") %>' ReadOnly="true" Visible="true" Width="250px"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="250px" />
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="Location">
                <ItemTemplate>
                   <asp:Label ID = "lblLocation" runat="server" Visible="false" Text='<%# Eval("Location") %>'> </asp:Label>
                    <asp:DropDownList ID="ddlLocationes" runat="server" Visible = "true" BackColor="#FFFFD2">
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Dist_Range">
                <ItemTemplate>
                    <asp:Label ID = "lbldist_range" runat="server" Visible="false" Text='<%# Eval("dist_range") %>'></asp:Label>
                    <asp:DropDownList ID="ddldist_range" runat="server" Visible = "true"  BackColor="#FFFFD2">                    
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Bag_Weight">
                <ItemStyle Width="150px" />                
                <ItemTemplate>
                    <asp:Label ID="lblBGWait" runat="server" Visible="false" Text='<%# Eval("bag_weight") %>'></asp:Label>
                    <%--<asp:TextBox ID="txtBGWait" runat="server" Text='<%# Eval("bag_weight") %>' Visible="true" ReadOnly="false" BackColor="#FFFFD2" onkeypress="return NumberOnly()"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlBGWait" runat="server" Visible = "true"  BackColor="#FFFFD2" Enabled="false">                    
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="loading">
                <ItemTemplate>
                    <%--<asp:Label ID="lblLoading" runat="server" Text='<%# Eval("loading") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtLoading" runat="server" Text='<%# Eval("loading") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="unloading">
                <ItemTemplate>
                   <%-- <asp:Label ID="lblUnLoading" runat="server" Text='<%# Eval("unloading") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtUnLoading" runat="server" Text='<%# Eval("unloading") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="sorting">
                <ItemTemplate>
                    <%--<asp:Label ID="lblsorting" runat="server" Text='<%# Eval("sorting") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtsorting" runat="server" Text='<%# Eval("sorting") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="stiching">
                <ItemTemplate>
                    <%--<asp:Label ID="lblstiching" runat="server" Text='<%# Eval("stiching") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtstiching" runat="server" Text='<%# Eval("stiching") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="stacking">
                <ItemTemplate>
                    <%--<asp:Label ID="lblstacking" runat="server" Text='<%# Eval("stacking") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtstacking" runat="server" Text='<%# Eval("stacking") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="restacking">
                <ItemTemplate>
                    <%--<asp:Label ID="lblrestacking" runat="server" Text='<%# Eval("restacking") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtrestacking" runat="server" Text='<%# Eval("restacking") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="weighing">
                <ItemTemplate>
                    <%--<asp:Label ID="lblweighing" runat="server" Text='<%# Eval("weighing") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtweighing" runat="server" Text='<%# Eval("weighing") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="bundle_preparation">
                <ItemTemplate>
                    <%--<asp:Label ID="lblbundle_preparation" runat="server" Text='<%# Eval("bundle_preparation") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtbundle_preparation" runat="server" Text='<%# Eval("bundle_preparation") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>          
            
            <asp:TemplateField HeaderText="opening_feeding">
                <ItemTemplate>
                    <%--<asp:Label ID="lblopening_feeding" runat="server" Text='<%# Eval("opening_feeding") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtopening_feeding" runat="server" Text='<%# Eval("opening_feeding") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            
            <%--<asp:TemplateField HeaderText="loading_unloading">
                <ItemTemplate>
                    <%--<asp:Label ID="lblLoadUnLoad" runat="server" Text='<%# Eval("loading_unloading") %>'></asp:Label>
                    <asp:TextBox ID="txtLoadUnLoad" runat="server" Text='<%# Eval("loading_unloading") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>--%>
            
            
            
            <asp:TemplateField HeaderText="unload_stack">
                <ItemTemplate>
                    <%--<asp:Label ID="lblLoadUnLoad" runat="server" Text='<%# Eval("loading_unloading") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtunloadstack" runat="server" Text='<%# Eval("unload_stack") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Unp_Recd_Unl_Wt_Stk">
                <ItemTemplate>
                    <%--<asp:Label ID="lblUNP_RECD_UNL_WT_STK" runat="server" Text='<%# Eval("UNP_RECD_UNL_WT_STK") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtUnp_Recd_Unl_Wt_Stk" runat="server" Text='<%# Eval("Unp_Recd_Unl_Wt_Stk") %>' Visible="true" BackColor="#FFFFD2"  onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="250px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Unp_Recd_Unl_Srt_Wt_Stk">
                <ItemTemplate>
                    <%--<asp:Label ID="lblUNP_RECD_UNL_SRT_WT_STK" runat="server" Text='<%# Eval("UNP_RECD_UNL_SRT_WT_STK") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtUnp_Recd_Unl_Srt_Wt_Stk" runat="server" Text='<%# Eval("Unp_Recd_Unl_Srt_Wt_Stk") %>' Visible="true" BackColor="#FFFFD2"  onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            
            
            <%--<asp:TemplateField HeaderText="Dh_To_Ac_Stk">
                <ItemTemplate>
                    <%--<asp:Label ID="lblheightDiff" runat="server" Text='<%# Eval("heightDiff") %>'></asp:Label>
                    <asp:TextBox ID="txtDh_To_Ac_Stk" runat="server" Text='<%# Eval("Dh_To_Ac_Stk") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>--%>
            
            
            <%--<asp:TemplateField HeaderText="Ac_To_Dh_Stk">
                <ItemTemplate>
                    <%--<asp:Label ID="lblheightDiff" runat="server" Text='<%# Eval("heightDiff") %>'></asp:Label>
                    <asp:TextBox ID="txtAc_To_Dh_Stk" runat="server" Text='<%# Eval("Ac_To_Dh_Stk") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>--%>
            
            
            <asp:TemplateField HeaderText="Loading_Dh">
                <ItemTemplate>
                    <%--<asp:Label ID="lblheightDiff" runat="server" Text='<%# Eval("heightDiff") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtLoading_Dh" runat="server" Text='<%# Eval("Loading_Dh") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            
            <asp:TemplateField HeaderText="UnLoading_Dh">
                <ItemTemplate>
                    <%--<asp:Label ID="lblheightDiff" runat="server" Text='<%# Eval("heightDiff") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtUnLoading_Dh" runat="server" Text='<%# Eval("UnLoading_Dh") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Varai">
                <ItemTemplate>
                    <%--<asp:Label ID="lblheightDiff" runat="server" Text='<%# Eval("heightDiff") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtVarai" runat="server" Text='<%# Eval("Varai") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return NumberOnly()" maxlength="3"  ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Remark">
                <ItemTemplate>
                    <%--<asp:Label ID="lblRemark" runat="server" Text='<%# Eval("remark") %>'></asp:Label>--%>
                    <asp:TextBox ID="txtRemark" runat="server" Text='<%# Eval("remark") %>' Visible="true" BackColor="#FFFFD2" onkeypress="return validSpecialChar()"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            
            
        </Columns>
    </asp:GridView>
    <br />
    </div>
    <br />
    
    <img id="imgsubmit" style="cursor:pointer;" class="vtip" title="Submit the data to Database." alt="Submit" src="img/Checklist/submit.png" onclick="javascript:validateCheckBoxes()" visible="false"  />
    <asp:HiddenField ID="HiddenFieldSVID" runat="server" />
        <asp:HiddenField ID="HiddenFieldPC" runat="server" />
        &nbsp;    
    <%--<input type="button" id="btnGet" value="Get Selected Values" /><br /><br />
<b>Select UserNames:</b><label id="lbltxt"/>--%>
<asp:HiddenField ID="hdnRndNo" runat="server" /><%--used for Request Flooding Revalidation added this on 12-05-2020--%>
 <asp:HiddenField ID="ClnthdnRndNo" runat="server" /> <%--used for Request Flooding Revalidation added this on 12-05-2020--%>
    </form>
</body>
</html>
