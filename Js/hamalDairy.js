
//------------------------------ code for calender-------------------------------
//Javascript name: My Date Time Picker
//Date created: 16-Nov-2003 23:19
//Scripter: TengYong Ng
//Website: http://www.rainforestnet.com
//Copyright (c) 2003 TengYong Ng
//FileName: DateTimePicker.js
//Version: 0.8
//Contact: contact@rainforestnet.com
// Note: Permission given to use this script in ANY kind of applications if
//       header lines are left unchanged.

//Global variables
var winCal;
var dtToday=new Date();
var Cal;
var docCal;
var MonthName=["January", "February", "March", "April", "May", "June","July", 
	"August", "September", "October", "November", "December"];
var WeekDayName=["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"];	
var exDateTime;//Existing Date and Time

//Configurable parameters
var cnTop="200";//top coordinate of calendar window.
var cnLeft="500";//left coordinate of calendar window
var WindowTitle ="DateTime Picker";//Date Time Picker title.
var WeekChar=2;//number of character for week day. if 2 then Mo,Tu,We. if 3 then Mon,Tue,Wed.
var CellWidth=20;//Width of day cell.
var DateSeparator="-";//Date Separator, you can change it to "/" if you want.
var TimeMode=24;//default TimeMode value. 12 or 24

var ShowLongMonth=true;//Show long month name in Calendar header. example: "January".
var ShowMonthYear=true;//Show Month and Year in Calendar header.
var MonthYearColor="#cc0033";//Font Color of Month and Year in Calendar header.
var WeekHeadColor="#0099CC";//Background Color in Week header.
var SundayColor="#6699FF";//Background color of Sunday.
var SaturdayColor="#CCCCFF";//Background color of Saturday.
var WeekDayColor="white";//Background color of weekdays.
var FontColor="blue";//color of font in Calendar day cell.
var TodayColor="#FFFF33";//Background color of today.
var SelDateColor="#FFFF99";//Backgrond color of selected date in textbox.
var YrSelColor="#cc0033";//color of font of Year selector.
var ThemeBg="";//Background image of Calendar window.
//end Configurable parameters
//end Global variable

function NewCal(pCtrl,pFormat,pShowTime,pTimeMode)
{
	Cal=new Calendar(dtToday);
	if ((pShowTime!=null) && (pShowTime))
	{
		Cal.ShowTime=true;
		if ((pTimeMode!=null) &&((pTimeMode=='12')||(pTimeMode=='24')))
		{
			TimeMode=pTimeMode;
		}		
	}	
	if (pCtrl!=null)
		Cal.Ctrl=pCtrl;
	if (pFormat!=null)
		Cal.Format=pFormat.toUpperCase();
	
	exDateTime=document.getElementById(pCtrl).value;
	if (exDateTime!="")//Parse Date String
	{
		var Sp1;//Index of Date Separator 1
		var Sp2;//Index of Date Separator 2 
		var tSp1;//Index of Time Separator 1
		var tSp1;//Index of Time Separator 2
		var strMonth;
		var strDate;
		var strYear;
		var intMonth;
		var YearPattern;
		var strHour;
		var strMinute;
		var strSecond;
		//parse month
		Sp1=exDateTime.indexOf(DateSeparator,0)
		Sp2=exDateTime.indexOf(DateSeparator,(parseInt(Sp1)+1));
		if ((Cal.Format.toUpperCase()=="DDMMYYYY") || (Cal.Format.toUpperCase()=="DDMMMYYYY"))
		{
			strMonth=exDateTime.substring(Sp1+1,Sp2);
			strDate=exDateTime.substring(0,Sp1);
		}
		else if ((Cal.Format.toUpperCase()=="MMDDYYYY") || (Cal.Format.toUpperCase()=="MMMDDYYYY"))
		{
			strMonth=exDateTime.substring(0,Sp1);
			strDate=exDateTime.substring(Sp1+1,Sp2);
		}
		if (isNaN(strMonth))
			intMonth=Cal.GetMonthIndex(strMonth);
		else
			intMonth=parseInt(strMonth,10)-1;	
		if ((parseInt(intMonth,10)>=0) && (parseInt(intMonth,10)<12))
			Cal.Month=intMonth;
		//end parse month
		//parse Date
		if ((parseInt(strDate,10)<=Cal.GetMonDays()) && (parseInt(strDate,10)>=1))
			Cal.Date=strDate;
		//end parse Date
		//parse year
		strYear=exDateTime.substring(Sp2+1,Sp2+5);
		YearPattern=/^\d{4}$/;
		if (YearPattern.test(strYear))
			Cal.Year=parseInt(strYear,10);
		//end parse year
		//parse time
		if (Cal.ShowTime==true)
		{
			tSp1=exDateTime.indexOf(":",0)
			tSp2=exDateTime.indexOf(":",(parseInt(tSp1)+1));
			strHour=exDateTime.substring(tSp1,(tSp1)-2);
			Cal.SetHour(strHour);
			strMinute=exDateTime.substring(tSp1+1,tSp2);
			Cal.SetMinute(strMinute);
			strSecond=exDateTime.substring(tSp2+1,tSp2+3);
			Cal.SetSecond(strSecond);
		}	
	}
	winCal=window.open("","DateTimePicker","toolbar=0,status=0,menubar=0,fullscreen=no,width=195,height=245,resizable=0,top="+cnTop+",left="+cnLeft);
	docCal=winCal.document;
	RenderCal();
}

