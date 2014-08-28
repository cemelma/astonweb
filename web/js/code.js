var over = 0;
function fadeEngine(x) {
     
     var total_divs = 5;

     var y=x;
     if (x==0){
     	$(".element img").attr("src","images/inaktiv.png"); 
  	$(".element11").attr("src","images/aktiv.png"); 
  	 $("#layer1 ul li:nth-child(5)").fadeOut(2500);
     } else if (x==1){
     	$(".element img").attr("src","images/inaktiv.png"); 
  	$(".element22").attr("src","images/aktiv.png"); 
     } else if (x==2){
     	$(".element img").attr("src","images/inaktiv.png"); 
  	$(".element33").attr("src","images/aktiv.png"); 
     } else if (x==3){
     	$(".element img").attr("src","images/inaktiv.png"); 
  	$(".element44").attr("src","images/aktiv.png"); 
     } else if (x==4){
     	$(".element img").attr("src","images/inaktiv.png"); 
  	$(".element55").attr("src","images/aktiv.png"); 
     }
     if ( over != 1 ) {
          if(x==total_divs) y=1; else y++;
          $("#layer1 ul li:nth-child("+x+")").fadeOut(2500);
          $("#slidercontent ul li:nth-child("+x+")").fadeOut(500);
          $("#layer1 ul li:nth-child("+y+")").fadeIn(2500); 
          $("#slidercontent ul li:nth-child("+y+")").fadeIn(500); 
     }
     if (x==4)
        y=0
       
     setTimeout("fadeEngine("+y+")",10000);
}


fadeEngine(0); 

// accending sort
function asc_sort(a, b){
        return ($(b).text()) < ($(a).text()) ? 1 : -1;    
    }

    // decending sort
    function dec_sort(a, b){
        return ($(b).text()) > ($(a).text()) ? 1 : -1;    
    }

$(document).ready(function() {

    $("#formbox1 .list option").sort(asc_sort).appendTo('#formbox1 .list');
    $("#formbox2 .list option").sort(asc_sort).appendTo('#formbox2 .list');
    $("#selectland option").sort(asc_sort).appendTo('#selectland');
    
    $(".contenttable tr:odd").addClass("odd");

   
    $("#sorttable").tablesorter({
     headers: { 
       2:{sorter: 'digit'},
       3:{sorter: 'digit'},
       4:{sorter: 'digit'},
       5:{sorter: 'digit'}
     }
  }); 
   
   
 var usergroup = $("#usergroup").text();
 
 if (usergroup == 1){
 $("#buttonform").show();
 $("#infotextform").hide();
 }
 else {
    $("#buttonform").hide();
$("input[type=checkbox]").click(function(){	
var checked1 = $("#appi1 input[type=checkbox]:checked").length;
var checked2 = $("#appi2 input[type=checkbox]:checked").length;
var checked3 = $("#appi3 input[type=checkbox]:checked").length;
var checked4 = $("#inst input[type=checkbox]:checked").length;
   if (checked1 >= 4) {
	$("#buttonform").hide();
   } 
   else if (checked2 >= 3) {
	   $("#buttonform").hide();
   }
   else if (checked3 >= 2) {
   	$("#buttonform").hide();
   }
   else if (checked4 >= 3) {
   	$("#buttonform").hide();
   }
   else if (checked1 == 0 && 
            checked2 == 0 && 
            checked3 == 0 &&
            checked4 == 0) {
	$("#buttonform").hide();
   } 
   else {
   	$("#buttonform").show();
   }
});
}

if ($("#appi1 input").attr("checked")) {
$("#buttonform").show();
}
if ($("#appi2 input").attr("checked")) {
$("#buttonform").show();
}
if ($("#appi3 input").attr("checked")) {
$("#buttonform").show();
}
if ($("#inst input").attr("checked")) {
$("#buttonform").show();
}


$("#idsuche").keyup(function(){
    if($(this).val().length >= 1){
        $("#buttonform").show();
    } else {
        $("#buttonform").hide();
    }
});

$(".headersliderbox").click(function(){	
		$(this).next().slideToggle('slow');
	});


	$("a[rel^='prettyPhoto']").prettyPhoto({animationSpeed:'fast',slideshow:10000, theme:'dark_rounded'});

$("#layer1 ul li:nth-child(1)").show();
$("#slidercontent ul li:nth-child(1)").show();

$('.element11').click(function() {
  $("#layer1 ul li").hide();
  $("#layer1 ul li:nth-child(1)").show();
  $("#slidercontent ul li").hide();
  $("#slidercontent ul li:nth-child(1)").show();
  
  $(".element img").attr("src","/fileadmin/templates/images/inaktiv.png"); 
  $(this).attr("src","/fileadmin/templates/images/aktiv.png"); 
  over = 1;
});
$('.element22').click(function() {
  $("#layer1 ul li").hide();
  $("#layer1 ul li:nth-child(2)").show();
  $("#slidercontent ul li").hide();
  $("#slidercontent ul li:nth-child(2)").show();
  
  $(".element img").attr("src","/fileadmin/templates/images/inaktiv.png"); 
  $(this).attr("src","/fileadmin/templates/images/aktiv.png"); 
  over = 1;
});
$('.element33').click(function() {
  $("#layer1 ul li").hide();
  $("#layer1 ul li:nth-child(3)").show();
  $("#slidercontent ul li").hide();
  $("#slidercontent ul li:nth-child(3)").show();
  
  $(".element img").attr("src","/fileadmin/templates/images/inaktiv.png"); 
  $(this).attr("src","/fileadmin/templates/images/aktiv.png"); 
  over = 1;
});
$('.element44').click(function() {
  $("#layer1 ul li").hide();
  $("#layer1 ul li:nth-child(4)").show();
  $("#slidercontent ul li").hide();
  $("#slidercontent ul li:nth-child(4)").show();
  
  $(".element img").attr("src","/fileadmin/templates/images/inaktiv.png"); 
  $(this).attr("src","/fileadmin/templates/images/aktiv.png"); 
  over = 1;
});
$('.element55').click(function() {
  $("#layer1 ul li").hide();
  $("#layer1 ul li:nth-child(5)").show();
  $("#slidercontent ul li").hide();
  $("#slidercontent ul li:nth-child(5)").show();
  
  $(".element img").attr("src","/fileadmin/templates/images/inaktiv.png"); 
  $(this).attr("src","/fileadmin/templates/images/aktiv.png"); 
  over = 1;
});

$("#slidercontent").hover(
function () { over = 1; },
function () { over = 0; }    );


$("#slider").easySlider({
	auto: true, 
	continuous: true,
	prevId: 		'prev',
	prevText: 	'<<',
	nextId: 		'next',	
	nextText: 	'>>',
	speed: 		 1000,
	pause:		 8000
});

$("#commentForm").validate();

 $("ul.dropdown li").hover(function(){
   $(this).addClass("hover");
   $('> .dir',this).addClass("open");
   $('ul:first',this).css('visibility', 'visible');
 },function(){
   $(this).removeClass("hover");
   $('.open',this).removeClass("open");
   $('ul:first',this).css('visibility', 'hidden');
 });
 

});// JavaScript Document