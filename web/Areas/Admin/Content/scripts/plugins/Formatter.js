function regReplace(sOrgVal,sSearchVal,sReplaceVal)
{
	var sVal;
	try
	{
		sVal = new String(sOrgVal);
		if (sVal.length < 1) { return sVal; }
		var sRegExp = eval("/\\" + sSearchVal + "/g");
		sVal = sVal.replace(sRegExp,sReplaceVal);
	}
	catch (exception) { }
	return sVal;

}
function isMaxLength(object, maxlength, e)
{
	if (isNavigation(e))
	{
		return true;
	}
	if (object.value.length >= maxlength)
	{
		return false;
	}
	return true;
}
function isNumberMaxLength(object, maxlength, e)
{
	if (isNavigation(e))
	{
		return true;
	}
	if (!isNumber(e))
	{
		return false;
	}
	if (object.value.replace(/,/g,"").replace(/\./g,"").length >= maxlength)
	{
		
		return false;
	}
	return true;
}
function isNumber(e) {
   
    if (e.keyCode) keycode = e.keyCode
    else keycode = e.which;
    if ((keycode == 46)) return true;
    if ((keycode == 44)) return true;
    //if ((keycode == 127)) return true;
    if ((keycode < 48) || (keycode > 57)) {
       // if (keycode != 127)
       // {
            return isNavigation(e);
       // }
       // else {
       //     alert(keycode);
        //    return true;
         //}
    }
    else
        return true;
	
}
function BuyukHarf(strInp)
{
	var iLength=0;
	var i=0;
	iLength=strInp.length;
	
	var result="";
	for (i=0;i<=strInp.length-1;i++)
	
	{
		
		switch (strInp.charAt(i))	
		{
			case 'þ':
				result=result + "Þ";
				break;
			case 'Þ':
				result=result + "Þ";
				break;
			case 'ç':
				result=result + "Ç";
				break;
			case 'Ç':
				result=result + "Ç";
				break;
			case 'i':
				result=result + "Ý";
				break;
			case 'ð':
				result=result + "Ð";
				break;
			case 'Ð':
				result=result + "Ð";
				break;
			case 'ý':
				result=result + "I";
				break;
			case 'Ý':
				result=result + "Ý";
				break;
			case 'ö':
				result=result + "Ö";
				break;
			case 'Ö':
				result=result + "Ö";
				break;
			case 'ü':
				result=result + "Ü";
				break;
			case 'Ü':
				result=result + "Ü";
				break;
			default:
				
				result=result + strInp.charAt(i).toUpperCase();
				break;
				
		}
	}

	return result;
}
function fractionFormat(field, e)
{
	if (isNavigation(e)) // tabda falan saçmalamasýn diye
	{
		return;
	}
	var value = field.value;
	var maxLength = field.maxLength;
	var newValue = "";
	for (var i = 0 ; i < value.length && i < maxLength; i++)
	{
		switch(value.substring(i,i+1))
		{
			case "0":
			case "1":
			case "2":
			case "3":
			case "4":
			case "5":
			case "6":
			case "7":
			case "8":
			case "9":
			case ",":
				newValue += value.substring(i,i+1);
				break;
			default:
				break;
		}
	}
	field.value = newValue;
}
function numberFormat(number,decimalNumber,showLeadingZeros,parent,boolean)
{
	var leadingZeroCount = 0;
	var i = 0;
	for (i = 0; i < number.length; i++)
	{
		if (number.substring(i,i+1) == 0){
			leadingZeroCount++;
		}else{
			break;
		}
	}
	number = regReplace(number,".","");
	var templateNumber = parseInt(number,10);
	var iSign = number < 0 ? -1 : 1;
	templateNumber *= Math.pow(10,decimalNumber);
	templateNumber = Math.round(Math.abs(templateNumber))
	templateNumber /= Math.pow(10,decimalNumber);
	templateNumber *= iSign;
	var returnNumber = new String(templateNumber);
	/*if (!zero && number < 1 && number > -1 && number != 0)
	if (number > 0)
	returnNumber = returnNumber.substring(1,returnNumber.length);
	else
	returnNumber = "-" + returnNumber.substring(2,returnNumber.length);*/
	if (boolean && (number >= 1000 || number <= -1000))
	{
		var iStart = returnNumber.indexOf(".");
		if (iStart < 0)
		iStart = returnNumber.length;
		iStart -= 3;
		while (iStart >= 1)
		{
			returnNumber = returnNumber.substring(0,iStart) + "." + returnNumber.substring(iStart,returnNumber.length)
			iStart -= 3;
		}
	}
	if (parent && number < 0)
	{
		returnNumber = "(" + returnNumber.substring(1,returnNumber.length) + ")";
	}
	if (returnNumber == 'NaN')
	returnNumber = '';
	if (showLeadingZeros)
	{
		for (i = 0; i < returnNumber.length;)
		{
			if (returnNumber.substring(i,i+1) == 0){
				returnNumber = returnNumber.replace("0", "");
			}else{
				break;
			}
		}
		for (i = 0; i < leadingZeroCount; i++)
		{
			returnNumber = "0" + returnNumber;
		}
	}
	return returnNumber;
}

function FocusNextFieldOnLength(object, MaxLength, NextObject, event_obj)
{
	if (object.value.length == MaxLength)
	{
		var x = isNavigation(event_obj);
		if (!x){
			eval("document.forms[0]." + NextObject).focus();
		}
	}
}
function isNavigation(e)
{
	if (e.ctrlKey && e.which==118)
	{
		return true;
	}
	var keycode=e.keyCode;
     switch(keycode)
		{
			case 8://backspace
			case 9://tab
			case 13://enter
			case 16://shift
			case 17://ctrl
			
				return true;
				break;
			case 35://end
			case 36://home
			case 37://left
			case 39://right
			case 46://delete
				browser = CheckBrowser();
				if (browser.indexOf("ie") != -1)
				{
					return false;
				}
				return true;
				break;
		}
	return false;
}
function AccountComboInitialize(element)
{
      if (isHesDVMISelected(eval(element)))
      {
            eval(element).selectedIndex = 0;
      }
}

function getDateObject(dateString, dateSeperator)
{
	var curValue=dateString;
	var sepChar=dateSeperator;
	var curPos=0;
	var cDate,cMonth,cYear;

	//extract day portion
	curPos=dateString.indexOf(sepChar);
	cDate=dateString.substring(0,curPos);
	
	//extract month portion				
	endPos=dateString.indexOf(sepChar,curPos+1);
	cMonth=dateString.substring(curPos+1,endPos); // -1

	//extract year portion				
	curPos=endPos;
	endPos=curPos+5;			
	cYear=curValue.substring(curPos+1,endPos);
	
	//Create Date Object
	dtObject=new Date(cYear,cMonth,cDate);	
	return dtObject;
}


function intOnly(i) 
{
	if(i.value.length>0) 
	{
		i.value = i.value.replace(/[^\d]+/g, ''); 
	}
}