function RenderCal()
{
	var vCalHeader;
	var vCalData;
	var vCalTime;
	var i;
	var j;
	var SelectStr;
	var vDayCount=0;
	var vFirstDay;

	docCal.open();
	docCal.writeln("<html><head><title>"+WindowTitle+"</title>");
	docCal.writeln("<script>var winMain=window.opener;</script>");
	docCal.writeln("</head><body background='"+ThemeBg+"' link="+FontColor+" vlink="+FontColor+"><form name='Calendar'>");

	vCalHeader="<table border=1 cellpadding=1 cellspacing=1 width='100%' align=\"center\" valign=\"top\">\n";
	//Month Selector
	vCalHeader+="<tr>\n<td colspan='7'><table border=0 width='100%' cellpadding=0 cellspacing=0><tr><td align='left'>\n";
	vCalHeader+="<select name=\"MonthSelector\" onchange=\"javascript:winMain.Cal.SwitchMth(this.selectedIndex);winMain.RenderCal();\">\n";
	for (i=0;i<12;i++)
	{
		if (i==Cal.Month)
			SelectStr="Selected";
		else
			SelectStr="";	
		vCalHeader+="<option "+SelectStr+" value >"+MonthName[i]+"\n";
	}
	vCalHeader+="</select></td>";
	//Year selector
	vCalHeader+="\n<td align='right'><a href=\"javascript:winMain.Cal.DecYear();winMain.RenderCal()\"><b><font color=\""+YrSelColor+"\"><</font></b></a><font face=\"Verdana\" color=\""+YrSelColor+"\" size=2><b> "+Cal.Year+" </b></font><a href=\"javascript:winMain.Cal.IncYear();winMain.RenderCal()\"><b><font color=\""+YrSelColor+"\">></font></b></a></td></tr></table></td>\n";	
	vCalHeader+="</tr>";
	//Calendar header shows Month and Year
	if (ShowMonthYear)
		vCalHeader+="<tr><td colspan='7'><font face='Verdana' size='2' align='center' color='"+MonthYearColor+"'><b>"+Cal.GetMonthName(ShowLongMonth)+" "+Cal.Year+"</b></font></td></tr>\n";
	//Week day header
	vCalHeader+="<tr bgcolor="+WeekHeadColor+">";
	for (i=0;i<7;i++)
	{
		vCalHeader+="<td align='center'><font face='Verdana' size='2'>"+WeekDayName[i].substr(0,WeekChar)+"</font></td>";
	}
	vCalHeader+="</tr>";	
	docCal.write(vCalHeader);
	
	//Calendar detail
	CalDate=new Date(Cal.Year,Cal.Month);
	CalDate.setDate(1);
	vFirstDay=CalDate.getDay();
	vCalData="<tr>";
	for (i=0;i<vFirstDay;i++)
	{
		vCalData=vCalData+GenCell();
		vDayCount=vDayCount+1;
	}
	for (j=1;j<=Cal.GetMonDays();j++)
	{
		var strCell;
		vDayCount=vDayCount+1;
		if ((j==dtToday.getDate())&&(Cal.Month==dtToday.getMonth())&&(Cal.Year==dtToday.getFullYear()))
			strCell=GenCell(j,true,TodayColor);//Highlight today's date
		else
		{
			if (j==Cal.Date)
			{
				strCell=GenCell(j,true,SelDateColor);
			}
			else
			{	 
				if (vDayCount%7==0)
					strCell=GenCell(j,false,SaturdayColor);
				else if ((vDayCount+6)%7==0)
					strCell=GenCell(j,false,SundayColor);
				else
					strCell=GenCell(j,null,WeekDayColor);
			}		
		}						
		vCalData=vCalData+strCell;

		if((vDayCount%7==0)&&(j<Cal.GetMonDays()))
		{
			vCalData=vCalData+"</tr>\n<tr>";
		}
	}
	docCal.writeln(vCalData);	
	//Time picker
	if (Cal.ShowTime)
	{
		var showHour;
		showHour=Cal.getShowHour();		
		vCalTime="<tr>\n<td colspan='7' align='center'>";
		vCalTime+="<input type='text' name='hour' maxlength=2 size=1 style=\"WIDTH: 22px\" value="+showHour+" onchange=\"javascript:winMain.Cal.SetHour(this.value)\">";
		vCalTime+=" : ";
		vCalTime+="<input type='text' name='minute' maxlength=2 size=1 style=\"WIDTH: 22px\" value="+Cal.Minutes+" onchange=\"javascript:winMain.Cal.SetMinute(this.value)\">";
		vCalTime+=" : ";
		vCalTime+="<input type='text' name='second' maxlength=2 size=1 style=\"WIDTH: 22px\" value="+Cal.Seconds+" onchange=\"javascript:winMain.Cal.SetSecond(this.value)\">";
		if (TimeMode==12)
		{
			var SelectAm =(parseInt(Cal.Hours,10)<12)? "Selected":"";
			var SelectPm =(parseInt(Cal.Hours,10)>=12)? "Selected":"";

			vCalTime+="<select name=\"ampm\" onchange=\"javascript:winMain.Cal.SetAmPm(this.options[this.selectedIndex].value);\">";
			vCalTime+="<option "+SelectAm+" value=\"AM\">AM</option>";
			vCalTime+="<option "+SelectPm+" value=\"PM\">PM<option>";
			vCalTime+="</select>";
		}	
		vCalTime+="\n</td>\n</tr>";
		docCal.write(vCalTime);
	}	
	//end time picker
	docCal.writeln("\n</table>");
	docCal.writeln("</form></body></html>");
	docCal.close();
}

function GenCell(pValue,pHighLight,pColor)//Generate table cell with value
{
	var PValue;
	var PCellStr;
	var vColor;
	var vHLstr1;//HighLight string
	var vHlstr2;
	var vTimeStr;
	
	if (pValue==null)
		PValue="";
	else
		PValue=pValue;
	
	if (pColor!=null)
		vColor="bgcolor=\""+pColor+"\"";
	else
		vColor="";	
	if ((pHighLight!=null)&&(pHighLight))
		{vHLstr1="color='red'><b>";vHLstr2="</b>";}
	else
		{vHLstr1=">";vHLstr2="";}	
	
	if (Cal.ShowTime)
	{
		vTimeStr="winMain.document.getElementById('"+Cal.Ctrl+"').value+=' '+"+"winMain.Cal.getShowHour()"+"+':'+"+"winMain.Cal.Minutes"+"+':'+"+"winMain.Cal.Seconds";
		if (TimeMode==12)
			vTimeStr+="+' '+winMain.Cal.AMorPM";
	}	
	else
		vTimeStr="";		
	PCellStr="<td "+vColor+" width="+CellWidth+" align='center'><font face='verdana' size='2'"+vHLstr1+"<a href=\"javascript:winMain.document.getElementById('"+Cal.Ctrl+"').value='"+Cal.FormatDate(PValue)+"';"+vTimeStr+";window.close();\">"+PValue+"</a>"+vHLstr2+"</font></td>";
	return PCellStr;
}

