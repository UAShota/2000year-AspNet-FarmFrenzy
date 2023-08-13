// Получение кукисов по их имени
function getCookie(name) {
  var cookie = " " + document.cookie;
  var search = " " + name + "=";
  var setStr = null;
  var offset = 0;
  var end = 0;
  if (cookie.length > 0) {
    offset = cookie.indexOf(search);
    if (offset != -1) {
      offset += search.length;
      end = cookie.indexOf(";", offset)
      if (end == -1) {
        end = cookie.length;
      }
      setStr = unescape(cookie.substring(offset, end));
    }
  }
  return(setStr);
}

function ClearCookieArray()
{
  document.cookie = "array=" + (new Array()) + ";expires=Mon, 01-Jan-2020 00:00:00 GMT";
}

function SetCookieArray(array)
{
  document.cookie = "array=" + array + ";expires=Mon, 01-Jan-2020 00:00:00 GMT";
}

function GetCookieArray()
{
  _items = getCookie("array");
  if(_items == null) _items = new Array(); else 
  {
    //_items = (_items + ",");
    _items = _items.split(",");
  }
  
  return _items;
}

function SelectCell(ID)
{
  _items = GetCookieArray();
  
  oObj = document.getElementById(ID);
  if (!oObj.checked) {
    oObj.checked = true; 
    oObj.className = "bg_cellsel";
  } else  { 
    oObj.checked = false;
    oObj.className = "bg_cellnosel";
  } 
  
  var FlagFound = false;
  for (var i = 0; i <= _items.length - 1; i++) {
    if (_items[i] == ID) {
      FlagFound = true;
      break;
    }
  }
  if (!FlagFound) _items.push(ID); else _items.splice(i, 1);

  oGraph = document.getElementById("CPgraphics");  
  oGraph.disabled = (_items.length <= 0);
  
  SetCookieArray(_items);
}

function SelectRootCell(ID)
{
  _items = GetCookieArray();
  
  oObj = document.getElementById(ID);
  if (!oObj.checked) {
    oObj.checked = true; 
    oObj.className = "bg_header_sel";
  } else  { 
    oObj.checked = false;
    oObj.className = "bg_header";
  } 
  
  var FlagFound = false;
  for (var i = 0; i <= _items.length - 1; i++) {
    if (_items[i] == ID) {
      FlagFound = true;
      break;
    }
  }
  if (!FlagFound) _items.push(ID); else _items.splice(i, 1);

  oGraph = document.getElementById("CPgraphics");  
  oGraph.disabled = (_items.length <= 0);
  
  SetCookieArray(_items);
}

function FocusCell(ID)
{
  oObj = document.getElementById(ID);
  oObj.style.color = "red";
}

function UnFocusCell(ID)
{
  oObj = document.getElementById(ID);
  oObj.style.color = "white";
}

function OpenWindow(IdInt)
{ 
  var Params = "";
  for (var i = 0; i <= _items.length - 1; i++) Params += "ora64.dat.nom[" + _items[i] + "].val[" + IdInt + "];"; 
  
  window.showModalDialog('http://isphp/myrun/startgraf.php?parameters=,;' + Params, window, 
    "dialogHeight:600px;dialogWidth:800px;center=yes;resizable=yes;scroll=no;status=no;help=no;");
}