

function checkActivity(i)
{
	var activity=document.getElementById("activity"+i).value;
	document.getElementById("weight"+i).options.length=0;
	document.getElementById("weight"+i).options[0]=new Option('-Select-','');

	if(activity=='ha011')
	{	
		
		document.getElementById("weight"+i).options[1]=new Option('300Kg','300');
		document.getElementById("weight"+i).options[2]=new Option('400Kg','400');
		
	}
	else
	{
	    document.getElementById("weight"+i).options[1]=new Option('15Kg','15');
		document.getElementById("weight"+i).options[2]=new Option('40Kg','40');
		document.getElementById("weight"+i).options[3]=new Option('100Kg','100');
	}
}

function chkplant(plant,rowId)
{
	
	document.getElementById("loading"+rowId).value="";
	document.getElementById("unloading"+rowId).value="";
	document.getElementById("sorting"+rowId).value="";
	document.getElementById("stitching"+rowId).value="";
	document.getElementById("stacking"+rowId).value="";
	document.getElementById("reStacking"+rowId).value="";
	document.getElementById("weighing"+rowId).value="";
	document.getElementById("bundlePreparation"+rowId).value="";
	document.getElementById("openingFeeding"+rowId).value="";
	document.getElementById("noStack"+rowId).value="";
	document.getElementById("stackDhToAnti"+rowId).value="";
	document.getElementById("stackPlantToGodown"+rowId).value="";
	document.getElementById("loadingUnloading"+rowId).value="";
	if(plant=="")
	{
		plant=document.getElementById("plant"+rowId).value;
	}
	//alert("plant "+plant);
	if(plant==1156||plant==1179||plant==1181||plant==1180)
	{
		
		document.getElementById("distance"+rowId).disabled=true;
		document.getElementById("layer"+rowId).disabled=true;
		document.getElementById("weight"+rowId).disabled=false;  // change the status to false on 15-12-2011 12.12PM 'Vaibhav'
		var rateStr=document.frm.rateStr.value;
		var rateArr=rateStr.split("@");
		for(var i=0;i<rateArr.length;i++)
		{
			var plantRate=rateArr[i].split("!");
			pln=document.getElementById("plant"+rowId).value;
			if(pln==plantRate[0])
			{
				var act=document.getElementById("activity"+rowId).value;
				//alert("activity: "+act);
				if(act==plantRate[1])
				{
					document.getElementById("loading"+rowId).value=plantRate[5];
					document.getElementById("unloading"+rowId).value=plantRate[6];
					document.getElementById("sorting"+rowId).value=plantRate[7];
					document.getElementById("stitching"+rowId).value=plantRate[8];
					document.getElementById("stacking"+rowId).value=plantRate[9];
					document.getElementById("reStacking"+rowId).value=plantRate[10];
					document.getElementById("weighing"+rowId).value=plantRate[11];
					document.getElementById("bundlePreparation"+rowId).value=plantRate[12];
					document.getElementById("openingFeeding"+rowId).value=plantRate[13];
					document.getElementById("noStack"+rowId).value=plantRate[14];
					document.getElementById("stackDhToAnti"+rowId).value=plantRate[15];
					document.getElementById("stackPlantToGodown"+rowId).value=plantRate[16];
					document.getElementById("loadingUnloading"+rowId).value=plantRate[17];
					document.getElementById("heightDiff"+rowId).value=plantRate[18];
												
				}
			}
		}
	}
	else
	{
		//alert("hello");
		document.getElementById("distance"+rowId).disabled=false;
		document.getElementById("layer"+rowId).disabled=false;
		document.getElementById("weight"+rowId).disabled=false;
		var rateStr=document.frm.rateStr.value;
		var rateArr=rateStr.split("@");
		for(var i=0;i<rateArr.length;i++)
		{
			var plantRate=rateArr[i].split("!");
			pln=document.getElementById("plant"+rowId).value;
			if(pln==plantRate[0])
			{
				var act=document.getElementById("activity"+rowId).value;
				//alert("activity: "+act);
				if(act==plantRate[1])
				{
					var dist=document.getElementById("distance"+rowId).value;	
					//alert("distance"+dist);
					if(dist==plantRate[2])
					{
						var layer=document.getElementById("layer"+rowId).value;
						//alert(layer);
						if(layer==plantRate[3])
						{
							var wei=document.getElementById("weight"+rowId).value;						
							//alert("activity: "+act+":"+plantRate[1]+"distance: "+dist+":"+plantRate[2]+" layer: "+layer+":"+plantRate[3]+" weight: "+wei+":"+plantRate[4]);
							if((wei+".0")==plantRate[4])
							{
								document.getElementById("loading"+rowId).value=plantRate[5];
								document.getElementById("unloading"+rowId).value=plantRate[6];
								document.getElementById("sorting"+rowId).value=plantRate[7];
								document.getElementById("stitching"+rowId).value=plantRate[8];
								document.getElementById("stacking"+rowId).value=plantRate[9];
								document.getElementById("reStacking"+rowId).value=plantRate[10];
								document.getElementById("weighing"+rowId).value=plantRate[11];
								document.getElementById("bundlePreparation"+rowId).value=plantRate[12];
								document.getElementById("openingFeeding"+rowId).value=plantRate[13];
								document.getElementById("noStack"+rowId).value=plantRate[14];
								document.getElementById("stackDhToAnti"+rowId).value=plantRate[15];
								document.getElementById("stackPlantToGodown"+rowId).value=plantRate[16];
								document.getElementById("loadingUnloading"+rowId).value=plantRate[17];
								document.getElementById("heightDiff"+rowId).value=plantRate[18];
												
							}
						}
					}
				}
			}
		}
	}
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
	 
		 return false;
		  
	}
}
function rowCount()
{
	var tbl = document.getElementById('hamalRateBody');
	var rowCount = tbl.rows.length;
	return rowCount;
}