function Calendar(pDate,pCtrl)
{
	//Properties
	this.Date=pDate.getDate();//selected date
	this.Month=pDate.getMonth();//selected month number
	this.Year=pDate.getFullYear();//selected year in 4 digits
	this.Hours=pDate.getHours();	
	
	if (pDate.getMinutes()<10)
		this.Minutes="0"+pDate.getMinutes();
	else
		this.Minutes=pDate.getMinutes();
	
	if (pDate.getSeconds()<10)
		this.Seconds="0"+pDate.getSeconds();
	else		
		this.Seconds=pDate.getSeconds();
		
	this.MyWindow=winCal;
	this.Ctrl=pCtrl;
	this.Format="ddMMyyyy";
	this.Separator=DateSeparator;
	this.ShowTime=false;
	if (pDate.getHours()<12)
		this.AMorPM="AM";
	else
		this.AMorPM="PM";	
}

function GetMonthIndex(shortMonthName)
{
	for (i=0;i<12;i++)
	{
		if (MonthName[i].substring(0,3).toUpperCase()==shortMonthName.toUpperCase())
		{	return i;}
	}
}
Calendar.prototype.GetMonthIndex=GetMonthIndex;

function IncYear()
{	Cal.Year++;}
Calendar.prototype.IncYear=IncYear;

function DecYear()
{	Cal.Year--;}
Calendar.prototype.DecYear=DecYear;
	
function SwitchMth(intMth)
{	Cal.Month=intMth;}
Calendar.prototype.SwitchMth=SwitchMth;

function SetHour(intHour)
{	
	var MaxHour;
	var MinHour;
	if (TimeMode==24)
	{	MaxHour=23;MinHour=0}
	else if (TimeMode==12)
	{	MaxHour=12;MinHour=1}
	else
		alert("TimeMode can only be 12 or 24");		
	var HourExp=new RegExp("^\\d\\d$");
	if (HourExp.test(intHour) && (parseInt(intHour,10)<=MaxHour) && (parseInt(intHour,10)>=MinHour))
	{	
		if ((TimeMode==12) && (Cal.AMorPM=="PM"))
		{
			if (parseInt(intHour,10)==12)
				Cal.Hours=12;
			else	
				Cal.Hours=parseInt(intHour,10)+12;
		}	
		else if ((TimeMode==12) && (Cal.AMorPM=="AM"))
		{
			if (intHour==12)
				intHour-=12;
			Cal.Hours=parseInt(intHour,10);
		}
		else if (TimeMode==24)
			Cal.Hours=parseInt(intHour,10);	
	}
}
Calendar.prototype.SetHour=SetHour;

function SetMinute(intMin)
{
	var MinExp=new RegExp("^\\d\\d$");
	if (MinExp.test(intMin) && (intMin<60))
		Cal.Minutes=intMin;
}
Calendar.prototype.SetMinute=SetMinute;

function SetSecond(intSec)
{	
	var SecExp=new RegExp("^\\d\\d$");
	if (SecExp.test(intSec) && (intSec<60))
		Cal.Seconds=intSec;
}
Calendar.prototype.SetSecond=SetSecond;

function SetAmPm(pvalue)
{
	this.AMorPM=pvalue;
	if (pvalue=="PM")
	{
		this.Hours=(parseInt(this.Hours,10))+12;
		if (this.Hours==24)
			this.Hours=12;
	}	
	else if (pvalue=="AM")
		this.Hours-=12;	
}
Calendar.prototype.SetAmPm=SetAmPm;

function getShowHour()
{
	var finalHour;
    if (TimeMode==12)
    {
    	if (parseInt(this.Hours,10)==0)
		{
			this.AMorPM="AM";
			finalHour=parseInt(this.Hours,10)+12;	
		}
		else if (parseInt(this.Hours,10)==12)
		{
			this.AMorPM="PM";
			finalHour=12;
		}		
		else if (this.Hours>12)
		{
			this.AMorPM="PM";
			if ((this.Hours-12)<10)
				finalHour="0"+((parseInt(this.Hours,10))-12);
			else
				finalHour=parseInt(this.Hours,10)-12;	
		}
		else
		{
			this.AMorPM="AM";
			if (this.Hours<10)
				finalHour="0"+parseInt(this.Hours,10);
			else
				finalHour=this.Hours;	
		}
	}
	else if (TimeMode==24)
	{
		if (this.Hours<10)
			finalHour="0"+parseInt(this.Hours,10);
		else	
			finalHour=this.Hours;
	}	
	return finalHour;	
}				
Calendar.prototype.getShowHour=getShowHour;		

function GetMonthName(IsLong)
{
	var Month=MonthName[this.Month];
	if (IsLong)
		return Month;
	else
		return Month.substr(0,3);
}
Calendar.prototype.GetMonthName=GetMonthName;

