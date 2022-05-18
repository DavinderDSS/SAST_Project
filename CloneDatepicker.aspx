<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CloneDatepicker.aspx.vb" Inherits="CloneDatepicker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	         <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>Clone Datepicker</title>
   
        
        
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
		</script> 
		
		 
<script language="javascript" type="text/javascript">


function ReceiveServerData(rValue)
    {   
                   alert(rValue);  			
        var Diff = rValue.split("|")
//        var ValueTextArray = Diff[1].split(",");
		
		// to clear all further combobox
                    						
        switch(Diff[0])
        	{
        			
				                   
				     case "SaveData":
				                        //LoadComplete();
				                        //alert(Diff[1]);
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
 
 

 
 //validate();
 
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
                             alert(para);
                             // Loading();
                             CallServer(para,""); 
                        }
  }	 
		
	
		 </script>
		 
	 
		 
<%--<script language="javascript" type="text/javascript">

function validate()

{

        if(document.getElementById("<%=in_dateF[].ClientID %>").value=="" || document.getElementById("<%=in_dateT[].ClientID %>").value=="")

      {

                 alert("Please Enter Date");

                 document.getElementById("<%=in_dateF[].ClientID %>").focus();

                return false;

      }

      if(document.getElementById("<%=cmbA.ClientID%>").value=="0" || document.getElementById("<%=cmbP.ClientID%>").value=="0" || document.getElementById("<%=cmbD.ClientID%>").value=="" || document.getElementById("<%=cmbW.ClientID%>").value=="" )

      {

         alert("Please select DropDown Iteam Properly");
         document.getElementById("<%=cmbA.ClientID %>").focus();
         return false;

      }



}

// //code to validate date is here
// $(function() {

//  $( "#form1" ).validate();

//  $( "[type=text]" ).datepicker({
//    onClose: function() {
//      $( this ).valid();
//    }
//  });
//});​
// //code is end here
 
// //dropdown validation asp.net only
// if ($("#cmbP").val()>0) {
//                return true;
//                }
//                else {
//                alert('Please select Plant')
//                return false;
//                }
//                
// if ($("#cmbA").val()>0) {
//                return true;
//                }
//                else {
//                alert('Please select Activity')
//                return false;
//                }               
// //end here dropdown




</script>--%>

		 
</head>
<body>



<table id="topTable" style="width:100%; border:1;">
    <tr>    
    <td align="left"> <h3><u>HAMALI RATE MASTER :</u></h3></td>
    <td align="right">    
        <asp:HyperLink ID="HylHome" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HylLogOt" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>
        <br />
<form runat="server" id="formID" class="formular" action=""> 
	<!-- test table 2 --> 
	<table id="MyTable" border="1"> 
		<caption></caption> 
		<tr> 
		<td><img src="CloneDatepicker/add.png" class="cloneTableRows" alt=""   title="Click here to add New Entry"  /></td>  <%--onclick="BlankRow();"--%>

			<th class=".MyTH">FromDate</th>
			<th class=".MyTH">ToDate</th>  
			<th class=".MyTH">Plant</th>
			<th class=".MyTH">Activity</th>
			<th class=".MyTH">Distance</th>
			<th class=".MyTH">Weight</th>
			<th class=".MyTH">Loading</th>
			<th class=".MyTH">Unloading</th>
			<th class=".MyTH">Sorting</th>
			<th class=".MyTH">Stitching</th>
			<th class=".MyTH">Stacking</th>
			<th class=".MyTH">Re-Stacking</th>
			<th class=".MyTH">Weighing</th>
			<th class=".MyTH">Bundle Preparation</th>
			<th class=".MyTH">Opening-feeding</th>			
			<th class=".MyTH">Loading Unloading</th>
			<th class=".MyTH">UNP_RECD_UNL_WT_STK </th>
			<th class=".MyTH">UNP_RECD_UNL_SRT_WT_STK</th>			
            <th class=".MyTH">Height Diff</th>
            			 
		</tr> 
		
		<tr> 
			<td><img src="CloneDatepicker/del.png" alt="" class="delRow" style="visibility: hidden;"  /></td> 
			<td><input type="text" name="in_dateF[]"  class="my_date"  /></td> 
			<td><input type="text" name="in_dateT[]"  class="my_date" /></td>  
			<td><asp:DropDownList ID="cmbP" runat="server">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>
			<td><asp:DropDownList id="cmbA" runat="server">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>
			<td><select style="width:200px" class="validate[required]">
			<option value="">Select</option> 
			<option value="L">206-Above</option> 
			<option value="M">106-205</option>
			<option value="N">0-105</option>			
			</select></td>
			<td><select style="width:200px" class="validate[required]">
			<option value="">Select</option> 
			<option value="15">15Kg</option> 
			<option value="40">40Kg</option>
			<option value="100">100kG</option>
			</select></td>
			<td><input type="text" name="in_firstName[]" value="0.0" class="validate[required] text-input" /></td>
			<td><input type="text" name="in_firstName[]" value="0.0" class="validate[required] text-input"  /></td>
			<td><input type="text" name="in_firstName[]" value="0.0"  /></td>
			<td><input type="text" name="in_firstName[]" value="0.0" /></td>
			<td><input type="text" name="in_firstName[]" value="0.0" /></td>
			<td><input type="text" name="in_firstName[]" value="0.0" /></td>
			<td><input type="text" name="in_firstName[]" value="0.0" /></td>
			<td><input type="text" name="in_firstName[]" value="0.0" /></td>
			<td><input type="text" name="in_firstName[]" value="0.0" /></td>
			<td><input type="text" name="in_firstName[]" value="0.0" /></td>
			<td><input type="text" name="in_firstName[]" value="0.0" /></td>
			<td><input type="text" name="in_firstName[]" value="0.0" /></td>
			<td><input type="text" name="in_firstName[]" value="0.0" /></td>			 
			 
		</tr> 
 
	</table> 
	
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
        <img id="imgsubmit" style="cursor:pointer;" class="vtip" title="Submit the data to Database.<br />You can not edit the data afterwards." alt="Submit" src="img/submit.png" onclick="return btnSubmit_onclick(1)" />&nbsp;
        <%--<img id="imgSWS" style="cursor:pointer;" class="vtip" title="Save without submit" alt="Submit" src="img/sws.png" onclick="checking()" />--%>
        <%--<input class="submit" type="submit" value="Validate &amp; Send the form!"/>--%>
</form>
</body>
</html>
