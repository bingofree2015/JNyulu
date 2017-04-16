/*
* jQuery FillOptions
*
* Author: luq885
* http://blog.csdn.net/luq885 (chinese) 
*
* Licensed like jQuery, see http://docs.jquery.com/License
*
* ���ߣ���������
* blog: http://blog.csdn.net/luq885
*/

var text;
var value;
var type;
var selected;
var keep;

jQuery.fn.FillOptions = function(url,options){
    if(url.length == 0) throw "request is required";        
    text = options.textfield || "text";
    value = options.valuefiled || "value";    
    type = options.datatype.toLowerCase() || "json";
    if(type != "xml")type="json";
    keep = options.keepold?true:false;
    selected = options.selectedindex || 0;

    jQuery.ajaxSetup({ async: false });
    var datas;
    if(type == "xml")
    {
      jQuery.get(url, function(xml) { datas = xml; });            
    }
    else
    {
      jQuery.getJSON(url, function(json) { datas = json; });
    }
    if(datas == undefined)
    {
        alert("no datas");
        return;
    }
    this.each(function(){
        if(this.tagName == "SELECT")
        {
            var select = this;
            if(!keep)jQuery(select).html("");
            addOptions(select,datas);
        }
    });
}


function addOptions(select,datas)
{        
    var options;
    var datas;
    if(type == "xml")
    {
        jQuery(text,datas).each(function(i){            
            option = new Option(jQuery(this).text(),jQuery(jQuery(value,datas)[i]).text());
            if(i==selected)option.selected=true;
            select.options.add(option);
        });
    }
    else
    {
        jQuery.each(datas,function(i,n){
            option = new Option(eval("n."+text),eval("n."+value));
            if(i==selected)option.selected=true;
            select.options.add(option);
        });
    }
}