function GetMonDays()//Get number of days in a month
{
	var DaysInMonth=[31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
	if (this.IsLeapYear())
	{
		DaysInMonth[1]=29;
	}	
	return DaysInMonth[this.Month];	
}
Calendar.prototype.GetMonDays=GetMonDays;

function IsLeapYear()
{
	if ((this.Year%4)==0)
	{
		if ((this.Year%100==0) && (this.Year%400)!=0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	else
	{
		return false;
	}
}
Calendar.prototype.IsLeapYear=IsLeapYear;

function FormatDate(pDate)
{
	if (this.Format.toUpperCase()=="DDMMYYYY")
		return (this.Year+DateSeparator+(this.Month+1)+DateSeparator+pDate);
	else if (this.Format.toUpperCase()=="DDMMMYYYY")
		return (pDate+DateSeparator+this.GetMonthName(false)+DateSeparator+this.Year);
	else if (this.Format.toUpperCase()=="MMDDYYYY")
		return ((this.Month+1)+DateSeparator+pDate+DateSeparator+this.Year);
	else if (this.Format.toUpperCase()=="MMMDDYYYY")
		return (this.GetMonthName(false)+DateSeparator+pDate+DateSeparator+this.Year);			
}
Calendar.prototype.FormatDate=FormatDate;	
//-------------------------------end of calender--------------------------------------

//------------------------------- for selecting all activity ---------------------------

function CheckAll(chk)
{
	for(var a=1;a<=rowCount();a++)
	{
		document.getElementById("approval"+a).checked=chk;
	}
}
function CheckDate1(e){
     var myKey;
     if (window.event){
          myKey=window.event.keyCode;
     }else if (e){
          myKey=e.which;
     }
     if(myKey!==48 && myKey!==49 && myKey!==50 && myKey!==51 && myKey!==52 && myKey!==53 && myKey!==54 && myKey!==55 && myKey!==56 && myKey!==57 && myKey!==8 && myKey!==0 && myKey!==45 )
          return false;
}


function validateFeild(e,ID1)
	{
			
     var myKey;
     if (window.event){
          myKey=window.event.keyCode;
     }else if (e){
          myKey=e.which;
     }
     if(myKey!==48 && myKey!==49 && myKey!==50 && myKey!==51 && myKey!==52 && myKey!==53 && myKey!==54 && myKey!==55 && myKey!==56 && myKey!==57 && myKey!==8 && myKey!==0 && myKey!==190 && myKey!==9 && myKey!==13 && myKey!==8 && myKey!==96&&myKey!==97&&myKey!==98&&myKey!==99&&myKey!==100&&myKey!==101&&myKey!==102&&myKey!==103&&myKey!==104&&myKey!==105&&myKey!==110&&myKey!==37&&myKey!==38&&myKey!==39&&myKey!==40&&myKey!==19 )
	{    document.getElementById(ID1).value="";
	 alert(myKey	);
		 alert("Enter valid data");
		 
		  
	}
}



function rowCount()
{
	var tbl = document.getElementById('hamalActivity');
	var rowCount = tbl.rows.length;
	return rowCount;
}

// -------------code for validation --------------------------
function valActivity()
{
	
	for(var i=1;i<=rowCount();i++)
	{
		//alert("hello"+"'remark"+i+"'");
		var vactivity="document.getElementById('activityId"+i+"')";		
		var vActivity=eval(vactivity).value;
		var vRemark=document.getElementById("remark"+i).value;
		if(vActivity=='ha012'||vActivity=='ha013'||vActivity=='ha024')
		{
			alert(vRemark);
			if(vRemark=="")
			{
				alert(vRemark);
			alert("For Selected activity remark is must");
			return false;
			}
		} 
	}
	return true;
}
function valHamalData()
{
    var c=rowCount()
	for(var i=1;i<=rowCount();i++)
	{
		//alert(c);
		var vdate="document.getElementById('date"+i+"')";
		var vDate=eval(vdate).value;
		//alert("date"+vDate);
		var vmatnr="document.getElementById('matnr"+i+"')";
		var vMatnr=eval(vmatnr).value;
		//alert("matnr"+vMatnr);
		var vactivity="document.getElementById('activityId"+i+"')";
		var vActivity=eval(vactivity).value;
		//alert("activity"+vActivity);
		var vlocation="document.getElementById('location"+i+"')";
		var vLocation=eval(vlocation).value;
		var vbag="document.getElementById('bag"+i+"')";
		var vBag=eval(vbag).value;
		//alert("loc"+vLocation);
		var vdist="document.getElementById('distanceRang"+i+"')";
		var vDist=eval(vdist).value;
		//alert("dist"+i+vDist);
		//var vlayer="document.getElementById('layerRang"+i+"')";
		//var vLayer=eval(vlayer).value;
		//alert(i+vLayer);
		var vloading="document.getElementById('loading"+i+"')";
		var vLoading=eval(vloading).value;
		//alert("load"+i+vLoading);
		var vunloading="document.getElementById('unloading"+i+"')";
		var vUnloading=eval(vunloading).value;
		//alert("unlod"+i+vUnloading);
		var vweighing="document.getElementById('weighing"+i+"')";
		var vWeighing=eval(vweighing).value;
		//alert("weigh"+i+vWeighing);
		var vsorting="document.getElementById('sorting"+i+"')";
		var vSorting =eval(vsorting).value;
		//alert("sort"+i+vSorting);
		var vstitching="document.getElementById('stitching"+i+"')";
		var vStitching=eval(vstitching).value;
		//alert("stiching"+i+vStitching);
		var vstacking="document.getElementById('stacking"+i+"')";
		var vStacking=eval(vstacking).value;
		//alert("stac"+i+vStacking);
		var vreStacking="document.getElementById('reStacking"+i+"')";
		var vReStacking=eval(vreStacking).value;
		//alert("rest"+i+vReStacking);
		var vopeningFeeding="document.getElementById('openingFeeding"+i+"')";
		var vOpeningFeeding=eval(vopeningFeeding).value;
		//alert("open"+i+vOpeningFeeding);
		var vbundlePreparation="document.getElementById('bundlePreparation"+i+"')";
		var vBundlePreparation=eval(vbundlePreparation).value;
		//alert("bundl"+i+vBundlePreparation);
		//var vremrk="document.frm.getElementById('remark."+i+").value"';
		//var vRemark=eval(vremark).value;
		
		var noStack="document.getElementById('stack"+i+"')";
		var vNoStack=eval(noStack).value;

		var loadingUnloading="document.getElementById('LoadingUnloading"+i+"')";
		var vLoadingUnloading=eval(loadingUnloading).value;

		var stackDhToAnti="document.getElementById('DhToAnti"+i+"')";
		var vStackDhToAnti=eval(stackDhToAnti).value;

		var plantToGodown="document.getElementById('plantToGodown"+i+"')";
		var vPlantToGodown=eval(plantToGodown).value;

	if (vDate==""|| vMatnr==""||vActivity==""||vLocation==""||vDist==""||vBag=="")
	{
		if(vDate==""&& vMatnr==""&&vActivity==""&&vLocation=="")
		{
		if(i==1)
		{
			alert("Field(s) in row-"+i+" are empty");
			return false;
		}
		return true;
		}
		else
		alert("Field(s) in row-"+i+" are empty");
		return false;
	}
	else
	{
		var vactivity="document.getElementById('activityId"+i+"')";		
		var vActivity=eval(vactivity).value;
		var vRemark=document.getElementById("remark"+i).value;
		if(vActivity=='ha012'||vActivity=='ha013'||vActivity=='ha024')
		{
			
			if(vRemark=="")
			{
				alert("For Selected activity remark is must");
				return false;
			}
		} 
		
	  else if(vLoading=="" && vUnloading=="" && vWeighing=="" && vSorting=="" && vStitching==""&& vStacking=="" && vOpeningFeeding=="" && vBundlePreparation=="" && vNoStack=="" && vLoadingUnloading=="" && vStackDhToAnti=="" && vPlantToGodown=="" )
		{
		    alert("Enter at least one Activity in row. - "+i);
				 return false;
		}
	   
	}
      c--;
	}
    
	return true;
}

function getmatnr1(matnr,type)
{
	var crop=document.getElementById(type).value; 
	var prodStr=document.frm.prodStr.value;
	var prodArr=prodStr.split("@");
	document.getElementById(matnr).length=0;
	option=new Option("-Select-","");
    document.getElementById(matnr).options[0]=option;
	var count=1;
	var count1=1;
	for(var i=0;i<prodArr.length;i++)
	{
		var prod=prodArr[i].split("#");
		for(var j=0;j<prod.length;j++)
		{
			var code=prod[0].substring(0,3);
			 if(crop=="field")
			{
				if(code<200&&(code!=113&&code!=114&&code!=115))
				{
					if(count1%2!=0)
					{
						option=new Option(prod[1],prod[0]);
						document.getElementById(matnr).options[count]=option;
						count++;
					}
					count1++;
				}
			}
			else if(crop=="cotton")
			{
				if(code==113||code==114||code==115)
				{
					if(count1%2!=0)
					{
						option=new Option(prod[1],prod[0]);
						document.getElementById(matnr).options[count]=option;
						count++;
					}
					count1++;
					
				}
			}
			else if(crop=="vegetable")
			{
				if(code>199)
				{
					if(count1%2!=0)
					{
						option=new Option(prod[1],prod[0]);
						document.getElementById(matnr).options[count]=option;
						count++;
					}
					count1++;
					
				}
			}
			else if(crop=="store")
			{
				option=new Option("Kind & Variety is not aplicable for store","store");
				document.getElementById(matnr).options[0]=option;
			}
		}
		
	}
	//alert(prodStr);
}
// ................... code for changing matnr combo value..................
function ClearCombo(matnr) 
{ 
  while(document.getElementById(matnr).length>0) 
  { 
  document.getElementById(matnr).remove(0); 
  } 
 // option = new Option('Any','%'); 
 // document.getElementById(obj).options[0]=option;    
}   
function getmatnr(matnr,type) 
{ 
	
	ClearCombo(matnr); 
	 var crop=document.getElementById(type).value; 
	 var hamalMat=document.getElementById("croptype").value;
	 var hamalM=hamalMat.split("!"); 
	 option=new Option("-Select-","");
	 document.getElementById(matnr).options[0]=option;
	 if(crop=="field")
	 {
	
		var hamal=hamalM[0].split("~");
		for(var i=0;i<hamal.length;i++)
		{
			 var h=hamal[i].split("@");
			 option=new Option(h[1],h[0]);
			 document.getElementById(matnr).options[i+1]=option;
		}
	 }

	else if(crop=="vegetable")
	{	 
		 if(hamalM.length>=1)
		 {
			 var hamal=hamalM[1].split("~");
			 for(var i=0;i<hamal.length;i++)
			 {
			 var h=hamal[i].split("@");
			 option=new Option(h[1],h[0]);
			 document.getElementById(matnr).options[i+1]=option;
		     }
		 }
	}
	else if(crop=="cotton")
	{
		if(hamalM.length>=2)
		{
			var hamal=hamalM[2].split("~");
			for(var i=0;i<hamal.length;i++)
			{
			var h=hamal[i].split("@");
			option=new Option(h[1],h[0]);
			document.getElementById(matnr).options[i+1]=option;
		    }
	   }
	}
	else if(crop=="store")
	{
		option=new Option("Kind & Variety is not aplicable for store","store");
		document.getElementById(matnr).options[0]=option;
	}
}
 
//-----------code for adding row to table-----------------------
function addRowToTable()
{
   var tbl = document.getElementById('hamalActivity');
   var lastRow = tbl.rows.length;
   var iteration = rowCount();
   iteration = iteration + 1;
   var row = tbl.insertRow(lastRow);
   row.backgroundcolor="#BECEEF";
   row.height="61";
   row.border=3;
   //row.width="2100";
   row.id="r"+iteration;
   document.getElementById("r"+iteration).style.backgroundColor = "#BECEEF";
   var srNo = row.insertCell(0);
   var textNodeSrNo = document.createTextNode(iteration);
   var SrNoCenter=document.createElement('center');
   var SrNoBlod=document.createElement("b");
   SrNoBlod.appendChild(textNodeSrNo);
   SrNoCenter.appendChild(SrNoBlod);
     srNo.appendChild(SrNoCenter);


   var dateRow= row.insertCell(1);
   var dateInput = document.createElement('input');
  // var dateSpace1 = document.createTextNode("af1'");
  var rowId=document.createElement('input');
  rowId.type='hidden';
  rowId.name='rowId'+iteration;
  rowId.id='rowId'+iteration;
  rowId.value='0';
  dateRow.appendChild(rowId);
  var dateText=document.createTextNode('Date');
  var b=document.createElement('b');
  b.appendChild(dateText);
  dateRow.appendChild(b);
  var dtext=document.createTextNode('                    ');
  dateRow.appendChild(dtext);
  var dateSpaceFont1 = document.createElement("font");   
   dateSpaceFont1.color="#BECEEF";
   dateInput.type = 'text';
   dateInput.name = 'cal' + iteration;
   dateInput.id = 'date'+ iteration;
   dateInput.value = '';
   dateInput.size = 10;
   var dateId='cal'+iteration;
   dateInput.onkeyup=function()
	{
	   return CheckDate1(event,dateId);
	}
   var name="date"+iteration;
   var a1=document.createElement('a');
   a1.href="javascript:NewCal('"+name+"','ddmmyyyy')";
   var img1=document.createElement("img");
   var dateSpace2 = document.createTextNode("'");
   var dateSpaceFont2 = document.createElement("font");
    dateSpaceFont2.color="#BECEEF";
   img1.src="cal.gif";
   img1.width="16";
   img1.border="0";
   img1.height="16";
   img1.alt="Pick a date";
   //dateSpaceFont1.appendChild(dateSpace1);
   a1.appendChild(img1);
   dateRow.appendChild(dateSpaceFont1);   
   dateRow.appendChild(dateInput);
   dateSpaceFont2.appendChild(dateSpace2);
   dateRow.appendChild(dateSpaceFont2);
   dateRow.appendChild(a1);

  // for select type Combo........................
  var sty=document.createTextNode('                  ');
  dateRow.appendChild(sty);
  var distName=document.createTextNode('Slect Type');
  var bdist=document.createElement('b');
  bdist.appendChild(distName);
  dateRow.appendChild(bdist);
  var spdist=document.createTextNode('          ');
  dateRow.appendChild(spdist);
  var distRangInput=document.createElement('select');
  var mat="matnr"+iteration;
  var ty="type"+iteration;
  distRangInput.onchange=function()
	{
	  return getmatnr1(mat,ty);
	}
  distRangInput.id='type'+iteration;
  distRangInput.name='type'+iteration;
  distRangInput.options[0]=new Option('-Select-','');
  distRangInput.options[1]=new Option('Cotton','cotton');
  distRangInput.options[2]=new Option('Field Crop','field');
  distRangInput.options[3]=new Option('Vegetable','vegetable');
  distRangInput.options[4]=new Option('Store/Other','store');
  dateRow.appendChild(distRangInput);
   var stype=document.createElement('br');
  dateRow.appendChild(stype);
   // for activity selection combo............................

   var activitySelect = document.createElement('select');
  var actName=document.createTextNode('Activity/Purpose');
  var bact=document.createElement('b');
  var spact=document.createTextNode(' ');  
  bact.appendChild(actName);
  dateRow.appendChild(bact);
  dateRow.appendChild(spact);
   activitySelect.id = 'activityId' + iteration;
   activitySelect.name='activityId'+iteration;
   var activityStr = document.frm.activity.value;
   var activityArr = activityStr.split("@");
   activitySelect.options[0] = new Option ('-Select-','');	
   for(var i=0;i<activityArr.length;i++)
	  {
			var name = "";
			var code = "";
			var activitySplit = activityArr[i].split("#");
			name = activitySplit[0];
			code = activitySplit[1];
			activitySelect.options[i+1] = new Option(code,name);
	  }
  dateRow.appendChild(activitySelect);
  var bract=document.createElement('br');
  
  dateRow.appendChild(bract);
  

 // for matnr selection combo.......................
   var spM=document.createTextNode('          ');
   dateRow.appendChild(spM);
   var matnrSelect = document.createElement('select');
    matnrSelect.id = 'matnr' + iteration;
   matnrSelect.name='matnr'+iteration;
   var matnrStr = document.frm.matnr.value;
   var matnrArr = matnrStr.split("@");
   matnrSelect.options[0] = new Option ('-Select-','');	
	 for(var i=0;i<matnrArr.length;i++)
	  {
		 var name = "";
		 var code = "";
		 var matnrSplit = matnrArr[i].split("#");
		 var name = matnrSplit[0];
		 var code = matnrSplit[1];
		  matnrSelect.options[i+1] = new Option(code,name);
	  }
	  var matnr=document.createTextNode('Kind & Variety');
	  var bmatnr=document.createElement('b');
	  bmatnr.appendChild(matnr);
	  dateRow.appendChild(bmatnr);
	  var spMat=document.createTextNode('   ');
	  dateRow.appendChild(spMat);
	  dateRow.appendChild(matnrSelect);
	  var brmatnr=document.createElement('br');
	  dateRow.appendChild(brmatnr);

// for location combo..................................

var locSelect = document.createElement('select');
var locName=document.createTextNode('Location');
var bloc=document.createElement('b');
bloc.appendChild(locName);
dateRow.appendChild(bloc);
var sploc=document.createTextNode('              ');
dateRow.appendChild(sploc);
locSelect.id = 'location' + iteration;
locSelect.name='location'+iteration;
var locStr = document.frm.loc1.value;
var locArr = locStr.split("@");
locSelect.options[0] = new Option ('-Select-','');	
   for(var i=0;i<locArr.length;i++)
	  {
			var name = "";
			var code = "";
			var locSplit = locArr[i].split("#");
			code = locSplit[0];
			name = locSplit[1];
			locSelect.options[i+1] = new Option(code+":"+name,code);
	  }
	
    dateRow.appendChild(locSelect);
 var bagWtSelect=document.createElement('select');
 var bagWtName=document.createTextNode('Bag Weight');
 var b=document.createElement('b');
 b.appendChild(bagWtName);
 dateRow.appendChild(b);
 var spBagWt=document.createTextNode('       ');
 dateRow.appendChild(spBagWt);
 bagWtSelect.id='bag'+iteration;
 bagWtSelect.name='bag'+iteration;
 bagWtSelect.options[0]=new Option('-Select-','');
 bagWtSelect.options[1]=new Option('40Kg','40');
 bagWtSelect.options[2]=new Option('100Kg','100');
 dateRow.appendChild(bagWtSelect);
 var brl=document.createElement('br');
  dateRow.appendChild(brl);



  // for Distance Range Combo........................

  var distName=document.createTextNode('Distance Range');
  var bdist=document.createElement('b');
  bdist.appendChild(distName);
  dateRow.appendChild(bdist);
  var spdist=document.createTextNode('  ');
  dateRow.appendChild(spdist);
  var distRangInput=document.createElement('select');
  distRangInput.id='distanceRang'+iteration;
  distRangInput.name='distanceRang'+iteration;
  distRangInput.options[0]=new Option('Between 0-105','N');
  distRangInput.options[1]=new Option('More than 206','L');
  distRangInput.options[2]=new Option('Between 106-205','M');
  dateRow.appendChild(distRangInput);

// for layer combo..........................

/*var spName=document.createTextNode('           ');
dateRow.appendChild(spName);
var layName=document.createTextNode('Layer');
var blay=document.createElement('b');
blay.appendChild(layName);
var splay=document.createTextNode('                 ');
dateRow.appendChild(blay);
dateRow.appendChild(splay);
var layerRangInput=document.createElement('select');
 layerRangInput.id='layerRang'+iteration;
 layerRangInput.name='layerRang'+iteration;
 layerRangInput.options[0]=new Option('Less than or equal 10','S');
 layerRangInput.options[1]=new Option('More than 10','G');
 dateRow.appendChild(layerRangInput);*/
 


   var  ActivityRow = row.insertCell(2);
  ActivityRow.style.backgroundColor="#B3D9FF";
  // for loding text box..................

 var loading = document.createTextNode('Loading');
 var blod=document.createElement('b');
 blod.appendChild(loading);
 ActivityRow.appendChild(blod);
 var loadfont=document.createElement('font');
 loadfont.color='#BECEEF';
 var spLoad=document.createTextNode('    ,      ');
 loadfont.appendChild(spLoad);
 ActivityRow.appendChild(loadfont);
   var loadingInput = document.createElement('input');
   loadingInput.type = 'text';
   loadingInput.name = 'loading' + iteration;
   loadingInput.id = 'loading'+ iteration;
   loadingInput.value = "";
   loadingInput.size = 5;
   var loadingId='loading'+iteration;
   loadingInput.onkeyup=function()
	{
	   return validateFeild(event,loadingId);
	}
   ActivityRow.appendChild(loadingInput);
   // for opening and feeding text box.....................

   var openingFeeding = document.createTextNode('Opening-Feeding');
   var spOp=document.createTextNode('   ');
	ActivityRow.appendChild(spOp);
   var bopen=document.createElement('b');
   bopen.appendChild(openingFeeding);
   ActivityRow.appendChild(bopen);
   var spopen=document.createTextNode('    ');
	ActivityRow.appendChild(spopen);
   var openingFeedingInput = document.createElement('input');
   openingFeedingInput.type = 'text';
   openingFeedingInput.name = 'openingFeeding' + iteration;
   openingFeedingInput.id = 'openingFeeding'+ iteration;
   openingFeedingInput.value = '';
   openingFeedingInput.size = 5;
   var openingFeedingId='openingFeeding'+iteration;
   openingFeedingInput.onkeyup=function()
	{
	   return validateFeild(event,openingFeedingId);
	}
   ActivityRow.appendChild(openingFeedingInput);
   

// for unloading text box .....................

    var splo=document.createTextNode('   ');
	ActivityRow.appendChild(splo);
    var unloading = document.createTextNode('Unloading');
    var bunload=document.createElement('b');
	bunload.appendChild(unloading);
	ActivityRow.appendChild(bunload);
	var spUnlo=document.createTextNode('     ');
	ActivityRow.appendChild(spUnlo);
   var unloadingInput = document.createElement('input');
   unloadingInput.type = 'text';
   unloadingInput.name = 'unloading' + iteration;
   unloadingInput.id = 'unloading'+ iteration;
   unloadingInput.value = "";
   unloadingInput.size = 5;
   var unloadingId='unloading'+iteration;
   unloadingInput.onkeyup=function()
	{
	   return validateFeild(event,unloadingId);
	}
   ActivityRow.appendChild(unloadingInput);
  var bropen=document.createElement('br');
   ActivityRow.appendChild(bropen);


   // for Sorting text box ........................

   var sorting=document.createTextNode('Sorting');
   var bsort=document.createElement('b');
   bsort.appendChild(sorting);
   ActivityRow.appendChild(bsort);
   sortfont=document.createElement('font');
   sortfont.color='#BECEEF';
   sortfont.size='3';
    var spSort=document.createTextNode('            ');
	sortfont.appendChild(spSort);
 ActivityRow.appendChild(sortfont);
   var sortingInput = document.createElement('input');
   sortingInput.type = 'text';
   sortingInput.name = 'sorting' + iteration;
   sortingInput.id = 'sorting'+ iteration;
   sortingInput.value = '';
   sortingInput.size = 5;
   var sortingId='sorting'+iteration;
   sortingInput.onkeyup=function()
	{
	  
	   return validateFeild(event,sortingId);
	}
   ActivityRow.appendChild(sortingInput);
   // for reStacking text box.......................

    var reStacking =document.createTextNode('Re-Stacking');
	var spRe=document.createTextNode('   ');
	ActivityRow.appendChild(spRe);
	var bre=document.createElement('b');
	bre.appendChild(reStacking);
	ActivityRow.appendChild(bre);
	var spre=document.createTextNode('            ');
	ActivityRow.appendChild(spre);
   var reStackingInput = document.createElement('input');
   var reStackingCenter=document.createElement("center");
   reStackingInput.type = 'text';
   reStackingInput.name = 'reStacking' + iteration;
   reStackingInput.id = 'reStacking'+ iteration;
   reStackingInput.value = '';
   reStackingInput.size = 5;
   var reStackingId='reStacking'+iteration;
   reStackingInput.onkeyup=function()
	{
	   return validateFeild(event,reStackingId);
	}
   ActivityRow.appendChild(reStackingInput);
  

// for Stitching text box.....................

 var stitching =document.createTextNode('Stitching');
  var spst=document.createTextNode('    ');
	ActivityRow.appendChild(spst);
 var bst=document.createElement('b');
 bst.appendChild(stitching);
 ActivityRow.appendChild(bst);
 var spSt=document.createTextNode('       ');
	ActivityRow.appendChild(spSt);
 var stitchingInput = document.createElement('input');
 stitchingInput.type = 'text';
 stitchingInput.name = 'stitching' + iteration;
 stitchingInput.id = 'stitching'+ iteration;
 stitchingInput.value = '';
 stitchingInput.size = 5;
 var stitchingId='stitching'+iteration;
 stitchingInput.onkeyup=function()
	{
	   return validateFeild(event,stitchingId);
	}
   ActivityRow.appendChild(stitchingInput);
	var brRe=document.createElement('br');
   ActivityRow.appendChild(brRe);

   // for Stacking text box..................

   var stacking = document.createTextNode('Stacking');
   var bstack=document.createElement('b');
   bstack.appendChild(stacking);
   ActivityRow.appendChild(bstack);
    var spSt=document.createTextNode('          ');
 ActivityRow.appendChild(spSt);
   var stackingInput = document.createElement('input');
   stackingInput.type = 'text';
   stackingInput.name = 'stacking' + iteration;
   stackingInput.id = 'stacking'+ iteration;
   stackingInput.value = "";
   stackingInput.size = 5;
   var stackingId='stacking'+iteration;
   stackingInput.onkeyup=function()
	{
	   return validateFeild(event,stackingId);
	}
   ActivityRow.appendChild(stackingInput);
   // for bundle preparation text box...............................

var bundlePreparation =document.createTextNode('Bundle Preparation');
var spBu=document.createTextNode('   ');
	ActivityRow.appendChild(spBu);
var bbun=document.createElement('b');
bbun.appendChild(bundlePreparation);
ActivityRow.appendChild(bbun);
var spbun=document.createTextNode('');
	ActivityRow.appendChild(spbun);
var bundlePreparationInput = document.createElement('input');
var bundlePreparationCenter=document.createElement("center");
bundlePreparationInput.type = 'text';
bundlePreparationInput.name = 'bundlePreparation' + iteration;
bundlePreparationInput.id = 'bundlePreparation'+ iteration;
bundlePreparationInput.value = '';
bundlePreparationInput.size = 5;
var bundlePreparationId='bundlePreparation'+iteration;
bundlePreparationInput.onkeyup=function()
	{
	   return validateFeild(event,bundlePreparationId);
	}
ActivityRow.appendChild(bundlePreparationInput);

   // for weighing text box .........................

   var weighing =document.createTextNode('Weighing');
    var spwe=document.createTextNode('   ');
	ActivityRow.appendChild(spwe);
   var bwe=document.createElement('b');
   bwe.appendChild(weighing);
   ActivityRow.appendChild(bwe);
   var spWe=document.createTextNode('      ');
	ActivityRow.appendChild(spWe);
   var weighingInput = document.createElement('input');
   weighingInput.type = 'text';
   weighingInput.name = 'weighing' + iteration;
   weighingInput.id = 'weighing'+ iteration;
   weighingInput.value = '';
   weighingInput.size = 5;
   var weighingId='weighing'+iteration;
   weighingInput.onkeyup=function()
	{
	   
	   return validateFeild(event,weighingId);
	}
  ActivityRow.appendChild(weighingInput);
  var brbun=document.createElement('br');
  ActivityRow.appendChild(brbun);
//------------------------ no of stack-----------------------
var weighing =document.createTextNode('No Stack');
    var spwe=document.createTextNode('   ');
	ActivityRow.appendChild(spwe);
   var bwe=document.createElement('b');
   bwe.appendChild(weighing);
   ActivityRow.appendChild(bwe);
   var spWe=document.createTextNode('         ');
	ActivityRow.appendChild(spWe);
   var weighingInput = document.createElement('input');
   weighingInput.type = 'text';
   weighingInput.name = 'stack' + iteration;
   weighingInput.id = 'stack'+ iteration;
   weighingInput.value = '';
   weighingInput.size = 5;
   var weighingId='stack'+iteration;
   weighingInput.onkeyup=function()
	{
	   
	   return validateFeild(event,weighingId);
	}
  ActivityRow.appendChild(weighingInput);

 //-----------------------loading unloading-------------------------

var bundlePreparation =document.createTextNode('LoadingUnloading');
var spBu=document.createTextNode('   ');
	ActivityRow.appendChild(spBu);
var bbun=document.createElement('b');
bbun.appendChild(bundlePreparation);
ActivityRow.appendChild(bbun);
var spbun=document.createTextNode('  ');
	ActivityRow.appendChild(spbun);
var bundlePreparationInput = document.createElement('input');
var bundlePreparationCenter=document.createElement("center");
bundlePreparationInput.type = 'text';
bundlePreparationInput.name = 'LoadingUnloading' + iteration;
bundlePreparationInput.id = 'LoadingUnloading'+ iteration;
bundlePreparationInput.value = '';
bundlePreparationInput.size = 5;
var bundlePreparationId='LoadingUnloading'+iteration;
bundlePreparationInput.onkeyup=function()
	{
	   return validateFeild(event,bundlePreparationId);
	}
ActivityRow.appendChild(bundlePreparationInput);

    //var brbun=document.createElement('br');
	// ActivityRow.appendChild(brbun);


  // for stack DH to Antichamber................................


  var stackDhToAnti =document.createTextNode('StkDH-Anti');
var spBu=document.createTextNode('   ');
	ActivityRow.appendChild(spBu);
var bbun=document.createElement('b');
bbun.appendChild(stackDhToAnti);
ActivityRow.appendChild(bbun);
var spbun=document.createTextNode('  ');
	ActivityRow.appendChild(spbun);
var stackDhToAntiInput = document.createElement('input');
var stackDhToAntiCenter=document.createElement('center');
stackDhToAntiInput.type = 'text';
stackDhToAntiInput.name = 'DHToAnti' + iteration;
stackDhToAntiInput.id = 'DHToAnti'+ iteration;
stackDhToAntiInput.value = '';
stackDhToAntiInput.size = 5;
var bundlePreparationId='DHToAnti'+iteration;
bundlePreparationInput.onkeyup=function()
	{
	   return validateFeild(event,bundlePreparationId);
	}
ActivityRow.appendChild(stackDhToAntiInput);

    var brbun=document.createElement('br');
  ActivityRow.appendChild(brbun);



// for Stacked Plant to Godown...............................

 var stackedPlantToGodown =document.createTextNode('StkPlt-Gn ');
var spBu=document.createTextNode('   ');
	ActivityRow.appendChild(spBu);
var bbun=document.createElement('b');
bbun.appendChild(stackedPlantToGodown);
ActivityRow.appendChild(bbun);
var spbun=document.createTextNode('       ');
	ActivityRow.appendChild(spbun);
var stackedPlantToGodownInput = document.createElement('input');
var stackedPlantToGodownCenter=document.createElement("center");
stackedPlantToGodownInput.type = 'text';
stackedPlantToGodownInput.name = 'plantToGodown' + iteration;
stackedPlantToGodownInput.id = 'plantToGodown'+ iteration;
stackedPlantToGodownInput.value = '';
stackedPlantToGodownInput.size = 5;
var bundlePreparationId='plantToGodown'+iteration;
bundlePreparationInput.onkeyup=function()
	{
	   return validateFeild(event,bundlePreparationId);
	}
ActivityRow.appendChild(stackedPlantToGodownInput);

// for Height Differance...............................

 var heightDiff =document.createTextNode('Height Diff ');
var spHeight=document.createTextNode('   ');
	ActivityRow.appendChild(spHeight);
var hbun=document.createElement('b');
hbun.appendChild(heightDiff);
ActivityRow.appendChild(hbun);
var spheight=document.createTextNode('             ');
	ActivityRow.appendChild(spheight);
var heightInput = document.createElement('input');
var heightCenter=document.createElement("center");
heightInput.type = 'text';
heightInput.name = 'heightDiff' + iteration;
heightInput.id = 'heightDiff'+ iteration;
heightInput.value = '';
heightInput.size = 5;
var heightId='heightDiff'+iteration;
heightInput.onkeyup=function()
	{
	   return validateFeild(event,heightId);
	}
ActivityRow.appendChild(heightInput);

 // for remark text box.........................................
 
   var remark =document.createTextNode('Remak');
   var brem=document.createElement('b');
   var spRemark=document.createTextNode('   ');
   ActivityRow.appendChild(spRemark);
   brem.appendChild(remark);
   ActivityRow.appendChild(brem);
    var spRe=document.createTextNode('         ');
 ActivityRow.appendChild(spRe);
   var remarkInput = document.createElement('textarea');
   var remarkCenter=document.createElement("center");
   remarkInput.name = 'remark' + iteration;
   remarkInput.id = 'remark'+ iteration;
   remarkInput.value = '';
   remarkInput.rows="1";
   remarkInput.cols="5";
   ActivityRow.appendChild(remarkInput);
}