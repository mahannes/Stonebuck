function myMap() {
    var pos = new google.maps.LatLng(55.702927, 12.524019);
    var mapCanvas = document.getElementById("map");
    var mapOptions = {
        center: pos,
        zoom: 15
        
    };
    var map = new google.maps.Map(mapCanvas, mapOptions);
    var marker = new google.maps.Marker({
        position: pos,
        map: map
    });
};