import{u as Y,o as n,b as T,w as I,a as e,t as p,d as m,T as E,r as b,e as F,f as N,g as z,h as H,c as i,n as L,F as _,i as V,j as h,k as R,l as s,v as d,m as X,p as O,q as $,s as M,x,C as D}from"./index.72139b06.js";const A={class:"bg-white h-12 px-4 flex items-center justify-between shadow-sm"},G={class:"flex gap-4"},q={class:"text-lg text-charcoal75"},W={__name:"EditorSubHeader",setup(l){const r=Y(),t=()=>{r.saveChanges().then(o=>{console.log(o)})};return(o,c)=>(n(),T(E,{"enter-active-class":"duration-300 ease-out","enter-from-class":"translate-y-[50px]"},{default:I(()=>[e("div",A,[e("div",G,[e("p",q,p(m(r).layout.name)+" - ",1),e("button",{class:"btn-primary text-xs",onClick:t},"Save layout"),e("button",{class:"btn-secondary text-xs",onClick:c[0]||(c[0]=(...u)=>o.browseVariables&&o.browseVariables(...u))},"Browse variables")]),e("p",null,p(m(r).selectedProduct?m(r).selectedProduct.title:"No product data loaded"),1)])]),_:1}))}},J={Base64ToImage(l,r){var t=new Image;t.onload=function(){r(t)},t.src=l},downloadFromBase64(l,r){let t=document.createElement("a");t.href=l,t.download=`${r}.png`,document.body.appendChild(t),t.click(),document.body.removeChild(t)}};const K={class:""},Q=e("h3",null,"preview",-1),Z={class:"relative max-h-[700px] flex"},ee={__name:"EditorPreview",props:{layoutTemplate:Object,selectedProduct:Object,base64ImgString:String},setup(l){const r=l,t=b(),o=b(!1);b(!1);const c=F(()=>t.value?t.value.getBoundingClientRect():null),u=F(()=>c.value?c.value.width/r.layoutTemplate.width:1);N(()=>r.base64ImgString,()=>S(r.base64ImgString));const k=b(),S=a=>{J.Base64ToImage(a,function(f){k.value.innerHTML="",k.value.appendChild(f)})},C=a=>{let f={top:`${a.posY*u.value}px`,left:`${a.posX*u.value}px`,color:a.textColor,fontFamily:`"${a.fontFamily}"`,fontSize:`${a.textSize*u.value}px`};if(a.hasBackground){let w={padding:`${a.inflateY*u.value}px ${a.inflateX*u.value}px`,borderRadius:`${a.borderRadius*u.value}px`};a.backgroundStyle==="Fill"&&(w.backgroundColor=a.backgroundColor),f={...f,...w},console.log(f)}return f},U=a=>({top:`${a.posY*u.value}px`,left:`${a.posX*u.value}px`,width:`${a.width*u.value}px`,height:`${a.height*u.value}px`,objectFit:`${a.objectFit}`,borderColor:"#000",borderWidth:"1px",borderStyle:"dashed"}),g=a=>({top:`${a.posY*u.value}px`,left:`${a.posX*u.value}px`,width:`${a.width*u.value}px`,height:`${a.height*u.value}px`,background:a.backgroundColor,border:a.backgroundStyle=="Stroke"?a.backgroundColor:"none",borderRadius:a.borderRadius}),y=()=>{o.value=!1,R(()=>{o.value=!0})};return z(()=>{o.value=!0,window.addEventListener("resize",y)}),H(()=>{window.removeEventListener("resize",y)}),(a,f)=>{var w,j;return n(),i("div",K,[Q,e("div",Z,[o.value?(n(),i("div",{key:0,id:"templateCanvas",ref_key:"canvas",ref:t,style:L({aspectRatio:l.layoutTemplate.width/l.layoutTemplate.height,width:l.layoutTemplate.width+"px"}),class:"border border-charcoal border-dashed overflow-hidden relative"},[e("div",{ref_key:"previewimg",ref:k,style:L({width:((w=m(c))==null?void 0:w.width)+"px",height:((j=m(c))==null?void 0:j.height)+"px"}),class:"absolute z-0"},null,4),(n(!0),i(_,null,V(l.layoutTemplate.layers,(v,P)=>(n(),i("div",{key:P,class:""},[v.layerType=="TextLayer"?(n(),i("div",{key:0,class:"absolute z-0",style:L(C(v))},p(v.text),5)):h("",!0),v.layerType=="ImageLayer"?(n(),i("div",{key:1,class:"absolute z-0",style:L(U(v))},null,4)):h("",!0),v.layerType=="ShapeLayer"?(n(),i("div",{key:2,class:"absolute z-0",style:L(g(v))},null,4)):h("",!0)]))),128))],4)):h("",!0)])])}}},B=["Roboto","Arial","Times new roman"],te={class:"flex flex-col gap-2"},le={class:"flex flex-col gap-2 border-b border-charcoal50 pb-2 mb-2"},oe={class:"form-group"},ae=e("label",{for:"text"},"Text ",-1),se={class:"form-group"},ne=e("label",{for:"textSize"},"Font family",-1),re=["value"],de={class:"form-group"},ie=e("label",{class:"cursor-pointer",for:"textColor"},"Text color ",-1),ue={class:"flex flex-row items-center justify-between gap-2"},ce={class:"form-group-y"},ye=e("label",{for:"textSize"},"Text size",-1),me={class:"form-group-y"},be=e("label",{for:"posX"},"Position X",-1),fe={class:"form-group-y"},he=e("label",{for:"posY"},"Position Y",-1),ge={class:"flex flex-col gap-2"},pe={class:"flex flex-col justify-start"},ve=e("label",{class:"text-charcoal",for:"hasBackground"},"Has background",-1),xe={class:"form-group-y"},$e=e("label",{for:"inflateX"},"Padding X",-1),ke={class:"form-group-y"},we=e("label",{for:"inflateY"},"Padding Y",-1),Le={class:"form-group-y"},Se=e("label",{for:"borderRadius"},"Border radius",-1),Ce={class:"form-group"},Te=e("label",{class:"cursor-pointer",for:"textColor"},"Background color ",-1),_e={class:"flex flex-row items-center justify-between gap-2"},Ve={class:"flex gap-4"},Ue={class:"form-group"},Ie=e("label",{class:"text-charcoal",for:"fill"},"Fill",-1),Fe={class:"form-group"},Xe=e("label",{class:"text-charcoal",for:"stroke"},"Stroke",-1),Ye={__name:"TextLayerEditor",props:{selectedLayerIndex:Number,layer:Object},setup(l){return(r,t)=>(n(),i("div",te,[e("div",le,[e("div",oe,[ae,s(e("input",{type:"text",name:"text","onUpdate:modelValue":t[0]||(t[0]=o=>l.layer.text=o)},null,512),[[d,l.layer.text]])]),e("div",se,[ne,m(B)?s((n(),i("select",{key:0,name:"fontFamily","onUpdate:modelValue":t[1]||(t[1]=o=>l.layer.fontFamily=o),class:"p-2 border rounded-md border-charcoal50 hover:border-charcoal focus:border-charcoal focus:outline-none text-sm"},[(n(!0),i(_,null,V(m(B),(o,c)=>(n(),i("option",{key:c,value:o},p(o),9,re))),128))],512)),[[X,l.layer.fontFamily]]):h("",!0)]),e("div",de,[ie,e("div",ue,[s(e("input",{class:"cursor-pointer",type:"color",name:"textColor","onUpdate:modelValue":t[2]||(t[2]=o=>l.layer.textColor=o)},null,512),[[d,l.layer.textColor]]),s(e("input",{class:"flex-1",type:"text",name:"textColor","onUpdate:modelValue":t[3]||(t[3]=o=>l.layer.textColor=o)},null,512),[[d,l.layer.textColor]])])]),e("div",ce,[ye,s(e("input",{type:"number",name:"textSize","onUpdate:modelValue":t[4]||(t[4]=o=>l.layer.textSize=o)},null,512),[[d,l.layer.textSize]])]),e("div",me,[be,s(e("input",{type:"number",name:"posX","onUpdate:modelValue":t[5]||(t[5]=o=>l.layer.posX=o)},null,512),[[d,l.layer.posX]])]),e("div",fe,[he,s(e("input",{type:"number",name:"posY","onUpdate:modelValue":t[6]||(t[6]=o=>l.layer.posY=o)},null,512),[[d,l.layer.posY]])])]),e("div",ge,[e("div",pe,[ve,s(e("input",{class:"self-start",name:"hasBackground",type:"checkbox","onUpdate:modelValue":t[7]||(t[7]=o=>l.layer.hasBackground=o)},null,512),[[O,l.layer.hasBackground]])]),e("div",xe,[$e,s(e("input",{type:"number",name:"inflateX","onUpdate:modelValue":t[8]||(t[8]=o=>l.layer.inflateX=o)},null,512),[[d,l.layer.inflateX]])]),e("div",ke,[we,s(e("input",{type:"number",name:"inflateY","onUpdate:modelValue":t[9]||(t[9]=o=>l.layer.inflateY=o)},null,512),[[d,l.layer.inflateY]])]),e("div",Le,[Se,s(e("input",{type:"number",name:"borderRadius","onUpdate:modelValue":t[10]||(t[10]=o=>l.layer.borderRadius=o)},null,512),[[d,l.layer.borderRadius]])]),e("div",Ce,[Te,e("div",_e,[s(e("input",{class:"cursor-pointer",type:"color",name:"textColor","onUpdate:modelValue":t[11]||(t[11]=o=>l.layer.backgroundColor=o)},null,512),[[d,l.layer.backgroundColor]]),s(e("input",{class:"flex-1",type:"text",name:"textColor","onUpdate:modelValue":t[12]||(t[12]=o=>l.layer.backgroundColor=o)},null,512),[[d,l.layer.backgroundColor]])])]),e("div",Ve,[e("div",Ue,[Ie,s(e("input",{class:"self-start mt-1",type:"radio",id:"fill",name:"backgroundStyle",value:"Fill","onUpdate:modelValue":t[13]||(t[13]=o=>l.layer.backgroundStyle=o)},null,512),[[$,l.layer.backgroundStyle]])]),e("div",Fe,[Xe,s(e("input",{class:"self-start mt-1",type:"radio",id:"stroke",name:"backgroundStyle",value:"Stroke","onUpdate:modelValue":t[14]||(t[14]=o=>l.layer.backgroundStyle=o)},null,512),[[$,l.layer.backgroundStyle]])])])])]))}},je={class:"flex flex-col gap-2"},Be={class:"flex flex-col gap-2 border-b border-charcoal50 pb-2 mb-2"},ze={class:"form-group"},Re=e("label",{for:"source"},"Source (Variable)",-1),Pe={class:"form-group-y"},Ee=e("label",{for:"width"},"width",-1),Ne={class:"form-group-y"},He=e("label",{for:"height"},"height",-1),Oe={class:"form-group-y"},Me=e("label",{for:"posX"},"Position X",-1),De={class:"form-group-y"},Ae=e("label",{for:"posY"},"Position Y",-1),Ge={class:"flex gap-4"},qe={class:"form-group"},We=e("label",{class:"text-charcoal",for:"fit"},"Fit",-1),Je={class:"form-group"},Ke=e("label",{class:"text-charcoal",for:"cover"},"Cover",-1),Qe={class:"form-group"},Ze=e("label",{for:"alignHorizontal"},"Horizontal alignment",-1),et=e("option",{value:"Left"},"Left",-1),tt=e("option",{value:"Center"},"Center",-1),lt=e("option",{value:"Right"},"Right",-1),ot=[et,tt,lt],at={class:"form-group"},st=e("label",{for:"alignVertical"},"Vertical alignment",-1),nt=e("option",{value:"Top"},"Top",-1),rt=e("option",{value:"Center"},"Center",-1),dt=e("option",{value:"Bottom"},"Bottom",-1),it=[nt,rt,dt],ut={__name:"ImageLayerEditor",props:{selectedLayerIndex:Number,layer:Object},setup(l){return(r,t)=>(n(),i("div",je,[e("div",Be,[e("div",ze,[Re,s(e("input",{type:"text",name:"source","onUpdate:modelValue":t[0]||(t[0]=o=>l.layer.source=o)},null,512),[[d,l.layer.source]])]),e("div",Pe,[Ee,s(e("input",{type:"number",name:"width","onUpdate:modelValue":t[1]||(t[1]=o=>l.layer.width=o)},null,512),[[d,l.layer.width]])]),e("div",Ne,[He,s(e("input",{type:"number",name:"height","onUpdate:modelValue":t[2]||(t[2]=o=>l.layer.height=o)},null,512),[[d,l.layer.height]])]),e("div",Oe,[Me,s(e("input",{type:"number",name:"posX","onUpdate:modelValue":t[3]||(t[3]=o=>l.layer.posX=o)},null,512),[[d,l.layer.posX]])]),e("div",De,[Ae,s(e("input",{type:"number",name:"posY","onUpdate:modelValue":t[4]||(t[4]=o=>l.layer.posY=o)},null,512),[[d,l.layer.posY]])]),e("div",Ge,[e("div",qe,[We,s(e("input",{class:"self-start mt-1",type:"radio",id:"fit",name:"imageFit",value:"Fit","onUpdate:modelValue":t[5]||(t[5]=o=>l.layer.objectFit=o)},null,512),[[$,l.layer.objectFit]])]),e("div",Je,[Ke,s(e("input",{class:"self-start mt-1",type:"radio",id:"cover",name:"imageFit",value:"Cover","onUpdate:modelValue":t[6]||(t[6]=o=>l.layer.objectFit=o)},null,512),[[$,l.layer.objectFit]])])]),e("div",Qe,[Ze,s(e("select",{name:"alignHorizontal","onUpdate:modelValue":t[7]||(t[7]=o=>l.layer.alignHorizontal=o),class:"p-2 border rounded-md border-charcoal50 hover:border-charcoal focus:border-charcoal focus:outline-none text-sm"},ot,512),[[X,l.layer.alignHorizontal]])]),e("div",at,[st,s(e("select",{name:"alignVertical","onUpdate:modelValue":t[8]||(t[8]=o=>l.layer.alignVertical=o),class:"p-2 border rounded-md border-charcoal50 hover:border-charcoal focus:border-charcoal focus:outline-none text-sm"},it,512),[[X,l.layer.alignVertical]])])])]))}},ct={class:"flex flex-col gap-2"},yt={class:"flex flex-col gap-2 border-b border-charcoal50 pb-2 mb-2"},mt={class:"form-group-y"},bt=e("label",{for:"width"},"width",-1),ft={class:"form-group-y"},ht=e("label",{for:"height"},"height",-1),gt={class:"form-group-y"},pt=e("label",{for:"posX"},"Position X",-1),vt={class:"form-group-y"},xt=e("label",{for:"posY"},"Position Y",-1),$t={class:"form-group-y"},kt=e("label",{for:"borderRadius"},"Border radius",-1),wt={class:"form-group"},Lt=e("label",{class:"cursor-pointer",for:"textColor"},"Background color ",-1),St={class:"flex flex-row items-center justify-between gap-2"},Ct={class:"flex gap-4"},Tt={class:"form-group"},_t=e("label",{class:"text-charcoal",for:"fill"},"Fill",-1),Vt={class:"form-group"},Ut=e("label",{class:"text-charcoal",for:"stroke"},"Stroke",-1),It={__name:"ShapeLayerEditor",props:{selectedLayerIndex:Number,layer:Object},setup(l){return(r,t)=>(n(),i("div",ct,[e("div",yt,[e("div",mt,[bt,s(e("input",{type:"number",name:"width","onUpdate:modelValue":t[0]||(t[0]=o=>l.layer.width=o)},null,512),[[d,l.layer.width]])]),e("div",ft,[ht,s(e("input",{type:"number",name:"height","onUpdate:modelValue":t[1]||(t[1]=o=>l.layer.height=o)},null,512),[[d,l.layer.height]])]),e("div",gt,[pt,s(e("input",{type:"number",name:"posX","onUpdate:modelValue":t[2]||(t[2]=o=>l.layer.posX=o)},null,512),[[d,l.layer.posX]])]),e("div",vt,[xt,s(e("input",{type:"number",name:"posY","onUpdate:modelValue":t[3]||(t[3]=o=>l.layer.posY=o)},null,512),[[d,l.layer.posY]])])]),e("div",$t,[kt,s(e("input",{type:"number",name:"borderRadius","onUpdate:modelValue":t[4]||(t[4]=o=>l.layer.borderRadius=o)},null,512),[[d,l.layer.borderRadius]])]),e("div",wt,[Lt,e("div",St,[s(e("input",{class:"cursor-pointer",type:"color",name:"textColor","onUpdate:modelValue":t[5]||(t[5]=o=>l.layer.backgroundColor=o)},null,512),[[d,l.layer.backgroundColor]]),s(e("input",{class:"flex-1",type:"text",name:"textColor","onUpdate:modelValue":t[6]||(t[6]=o=>l.layer.backgroundColor=o)},null,512),[[d,l.layer.backgroundColor]])])]),e("div",Ct,[e("div",Tt,[_t,s(e("input",{class:"self-start mt-1",type:"radio",id:"fill",name:"backgroundStyle",value:"Fill","onUpdate:modelValue":t[7]||(t[7]=o=>l.layer.backgroundStyle=o)},null,512),[[$,l.layer.backgroundStyle]])]),e("div",Vt,[Ut,s(e("input",{class:"self-start mt-1",type:"radio",id:"stroke",name:"backgroundStyle",value:"Stroke","onUpdate:modelValue":t[8]||(t[8]=o=>l.layer.backgroundStyle=o)},null,512),[[$,l.layer.backgroundStyle]])])])]))}},Ft={key:0},Xt={class:"bg-white p-2 rounded shadow"},Yt={key:1,class:"bg-white p-2 rounded shadow"},jt=e("p",null,"Select a layer",-1),Bt=[jt],zt={__name:"EditorLayerEditor",props:{selectedLayerIndex:Number,layer:Object},setup(l){const r=l;return z(()=>{}),(t,o)=>(n(),i("div",null,[e("h3",null,"Layer "+p(r.selectedLayerIndex!=null?r.selectedLayerIndex+1:": None selected"),1),l.layer?(n(),i("div",Ft,[e("div",Xt,[l.layer.layerType=="TextLayer"?(n(),T(Ye,{key:0,layer:l.layer,class:"flex flex-col gap-2"},null,8,["layer"])):h("",!0),l.layer.layerType=="ImageLayer"?(n(),T(ut,{key:1,layer:l.layer,class:"flex flex-col gap-2"},null,8,["layer"])):h("",!0),l.layer.layerType=="ShapeLayer"?(n(),T(It,{key:2,layer:l.layer,class:"flex flex-col gap-2"},null,8,["layer"])):h("",!0)])])):(n(),i("div",Yt,Bt))]))}},Rt=e("h3",null,"Layers",-1),Pt={class:"pb-2"},Et=["onClick"],Nt=["onClick"],Ht=e("span",{class:"material-symbols-outlined text-charcoal50 hover:text-flame"}," delete ",-1),Ot=[Ht],Mt=e("div",{class:"text-left p-2 rounded shadow w-full text-charcoal bg-white hover:bg-alabaster font-bold"}," + Layer ",-1),Dt={class:"flex flex-col justify-start"},At=["onClick"],Gt={__name:"EditorLayers",props:{layoutTemplate:Object,selectedLayerIndex:Number},setup(l){return(r,t)=>(n(),i("div",null,[Rt,e("div",Pt,[(n(!0),i(_,null,V(l.layoutTemplate.layers,(o,c)=>(n(),i("div",{key:c,class:M(["bg-white p-2 mb-2 rounded shadow cursor-pointer flex flex-row justify-between",{"selected-layer":l.selectedLayerIndex==c}]),onClick:u=>r.$emit("selectLayer",c)},[e("div",null,[e("span",null,"Layer "+p(c+1)+" - ",1),e("span",null,p(o.layerType),1)]),e("button",{onClick:u=>r.$emit("removeLayer",c),class:"flex items-center"},Ot,8,Nt)],10,Et))),128)),x(D,{title:"addLayer"},{toggle:I(()=>[Mt]),default:I(()=>[e("div",Dt,[(n(),i(_,null,V(["ImageLayer","TextLayer","ShapeLayer"],(o,c)=>e("button",{key:c,onClick:u=>r.$emit("newLayer",o),class:"text-left p-2 hover:bg-alabaster"},p(r.getPrettyName(o)),9,At)),64))])]),_:1})])]))}},qt=e("h3",null,"Layout dimensions",-1),Wt={class:"bg-white p-2 rounded shadow"},Jt={class:"form-group"},Kt=e("label",{for:"height"},"Layout height",-1),Qt={class:"form-group"},Zt=e("label",{for:"height"},"Layout width",-1),el={__name:"EditorBaseEditor",props:{layout:Object},setup(l){return Y(),(r,t)=>(n(),i("div",null,[qt,e("div",Wt,[e("div",Jt,[Kt,s(e("input",{type:"number",name:"height","onUpdate:modelValue":t[0]||(t[0]=o=>l.layout.height=o)},null,512),[[d,l.layout.height]])]),e("div",Qt,[Zt,s(e("input",{type:"number",name:"height","onUpdate:modelValue":t[1]||(t[1]=o=>l.layout.width=o)},null,512),[[d,l.layout.width]])])])]))}},tl={key:0,class:"grid grid-cols-8 gap-2 max-w-screen-2xl mx-auto mt-8 p-4"},ll={class:"col-span-2 flex flex-col"},al={__name:"EditorView",setup(l){b("");const r=b(),t=Y(),o=b(null),c=F(()=>o.value!=null),u=g=>{o.value=0,t.$patch(y=>{y.layoutTemplate.layers.splice(g,1)})},k=g=>{t.$patch(y=>{y.layoutTemplate.layers.push(y.layoutTemplate.createLayertype({layerType:g}))})};b(!1),b("");const S=b(""),C=b(!1),U=()=>{t.testGenerateTemp(t.layoutTemplate.export()).then(g=>{S.value=`data:image/png;base64,${g}`,R(()=>C.value=!0)})};return(g,y)=>(n(),i("div",null,[x(W),m(t).layoutTemplate?(n(),i("main",tl,[x(ee,{base64ImgString:S.value,base64StringLoaded:C.value,layoutTemplate:m(t).layoutTemplate,selectedProduct:m(t).selectedProduct,class:"col-span-4"},null,8,["base64ImgString","base64StringLoaded","layoutTemplate","selectedProduct"]),x(zt,{layer:m(c)?m(t).layoutTemplate.layers[o.value]:null,selectedLayerIndex:o.value,class:"col-span-2"},null,8,["layer","selectedLayerIndex"]),e("div",ll,[x(el,{layout:m(t).layoutTemplate},null,8,["layout"]),x(Gt,{layoutTemplate:m(t).layoutTemplate,selectedLayerIndex:o.value,onSelectLayer:y[0]||(y[0]=a=>{o.value=a}),onNewLayer:y[1]||(y[1]=a=>k(a)),onRemoveLayer:y[2]||(y[2]=a=>{u(a)})},null,8,["layoutTemplate","selectedLayerIndex"])])])):h("",!0),e("button",{onClick:y[3]||(y[3]=a=>U())},"Click to test"),e("div",{ref_key:"main",ref:r},null,512)]))}};export{al as default};