// JScript File
//function GetSelectedValues(Obj)
//  {
//  
//        var tableBody = document.getElementById(Obj).childNodes[0]; 
//        var SelectedValues="";
//        for (var i=1;i<tableBody.childNodes.length; i++)
//            {
//                var currentTd = tableBody.childNodes[i].childNodes[0];
//                var listControl = currentTd.childNodes[0];

//                    if ( listControl.checked == true )
//                        {
//                             var ProblemText = tableBody.childNodes[i].childNodes[tableBody.childNodes[i].childNodes.length-1].innerText;
//                             //var ProblemText = tableBody.childNodes[i].childNodes[1].innerText;
//                             
//                             SelectedValues = SelectedValues + "'" + ProblemText + "'," ;
//                            
//                        }
//    
//            }
//            
//          return SelectedValues;  
//  
//  }

function clearinputs(sType) 
{
  a = document.getElementsByTagName("input");

  for(i = 0; i < a.length; i++) 
        {

            if(a[i].type==sType) 
            {

              a[i].value = "";

            }

        }

}




function ForAJAX(QueryString,SuccessFunction,ErrorFunction)
{
//alert(QueryString);
//alert(SuccessFunction);

$.ajax({
        url: "AJAXHandler.ashx",
        data: QueryString,
        dataType: "html",
        cache:false,
        success: SuccessFunction,
        error: ErrorFunction
      });
}
  
  function ForNewAJAX(QueryString,SuccessFunction,ErrorFunction)
{
//alert(QueryString);
//alert(SuccessFunction);

$.ajax({
        url: "NewAJAXHandler.ashx",
        data: QueryString,
        dataType: "html",
        cache:false,
        success: SuccessFunction,
        error: ErrorFunction
      });
}

function validate(field)
 {

if (field.value.length != 0)
        {
            var valid = "'~`&^";
            var ok = "yes";
            var temp;
            var invalidChar='';
            for (var i=0; i<field.value.length; i++)
            {
            temp = "" + field.value.substring(i, i+1);
                if (!(valid.indexOf(temp) == "-1"))
                {
                    invalidChar = invalidChar + '   ' + temp;
                    ok = "no";
                }
            }
            if (ok == "no")
            {
            var invalidChar1=invalidChar.split(',');

            alert("Invalid entry! due to following Characters (Single Quote): \n\n " + invalidChar );
            field.focus();
            //field.select();
            }
            
        } 
           
//          field.style.backgroundColor='white';
  }

function validateLen(field)
 {

if (field.value.length != 0)
        {
            
             if(field.value.length > 120)
             {
             alert('max 120 characters allowed!!!');
             field.focus();
             return false;
             }
            var valid = "'~`&^";
            var ok = "yes";
            var temp;
            var invalidChar='';
            for (var i=0; i<field.value.length; i++)
            {
            temp = "" + field.value.substring(i, i+1);
                if (!(valid.indexOf(temp) == "-1"))
                {
                    invalidChar = invalidChar + '   ' + temp;
                    ok = "no";
                }
            }
            if (ok == "no")
            {
            var invalidChar1=invalidChar.split(',');

            alert("Invalid entry! due to following Characters (Single Quote): \n\n " + invalidChar );
            field.focus();
            //field.select();
            }
            
            
            
        } 
           
//          field.style.backgroundColor='white';
  }



function makeVisible(Obj)
{
 var x=document.getElementById(Obj)
 x.style.display='';
}

