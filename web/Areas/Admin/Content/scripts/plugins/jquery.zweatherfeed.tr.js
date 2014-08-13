/**
 * Plugin: jquery.zWeatherFeed
 * 
 * Version: 1.0.2
 * (c) Copyright 2010, Zazar Ltd
 * 
 * Description: jQuery plugin for display of Yahoo! Weather feeds
 * 
 * History:
 * 1.0.2 - Correction to options / link
 * 1.0.1 - Added hourly caching to YQL to avoid rate limits
 *         Uses Weather Channel location ID and not Yahoo WOEID
 *         Displays day or night background images
 *
 **/

(function($){

	var row = 'odd';

	$.fn.weatherfeed = function(locations, options) {	
	
		// Set pluign defaults
		var defaults = {
			unit: 'c',
			image: true,
			highlow: true,
			wind: true,
			link: true,
			showerror: true
		};  
		var options = $.extend(defaults, options); 
		
		// Functions
		return this.each(function(i, e) {
			var $e = $(e);
			
			// Add feed class to user div
			if (!$e.hasClass('weatherFeed')) $e.addClass('weatherFeed');

			// Check and append locations
			if (!$.isArray(locations)) return false;
			var count = locations.length;
			if (count > 10) count = 10;
			var locationid = '';
			for (var i=0; i<count; i++) {
				if (locationid != '') locationid += ',';
				locationid += "'"+ locations[i] + "'";
			}

			// Cache results for an hour to prevent overuse
			now = new Date()
					
			// Create Yahoo Weather feed API address
			var query = "select * from weather.forecast where location in ("+ locationid +") and u='"+ options.unit +"'";
			var api = 'http://query.yahooapis.com/v1/public/yql?q='+ encodeURIComponent(query) +'&rnd='+ now.getFullYear() + now.getMonth() + now.getDay() + now.getHours() +'&format=json&callback=?';

			// Send request
			//$.getJSON(api, function(data) {
			$.ajax({
				type: 'GET',
				url: api,
				dataType: 'json',
				success: function(data) {

					if (data.query) {
			
						if (data.query.results.channel.length > 0 ) {
							
							// Multiple locations
							var result = data.query.results.channel.length;
							for (var i=0; i<result; i++) {
							
								// Create weather feed item
								_callback(e, data.query.results.channel[i], options);
							}
						} else {

							// Single location only
							_callback(e, data.query.results.channel, options);
						}
					} else {
						if (options.showerror) $e.html('<p>Hava durumu bilgisi bulunamadı</p>');
					}
				},
				error: function(data) {
					if (options.showerror)  $e.html('<p>Hava durumu bilgisi alınamıyor</p>');
				}
			});

		});
	};

	// Function to each feed item
	var _callback = function(e, feed, options) {
		var $e = $(e);

		// Format feed items
		var wd = feed.wind.direction;
		if (wd >= 348.75 && wd <= 360) { wd = "N" }; if (wd >= 0 && wd < 11.25) { wd = "N" }; if (wd >= 11.25 && wd < 33.75) { wd = "NNE" }; if (wd >= 33.75 && wd < 56.25) { wd = "NE" }; if (wd >= 56.25 && wd < 78.75) { wd = "ENE" }; if (wd >= 78.75 && wd < 101.25) { wd = "E" }; if (wd >= 101.25 && wd < 123.75) { wd = "ESE" }; if (wd >= 123.75 && wd < 146.25) { wd = "SE" }; if (wd >= 146.25 && wd < 168.75) { wd = "SSE" }; if (wd >= 168.75 && wd < 191.25) { wd = "S" }; if (wd >= 191.25 && wd < 213.75) { wd = "SSW" }; if (wd >= 213.75 && wd < 236.25) { wd = "SW" }; if (wd >= 236.25 && wd < 258.75) { wd = "WSW" }; if (wd >= 258.75 && wd < 281.25) { wd = "W" }; if (wd >= 281.25 && wd < 303.75) { wd = "WNW" }; if (wd >= 303.75 && wd < 326.25) { wd = "NW" }; if (wd >= 326.25 && wd < 348.75) { wd = "NNW" };
		var wf = feed.item.forecast[0];
		
		// Determine day or night image
		wpd = feed.item.pubDate;
		n = wpd.indexOf(":");
		tpb = _getTimeAsDate(wpd.substr(n-2,8));
		tsr = _getTimeAsDate(feed.astronomy.sunrise);
		tss = _getTimeAsDate(feed.astronomy.sunset);

		if (tpb>tsr && tpb<tss) { daynight = 'd'; } else { daynight = 'n'; }

		// Add item container
		var html = '<div class="weatherItem '+ row +'"';
		if (options.image) html += ' style="background-image: url(http://l.yimg.com/a/i/us/nws/weather/gr/'+ feed.item.condition.code + daynight +'.png); background-repeat: no-repeat;"';
		html += '>';
		var ido = new Array();
    ido = {
      'tornado' : 'hortum',
      'tropical storm' : 'şiddetli fırtına',
      'hurricane' : 'kasırga',
      'severe thunderstorms' : 'gök gürültülü sağanak yağışlı',
      'thunderstorms': 'gök gürültülü sağanak yağışlı',
      'mixed rain and snow' : 'yağmur kar karışık yağışlı',
      'mixed rain and sleet': 'yağmur dolu karışık yağışlı',
      'mixed snow and sleet' : 'kar dolu karışık yağışlı',
      'freezing drizzle' : 'karlı yağmur',
      'drizzle' : 'hafif yağmur',
      'freezing rain' : 'donan yağmurlu',
      'showers' : 'kısa yağmur',
      'showers': 'kısa yağmur',
      'snow flurries' : 'kar fırtınası',
      'light snow showers' : 'hafif kar yağışı',
      'blowing snow' : 'tipi',
      'snow' : 'kar',
      'hail' : 'dolu',
      'sleet' : 'karla karışık yağmurlu',
      'dust' : 'tozlu',
      'foggy' : 'sisli',
      'haze' : 'hafif sisli',
      'smoky' : 'dumanlı',
      'blustery' : 'sert rüzgarlı',
      'windy' : 'rüzgarlı',
      'cold' : 'soğuk',
      'cloudy' : 'bulutlu',
      'mostly cloudy' : 'çok bulutlu',
      'mostly cloudy': 'çok bulutlu',
      'partly cloudy' : 'parçalı bulutlu',
      'partly cloudy': 'parçalı bulutlu',
      'clear': 'açık',
      'sunny' : 'güneşli',
      'fair' : 'açık',
      'fair' : 'açık',
      'mixed rain and hail' : 'yağmur ve dolu karışık yağışlı',
      'hot' : 'sıcak',
      'isolated thunderstorms' : 'ayrık gök gürültülü',
      'scattered thunderstorms' : 'zaman zaman gök gürültülü',
      'scattered thunderstorms': 'zaman zaman gök gürültülü',
      'scattered showers' : 'aralıklı yağış',
      'heavy snow' : 'ağır kar yağışlı',
      'scattered snow showers' : 'zaman zaman kar yağışlı',
      'heavy snow' : 'ağır kar yağışlı',
      'partly cloudy' : 'parçalı bulutlu',
      'thundershowers' : 'gök gürültülü sağanak yağışlı',
      'snow showers' : 'kar yağışlı',
      'isolated thundershowers' : 'ayrık kar yağışlı',
      'not available' : 'tahmin edilemiyor' };

		// Add item data
		html += '<div class="weatherCity">'+ feed.location.city +'</div>';
		html += '<div class="weatherTemp">'+ feed.item.condition.temp +'&deg;</div>';
		html += '<div class="weatherDesc">'+ ido[feed.item.condition.text.toLowerCase()] +'</div>';
		if (options.highlow) html += '<div class="weatherRange">En yüksek: '+ wf.high +'&deg; En düşük: '+ wf.low +'&deg;</div>';
		if (options.wind) html += '<div class="weatherWind">Rüzgar Hızı: '+ wd +' '+ feed.wind.speed + feed.units.speed +'</div>';
		//if (options.link) html += '<div class="weatherLink"><a href="'+ feed.item.link +'">Teljes előrejelzés</a></div>';
		
		html += '</div>';

		// Alternate row classes
		if (row == 'odd') { row = 'even'; } else { row = 'odd';	}
		
		$e.append(html);
	};

	// Get time string as date
	var _getTimeAsDate = function(t) {
		
		d = new Date();
		r = new Date(d.toDateString() +' '+ t);

		return r;
	};
})(jQuery);
