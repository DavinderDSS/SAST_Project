<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmHamaliCount.aspx.vb" Inherits="frmHamaliCount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>HAMALI COUNT</title>
          
        <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
        <link type="text/css" href="CloneDatepicker/172jquery-ui.custom.css" rel="stylesheet" /> 
        <script type="text/javascript" src="Js/AllJS.js"></script> <%--/*PMS ALLJS*/--%>
        <script type="text/javascript" src="AllJS.js"></script>
		<script type="text/javascript" src="CloneDatepicker/360jquery.min.js"></script> 
		<script type="text/javascript" src="CloneDatepicker/172jquery-ui.custom.min.js"></script>
        
        <script type="text/javascript" language="javascript"> 
					
			
			$(document).ready(function(){				
								
		//<!-- TODO: cut,copy & paste options are disabled Added this  on 06-01-2020 as for password value issue -->
		$('input:text').bind('copy paste cut',function(e) { 
         e.preventDefault(); //disable cut,copy,paste
         alert('cut,copy & paste options are disabled !!');
         });
		//<!-- TODO: cut,copy & paste options are disabled Added this  on 06-01-2020 as for password value issue -->								
								
								
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



function Validation()
                 {
                 var validate=true;
                 $("#tblHamaliCount").find("tr").not("first").each(function(){
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


                    $("table#tblHamaliCount").find("tr").not(':first').each ( function() 
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
                        para=para +"'" + $(this).attr('value')+ "'," ;  
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
        function NumberOnly() 
        { 
            var AsciiValue = event.keyCode 
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue == 46)) 
            event.returnValue = true; 
            else 
            event.returnValue = false; 
        } 

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
        
        
     
</head>
<body>

<table id="topTable" style="width:100%; border:1;">
<tr><td align="center" style="height: 53px"><h3><u>HAMALI COUNT FORM</u></h3></td></tr>
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
    <tr>    
    <td align="left"> <h3><u> :</u></h3></td>
    <td align="right">    
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>--%>


    <form id="form1" runat="server"> 
    <%--<center>--%>
    <table id="tblHamaliCount" border="1" width="95%">
    
    <tr> 
		<td><img src="CloneDatepicker/add.png" class="cloneTableRows" alt=""  title="Click here to add New Entry"  /></td>  <%--onclick="BlankRow();"--%>
            <th class=".MyTH" align="left">Date</th>
            <th class=".MyTH" align="left">Plant</th>
			<th class=".MyTH" align="left">Name Of Hamal Contractor</th>
			<th class=".MyTH" align="left">Number Of Hamal</th>            			 
		</tr> 
    
    <tr> 
			<td><img src="CloneDatepicker/del.png" alt="" class="delRow" style="visibility: hidden;"/></td> 
			<td><input type="text" name="in_dateF[]" class="my_date" style="width:90%" readonly="readonly" /></td> 			  
			<td><asp:DropDownList ID="cmbP" runat="server"  style="width:90%">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>
			<td><asp:DropDownList id="cmbC" runat="server"   style="width:90%">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>			
			<td><input type="text" name="in_firstName[]" onkeypress="return NumberOnly()"   style="width:30%" maxlength="3"  /></td>					 
			 
		</tr>
     
    </table>
    
    <%--</center>--%>
    
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
<%--<center>--%>
        <img id="imgsubmit" style="cursor:pointer;" class="vtip" title="Submit the data to Database.<br />You can not edit the data afterwards." alt="Submit" src="img/Checklist/submit.png" onclick="Validation()" />&nbsp;
        <%--<img id="imgSWS" style="cursor:pointer;" class="vtip" title="Save without submit" alt="Submit" src="img/sws.png" onclick="checking()" />
        <input class="submit" type="submit" value="Validate &amp; Send the form!"/>--%>
    
 <%--</center>--%>   
	<%--<asp:HiddenField ID="hdnPlantCode" runat="server"/>--%>	
	
	<asp:HiddenField ID="hdnRndNo" runat="server" /><%--used for Request Flooding Revalidation added this on 12-05-2020--%>
 <asp:HiddenField ID="ClnthdnRndNo" runat="server" /> <%--used for Request Flooding Revalidation added this on 12-05-2020--%>
 
    </form>
</body>
</html>