function addRowToTable()
{
   var tbl = document.getElementById('hamalRateBody');
   var lastRow = tbl.rows.length;
   var iteration = rowCount();
   iteration = iteration + 1;
   var row = tbl.insertRow(lastRow);
   row.backgroundcolor="#B3D9FF";
   row.height="40";
   row.border=3;
   //row.width="2100";
   row.id="r"+iteration;
   document.getElementById("r"+iteration).style.backgroundColor = "#B3D9FF";
   var srNo = row.insertCell(0);
   var textNodeSrNo = document.createTextNode(iteration);
   var SrNoCenter=document.createElement('center');
   var SrNoBlod=document.createElement("b");
   SrNoBlod.appendChild(textNodeSrNo);
   SrNoCenter.appendChild(SrNoBlod);
     //srNo.appendChild(SrNoCenter);


   var plantRow= row.insertCell(1);   
   var plantCombo = document.createElement('select');   
   plantCombo.name="plant"+iteration;
   plantCombo.id="plant"+iteration;   
   var plantStr=document.form1.plantStr.value;
   var plantArr=plantStr.split("@");
    plantCombo.options[0]=new Option("-Select-","");
   for(var i=0;i<plantArr.length;i++)
   {
	 var plnArr=plantArr[i].split("!");
	 plantCombo.options[i+1]=new Option(plnArr[1],plnArr[0]);
   }
   plantCombo.onchange=function()
	{
		chkplant(this.options[this.selectedIndex].value,iteration);
	}
   plantRow.appendChild(plantCombo);

   var actRow=row.insertCell(2);
   var actCombo=document.createElement("select");
   actCombo.name="activity"+iteration;
   actCombo.id="activity"+iteration;
   actCombo.options[0]=new Option("-Select-","");
   actCombo.options[1]=new Option("Common","comm");
   var actStr=document.getElementById("actStr").value;
   var actArr=actStr.split("@");
   for(var i=0;i<actArr.length;i++)
	{
	   
	   var act=actArr[i].split("!");
       actCombo.options[i+2]=new Option(act[1],act[0]);
	}
	actCombo.onchange=function()
	{
		chkplant("",iteration);
		checkActivity(iteration);
	}
	actRow.appendChild(actCombo);

   var distRow=row.insertCell(3);
   var distCombo=document.createElement("select");
   distCombo.name="distance"+iteration;
   distCombo.id="distance"+iteration;
   distCombo.options[0]=new Option("-Select-","");
   distCombo.options[1]=new Option("More than 206","L");
   distCombo.options[2]=new Option("Between 106-205","M");
   distCombo.options[3]=new Option("Between 0-105","N");  
   distCombo.onchange=function()
	{
		chkplant("",iteration);
	}
   distRow.appendChild(distCombo);

     var layCombo=document.createElement("input");
   layCombo.type='hidden'
   layCombo.name="layer"+iteration;
   layCombo.id="layer"+iteration;
   layCombo.value='S';
   distRow.appendChild(layCombo);

   var weiRow=row.insertCell(4);
   var weiCombo=document.createElement("select");
   weiCombo.name="weight"+iteration;
   weiCombo.id="weight"+iteration;
   weiCombo.options[0]=new Option("-Select-","");
   weiCombo.options[1]=new Option("15 Kg","15");
   weiCombo.options[2]=new Option("40 Kg","40");
   weiCombo.options[3]=new Option("100 Kg","100");
   weiCombo.onchange=function()
	{
		chkplant("",iteration);
	}
   weiRow.appendChild(weiCombo);
  
  var loadingRow=row.insertCell(5);
  var loadingInput = document.createElement('input');
   loadingInput.type = 'text';
   loadingInput.name = 'loading' + iteration;
   loadingInput.id = 'loading'+ iteration;
   loadingInput.value = "";
   loadingInput.size = 5;
   var loadingId='loading'+iteration;
   loadingInput.onkeydown=function()
	{
	   return validateFeild(event,loadingId);
	}
	loadingRow.appendChild(loadingInput);

	
	//------------unloading text box------------------

	var unloadingRow=row.insertCell(6);
	var unloadingInput = document.createElement('input');
    unloadingInput.type = 'text';
    unloadingInput.name = 'unloading' + iteration;
    unloadingInput.id = 'unloading'+ iteration;
    unloadingInput.value = "";
    unloadingInput.size = 5;
    var unloadingId='unloading'+iteration;
    unloadingInput.onkeydown=function()
	{
	   return validateFeild(event,unloadingId);
	}
   
    unloadingRow.appendChild(unloadingInput);

	//-----------Sorting-----------------------

   var sortingRow=row.insertCell(7);
   var sortingInput = document.createElement('input');
   sortingInput.type = 'text';
   sortingInput.name = 'sorting' + iteration;
   sortingInput.id = 'sorting'+ iteration;
   sortingInput.value = '';
   sortingInput.size = 5;
   var sortingId='sorting'+iteration;
   sortingInput.onkeydown=function()
	{
	  
	   return validateFeild(event,sortingId);
	}
	sortingRow.appendChild(sortingInput);

	//---------------Stiching-------------------------

 var stichingRow=row.insertCell(8);
 var stitchingInput = document.createElement('input');
 stitchingInput.type = 'text';
 stitchingInput.name = 'stitching' + iteration;
 stitchingInput.id = 'stitching'+ iteration;
 stitchingInput.value = '';
 stitchingInput.size = 5;
 var stitchingId='stitching'+iteration;
 stitchingInput.onkeydown=function()
	{
	   return validateFeild(event,stitchingId);
	}
   stichingRow.appendChild(stitchingInput);

   //---------------Stacking-----------------------------

   var stackingRow=row.insertCell(9);
    var stackingInput = document.createElement('input');
   stackingInput.type = 'text';
   stackingInput.name = 'stacking' + iteration;
   stackingInput.id = 'stacking'+ iteration;
   stackingInput.value = "";
   stackingInput.size = 5;
   var stackingId='stacking'+iteration;
   stackingInput.onkeydown=function()
	{
	   return validateFeild(event,stackingId);
	}
   stackingRow.appendChild(stackingInput);

   //-----------------------Restacking----------------------------

   var reStackingRow=row.insertCell(10);
   var reStackingInput = document.createElement('input');  
   reStackingInput.type = 'text';
   reStackingInput.name = 'reStacking' + iteration;
   reStackingInput.id = 'reStacking'+ iteration;
   reStackingInput.value = '';
   reStackingInput.size = 5;
   var reStackingId='reStacking'+iteration;
   reStackingInput.onkeydown=function()
	{
	   return validateFeild(event,reStackingId);
	}
   reStackingRow.appendChild(reStackingInput);

   //------------------- Weighing-----------------------------

   var weigRow=row.insertCell(11);
   var weighingInput = document.createElement('input');
   weighingInput.type = 'text';
   weighingInput.name = 'weighing' + iteration;
   weighingInput.id = 'weighing'+ iteration;
   weighingInput.value = '';
   weighingInput.size = 5;
   var weighingId='weighing'+iteration;
   weighingInput.onkeydown=function()
	{
	   
	   return validateFeild(event,weighingId);
	}
  weigRow.appendChild(weighingInput);

  //------------------------Bundle Preparation--------------------------

var bunPreRow=row.insertCell(12);
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
bunPreRow.appendChild(bundlePreparationInput);

//-----------------opening Feeding--------------------------------------

var openFeedingRow=row.insertCell(13);
var openingFeedingInput = document.createElement('input');
   openingFeedingInput.type = 'text';
   openingFeedingInput.name = 'openingFeeding' + iteration;
   openingFeedingInput.id = 'openingFeeding'+ iteration;
   openingFeedingInput.value = '';
   openingFeedingInput.size = 5;
   var openingFeedingId='openingFeeding'+iteration;
   openingFeedingInput.onkeydown=function()
	{
	   return validateFeild(event,openingFeedingId);
	}
   openFeedingRow.appendChild(openingFeedingInput);

   //-------------------------- no Stack-------------------------------

   var noStackRow=row.insertCell(14);
   var noStackInput=document.createElement('input');
   noStackInput.type='text';
   noStackInput.name='noStack'+iteration;
   noStackInput.id='noStack'+iteration;
   noStackInput.value='';
   noStackInput.size=5;
   var noStackId='noStack'+iteration;
   noStackInput.onkeydown=function()
	{
	   return validateFeild(event,noStackId);
	}
	noStackRow.appendChild(noStackInput);

	//-------------loading unloading---------------------------------------------

	var loaUnloRow=row.insertCell(15);
	var loaUnlInput=document.createElement('input');
	loaUnlInput.type='text';
	loaUnlInput.name='loadingUnloading'+iteration;
	loaUnlInput.id='loadingUnloading'+iteration;
	loaUnlInput.value='';
	loaUnlInput.size='5';
	var loaUnlId="loadingUnloading"+iteration;
	loaUnlInput.onkeydown=function()
	{
		return validateFeild(event,loaUnlId);
	}
	loaUnloRow.appendChild(loaUnlInput);


	//-------------Stack Dh To Anti---------------------------------------------

	var dhToAntiRow=row.insertCell(16);
	var dhToAntiInput=document.createElement('input');
	dhToAntiInput.type='text';
	dhToAntiInput.name='stackDhToAnti'+iteration;
	dhToAntiInput.id='stackDhToAnti'+iteration;
	dhToAntiInput.value='';
	dhToAntiInput.size='5';
	var dhToAntiId="stackDhToAnti"+iteration;
	loaUnlInput.onkeydown=function()
	{
		return validateFeild(event,dhToAntiId);
	}
	dhToAntiRow.appendChild(dhToAntiInput);


	//-------------Stack Plant To Godown---------------------------------------------

	var plantToGodownRow=row.insertCell(17);
	var plantToGodownInput=document.createElement('input');
	plantToGodownInput.type='text';
	plantToGodownInput.name='stackPlantToGodown'+iteration;
	plantToGodownInput.id='stackPlantToGodown'+iteration;
	plantToGodownInput.value='';
	plantToGodownInput.size='5';
	var Id="stackPlantToGodown"+iteration;
	loaUnlInput.onkeydown=function()
	{
		return validateFeild(event,Id);
	}
	plantToGodownRow.appendChild(plantToGodownInput);


	//-------------Height Differance---------------------------------------------

	var heightRow=row.insertCell(18);
	var heightInput=document.createElement('input');
	heightInput.type='text';
	heightInput.name='heightDiff'+iteration;
	heightInput.id='heightDiff'+iteration;
	heightInput.value='';
	heightInput.size='5';
	var Id="heightDiff"+iteration;
	loaUnlInput.onkeydown=function()
	{
		return validateFeild(event,Id);
	}
	heightRow.appendChild(heightInput);


}