function makeHidden(Obj)
{
var x=document.getElementById(Obj)
 x.style.display='none';
}
  function makeDisable(Obj){
    var x=document.getElementById(Obj)
    x.disabled=true
}
function makeEnable(Obj){
    var x=document.getElementById(Obj)
    x.disabled=false
}

  function show_popup(Data,e)
                    {
                        var centerWidth = (window.screen.width - 200) / 2;
                        var centerHeight = (window.screen.height - 60) / 2;

                        var p = window.createPopup();
                        var pbody = p.document.body;
                        pbody.style.backgroundColor = 'beige';
                        pbody.style.border = 'solid black 1px';
                        pbody.innerHTML = Data;
                        p.show(e.x+10,e.y+10, 200, 60, document.body);
                        //p.show(centerWidth,centerHeight,200,60,document.body);
                    }
                    
                    
                    
    
  function GetSelectedHTML(Obj)
  {
  
        var RelatedTo='';
        var ProbDesc='';
        var TotalString='';
        var FilePath='';
        var AllFiles='';
        var FCount=1;
      
        
        var tableBody = document.getElementById(Obj).childNodes[0]; 
       
        var TableHTML='<table width="90%" border="0" cellspacing="0">';
        var SelectedValues="";
        for (var i=1;i<tableBody.childNodes.length; i++)
            {
                var currentTd = tableBody.childNodes[i].childNodes[0];
                var listControl = currentTd.childNodes[0];
                
                if (i==0)
                {
//                    TableHTML=TableHTML + '<tr>'

//                             for (var j=1;j<tableBody.childNodes[i].childNodes.length; j++)
//                                {
//                                     var ProblemText = tableBody.childNodes[i].childNodes[j].innerText;
//                                     if (ProblemText=='')
//                                     {
//                                        ProblemText='-';
//                                     }
//                                     TableHTML=TableHTML + '<td>' + ProblemText + '</td>';
//                                     TotalString = TotalString + ProblemText + '$';
//                                }
//                   TotalString = TotalString + '^';             
//                             
//                 TableHTML=TableHTML + '</tr>'
                                     
                }
                else
                {
                    if ( listControl.checked == true )
                        {
//                             alert(i);
                             //FilePath=document.frames(i-1).document.forms("form1").elements("FPath").value;
                             FilePath=document.frames(FCount).document.forms("form1").elements("FPath").value; 
                             FCount=FCount+1; // to count number of frames in the document                          
                             for (var j=0;j<tableBody.childNodes[i].childNodes.length-1; j++)
                                {
                                   
                                     var ProblemText='';
                                     ProblemText = tableBody.childNodes[i].childNodes[j].innerText;
                                     
                                     if (ProblemText=='')
                                     {
                                        ProblemText='-';
                                     }
                                     
                                     if (j==1)
                                     {
                                        RelatedTo=RelatedTo + ProblemText + '|';
                                     }
                                     
                                     if (j==tableBody.childNodes[i].childNodes.length-2)
                                     {
                                        ProbDesc=ProbDesc + ProblemText + '|';
                                     }
                                     
                                     
                                                                          
                                     TotalString = TotalString + ProblemText + '$';
                                     
                                }
                                AllFiles = AllFiles + FilePath + '|';
                                TotalString = TotalString + FilePath;
                             TotalString = TotalString + '^';
                                                     
                            
                        }
                  }  
    
            }
            TableHTML=TableHTML + '</table></div>'
           return TotalString + '~' + "'" + RelatedTo + "'," + "'" + ProbDesc + "'," + "'" + AllFiles + "'" + '~' + ReadComboValues('cmbCategory');  
  
  }
  
  
  
  function GetSelectedHTML1(Obj)
  {
  
        var RelatedTo='';
        var ProbDesc='';
        var TotalString='';
        var FilePath='';
        var AllFiles='';
        var FCount=1;
      
        
        var tableBody = document.getElementById(Obj).childNodes[0]; 
       
        var SelectedValues="";
        for (var i=1;i<tableBody.childNodes.length; i++)
            {
                var currentTd = tableBody.childNodes[i].childNodes[0];
                var listControl = currentTd.childNodes[0];
                
                if (i==0)
                {
            
                }
                else
                {
                    if ( listControl.checked == true )
                        {
                             var SendPara='';
                             FilePath=document.frames(FCount).document.forms("form1").elements("FPath").value; 
                             FCount=FCount+1; // to count number of frames in the document                          
                             for (var j=0;j<tableBody.childNodes[i].childNodes.length-1; j++)
                                {
                                  SendPara = SendPara +  tableBody.childNodes[i].childNodes[j].innerText + "~";
                                   
                                }
                               
                            SendPara = ReadComboValues('cmbCategory') + "~" + SendPara + FilePath;  
                            
                             return SendPara;                            
                        }
                  }  
                  
                  
    
            }
           
          //return TotalString + '~' + "'" + RelatedTo + "'," + "'" + ProbDesc + "'," + "'" + AllFiles + "'" + '~' + ReadComboValues('cmbCategory');  
  
  }
  
  
  function ReadComboText(obj)
    {
         return document.getElementById(obj).options[document.getElementById(obj).selectedIndex].text;
    } 
    
    function ReadComboValues(obj)
        {
            return document.getElementById(obj).options[document.getElementById(obj).selectedIndex].value;
        } 
        
  function ReadTextValues(obj)
    {
        if (document.getElementById(obj).value=='')
        {
           return null;           
        }
        else
        {
            return document.getElementById(obj).value ;
        }
    }
          
function ShowWait(Obj,Data)
{
    document.getElementById(Obj).dispaly='';
    document.getElementById(Obj).innerHTML=Data;
}

function HideWait(Obj,Data)
{
 document.getElementById(Obj).innerHTML='';
  document.getElementById(Obj).dispaly='none';
}

function numberOfFileUpload( f ) {
 for(var i = 0; i < f.elements.length; i ++) {
  if( f.elements[i].type=='file' ){
   return i;
  }
 }
}

 function ColorRow(CheckBoxObj)
                {   
                    if (CheckBoxObj.checked == true)
                     {
                        //CheckBoxObj.style.backgroundColor='#88AAFF';
                        CheckBoxObj.style.backgroundColor='blue';
                        CheckBoxObj.style.font.bold='true';             
                     }
                    else
                    {
                        //CheckBoxObj.style.backgroundColor='#FFFFFF';
                        CheckBoxObj.style.backgroundColor='white';
                        
                        CheckBoxObj.style.font.bold='false';
                    }
                }
                
 function trim(stringToTrim) {
	return stringToTrim.replace(/^\s+|\s+$/g,"");
}

function ltrim(stringToTrim) {
	return stringToTrim.replace(/^\s+/,"");
}

function rtrim(stringToTrim) {
	return stringToTrim.replace(/\s+$/,"");
}

function getQuerystring1(key, default_)
{
  if (default_==null) default_=""; 
  key = key.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");
  var regex = new RegExp("[\\?&]"+key+"=([^&#]*)");
  var qs = regex.exec(window.location.href);
  if(qs == null)
    return default_;
  else
    return qs[1];
}

function getQuerystring(key, default_) 
{ 
    if (default_==null) 
    { 
        default_=""; 
    } 
    var search = unescape(location.search); 
    if (search == "") 
    { 
        return default_; 
    } 
    search = search.substr(1); 
    var params = search.split("&"); 
    for (var i = 0; i < params.length; i++) 
    { 
        var pairs = params[i].split("="); 
        if(pairs[0] == key) 
        { 
            return pairs[1]; 
        } 
    } 
    return default_; 
}
               
