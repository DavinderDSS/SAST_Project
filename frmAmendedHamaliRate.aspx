<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAmendedHamaliRate.aspx.vb" Inherits="frmAmendedHamaliRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>AMENDED HAMALI RATE</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
        <link type="text/css" href="CloneDatepicker/172jquery-ui.custom.css" rel="stylesheet" /> 
        <script type="text/javascript" src="Js/AllJS.js"></script> <%--/*PMS ALLJS*/--%>
        <script type="text/javascript" src="AllJS.js"></script>
		<script type="text/javascript" src="CloneDatepicker/360jquery.min.js"></script> 
		<script type="text/javascript" src="CloneDatepicker/172jquery-ui.custom.min.js"></script>
		
		<script type="text/javascript" language="javascript">        
       	
			
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
					$('#'+thisTableId + " tr:last td :input").val('');  //This Line is commented on 22-11-2012 for copeing textbox values 
					//$('#'+thisTableId + " tr:last td :select").val(''); //select:eq(0)
										
					// new rows datepicker need to be re-initialized
					$(newRow).find("input,select").each(function(){
						//if($(this).hasClass("hasDatepicker")){ // if the current input has the hasDatpicker class
							var this_id = $(this).attr("id"); // current inputs id
							var new_id = this_id +1; // a new id
							$(this).attr("id", new_id); // change to new id
							
							if($(this).hasClass("hasDatepicker")){ // if the current input has the hasDatpicker class
							
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
		</script> 
		
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
		
		
function FillDataText(obj)
{
        var el=obj;//document.getElementById(obj);
        //var code=el.options[el.selectedIndex].value;
        
        //var Para=$(obj).attr("id") + "#" + code;
        var para='FillDataText'+ $(obj).attr("id") + '#' ;// + "#" + code;
        //alert(para);        
        $(obj).parent().parent().find("input[type=text],select").each(function(index)
            {
            //alert(index);
            if(index <='6')
            {
            var ans=$(this).is("select");
                                    //alert(ans);
                                    
                                    if(ans=='true')
                                    { 
                                    para=para +"'" + $("#"+ $(this).attr("id")+"option:selected").val()+ "'," ;  
                                    }
                                    else
                                    {
                                    para=para +"'" + $(this).attr('value')+ "'," ;  
                                    }                       
                                   //para=para +'~'; 
                                   para=para;
                                   
            }
            }
            );
            //alert(para);
CallServer(para,"");
}



function Existingdata(obj)
{
            //alert(" i Am in Existingdata");

            var para='Existingdata#';
            $(obj).parent().parent().find("input[type=text],select").each(function(index)
            {
            //alert(index);
            if(index <='6')
            {
            var ans=$(this).is("select");
                                    //alert(ans);
                                    
                                    if(ans=='true')
                                    { 
                                    para=para +"'" + $("#"+ $(this).attr("id")+"option:selected").val()+ "'," ;  
                                    }
                                    else
                                    {
                                    para=para +"'" + $(this).attr('value')+ "'," ;  
                                    }                       
                                   //para=para +'~'; 
                                   para=para;
                                   
            }
            }
            );
            //alert(para);
CallServer(para,"");
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


function DistComboActive(ob)
            {
            
            var Data=$(ob).attr("id");
            var DataVal=Data.substring(13,30);
               
                
                var V='1157';
                
                var Val=$("#cmbP"+DataVal).val();                
                //alert(Val);
                              
                    if (Val==V)
                    {
                    //alert("I am In If");
                    $("#cmbDist"+DataVal).attr('disabled',false);
                    $("#cmbDist"+DataVal).attr('value',' ');
                    //document.getElementById("cmbDist").disabled=false;
                    //document.getElementById("cmbDist").value=' ';       
                    }
                    else
                    {
                    //alert("I am In else");
                    $("#cmbDist"+DataVal).attr('disabled','disabled');
                    $("#cmbDist"+DataVal).attr('value','Z');                    
                   //document.getElementById("cmbDist").disabled=true;
                   //document.getElementById("cmbDist").value='Z';       
                    }
}

function WeightComboActive(ob)
            {
            //alert("I Am in WeightCombo");
            var Data=$(ob).attr("id");
            //alert(Data);
            var DataVal=Data.substring(4,30);
           
                   var V='1157';
                   var ValPlant=$("#cmbP"+DataVal).val();
                   var ValActivity=$("#cmbA"+DataVal).val();
                   if (ValPlant==V)
                    {
                            
                            if (ValActivity=='ha001')    //Bale-G.Bag
                                        {
                                        //alert("I am In else");                                        
                                        $("#cmbDist"+DataVal).attr('disabled','disabled');
                                        $("#cmbDist"+DataVal).attr('value','Z');
                                        $("#cmbW"+DataVal).attr('disabled','disabled');
                                        $("#cmbW"+DataVal).attr('value','00');           
                                        }                     
                            
                            
                            
                            if (ValActivity=='ha002')       //Bundle-G.Bag
                                        {
                                        //alert("I am In else");                                        
                                        $("#cmbDist"+DataVal).attr('disabled','disabled');
                                        $("#cmbDist"+DataVal).attr('value','Z');
                                        $("#cmbW"+DataVal).attr('disabled',false);
                                        $("#cmbW"+DataVal).attr('value','0');           
                                        }
                            
                            if (ValActivity=='ha003')  //common
                                    {
                                    //alert("I am In If");
                                    //$("#cmbDist"+DataVal).attr('disabled','disabled'); commented on 22-3-2013 work fine add below line
                                    //$("#cmbDist"+DataVal).attr('value','Z');commented on 22-3-2013 work fine add below line
                                    $("#cmbDist"+DataVal).attr('disabled',false);
                                    $("#cmbDist"+DataVal).attr('value',' ');
                                    $("#cmbW"+DataVal).attr('disabled',false);
                                    $("#cmbW"+DataVal).attr('value','0');
                                    //$("#cmbDist"+DataVal).attr('disabled',false);
                                    //$("#cmbDist"+DataVal).attr('value',' ');
                                    }
                                    
                            if (ValActivity=='ha004' || ValActivity=='ha005')  //Stacking/Restacking distance above 105/205 feet
                                    {
                                    //alert("I am In If");
                                    $("#cmbDist"+DataVal).attr('disabled',false);
                                    $("#cmbDist"+DataVal).attr('value',' ');
                                    $("#cmbW"+DataVal).attr('disabled',false);
                                    $("#cmbW"+DataVal).attr('value','0');
                                    //$("#cmbDist"+DataVal).attr('disabled',false);
                                    //$("#cmbDist"+DataVal).attr('value',' ');
                                    }       
                                    
                            
                    }
 
                         if (ValPlant!=V)
                            {
                            
                             //alert(ValPlant);
                                 if (ValActivity=='ha003' || ValActivity=='ha004' || ValActivity=='ha005')
                                    {
                              //alert(ValActivity);
                                    //alert("I am In If");
                                    $("#cmbW"+DataVal).attr('disabled',false);
                                    $("#cmbW"+DataVal).attr('value','0');
                                    //document.getElementById("cmbDist").disabled=false;
                                    //document.getElementById("cmbDist").value=' ';       
                                    }
                                    else
                                    {
                               //alert(ValActivity);
                                    //alert("I am In else");
                                    $("#cmbW"+DataVal).attr('disabled','disabled');
                                    $("#cmbW"+DataVal).attr('value','00');                                        
                                   //document.getElementById("cmbDist").disabled=true;
                                   //document.getElementById("cmbDist").value='Z';       
                                    }
                           }
             }



function ReceiveServerData(rValue)
    {   
        //alert(rValue);  			
        var Diff = rValue.split("|")  
        var cmbContractorVal= Diff[0]
    
    switch(Diff[0])
        	{
        			
				                   
				     case "SaveData":
				                        //LoadComplete();
				                        alert(Diff[1]);
				                        //window.location="default.aspx";
				                        window.location="MenuPage.aspx";
				                        break	
				                        
				     case "Existingdata":
				                        //LoadComplete();
				                        alert(Diff[1]);
				                        //window.location="default.aspx";
				                        //window.location="MenuPage.aspx";
				                        break			                        
     
				     case "Error":              
				                    //LoadComplete();
				                    alert(Diff[1]);
				               		                    
				                                                                    
			}
    
    
    
    
    
    
             
        //alert(Diff);
//        var ValueTextArray = Diff[1].split(",");
		
		// to clear all further combobox
       // New Code is added below on 7-12-2012
        var cmbContractorVal= Diff[0]
         //alert(cmbKNVVal);
        //alert(Diff[0]);
        var DiffVal=Diff[0].substring(0,13);
        //alert(DiffVal);
		
		// to clear all further combobox
        
        if(DiffVal=="cmbContractor")
        {
        
        switch(DiffVal)
        	{
        			
				                   
				     			                        
                     case "cmbContractor":
                     
                     //alert("I am in cmbContractor");
				                    ClearCombo(cmbContractorVal);				                    
				                    //alert(Diff[1]);
				                   var ValueTextArray=Diff[1].substring(0,Diff[1].indexOf("~"));
				                   //alert(ValueTextArray);				                   
				                   var ValueTextArray1=ValueTextArray.split(",");
				                   //alert(ValueTextArray);
				                   FillComboData(ValueTextArray1,cmbContractorVal);
                                   break                   
				     case "Error":              
				                    //LoadComplete();
				                    //alert("I am in Error");
				                    alert(Diff[0]);
				               		                    
				                                                                    
			}
       
       
          //New code for Filling values in text box if data is already present in Database 29-12-2012
			var cmbA=rValue.split("~")			
			var cmbAAllStr = cmbA[1].split("|")
			
             //New Code is added below on 7-12-2012
        
            var cmbAAllStrVal=cmbAAllStr[0].substring(0,4);
            var cmbAAllStrValDtl=cmbAAllStr[0];
            
        switch(cmbAAllStrVal)
        	{
        			
				     case "cmbA":
                     //alert("i am in cmbA case");
				                    ClearCombo(cmbAAllStrValDtl);
				                    
				                    var ValueTextArray = cmbAAllStr[1].split(",");
				                    //alert(ValueTextArray);				                   
				                    FillComboDataActivity(ValueTextArray,cmbAAllStrValDtl);
				                    //LoadComplete();
				                    break
                          
                                  
				     case "Error":              
				                    //LoadComplete();
				                    alert(Diff[1]);
				               		                    
				                                                                    
			}
       
       }
            
			
			//New code for Filling values in text box if data is already present in Database 29-12-2012
			
			var DiffFillTx=Diff[0].substring(0,12);
			
			if(DiffFillTx=="FillDataText")
            {
			//alert(" i am in FillDataText");
			var AllStr = rValue.split("|")
			//alert(AllStr);        
            var cmbWVal=AllStr[0].substring(12,30);
            //alert(cmbWVal);
            // New Code is added below on 7-12-2012
        
            var AllStrVal=AllStr[0].substring(0,12); 
		    //alert(AllStrVal);
        
        switch(AllStrVal)
        	{
        	 case "FillDataText":

                                  
                                  //This code is use to get assign string textbox value to table field
                                  
                                  var myarr=AllStr[1].split("~");
                                  //alert(myarr);                                  
                                  
                                  $("#cmbW"+cmbWVal).parent().parent().find("input[type=text]").each(function(index){
                                  
                                  //$("#cmbW"+cmbWVal).parent().parent().find("td:eq(7)").not(":first").each(function(index){
                                 //alert(index);.find("td:eq(5)")
                                  if(index > 1)
                                  {     
                                   $(this).attr("value",myarr[index]);
                                  }                                    
                                  });
                                  break
                                  
				     case "Error":              
				                    //LoadComplete();
				                    alert(Diff[1]);
				               		                    
				                                                                    
			}
	
	}
	
	
        //New code is end here 29-12-2012
        
			
			
			
           
	}


      function Validation()
                 {
                 var validate=true;
                 $("#MyTable").find("tr").not("first").each(function(){
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
btnSubmit_onclick(1);
}
      }

//////////////////////////////////////////////////////////////////////    
function btnSubmit_onclick(flag)
 {
  
                var insert_data = new Array();
                var para='';
                var total=1;
                para='SaveData#'
//                if (flag==1)
//                {
//                    if (total < 0)
//                        {
//                            jAlert("Weightage must be equal to 100","Alert Box");
//                            return false;
//                        }
//                }

                    $("table#MyTable").find("tr").not(':first').each ( function() 
                    {
                        $(this).find("td").each( function() {
                        //$(this).find("input[type=text],select").each(function(){para=para + $(this).attr('value') + '|';});
                        $(this).find("input[type=text],select").each(function(){
                        var ans=$(this).is("select");
                        //alert(ans);
                        
                        if(ans=='true')
                        { 
                        para=para +"'" + $("#"+ $(this).attr("id")+"option:selected").val()+ "'," ;  //+  '|'
                        }
                        else
                        {
                        para=para +"'" + $(this).attr('value')+ "'," ;  //+ '|'
                        }   
                       });
                       });
                       para=para +'~'; 
                                                 
                    });

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
		 
		 
<script language="javascript" type="text/javascript">
function NumberOnly() { 
var AsciiValue = event.keyCode 
if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue == 46)) 
event.returnValue = true; 
else 
event.returnValue = false; 
} 

</script>
		 
</head>
<body>
<table id="topTable" style="width:100%; border:1;">
<tr><td align="center" style="height: 53px"><h3><u>AMENDED HAMALI RATE MASTER FORM</u></h3></td></tr>
</table>
    <table id="topTable1" style="width:100%; border:1;">
    <tr>    
    <td align="left" style="height: 53px"><h3><u>PLANT </u> : <% Response.Write(Session("plantName"))%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <u>HEAD OFFICER </u> : <% Response.Write(Session("initial"))%>    
    </h3></td>
    <td align="right" style="height: 53px">    
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>


<%--<table id="topTable" style="width:100%; border:1;">
    <tr>    
    <td align="left"> <h3><u>HAMALI RATE MASTER :</u></h3></td>
    <td align="right">    
        <asp:HyperLink ID="HylHome" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HylLogOt" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>--%>
        
        <br />
<form runat="server" id="formID" > 
	<!-- test table 2  class="formular"--> 
	<div style="width:99%; overflow:auto">
	
	<table id="MyTable" border="1"> 
		<caption></caption> 
		<tr> 
		<td><img src="CloneDatepicker/add.png" class="cloneTableRows" alt=""   title="Click here to add New Entry"  /></td>  <%--onclick="BlankRow();"--%>

			<th class=".MyTH">FromDate</th>
			<th class=".MyTH">ToDate</th>  
			<th class=".MyTH">Plant</th>
			<th class=".MyTH">Contractor</th>
			<th class=".MyTH">MainActivity</th>
			<th class=".MyTH">Distance</th>
			<th class=".MyTH">Weight</th>
			<th class=".MyTH">RowID</th>
			<th class=".MyTH">Loading</th>
			<th class=".MyTH">Unloading</th>
			<th class=".MyTH">Sorting</th>
			<th class=".MyTH">Stitching</th>
			<th class=".MyTH">Stacking</th>
			<th class=".MyTH">Re-Stacking</th>
			<th class=".MyTH">Weighing</th>
			<th class=".MyTH">Bundle Bounding</th>
			<th class=".MyTH">Opening-feeding</th>			
			<th class=".MyTH">Loading Unloading</th>
			<th class=".MyTH">Unload_Stack</th>
			<th class=".MyTH">Unp_Recd_Unl_Wt_Stk</th>
			<th class=".MyTH">Unp_Recd_Unl_Srt_Wt_Stk</th>			
            <th class=".MyTH">Dh_To_Ac_Stk</th>
            <th class=".MyTH">Ac_To_Dh_Stk</th>
            <th class=".MyTH">Loading_Dh</th>
            <th class=".MyTH">UnLoading_Dh</th>
            <th class=".MyTH">Varai</th>
            <th class=".MyTH">Stitch_Stack_Plant_OP_Spc_Dry</th>            			 
		</tr> 
		
		<tr> 
			<td><img src="CloneDatepicker/del.png" alt="" class="delRow" style="visibility: hidden;"  /></td> 
			<td><input type="text" name="in_dateF[]" id="in_dateF"  class="my_date" readonly="readonly" tabindex="1"  /></td> <%--onblur="DateValidation(this);"--%> 
			<td><input type="text" name="in_dateT[]" id="in_dateT"  class="my_date" readonly="readonly" tabindex="2"  /></td> 
			<td><asp:DropDownList ID="cmbP" runat="server" TabIndex="3">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>
			<td><asp:DropDownList ID="cmbContractor" runat="server" TabIndex="4">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>	
			<td><asp:DropDownList id="cmbA" runat="server" TabIndex="5">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>
			<td>
			<%--<select style="width:92px" class="validate[required]">
			<option value="0">Select</option> 
			<option value="L">206-Above</option> 
			<option value="M">105-Above</option>
			<option value="N">0-105</option>
			<option value="Z">No Dist</option>			
			</select>--%>
			<asp:DropDownList ID="cmbDist" runat="server" TabIndex="6">
			<asp:ListItem Value=" ">Select</asp:ListItem>
			<asp:ListItem value="L">Above 205 feet</asp:ListItem> 
			<asp:ListItem value="M">Above 105 feet</asp:ListItem>
			<%--<asp:ListItem value="N">0-105</asp:ListItem>--%>
			<asp:ListItem value="Z">No Dist</asp:ListItem>
			</asp:DropDownList>			
			</td>
			<%--<td><select style="width:200px" class="validate[required]">
			<option value="0">Select</option> 
			<option value="15">15Kg</option> 
			<option value="40">40Kg</option>
			<option value="100">100kG</option>
			</select></td>--%>
			<td style="width: 92px"><asp:DropDownList ID="cmbW" runat="server" TabIndex="7">
			<asp:ListItem value="0">Select</asp:ListItem> 
			<asp:ListItem value="15">15Kg</asp:ListItem> 
			<asp:ListItem value="40">40Kg</asp:ListItem>
			<asp:ListItem value="100">100kg</asp:ListItem>
			<asp:ListItem Value="00">No Weight</asp:ListItem>
			</asp:DropDownList></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="26" readonly="readonly" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" onblur="Existingdata(this);" tabindex="8" /></td>  
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" onclick="FillDataText(this);" tabindex="9" /></td>  <%--onblur="FillDataText(this);"--%>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="10" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="11" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="12" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="13" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="14" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="15" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="16" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="17" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="18" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="19" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="20" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="21" /></td>			 
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="22" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="23" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="24" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="25" /></td>
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()" tabindex="26" /></td> 
		</tr> 
 
	</table> 
	<br />
	</div>
<div style="margin: 20px 0 0 110px;"> 
<script type="text/javascript"><!--
google_ad_client = "pub-3169650841509511";
/* 728x90, created 12/17/09 */
google_ad_slot = "5859528733";
google_ad_width = 728;
google_ad_height = 90;
//-->
</script> 
<script type="text/javascript"> /*src="http://pagead2.googlesyndication.com/pagead/show_ads.js"*/
</script> 
</div> 
<br />
        <img id="imgsubmit" style="cursor:pointer;" class="vtip" title="Submit the data to Database.<br />You can not edit the data afterwards." alt="Submit" src="img/Checklist/submit.png" onclick="Validation()"  />&nbsp;    
        <%--<img id="imgSWS" style="cursor:pointer;" class="vtip" title="Save without submit" alt="Submit" src="img/sws.png" onclick="checking()" />//btnSubmit_onclick(1)--%>
        <%--<input class="submit" type="submit" value="Validate &amp; Send the form!"/>--%>
        <asp:HiddenField ID="HiddenFieldPC" runat="server" />
    <br />
   <div id="message"></div>
   <asp:HiddenField ID="hdnRndNo" runat="server" /><%--used for Request Flooding Revalidation added this on 12-05-2020--%>
 <asp:HiddenField ID="ClnthdnRndNo" runat="server" /> <%--used for Request Flooding Revalidation added this on 12-05-2020--%>

</form>
</body>
</html>
