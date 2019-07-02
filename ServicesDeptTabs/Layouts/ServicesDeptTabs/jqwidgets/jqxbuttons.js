/*
jQWidgets v8.1.2 (2019-June)
Copyright (c) 2011-2019 jQWidgets.
License: https://jqwidgets.com/license/
*/
/* eslint-disable */

(function(a){a.jqx.cssroundedcorners=function(b){var d={all:"jqx-rc-all",top:"jqx-rc-t",bottom:"jqx-rc-b",left:"jqx-rc-l",right:"jqx-rc-r","top-right":"jqx-rc-tr","top-left":"jqx-rc-tl","bottom-right":"jqx-rc-br","bottom-left":"jqx-rc-bl"};for(var c in d){if(!d.hasOwnProperty(c)){continue}if(b==c){return d[c]}}};a.jqx.jqxWidget("jqxButton","",{});a.extend(a.jqx._jqxButton.prototype,{defineInstance:function(){var b={type:"",cursor:"arrow",roundedCorners:"all",disabled:false,height:null,width:null,overrideTheme:false,enableHover:true,enableDefault:true,enablePressed:true,imgPosition:"center",imgSrc:"",imgWidth:16,imgHeight:16,value:null,textPosition:"",textImageRelation:"overlay",rtl:false,_ariaDisabled:false,_scrollAreaButton:false,template:"default",aria:{"aria-disabled":{name:"disabled",type:"boolean"}}};if(this===a.jqx._jqxButton.prototype){return b}a.extend(true,this,b);return b},_addImage:function(c){var g=this;if(g.element.nodeName.toLowerCase()=="input"||g.element.nodeName.toLowerCase()=="button"||g.element.nodeName.toLowerCase()=="div"){if(!g._img){g.field=g.element;if(g.field.className){g._className=g.field.className}var i={title:g.field.title};var j=null;if(g.field.getAttribute("value")){var j=g.field.getAttribute("value")}else{if(g.element.nodeName.toLowerCase()!="input"){var j=g.element.innerHTML}}if(g.value){j=g.value}if(g.field.id.length){i.id=g.field.id.replace(/[^\w]/g,"_")+"_"+c}else{i.id=a.jqx.utilities.createId()+"_"+c}var b=document.createElement("div");b.id=i.id;b.title=i.title;b.style.cssText=g.field.style.cssText;b.style.boxSizing="border-box";var f=document.createElement("img");f.setAttribute("src",g.imgSrc);f.setAttribute("width",g.imgWidth);f.setAttribute("height",g.imgHeight);b.appendChild(f);g._img=f;var l=document.createElement("span");if(j){l.innerHTML=j;g.value=j}b.appendChild(l);g._text=l;g.field.style.display="none";if(g.field.parentNode){g.field.parentNode.insertBefore(b,g.field.nextSibling)}var e=g.host.data();g.host=a(b);g.host.data(e);g.element=b;g.element.id=g.field.id;g.field.id=i.id;var k=new a(g.element);var h=new a(g.field);if(g._className){k.addClass(g._className);h.removeClass(g._className)}if(g.field.tabIndex){var d=g.field.tabIndex;g.field.tabIndex=-1;g.element.tabIndex=d}}else{g._img.setAttribute("src",g.imgSrc);g._img.setAttribute("width",g.imgWidth);g._img.setAttribute("height",g.imgHeight);g._text.innerHTML=g.value}if(!g.imgSrc){g._img.style.display="none"}else{g._img.style.display="inline"}if(!g.value){g._text.style.display="none"}else{g._text.style.display="inline"}g._positionTextAndImage()}},_positionTextAndImage:function(){var k=this;var r=k.element.offsetWidth;var q=k.element.offsetHeight;var m=k.imgWidth;var v=k.imgHeight;if(k.imgSrc==""){m=0;v=0}var f=k._text.offsetWidth;var b=k._text.offsetHeight;var i=4;var c=4;var l=4;var n=0;var u=0;switch(k.textImageRelation){case"imageBeforeText":case"textBeforeImage":n=m+f+2*l+i+2*c;u=Math.max(v,b)+2*l+i+2*c;break;case"imageAboveText":case"textAboveImage":n=Math.max(m,f)+2*l;u=v+b+i+2*l+2*c;break;case"overlay":n=Math.max(m,f)+2*l;u=Math.max(v,b)+2*l;break}if(!k.width){k.element.style.width=n+"px";r=n}if(!k.height){k.element.style.height=u+"px";q=u}k._img.style.position="absolute";k._text.style.position="absolute";k.element.style.position="relative";k.element.style.overflow="hidden";var e={};var z={};var s=function(E,D,G,C,F){if(D.width<C){D.width=C}if(D.height<F){D.height=F}switch(G){case"left":E.style.left=D.left+"px";E.style.top=D.top+D.height/2-F/2+"px";break;case"topLeft":E.style.left=D.left+"px";E.style.top=D.top+"px";break;case"bottomLeft":E.style.left=D.left+"px";E.style.top=D.top+D.height-F+"px";break;default:case"center":E.style.left=D.left+D.width/2-C/2+"px";E.style.top=D.top+D.height/2-F/2+"px";break;case"top":E.style.left=D.left+D.width/2-C/2+"px";E.style.top=D.top+"px";break;case"bottom":E.style.left=D.left+D.width/2-C/2+"px";E.style.top=D.top+D.height-F+"px";break;case"right":E.style.left=D.left+D.width-C+"px";E.style.top=D.top+D.height/2-F/2+"px";break;case"topRight":E.style.left=D.left+D.width-C+"px";E.style.top=D.top+"px";break;case"bottomRight":E.style.left=D.left+D.width-C+"px";E.style.top=D.top+D.height-F+"px";break}};var g=0;var p=0;var x=r;var j=q;var A=(x-g)/2;var y=(j-p)/2;var B=k._img;var o=k._text;var t=j-p;var d=x-g;g+=c;p+=c;x=x-c-2;d=d-2*c-2;t=t-2*c-2;switch(k.textImageRelation){case"imageBeforeText":switch(k.imgPosition){case"left":case"topLeft":case"bottomLeft":z={left:g,top:p,width:g+m,height:t};e={left:g+m+i,top:p,width:d-m-i,height:t};break;case"center":case"top":case"bottom":z={left:A-f/2-m/2-i/2,top:p,width:m,height:t};e={left:z.left+m+i,top:p,width:x-z.left-m-i,height:t};break;case"right":case"topRight":case"bottomRight":z={left:x-f-m-i,top:p,width:m,height:t};e={left:z.left+m+i,top:p,width:x-z.left-m-i,height:t};break}s(B,z,k.imgPosition,m,v);s(o,e,k.textPosition,f,b);break;case"textBeforeImage":switch(k.textPosition){case"left":case"topLeft":case"bottomLeft":e={left:g,top:p,width:g+f,height:t};z={left:g+f+i,top:p,width:d-f-i,height:t};break;case"center":case"top":case"bottom":e={left:A-f/2-m/2-i/2,top:p,width:f,height:t};z={left:e.left+f+i,top:p,width:x-e.left-f-i,height:t};break;case"right":case"topRight":case"bottomRight":e={left:x-f-m-i,top:p,width:f,height:t};z={left:e.left+f+i,top:p,width:x-e.left-f-i,height:t};break}s(B,z,k.imgPosition,m,v);s(o,e,k.textPosition,f,b);break;case"imageAboveText":switch(k.imgPosition){case"topRight":case"top":case"topLeft":z={left:g,top:p,width:d,height:v};e={left:g,top:p+v+i,width:d,height:t-v-i};break;case"left":case"center":case"right":z={left:g,top:y-v/2-b/2-i/2,width:d,height:v};e={left:g,top:z.top+i+v,width:d,height:t-z.top-i-v};break;case"bottomLeft":case"bottom":case"bottomRight":z={left:g,top:j-v-b-i,width:d,height:v};e={left:g,top:z.top+i+v,width:d,height:b};break}s(B,z,k.imgPosition,m,v);s(o,e,k.textPosition,f,b);break;case"textAboveImage":switch(k.textPosition){case"topRight":case"top":case"topLeft":e={left:g,top:p,width:d,height:b};z={left:g,top:p+b+i,width:d,height:t-b-i};break;case"left":case"center":case"right":e={left:g,top:y-v/2-b/2-i/2,width:d,height:b};z={left:g,top:e.top+i+b,width:d,height:t-e.top-i-b};break;case"bottomLeft":case"bottom":case"bottomRight":e={left:g,top:j-v-b-i,width:d,height:b};z={left:g,top:e.top+i+b,width:d,height:v};break}s(B,z,k.imgPosition,m,v);s(o,e,k.textPosition,f,b);break;case"overlay":default:e={left:g,top:p,width:d,height:t};z={left:g,top:p,width:d,height:t};s(B,z,k.imgPosition,m,v);s(o,e,k.textPosition,f,b);break}},createInstance:function(d){var e=this;e._setSize();var b=e.isMaterialized();e.buttonObj=new a(e.element);if(e.imgSrc!=""||e.textPosition!=""||(e.element.value&&e.element.value.indexOf("<")>=0)||e.value!=null){e.refresh();e._addImage("jqxButton");e.buttonObj=new a(e.element)}if(!e._ariaDisabled){e.element.setAttribute("role","button")}if(e.type!==""){e.element.setAttribute("type",e.type)}if(!e.overrideTheme){e.buttonObj.addClass(e.toThemeProperty(a.jqx.cssroundedcorners(e.roundedCorners)));if(e.enableDefault){e.buttonObj.addClass(e.toThemeProperty("jqx-button"))}e.buttonObj.addClass(e.toThemeProperty("jqx-widget"))}e.isTouchDevice=a.jqx.mobile.isTouchDevice();if(!e._ariaDisabled){a.jqx.aria(this)}if(e.cursor!="arrow"){if(!e.disabled){e.element.style.cursor=e.cursor}else{e.element.style.cursor="arrow"}}var g="mouseenter mouseleave mousedown focus blur";if(e._scrollAreaButton){var g="mousedown"}if(e.isTouchDevice){e.addHandler(e.host,a.jqx.mobile.getTouchEventName("touchstart"),function(h){e.isPressed=true;e.refresh()});e.addHandler(a(document),a.jqx.mobile.getTouchEventName("touchend")+"."+e.element.id,function(h){e.isPressed=false;e.refresh()})}e.addHandler(e.host,g,function(h){switch(h.type){case"mouseenter":if(!e.isTouchDevice){if(!e.disabled&&e.enableHover){e.isMouseOver=true;e.refresh()}}break;case"mouseleave":if(!e.isTouchDevice){if(!e.disabled&&e.enableHover){e.isMouseOver=false;e.refresh()}}break;case"mousedown":if(!e.disabled){e.isPressed=true;e.refresh()}break;case"focus":if(!e.disabled){e.isFocused=true;e.refresh()}break;case"blur":if(!e.disabled){e.isFocused=false;e.refresh()}break}});e.mouseupfunc=function(h){if(!e.disabled){if(e.isPressed||e.isMouseOver){e.isPressed=false;e.refresh()}}};e.addHandler(document,"mouseup.button"+e.element.id,e.mouseupfunc);try{if(document.referrer!=""||window.frameElement){if(window.top!=null&&window.top!=window.that){var f="";if(window.parent&&document.referrer){f=document.referrer}if(f.indexOf(document.location.host)!=-1){if(window.top.document){window.top.document.addEventListener("mouseup",e._topDocumentMouseupHandler)}}}}}catch(c){}e.propertyChangeMap.roundedCorners=function(h,j,i,k){h.buttonObj.removeClass(h.toThemeProperty(a.jqx.cssroundedcorners(i)));h.buttonObj.addClass(h.toThemeProperty(a.jqx.cssroundedcorners(k)))};e.propertyChangeMap.disabled=function(h,j,i,k){if(i!=k){h.refresh();h.element.setAttribute("disabled",k);h.element.disabled=k;if(!k){h.element.style.cursor=h.cursor}else{h.element.style.cursor="default"}a.jqx.aria(h,"aria-disabled",h.disabled)}};e.propertyChangeMap.rtl=function(h,j,i,k){if(i!=k){h.refresh()}};e.propertyChangeMap.template=function(h,j,i,k){if(i!=k){h.buttonObj.removeClass(h.toThemeProperty("jqx-"+i));h.refresh()}};e.propertyChangeMap.theme=function(h,j,i,k){h.buttonObj.removeClass(h.element);if(i){h.buttonObj.removeClass("jqx-button-"+i);h.buttonObj.removeClass("jqx-widget-"+i);h.buttonObj.removeClass("jqx-fill-state-normal-"+i);h.buttonObj.removeClass(h.toThemeProperty(a.jqx.cssroundedcorners(h.roundedCorners))+"-"+i)}if(h.enableDefault){h.buttonObj.addClass(h.toThemeProperty("jqx-button"))}h.buttonObj.addClass(h.toThemeProperty("jqx-widget"));if(!h.overrideTheme){h.buttonObj.addClass(h.toThemeProperty(a.jqx.cssroundedcorners(h.roundedCorners)))}h._oldCSSCurrent=null;h.refresh()};if(e.disabled){e.element.disabled=true;e.element.setAttribute("disabled","true")}},resize:function(c,b){this.width=c;this.height=b;this._setSize()},val:function(d){var c=this;var b=c.host.find("input");if(b.length>0){if(arguments.length==0||typeof(d)=="object"){return b.val()}b.val(d);c.refresh();return b.val()}if(arguments.length==0||typeof(d)=="object"){if(c.element.nodeName.toLowerCase()=="button"){return a(c.element).text()}return c.element.value}if(arguments.length>0&&c._text){c._text.innerHTML=arguments[0];c.refresh();return}else{if(arguments.length>0&&c.element.nodeName==="DIV"){c.element.innerHTML=arguments[0];c.refresh()}}c.element.value=arguments[0];if(c.element.nodeName.toLowerCase()=="button"){a(c.element).html(arguments[0])}c.refresh()},_setSize:function(){var d=this;var b=d.height;var c=d.width;if(b){if(!isNaN(b)){b=b+"px"}d.element.style.height=b}if(c){if(!isNaN(c)){c=c+"px"}d.element.style.width=c}},_removeHandlers:function(){var b=this;b.removeHandler(b.host,"selectstart");b.removeHandler(b.host,"click");b.removeHandler(b.host,"focus");b.removeHandler(b.host,"blur");b.removeHandler(b.host,"mouseenter");b.removeHandler(b.host,"mouseleave");b.removeHandler(b.host,"mousedown");b.removeHandler(a(document),"mouseup.button"+b.element.id,b.mouseupfunc);window.top.document.removeEventListener("mouseup",b._topDocumentMouseupHandler);if(b.isTouchDevice){b.removeHandler(b.host,a.jqx.mobile.getTouchEventName("touchstart"));b.removeHandler(a(document),a.jqx.mobile.getTouchEventName("touchend")+"."+b.element.id)}b.mouseupfunc=null;delete b.mouseupfunc},focus:function(){this.host.focus()},destroy:function(){var b=this;b._removeHandlers();var c=a.data(b.element,"jqxButton");if(c){delete c.instance}b.host.removeClass();b.host.removeData();b.host.remove();delete b.set;delete b.get;delete b.call;delete b.element;delete b.host},render:function(){this.refresh()},propertiesChangedHandler:function(d,b,c){if(c&&c.width&&c.height&&Object.keys(c).length==2){d._setSize();d.refresh()}},propertyChangedHandler:function(b,c,e,d){if(this.isInitialized==undefined||this.isInitialized==false){return}if(d==e){return}if(b.batchUpdate&&b.batchUpdate.width&&b.batchUpdate.height&&Object.keys(b.batchUpdate).length==2){return}if(c==="type"){b.element.setAttribute("type",d)}if(c=="textImageRelation"||c=="textPosition"||c=="imgPosition"){if(b._img){b._positionTextAndImage()}else{b._addImage("jqxButton")}}if(c=="imgSrc"||c=="imgWidth"||c=="imgHeight"){b._addImage("jqxButton")}if(c==="value"){b.val(d)}if(c=="width"||c=="height"){b._setSize();b.refresh()}},refresh:function(){var c=this;if(c.overrideTheme){return}var e=c.toThemeProperty("jqx-fill-state-focus");var i=c.toThemeProperty("jqx-fill-state-disabled");var b=c.toThemeProperty("jqx-fill-state-normal");if(!c.enableDefault){b=""}var h=c.toThemeProperty("jqx-fill-state-hover");var f=c.toThemeProperty("jqx-fill-state-pressed");var g=c.toThemeProperty("jqx-fill-state-pressed");if(!c.enablePressed){f=""}var d="";if(!c.host){return}c.element.disabled=c.disabled;if(c.disabled){if(c._oldCSSCurrent){c.buttonObj.removeClass(c._oldCSSCurrent)}d=b+" "+i;if(c.template!=="default"&&c.template!==""){d+=" jqx-"+c.template;if(c.theme!=""){d+=" jqx-"+c.template+"-"+c.theme}}c.buttonObj.addClass(d);c._oldCSSCurrent=d;return}else{if(c.isMouseOver&&!c.isTouchDevice){if(c.isPressed){d=g}else{d=h}}else{if(c.isPressed){d=f}else{d=b}}}if(c.isFocused){d+=" "+e}if(c.template!=="default"&&c.template!==""){d+=" jqx-"+c.template;if(c.theme!=""){d+=" jqx-"+c.template+"-"+c.theme}}if(d!=c._oldCSSCurrent){if(c._oldCSSCurrent){c.buttonObj.removeClass(c._oldCSSCurrent)}c.buttonObj.addClass(d);c._oldCSSCurrent=d}if(c.rtl){c.buttonObj.addClass(c.toThemeProperty("jqx-rtl"));c.element.style.direction="rtl"}if(c.isMaterialized()){c.host.addClass("buttonRipple")}}});a.jqx.jqxWidget("jqxLinkButton","",{});a.extend(a.jqx._jqxLinkButton.prototype,{defineInstance:function(){this.disabled=false;this.height=null;this.width=null;this.rtl=false;this.href=null},createInstance:function(c){var f=this;this.host.onselectstart=function(){return false};this.host.attr("role","button");var b=this.height||this.element.offsetHeight;var d=this.width||this.element.offsetWidth;this.href=this.element.getAttribute("href");this.target=this.element.getAttribute("target");this.content=this.host.text();this.element.innerHTML="";var g=document.createElement("input");g.type="button";g.className="jqx-wrapper "+this.toThemeProperty("jqx-reset");this._setSize(g,d,b);g.value=this.content;var e=new a(this.element);e.addClass(this.toThemeProperty("jqx-link"));this.element.style.color="inherit";this.element.appendChild(g);this._setSize(g,d,b);var h=c==undefined?{}:c[0]||{};a(g).jqxButton(h);this.wrapElement=g;if(this.disabled){this.element.disabled=true}this.propertyChangeMap.disabled=function(i,k,j,l){i.element.disabled=l;i.wrapElement.jqxButton({disabled:l})};this.addHandler(a(g),"click",function(i){if(!this.disabled){f.onclick(i)}return false})},_setSize:function(c,d,b){var e=this;if(b){if(!isNaN(b)){b=b+"px"}c.style.height=b}if(d){if(!isNaN(d)){d=d+"px"}c.style.width=d}},onclick:function(b){if(this.target!=null){window.open(this.href,this.target)}else{window.location=this.href}}});a.jqx.jqxWidget("jqxRepeatButton","jqxButton",{});a.extend(a.jqx._jqxRepeatButton.prototype,{defineInstance:function(){this.delay=50},createInstance:function(d){var e=this;var c=a.jqx.mobile.isTouchDevice();var b=!c?"mouseup."+this.base.element.id:"touchend."+this.base.element.id;var f=!c?"mousedown."+this.base.element.id:"touchstart."+this.base.element.id;this.addHandler(a(document),b,function(g){if(e.timeout!=null){clearTimeout(e.timeout);e.timeout=null;e.refresh()}if(e.timer!=undefined){clearInterval(e.timer);e.timer=null;e.refresh()}});this.addHandler(this.base.host,f,function(g){if(e.timer!=null){clearInterval(e.timer)}e.timeout=setTimeout(function(){clearInterval(e.timer);e.timer=setInterval(function(h){e.ontimer(h)},e.delay)},150)});this.mousemovefunc=function(g){if(!c){if(g.which==0){if(e.timer!=null){clearInterval(e.timer);e.timer=null}}}};this.addHandler(this.base.host,"mousemove",this.mousemovefunc)},destroy:function(){var c=a.jqx.mobile.isTouchDevice();var b=!c?"mouseup."+this.base.element.id:"touchend."+this.base.element.id;var e=!c?"mousedown."+this.base.element.id:"touchstart."+this.base.element.id;this.removeHandler(this.base.host,"mousemove",this.mousemovefunc);this.removeHandler(this.base.host,e);this.removeHandler(a(document),b);this.timer=null;delete this.mousemovefunc;delete this.timer;var d=a.data(this.base.element,"jqxRepeatButton");if(d){delete d.instance}a(this.base.element).removeData();this.base.destroy();delete this.base},stop:function(){clearInterval(this.timer);this.timer=null},ontimer:function(b){var b=new a.Event("click");if(this.base!=null&&this.base.host!=null){this.base.host.trigger(b)}}});a.jqx.jqxWidget("jqxToggleButton","jqxButton",{});a.extend(a.jqx._jqxToggleButton.prototype,{defineInstance:function(){this.toggled=false;this.uiToggle=true;this.aria={"aria-checked":{name:"toggled",type:"boolean"},"aria-disabled":{name:"disabled",type:"boolean"}}},createInstance:function(b){var c=this;c.base.overrideTheme=true;c.isTouchDevice=a.jqx.mobile.isTouchDevice();a.jqx.aria(this);c.propertyChangeMap.roundedCorners=function(d,f,e,g){d.base.buttonObj.removeClass(d.toThemeProperty(a.jqx.cssroundedcorners(e)));d.base.buttonObj.addClass(d.toThemeProperty(a.jqx.cssroundedcorners(g)))};c.propertyChangeMap.toggled=function(d,f,e,g){d.refresh()};c.propertyChangeMap.disabled=function(d,f,e,g){d.base.disabled=g;d.refresh()};c.addHandler(c.base.host,"click",function(d){if(!c.base.disabled&&c.uiToggle){c.toggle()}});if(!c.isTouchDevice){c.addHandler(c.base.host,"mouseenter",function(d){if(!c.base.disabled){c.refresh()}});c.addHandler(c.base.host,"mouseleave",function(d){if(!c.base.disabled){c.refresh()}})}c.addHandler(c.base.host,"mousedown",function(d){if(!c.base.disabled){c.refresh()}});c.addHandler(a(document),"mouseup.togglebutton"+c.base.element.id,function(d){if(!c.base.disabled){c.refresh()}})},destroy:function(){this._removeHandlers();this.base.destroy()},_removeHandlers:function(){this.removeHandler(this.base.host,"click");this.removeHandler(this.base.host,"mouseenter");this.removeHandler(this.base.host,"mouseleave");this.removeHandler(this.base.host,"mousedown");this.removeHandler(a(document),"mouseup.togglebutton"+this.base.element.id)},toggle:function(){this.toggled=!this.toggled;this.refresh();a.jqx.aria(this,"aria-checked",this.toggled)},unCheck:function(){this.toggled=false;this.refresh()},check:function(){this.toggled=true;this.refresh()},refresh:function(){var c=this;var h=c.base.toThemeProperty("jqx-fill-state-disabled");var b=c.base.toThemeProperty("jqx-fill-state-normal");if(!c.base.enableDefault){b=""}var g=c.base.toThemeProperty("jqx-fill-state-hover");var e=c.base.toThemeProperty("jqx-fill-state-pressed");var f=c.base.toThemeProperty("jqx-fill-state-pressed");var d="";c.base.element.disabled=c.base.disabled;if(c.base.disabled){d=b+" "+h;c.base.buttonObj.addClass(d);return}else{if(c.base.isMouseOver&&!c.isTouchDevice){if(c.base.isPressed||c.toggled){d=f}else{d=g}}else{if(c.base.isPressed||c.toggled){d=e}else{d=b}}}if(c.base.template!=="default"&&c.base.template!==""){d+=" jqx-"+c.base.template;if(c.base.theme!=""){d+=" jqx-"+c.template+"-"+c.base.theme}}if(c.base.buttonObj.hasClass(h)&&h!=d){c.base.buttonObj.removeClass(h)}if(c.base.buttonObj.hasClass(b)&&b!=d){c.base.buttonObj.removeClass(b)}if(c.base.buttonObj.hasClass(g)&&g!=d){c.base.buttonObj.removeClass(g)}if(c.base.buttonObj.hasClass(e)&&e!=d){c.base.buttonObj.removeClass(e)}if(c.base.buttonObj.hasClass(f)&&f!=d){c.base.buttonObj.removeClass(f)}if(!c.base.buttonObj.hasClass(d)){c.base.buttonObj.addClass(d)}},_topDocumentMouseupHandler:function(c){var b=this;b.isPressed=false;b.refresh()}})})(jqxBaseFramework);

