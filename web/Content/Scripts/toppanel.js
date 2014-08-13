(function ($) {
    
    $.fn.extend({
        // --- -- - GOOGLE MAP PLUGIN - -- --- //
        iaosb_Map: function (myLocations) {
            if ($(this).length) {
                $.iaosb_Map_Maps = $(this);
                $.iaosb_Map_Locations = myLocations;
                jQuery.fn.iaosb_Map_CallBack();
            };
        },
        iaosb_Map_CallBack: function (myLoc) {
            $.iaosb_Map_Maps.each(function () {
                var mapName = $(this).attr('class');
                map = $(this),
				mapName = new google.maps.Map(map.get(0), { zoom: map.data("zoom") || 10, zoomControlOptions: { style: google.maps.ZoomControlStyle.SMALL }, panControl: false, streetViewControl: false, center: new google.maps.LatLng(39.674064, 27.935615), html: "DENEYSAN", popup: true, mapTypeId: google.maps.MapTypeId.HYBRID });

                locations = $.iaosb_Map_Locations || [
	                   ["<center style='padding:5px'><strong>DENEYSAN</strong><br />DENEYSAN EĞİTİM CİHAZLARI SAN. VE TİC. LTD. ŞTİ.<br /><img src=/Content/Images/Front/oncephe.jpg></center>", 39.674064, 27.935615, "DENEYSAN", "/Content/images/front/map-web.png"]
                    //,["<strong>İAOSB</strong><br />İzmir Atatürk Organize Sanayi Bölgesi<br />Atık Su Arıtma Tesisi", 38.481814, 27.031345, "Atık Su Arıtma Tesisi", "/Media/images/map-marker-3.png"]
                    //,["<strong>İAOSB</strong><br />İzmir Atatürk Organize Sanayi Bölgesi<br />ATAER Enerji", 38.478891, 27.032418, "ATAER Enerji", "/Media/images/map-marker-2.png"]
                    //,["<strong>İAOSB</strong><br />İzmir Atatürk Organize Sanayi Bölgesi<br />Sürekli Sergi Alanı", 38.498743, 27.043791, "Sürekli Sergi Alanı", "/Media/images/map-marker-4.png"]
				];
                var infowindow = new google.maps.InfoWindow();
                for (i = 0; i < locations.length; i++) {
                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(
							locations[i][1],
							locations[i][2]
						),
                        map: mapName,
                        title: locations[i][3],
                        icon: locations[i][4]
                    });
                    google.maps.event.addListener(marker, 'mouseover', (function (marker, i) { return function () { infowindow.setContent(locations[i][0]); infowindow.open(mapName, marker); } })(marker, i));
                }
            });
        }
    });
})(jQuery);

var optScript={};
jQuery(document).ready(function ($) {
    optScript = { initSite: function () {
        this.topPanel();
        this.loginPanel();
        $(".googleMap").iaosb_Map();


        $("#loginPanel").css({ marginTop: -110 });
    },
        topPanel: function () {
            var topPanel = $("#mapPanel"), panelHandle = $(".mapPanelHandle"), height = 0, wrapHeight = 0, imgs = topPanel.find("img"), imgsNum = imgs.length, readyImg = 0;
            if (imgsNum) { imgs.each(function () { $(this).load(function () { readyImg++; inittopPanel(); }); }); } else { inittopPanel(); }
            function inittopPanel() {
                if (imgsNum === readyImg) {
                    height = topPanel.height(); topPanel.css({ display: "block" }); wrapHeight = topPanel.find(".wrap").height();
                    topPanel.css({ marginTop: -height, height: height, display: "block", overflow: "visible" });
                    topPanel.find(".wrap").css({ height: wrapHeight, display: "block", overflow: "visible" });
                    panelHandle.click(function () { if (!$(this).hasClass("close")) { $("#loginPanel").hide(); $("#lang").hide(); topPanel.animate({ marginTop: 0 }, 350, function () { panelHandle.addClass("close") }); } else { topPanel.animate({ marginTop: -height }, 350, function () { panelHandle.removeClass("close"); $("#loginPanel").show(); $('#lang').show(); }); } });
                    $('a[href="#mapPanel"]').click(function () { $('html, body').animate({ scrollTop: 0 }, 300); panelHandle.trigger("click"); return false; });
                }
            }
        },
        loginPanel: function () {
            var loginPanel = $("#loginPanel"), loginpanelHandle = $("#loginPanelHandle"), height = 0, wrapHeight = 0, imgs = loginPanel.find("img"), imgsNum = imgs.length, readyImg = 0;
            if (imgsNum) { imgs.each(function () { $(this).load(function () { readyImg++; initloginPanel(); }); }); initloginPanel(); } else { initloginPanel(); }
            function initloginPanel() {
                if (imgsNum === readyImg) {
                    height = loginPanel.height(); loginPanel.css({ display: "block" }); wrapHeight = loginPanel.find(".wrap").height();
                    loginPanel.css({ marginTop: -height, height: 110, display: "block", overflow: "visible" });
                }
            }
            loginpanelHandle.click(function () { if (!$(this).hasClass("close")) { loginPanel.animate({ marginTop: 10 }, 350, function () { loginpanelHandle.addClass("close") });$('#lang').hide(); } else { loginPanel.animate({ marginTop: -110 }, 350, function () { loginpanelHandle.removeClass("close") });$('#lang').show(400); } });
        }
    }
    optScript.initSite();
});