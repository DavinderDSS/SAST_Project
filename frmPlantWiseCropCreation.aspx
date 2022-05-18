<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPlantWiseCropCreation.aspx.vb" Inherits="frmPlantWiseCropCreation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>HAMALI PLANT-WISE MATNR ENTRY FORM</title>
    
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
				                        //window.location="frmPlantWiseCropCreation.aspx";
				                        break
				                        				                        
                     case "UpdateData":
				                        //LoadComplete();
				                        //alert("i am in SaveData");
				                        alert(Diff[1]);
				                        //window.location="default.aspx";
				                        //window.location="frmPlantWiseCropCreation.aspx";
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
                //var Category=document.getElementById('HiddenFieldCID').value.toUpperCase();
                var Type=$('#cmbType').find('option:selected').val();
                //var PlantCd=document.getElementById('HiddenFieldPC').value;
                var PlantCd=$('#cmbPlant').find('option:selected').val();
                var StaffId=document.getElementById('HiddenFieldEmpCode').value;
                var CropType=$('#cmbCropType').find('option:selected').val();
                var CropGrp=$('#cmbCropGrp').find('option:selected').val();
                var StaffInitial=document.getElementById('HiddenFieldEmpInitial').value;
                //alert(Supervisor);
                var total=1;
                para='SaveData#'
               
               //New line added code start here on 14-12-2012 if have any prob comment
                
               $("#gvCrop").find("tr").not(":first").each(
                function()
                    {
                        if($(this).find(":checkbox").is(":checked"))
                            {
                            
                            if(Type=="NC")
                            {
                            ////var cell = $(this).find("td:eq(0)");      
                            ////alert(cell.html());
                            ////alert(cell);
                            
                            ////para=para +"'" + $(this).children("td:eq(0)").text()+ "','" + $(this).children("td:eq(2)").text() + "','" + $(this).children("td:eq(9)").val() + "','" + Category +  "'" ;
                            
                            para=para + "'" + $(this).find("td:eq(2)").find("select").val() + "','" + $(this).find("td:eq(3)").find("select").val() + "','" + Type + "','" + PlantCd + "','" + StaffId + "','" + CropType + "','" + CropGrp + "','" + StaffInitial + "'" ;
                            }
                            
                            //if(Type=="CC")
                            //{
                            //para=para +"'" + $(this).find("td:eq(1)").find("input[type=text]").attr('value')  + "','" + $(this).find("td:eq(3)").find("input[type=text]").attr('value')  + "','" + $(this).find("td:eq(8)").find("select").val() + "','" + Category + "'" ;
                            //}
                            
                            if(Type=="IC")
                            {
                            para=para + "'" + $(this).find("td:eq(2)").find("select").val() + "','" + $(this).find("td:eq(3)").find("select").val() + "','" + Type + "','" + PlantCd + "','" + StaffId + "','" + CropType + "','" + CropGrp + "','" + StaffInitial + "'";
                            }
                            
                            
//                            if(Type=="AIC")
//                            {
//                            para=para + "'" + $(this).find("td:eq(2)").find("select").val() + "','" + $(this).find("td:eq(3)").find("select").val() + "','" + Type + "','" + PlantCd + "','" + StaffId + "','" + CropType + "','" + CropGrp + "','" + StaffInitial + "'";
//                            }
                            
                            para=para +'|';
                            
                            }
                            
                            
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


//////////////////////////////////////////////////////////////////////    
function btnUpdate_onclick(flag)
 {
 
  
                    
                var insert_data = new Array();
                var para='';
                //var Category=document.getElementById('HiddenFieldCID').value.toUpperCase();
                //var PlantCd=document.getElementById('HiddenFieldPC').value;
                var Type=$('#cmbType').find('option:selected').val();
                var PlantCd=$('#cmbPlant').find('option:selected').val();
                var StaffId=document.getElementById('HiddenFieldEmpCode').value;
                var CropType=$('#cmbCropType').find('option:selected').val();
                var CropGrp=$('#cmbCropGrp').find('option:selected').val();
                var StaffInitial=document.getElementById('HiddenFieldEmpInitial').value;
                //alert(Supervisor);
                var total=1;
                para='UpdateData#'
               
               //New line added code start here on 14-12-2012 if have any prob comment
                
               $("#gvCrop").find("tr").not(":first").each(
                function()
                    {
                        if($(this).find(":checkbox").is(":checked"))
                            {                                                     
                            
                            if(Type=="IC")
                            {
                            para=para + "'" + $(this).find("td:eq(2)").find("select").val() + "','" + $(this).find("td:eq(3)").find("select").val() + "','" + Type + "','" + PlantCd + "','" + StaffId + "','" + CropType + "','" + CropGrp + "','" + StaffInitial + "'";
                            }
                            
                            if(Type=="AIC")
                            {
                            para=para + "'" + $(this).find("td:eq(2)").find("select").val() + "','" + $(this).find("td:eq(3)").find("select").val() + "','" + Type + "','" + PlantCd + "','" + StaffId + "','" + CropType + "','" + CropGrp + "','" + StaffInitial + "'";
                            }
                                                        
                            para=para +'|';
                            
                            }
                            
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
                             //alert(para);
                             // Loading();
                             CallServer(para,""); 
                        }
                        

                       
                        
  }
		
	
</script>
 
 
 <script type="text/javascript">
// Select/Deselect checkboxes based on header checkbox
function SelectheaderCheckboxes(headerchk) {
debugger
var gvcheck = document.getElementById('gvCrop');
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
var gvcheck = document.getElementById('gvCrop');
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

function NewValidateCheckBoxes() 
{
var status=true;

$("#gvCrop").find(":checkbox")


$("#gvCrop").find("tr").not(":first").each( function(){
if($(this).find(":checkbox").is(":checked"))
{

if( $(this).find("td:eq(2)").find("select").val()=="0" )
{
status=false;
}

}

});
if( status==true)
{
validateCheckBoxes();
}
else
{
alert("Select Value (Checked Matnr value) ");
}
}



function NewUpdateValidateCheckBoxes() 
{
var status=true;

$("#gvCrop").find(":checkbox")


$("#gvCrop").find("tr").not(":first").each( function(){
if($(this).find(":checkbox").is(":checked"))
{

if( $(this).find("td:eq(2)").find("select").val()=="0" )
{
status=false;
}

}

});
if( status==true)
{
UpdatevalidateCheckBoxes();
}
else
{
alert("Select Value (Checked Matnr value) ");
}
}


 function validateCheckBoxes() 
                {
                //alert("i am in validateCheckBoxes");
                    var isValid = false;
                    var gridView = document.getElementById('<%= gvCrop.ClientID %>');
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
               
               
               
function UpdatevalidateCheckBoxes() 
                {
                //alert("i am in validateCheckBoxes");
                    var isValid = false;
                    var gridView = document.getElementById('<%= gvCrop.ClientID %>');
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
                    
                    btnUpdate_onclick(1);
                    return true;                                        
                    }
                    }
                    }
                    }
                    alert("Please select atleast one checkbox");
                    return false;
               }
               
               
               function Validation()
                 {
                 var validate=true;
                 $("#gvCrop").find("tr").not("first").each(function(){
                                     $(this).find("select").each(function(){
                                                                            if($(this).is("select"))
                                                                            {
                                                                                if($(this).val()=="0")
                                                                                    {
                                                                                    //alert("combo");
                                                                                    validate=false;
                                                                                    }
                                                                            }
                                                                                 
                                                                            });
                 });                
                 


if(validate==false)
{
alert('please enter all the values');
return false;
}
else
{
validateCheckBoxes();
}
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
        var grd = document.getElementById('<%= gvCrop.ClientID %>');
        grd.rows[ri].style.display = 'none';
        //grd.cells[ri].style.display = 'none';
        grd.Columns[ri].style.display = 'none';          
  }
  
  }
   //onload="check()
   
   function hide_column(){
    grid = $('#gvCrop');
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
    <script language="javascript" type="text/javascript">
    function Validation()
                 {
                 var validate=true;
                 $("#gvCrop").find("tr").not("first").each(function(){
                                     $(this).find("input[type=text],select").each(function(){
                                                                            if($(this).is("select"))
                                                                            {
                                                                                if($(this).val()=="0")
                                                                                    {
                                                                                    //alert("combo");
                                                                                    validate=false;
                                                                                    }
                                                                            }
                                                                            else
                                                                            {
                                                                            var l="";
                                                                            l=$(this).attr('value');
                                                                            //alert(l.length);
                                                                                 if(l.length>0)
                                                                                    {}
                                                                                    else
                                                                                    {
                                                                                    //alert("text");
                                                                                    validate=false;
                                                                                    }
                                                                             }       
                                                                            });
                 });                
                 


if(validate==false)
{
alert('please enter all the values');
return false;
}
else
{
validateCheckBoxes();
}
      }
 </script> 
    
</head>
<body style = "font-family:Arial;font-size:10pt" >
<table id="topTable" style="width:100%; border:1;">
<tr><td align="center" style="height: 53px"><h3><u>HAMALI PLANT-WISE MATNR ENTRY FORM</u></h3></td></tr>
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
 
    
<div class="demo">
<table width="80%" id="MyTable" border="1">
<tr>
<td align="left" style="width:1%"  >
<label  style="font-size:larger">Plant</label> &nbsp;
</td>
<td align="left" Width="5%">
<asp:DropDownList ID="cmbPlant" runat="server">   <%--Width="50%"--%>
<asp:ListItem Value="0">Select</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="ReqcmbPlant" runat="server" SetFocusOnError="true" ErrorMessage="Select Plant " ControlToValidate="cmbPlant" InitialValue="0"></asp:RequiredFieldValidator>                                                            
</td> 
<td align="left" style="width:2%"  >
<label  style="font-size:larger">Crop Type</label> &nbsp;
</td>
<td align="left" Width="5%">
<asp:DropDownList ID="cmbCropType" runat="server">   <%--Width="50%"--%>
<asp:ListItem Value="0">Select</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="ReqcmbCropType" runat="server" SetFocusOnError="true" ErrorMessage="Select Crop Type " ControlToValidate="cmbCropType" InitialValue="0"></asp:RequiredFieldValidator>                                                            
</td>
</tr>
<tr>
<td align="left" style="width:2%"  >
<label  style="font-size:larger">Crop Group</label> &nbsp;
</td>
<td align="left" Width="3%">
<asp:DropDownList ID="cmbCropGrp" runat="server">   <%--Width="50%"--%>
<asp:ListItem Value="0">Select</asp:ListItem>
</asp:DropDownList>
<asp:RequiredFieldValidator ID="ReqcmbCropGrp" runat="server" SetFocusOnError="true" ErrorMessage="Select Crop Group " ControlToValidate="cmbCropGrp" InitialValue="0"></asp:RequiredFieldValidator>                                                            
</td>

<td align="left"  style="width:2%"  >
<label  style="font-size:larger">Action Type</label> &nbsp;
</td> 
<td align="left" Width="5%">
<asp:DropDownList ID="cmbType" runat="server"  AppendDataBoundItems="True" AutoPostBack="True" > 
<asp:ListItem Value="0">Select</asp:ListItem>
<asp:ListItem Value="CC">Show Plant Current Crop List</asp:ListItem>
<asp:ListItem Value="NC">Add New Crop</asp:ListItem>
<asp:ListItem Value="IC">InActive Crop From Plant List</asp:ListItem>
<asp:ListItem Value="AIC">Activate InActive Crop From Plant List</asp:ListItem>
        </asp:DropDownList>
</td>

</tr>
</table>
     </div>   
 <br />
 <table style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 180px">
 <tr>
 <td style="OVERFLOW: auto; WIDTH: 50%; HEIGHT: 180px">
 <div id="Div1" style="OVERFLOW: auto; WIDTH: 95%; HEIGHT: 180px">
  <asp:GridView ID="gvCrop" runat="server" AutoGenerateColumns="false"  OnRowDataBound = "OnRowDataBound" 
  DataKeyNames = "Matnr" EmptyDataText="No Record Found"  >  
        <HeaderStyle BackColor="#DF5015" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID = "chkAll" runat="server" AutoPostBack="false" onclick="javascript:SelectheaderCheckboxes(this)"  /> <%--OnCheckedChanged="OnCheckedChanged"--%>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="false"  /> <%-- OnRowDataBound = "OnRowDataBound" OnCheckedChanged="OnCheckedChanged"--%>
                    <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("Matnr") %>' />
                
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="SN">
                              <ItemTemplate><%# Container.DataItemIndex + 1 %>                              
                              </ItemTemplate>
            </asp:TemplateField>
                        
            
             <asp:TemplateField HeaderText="Matnr">
                <ItemTemplate>
                   <asp:Label ID = "lblMatnr" runat="server" Visible="false" Text='<%# Eval("Matnr") %>'> </asp:Label>
                    <asp:DropDownList ID="ddlMatnr" runat="server" Visible = "true" BackColor="#FFFFD2" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Mtart">
                <ItemTemplate>
                   <asp:Label ID = "lblMtart" runat="server" Visible="false" Text='<%# Eval("Mtart") %>'  > </asp:Label>
                    <asp:DropDownList ID="ddlMtart" runat="server" Visible = "true" BackColor="#FFFFD2" AppendDataBoundItems="True">
                    <%--<asp:ListItem Value="0">Select</asp:ListItem>--%>
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle Width="150px" />
            </asp:TemplateField>
            
            
        </Columns>
    </asp:GridView>    
    </div>
    </td>

<td style="OVERFLOW: auto; WIDTH: 50%; HEIGHT: 180px">
 <div id="Div2" style="OVERFLOW: auto; WIDTH: 95%; HEIGHT: 180px">
  <asp:GridView ID="GrdPlantCurCrop" runat="server" AutoGenerateColumns="true"  
  DataKeyNames = "Matnr" EmptyDataText="No Record Found"  Width="100%" >  
        <HeaderStyle BackColor="#DF5015" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="SN">
                              <ItemTemplate><%# Container.DataItemIndex + 1 %>                              
                              </ItemTemplate>
            </asp:TemplateField>
                        
         </Columns>
    </asp:GridView>    
    </div>
    </td></tr>
    </table>
    <br />
    
    <img id="imgsubmit" style="cursor:pointer;" class="vtip" title="Submit the data to Database." alt="Submit" src="img/Checklist/submit.png" onclick="javascript:NewValidateCheckBoxes()" visible="false" runat="server"  />
    <img id="imgUpdate" style="cursor:pointer;" class="vtip" title="Submit the data to Database." alt="Update" src="img/Checklist/Update.PNG" onclick="javascript:NewUpdateValidateCheckBoxes()" visible="false" runat="server" />
    <%--<img id="imgShow" style="cursor:pointer;" class="vtip" title="Submit the data to Database." alt="Show" src="img/Checklist/CheckList.png" onclick="javascript:validateCheckBoxes()" visible="false" runat="server" />--%>
    
<%--<img id="imgsubmit" style="cursor:pointer;" class="vtip" title="Submit the data to Database.<br />You can not edit the data afterwards." alt="Submit" src="img/Checklist/submit.png" onclick="Validation()" runat="server" />&nbsp;--%>
    <asp:HiddenField ID="HiddenFieldEmpCode" runat="server" />
        <asp:HiddenField ID="HiddenFieldPC" runat="server" />
        <asp:HiddenField ID="HiddenFieldEmpInitial" runat="server" />
        &nbsp;    
    <%--<input type="button" id="btnGet" value="Get Selected Values" /><br /><br />
<b>Select UserNames:</b><label id="lbltxt"/>--%>

<asp:HiddenField ID="hdnRndNo" runat="server" /><%--used for Request Flooding Revalidation added this on 12-05-2020--%>
 <asp:HiddenField ID="ClnthdnRndNo" runat="server" /> <%--used for Request Flooding Revalidation added this on 12-05-2020--%>
    </form>
</body>
</html>
