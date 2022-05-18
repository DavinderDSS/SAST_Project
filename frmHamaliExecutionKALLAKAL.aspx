<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmHamaliExecutionKALLAKAL.aspx.vb" Inherits="frmHamaliExecutionKALLAKAL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
             <%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
<meta http-equiv="Content-Security-Policy" content="default-src *; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval' " />
<%--''This code is used to cover the CSP Policy not implemented point (GIS / VAPT 2021 suggested) added this on 03-12-2021--%>
    
    	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
	<meta name="Referrer-Policy" value="no-referrer | same-origin" content="no-referrer | same-origin" />
	<%--''This code is used to cover the Referer Policy not implemented point (GIS / VAPT 2021 suggested) added this on 15-12-2021--%>
    <title>HAMALI EXECUTION</title>
        
        
        
        <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
        <link type="text/css" href="CloneDatepicker/172jquery-ui.custom.css" rel="stylesheet" /> 
        <script type="text/javascript" src="Js/AllJS.js"></script> <%--/*PMS ALLJS*/--%>
        <script type="text/javascript" src="AllJS.js"></script>
        
        <script src="CloneDatepicker/DisableFutureDatesin Calendar/182jquery.js" type="text/javascript"></script>
        
		<script type="text/javascript" src="CloneDatepicker/360jquery.min.js"></script> 
		<script type="text/javascript" src="CloneDatepicker/172jquery-ui.custom.min.js"></script>
        
        <%-- This below code is validate numeric values in textboxex 20-12-2012- start here --%>
        <%--<script type="text/javascript" src="CloneDatepicker/Numericval/172jquery-ui.custom.min.js"></script>
	    <script type="text/javascript" src="CloneDatepicker/Numericval/jquery.numeric.js"></script>--%>
        <%-- This below code is validate numeric values in textboxex 20-12-2012- end here --%>
        
        <%-- // This code is use to set date picker from current date and future date unavailable,This is for first row of table, added on 5-8-2013 if want furute date active comment below code--%>
           <script type="text/javascript"  language="javascript">
            $(function() {
            var date = new Date();
            var currentMonth = date.getMonth();
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();
            $('#in_dateF').datepicker({
            dateFormat: 'yy-mm-dd',
            maxDate: new Date(currentYear, currentMonth, currentDate)
            });
            });
            </script>     
         <%-- //This code is use to set date picker from current date and futuredate unavailable,This is for first row of table, added on 5-8-2013 end here uptill comment this while future date active --%>      


        
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
					
					// clear the inputs (Optional) //uncomment below line on 20-12-2012 for avoiding duplication of data with same value
					//$('#'+thisTableId + " tr:last td :input").val('');  //This Line is commented on 22-11-2012 for copeing textbox values 
					$('#'+thisTableId + " tr:last td :input").not(":first").val('0'); //This line is added on 18-07-2013 to add 0 in all text field with out first text field if wont like this comment this line and uncomment upper one
					//$('#'+thisTableId + " tr:last td :select").val('Select'); //This Line is add to blank cmb value null 6-12-2012 
					
					// new rows datepicker need to be re-initialized
					$(newRow).find("input,select").each(function(){
					var this_id = $(this).attr("id"); // current inputs id
							var new_id = this_id +1; // a new id
							$(this).attr("id", new_id); // change to new id
						if($(this).hasClass("hasDatepicker")){ // if the current input has the hasDatpicker class
							
							$(this).removeClass('hasDatepicker'); // remove hasDatepicker class
							 // re-init datepicker
                                
                                //This code is use to set date picker from current date and Futuredate unavailable added on 5-8-2013 if want Futuredate active comment below code
							    var date = new Date();
                                var currentMonth = date.getMonth();
                                var currentDate = date.getDate();
                                var currentYear = date.getFullYear();
							 //This code is use to set date picker from current date and futuredate unavailable added on 5-8-2013 end here uptill comment this while Future date active
							            
							$(this).datepicker({														
								dateFormat: 'yy-mm-dd',
								showButtonPanel: true,
								
								//This code is use to set date picker from current date and pastdate unavailable added on 5-8-2013 if want Futuredate active comment below code
								             maxDate: new Date(currentYear, currentMonth, currentDate)
								        //This code is use to set date picker from current date and Futuredate unavailable added on 5-8-2013 end here uptill comment this while Future date active
								
								
								
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

function FillDataText(obj)
{
        var el=obj;//document.getElementById(obj);
        var code=el.options[el.selectedIndex].value;
        //alert(el);
        //alert(code);
        //alert(obj.outerHTML);
        var Para=$(obj).attr("id") + "#" + code;
        //alert(Para);
        //CallServer(Para,"");
  
  //This code is use to get row data added on 4-1-2013
  
  //var tableObject = $('#tblHamaliExe').find("tr").not(':first').map(function(i) {
  $('#tblHamaliExe tr').map(function() {
  // $(this) is used more than once; cache it for performance.
  var $row = $(this);
 
  // For each row that's "mapped", return an object that
  //  describes the first and second <td> in the row.
  return {
    id: $row.find(':nth-child(1)').text(),
    text: $row.find(':nth-child(2)').text()
  };
  
}).get();


//Code is end here 2-1-2013

}


function DistComboActive(ob)
            {
            
                    var Data=$(ob).attr("id");
                    var DataVal=Data.substring(13,30);
                       
                        
                        var V=document.getElementById('HiddenFieldPC').value;   //'1157';
                        //alert(V);
                        //var Val=$("#cmbContractor"+DataVal).val();                
                        //alert(Val);
                                      
                            if (V=='1157')
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
                            //$("#cmbDist"+DataVal).attr('disabled','disabled'); //commented on 8-7-2014 due to missing combo value and add below line
                            $("#cmbDist"+DataVal).attr('disabled',true);
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
            //alert(DataVal);   
                
                //var V='1157';
                
                var Val=$("#cmbA"+DataVal).val();                
                //alert(Val);
                              
                    if (Val=='ha003' || Val=='ha004' || Val=='ha005')
                    {
                    //alert("I am In If");
                    $("#cmbW"+DataVal).attr('disabled',false);
                    $("#cmbW"+DataVal).attr('value','0');
                    //document.getElementById("cmbDist").disabled=false;
                    //document.getElementById("cmbDist").value=' ';       
                    }
                    else
                    {
                    //alert("I am In else");
                    $("#cmbW"+DataVal).attr('disabled','disabled');
                    $("#cmbW"+DataVal).attr('value','00');                    
                   //document.getElementById("cmbDist").disabled=true;
                   //document.getElementById("cmbDist").value='Z';       
                    }

 //below code is added on 30-8-2013 for entering specifice cell value on Bale-G.Bag and Bundle-G.Bag operation comment is error occue
                    if(Val=='ha001')
                    {
                    $("#Loading"+DataVal).attr('disabled',false);
                    $("#Unloading"+DataVal).attr('disabled','disabled');
                    $("#Sorting"+DataVal).attr('disabled','disabled');
                    $("#Stitching"+DataVal).attr('disabled','disabled');
                    $("#Stacking"+DataVal).attr('disabled','disabled');
                    $("#Re-Stacking"+DataVal).attr('disabled',false);
                    $("#Weighing"+DataVal).attr('disabled','disabled');
                    $("#BundleBounding"+DataVal).attr('disabled','disabled');
                    $("#Opening-feeding"+DataVal).attr('disabled','disabled');
                    $("#Unload_Stack"+DataVal).attr('disabled',false);
                    $("#Unp_Recd_Unl_Wt_Stk"+DataVal).attr('disabled','disabled');
                    $("#Unp_Recd_Unl_Srt_Wt_Stk"+DataVal).attr('disabled','disabled');
                    $("#Loading_Dh"+DataVal).attr('disabled','disabled');
                    $("#UnLoading_Dh"+DataVal).attr('disabled','disabled');
                    $("#Varai"+DataVal).attr('disabled','disabled');
                    }
                    
                    if(Val=='ha002')
                    {
                    $("#Loading"+DataVal).attr('disabled',false);
                    $("#Unloading"+DataVal).attr('disabled',false);
                    $("#Sorting"+DataVal).attr('disabled','disabled');
                    $("#Stitching"+DataVal).attr('disabled','disabled');
                    $("#Stacking"+DataVal).attr('disabled','disabled');
                    $("#Re-Stacking"+DataVal).attr('disabled','disabled');
                    $("#Weighing"+DataVal).attr('disabled','disabled');
                    $("#BundleBounding"+DataVal).attr('disabled',false);
                    $("#Opening-feeding"+DataVal).attr('disabled','disabled');
                    $("#Unload_Stack"+DataVal).attr('disabled','disabled');
                    $("#Unp_Recd_Unl_Wt_Stk"+DataVal).attr('disabled','disabled');
                    $("#Unp_Recd_Unl_Srt_Wt_Stk"+DataVal).attr('disabled','disabled');
                    $("#Loading_Dh"+DataVal).attr('disabled','disabled');
                    $("#UnLoading_Dh"+DataVal).attr('disabled','disabled');
                    $("#Varai"+DataVal).attr('disabled','disabled');
                    }
                    
                    if(Val=='ha003')
                    {
                    $("#Loading"+DataVal).attr('disabled',false);
                    $("#Unloading"+DataVal).attr('disabled',false);
                    $("#Sorting"+DataVal).attr('disabled',false);
                    $("#Stitching"+DataVal).attr('disabled',false);
                    $("#Stacking"+DataVal).attr('disabled',false);
                    $("#Re-Stacking"+DataVal).attr('disabled',false);
                    $("#Weighing"+DataVal).attr('disabled',false);
                    $("#BundleBounding"+DataVal).attr('disabled',false);
                    $("#Opening-feeding"+DataVal).attr('disabled',false);
                    $("#Unload_Stack"+DataVal).attr('disabled',false);
                    $("#Unp_Recd_Unl_Wt_Stk"+DataVal).attr('disabled',false);
                    $("#Unp_Recd_Unl_Srt_Wt_Stk"+DataVal).attr('disabled',false);
                    $("#Loading_Dh"+DataVal).attr('disabled',false);
                    $("#UnLoading_Dh"+DataVal).attr('disabled',false);
                    $("#Varai"+DataVal).attr('disabled',false);
                    }
                    
                    if(Val=='ha006')
                    {
                    $("#Loading"+DataVal).attr('disabled',false);
                    $("#Unloading"+DataVal).attr('disabled',false);
                    $("#Sorting"+DataVal).attr('disabled',false);
                    $("#Stitching"+DataVal).attr('disabled',false);
                    $("#Stacking"+DataVal).attr('disabled',false);
                    $("#Re-Stacking"+DataVal).attr('disabled',false);
                    $("#Weighing"+DataVal).attr('disabled',false);
                    $("#BundleBounding"+DataVal).attr('disabled',false);
                    $("#Opening-feeding"+DataVal).attr('disabled',false);
                    $("#Unload_Stack"+DataVal).attr('disabled',false);
                    $("#Unp_Recd_Unl_Wt_Stk"+DataVal).attr('disabled',false);
                    $("#Unp_Recd_Unl_Srt_Wt_Stk"+DataVal).attr('disabled',false);
                    $("#Loading_Dh"+DataVal).attr('disabled',false);
                    $("#UnLoading_Dh"+DataVal).attr('disabled',false);
                    $("#Varai"+DataVal).attr('disabled',false);
                    }                   
                    
//code is ended here added on 30-8-2013

             }


function TypeComboActive(ob)
            {
            
            var Data=$(ob).attr("id");
            var DataVal=Data.substring(9,30);
            $("#cmbT"+DataVal).attr('value','0');
            //$("#cmbKNV"+DataVal).attr('value',' '); // This is used to make drop down list empty not totaly.            
            $("#cmbKNV"+DataVal).empty(); // This is used to clear the dropdown list,totaly blank      
}

function FillCombo(obj)
    {
    
    
        //Loading();        
        var el=obj;//document.getElementById(obj);
        var code=el.options[el.selectedIndex].value;          
        
        //alert(obj.outerHTML);
//        //New code add on 22-2-2013 to get MatArt value
        var para='';
        $(obj).parent().parent().find("input[type=text],select").each(function(index)
            {
            //alert(index);
                if(index =='2')
                {
                var ans=$(this).is("select");
                                        //alert(ans);
                                        
                                        if(ans=='true')
                                        {                                        
                                        para=para +"'" + $($(this).attr("id")+"option:selected").val()+ "'," ;  
                                        }
                                        else
                                        {
                                        para=para + $(this).attr('value');//+"'" + "'," ;  
                                        }                                                           
                                       //para=para +'~'; 
                                       para=para;
                                       
                }
            }
            );
        //alert(para);
        //code is end here 22-2-2013
        
        var Para=$(obj).attr("id") + "#" + code + "#" + para ;
        //alert(Para);
        CallServer(Para,"");    
    }


function ReceiveServerData(rValue)
    {   
        //alert(rValue);  			
        var Diff = rValue.split("|")
        //alert (Diff);
         
        // New Code is added below on 7-12-2012
        var cmbKNVVal= Diff[0]
        //alert(cmbKNVVal);
        //alert(Diff[0]);
        var DiffVal=Diff[0].substring(0,6);
        //alert(DiffVal);
		
		// to clear all further combobox
        
        switch(DiffVal)
        	{
        			
				                   
				     			                        
                     case "cmbKNV":
                     //alert("i am in DiffVal case");
				                    ClearCombo(cmbKNVVal);
				                    
				                    var ValueTextArray = Diff[1].split(",");
				                    //alert(ValueTextArray);				                   
				                    FillComboData(ValueTextArray,cmbKNVVal);
				                    //LoadComplete();
				                    break
                          
                                  
				     case "Error":              
				                    //LoadComplete();
				                    alert(Diff[1]);
				               		                    
				                                                                    
			}
           
	
	  			
        var AllStr = rValue.split("|")        
         var cmbWVal=AllStr[0].substring(4,30);
         //alert(cmbWVal);
        // New Code is added below on 7-12-2012
        
        var AllStrVal=AllStr[0].substring(0,4); 
		
        
        switch(AllStrVal)
        	{
        		case "cmbW":
                                  //This code is use to get assign string textbox value to table field
                                  //var mystr="1~2~3~4~5";
                                  var myarr=AllStr[1].split("~");                                  
                                  $("#cmbW"+cmbWVal).parent().parent().find("input[type=text]").not(":first").each(function(index){
                                  //alert("hi");
                                  //$(this).html("dfd");
                                    $(this).attr("value",myarr[index]);
                                    //$(this).css("color","red");
                                  });
                                  //$(obj).parent().parent().find("textbox").each(function(i){
                                  //$(this).text()=myarr(i);
        
                                  //})
                                  break
                                  
				     case "Error":              
				                    //LoadComplete();
				                    alert(Diff[1]);
				               		                    
				                                                                    
			}
	
	
	
	
        //New code is end here 7-12-2012
        
        
        
                
                    						
        switch(Diff[0])
        	{
        			
				                   
				     case "SaveData":
				                        //LoadComplete();
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

                  //para=para +"'"+ Supervisor +"'," +"'"+ PlantCd +"'," 

                    $("table#tblHamaliExe").find("tr").not(':first').each ( function() 
                    {
                        $(this).find("td").each( function(index) {
                        //$(this).find("input[type=text],select").each(function(){para=para + $(this).attr('value') + '|';});
                        $(this).find("input[type=text],select").each(function(){
                        var ans=$(this).is("select");
                        //alert(ans);
                        if(index !="3")
                                    {                                                         
                                        if(ans=='true')
                                        {  
                                          para=para +"'" + $("#"+ $(this).attr("id")+"option:selected").val()+ "'," ;  //+  '|'
                                        }                                
                                        else
                                        {
                                        para=para +"'" + $(this).attr('value')+ "'," ;  
                                        }
                                    }
                       });
                       });
                       para=para +"'"+ Supervisor +"'," +"'"+ PlantCd +"',"+'~'; 
                                                 
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
		
function btnSubmit_onclick_CheckList(flag)
 {
 
                var insert_data = new Array();
                var para='';
                var Supervisor=document.getElementById('HiddenFieldSVID').value.toUpperCase();
                var PlantCd=document.getElementById('HiddenFieldPC').value;                
                var total=1;
                
                    $("table#tblHamaliExe").find("tr").not(':first').each ( function() 
                    {
                        $(this).find("td").each( function(index) 
                        {
                        $(this).find("input[type=text],select").each(function(){
                        var ans=$(this).is("select");
                       
                                    if(ans==true)
                                        {  
                                          //para=para +"'" + $("#"+ $(this).attr("id")+"option:selected").val()+ "'," ;  //+  '|'
                                          para=para +"'" +  $("#"+ $(this).attr("id")).find('option:selected').text()+ "'," ; 
                                          //alert(para);
                                        }                                
                                        else
                                        {
                                        para=para +"'" + $(this).attr('value')+ "'," ;  
                                        }
                       });
                     });
                     para=para +"\n"+"\n";
                 });
                 
                 alert(para);
  }	 			
</script>
    
    
    <script language="javascript" type="text/javascript" src="Js/hamalDairy.js"></script>
    
    <script language="javascript" type="text/javascript">
    function Validation()
                 {
                 var validate=true;
                 $("#tblHamaliExe").find("tr").not("first").each(function(){
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
   
    <style type='text/css'>
.back{background:"#5F5FAF";width:'115%';}
</style>
</head>
<body>
<table id="topTable" style="width:100%; border:1;">
<tr><td align="center" style="height: 53px"><h3><u>HAMALI DIARY</u></h3></td></tr>
</table>
    <table id="topTable1" style="width:100%; border:1;">
    <tr>    
    <td align="left"><h3><u>PLANT </u> : <% Response.Write(Session("plantName"))%>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <u>SUPERVISOR </u> : <% Response.Write(Session("initial"))%>    
    </h3></td>
    <td align="right">
    
    <%-- Below Code is add on 26-2-2013 for refreshing the page --%>
    <iframe height="0" width="0" src="refresh.aspx"> </iframe> 
    <%-- Below Code is add on 26-2-2013 for refreshing the page End here --%> 
      
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/MenuPage.aspx" ToolTip="Home" ImageUrl="img/PMS/home2.png" >Home</asp:HyperLink>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/LogOut.aspx" ToolTip="Sign Out" ImageUrl="img/PMS/Logout1.png" >Sign Out</asp:HyperLink>
        </td></tr></table>


    <form id="form1" runat="server">
    
    
    <%-- This div is add on 18-03-2011 to loading the combobox error free and fast--%>
<div id="lodingDiv" style="position:absolute; top:50%; width:350px; left:35%"></div>
<%-- 18-03-2011 code is ended here --%>
    <div style="width:99%; overflow:auto">
    <table id="tblHamaliExe" border="1" width="80%">
    <caption></caption>
    <tr> 
		<td><img src="CloneDatepicker/add.png" class="cloneTableRows" alt=""   title="Click here to add New Entry"  /></td>  <%--onclick="BlankRow();"--%>
            <th class=".MyTH">Date</th>
            <th class=".MyTH">Contractor</th>
            <th class=".MyTH">MatArt</th>
            <th class=".MyTH">Type</th>
            <th class=".MyTH">Kind & Variety</th>
            <th class=".MyTH">MainActivity</th>
            <th class=".MyTH">Purpose</th>
            <th class=".MyTH">Location</th>
            <th class=".MyTH">Weight</th>
            <th class=".MyTH" style="width: 92px">Distance</th>			
			<th class=".MyTH">Loading</th>
			<th class=".MyTH">Unloading</th>
			<th class=".MyTH">Sorting</th>
			<th class=".MyTH">Stitching</th>
			<th class=".MyTH">Stacking</th>			
			<th class=".MyTH">Re-Stacking</th>
			<th class=".MyTH">Weighing</th>
			<th class=".MyTH">Bundle Bounding</th>
			<th class=".MyTH">Opening-feeding</th>
			<%--<th class=".MyTH">Loading Unloading</th>--%>
			<th class=".MyTH">Unload_Stack</th>
			<th class=".MyTH">Unp_Recd_Unl_Wt_Stk</th>
			<th class=".MyTH">Unp_Recd_Unl_Srt_Wt_Stk</th>			
            <%--<th class=".MyTH">Dh_To_Ac_Stk</th>
            <th class=".MyTH">Ac_To_Dh_Stk</th>--%>
            <th class=".MyTH">Loading_Dh</th>
            <th class=".MyTH">UnLoading_Dh</th>
            <th class=".MyTH">Varai</th>	
            <th class=".MyTH">Remarks</th>            			 
		</tr> 
    
    <tr> 
			<td><img src="CloneDatepicker/del.png" alt="" class="delRow" style="visibility: hidden;" /></td> 
			<td><input type="text" name="in_date[]" id="in_dateF" class="my_date" readonly="readonly" /></td> 			  
			<td><asp:DropDownList ID="cmbContractor" runat="server">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>
			<td><asp:DropDownList ID="cmbMatArt" runat="server">
			<asp:ListItem Value="0">Select</asp:ListItem>
			<asp:ListItem Value="HALB">HALB</asp:ListItem>
			<asp:ListItem Value="FERT">FERT</asp:ListItem>
			<asp:ListItem Value="ROH">ROH</asp:ListItem>
			<asp:ListItem Value="VERP">VERP</asp:ListItem>
			<%--<asp:ListItem Value="UNBW">UNBW</asp:ListItem>--%>
			</asp:DropDownList></td>						
			<td><asp:DropDownList ID="cmbT" runat="server">
			<asp:ListItem Value="0">Select</asp:ListItem>
			<asp:ListItem value="cotton">Cotton</asp:ListItem>
			<asp:ListItem value="field">Field Crop</asp:ListItem>
			<asp:ListItem value="vegetable">Vegetable</asp:ListItem>
			<asp:ListItem value="store">Store/other</asp:ListItem>
			</asp:DropDownList></td>						
			<td><asp:DropDownList ID="cmbKNV" runat="server">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>
			<td><asp:DropDownList ID="cmbA" runat="server">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>
			<td><asp:DropDownList ID="cmbP" runat="server">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>
			<td><asp:DropDownList id="cmbL" runat="server">
			<asp:ListItem Value="0">Select</asp:ListItem>
			</asp:DropDownList></td>
			<%--<td><select style="width:100px" >
			<option value="">Select</option> 
			<option value="15">15Kg</option> 
			<option value="40">40Kg</option>
			<option value="100">100kG</option>
			</select></td>--%>
			<td style="width: 92px"><asp:DropDownList ID="cmbW" runat="server">
			<asp:ListItem value="0">Select</asp:ListItem> 
			<asp:ListItem value="15">15Kg</asp:ListItem> 
			<asp:ListItem value="40">40Kg</asp:ListItem>
			<asp:ListItem value="100">100kg</asp:ListItem>
			<asp:ListItem Value="00">No Weight</asp:ListItem>
			</asp:DropDownList></td>
			<td style="width: 92px"><asp:DropDownList ID="cmbDist" runat="server">
			<asp:ListItem Value=" ">Select</asp:ListItem>
			<asp:ListItem value="L">Above 205 Feet</asp:ListItem> 
			<asp:ListItem value="M">Above 105 Feet</asp:ListItem>
			<%--<asp:ListItem value="N">0-105</asp:ListItem>--%>
			<asp:ListItem value="Z">No Distance</asp:ListItem>
			</asp:DropDownList>
			<%--<select style="width:100px">
			<asp:DropDownList>
			<option value="">Select</option> 
			<option value="L">206-Above</option> 
			<option value="M">106-205</option>
			<option value="N">0-105</option>
			</asp:DropDownList>			
			</select>--%>
			</td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Loading"  /></td>					 
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Unloading"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Sorting"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Stitching"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Stacking" /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Re-Stacking"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Weighing"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="BundleBounding"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Opening-feeding"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Unload_Stack"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Unp_Recd_Unl_Wt_Stk"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Unp_Recd_Unl_Srt_Wt_Stk"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Loading_Dh"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="UnLoading_Dh" /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" style="width:50px" value="0" id="Varai"  /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return validSpecialChar()" value="0"   /></td>
			<%--<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" /></td>
			<td><input type="text" name="in_firstName[]"  onkeypress="return NumberOnly()" maxlength="3" /></td>
			<td><input type="text" name="in_firstName[]"  /></td>--%>
		</tr>
     
    </table>
    <br />
    
 </div>
    <br />
    <br />
    <br />
        <%--<img id="imgsubmit" style="cursor:pointer;" class="vtip" title="Submit the data to Database.<br />You can not edit the data afterwards." alt="Submit" src="img/submit.png" onclick="Validation()" />&nbsp;
        <img id="imgSWS" style="cursor:pointer;" class="vtip" title="Save without submit" alt=" Save With Out Submit" src="img/sws.png" onclick="" />
        <img id="img1" style="cursor:pointer;" class="vtip" title="Submit And Exit" alt="Submit And Exit" onclick="" />	--%>
        
        <img id="imgsubmit" style="cursor:pointer;" class="vtip" title="Submit the data to Database.<br />You can not edit the data afterwards." alt="Submit" src="img/Checklist/submit.png" onclick="Validation()" />&nbsp;
        
        <img id="btnCheckList" style="cursor:pointer;" class="vtip" title="Check the Enter Data List." alt="Check List" src="img/Checklist/CheckList.png" onclick="btnSubmit_onclick_CheckList(1)" />&nbsp;

        
        
        <asp:HiddenField ID="HiddenFieldSVID" runat="server" />
        <asp:HiddenField ID="HiddenFieldPC" runat="server" />
        
        <asp:HiddenField ID="hdnRndNo" runat="server" /><%--used for Request Flooding Revalidation added this on 12-05-2020--%>
 <asp:HiddenField ID="ClnthdnRndNo" runat="server" /> <%--used for Request Flooding Revalidation added this on 12-05-2020--%>
    </form>
    <%-- This below code is validate numeric values in textboxex 20-12-2012- start here --%>
    <%--<script type="text/javascript">
	        $(".numeric").numeric();
	        $(".integer").numeric(false, function() { alert("Integers only"); this.value = ""; this.focus(); });
	        $(".positive").numeric({ negative: false }, function() { alert("No negative values"); this.value = ""; this.focus(); });
	        $(".positive-integer").numeric({ decimal: false, negative: false }, function() { alert("Positive integers only"); this.value = ""; this.focus(); });
	        $("#remove").click(
		        function(e)
		        {
			        e.preventDefault();
			        $(".numeric,.integer,.positive").removeNumeric();
		        }
	        );
	</script>--%>
	<%-- This below code is validate numeric values in textboxex 20-12-2012- end here --%>
</body>
</html>
