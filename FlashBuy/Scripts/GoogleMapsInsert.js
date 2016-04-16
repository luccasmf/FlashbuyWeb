var map;
var markers = [];
var marker = new google.maps.Marker();
var coordenadaASalvar;

google.maps.event.addDomListener(window, 'load', initialize);

function initialize() {    //função de iniciar o mapa
    var mapOptions = {
        zoom: 15,
        disableDefaultUI: true

    };
    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

    //função de geolocation
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = new google.maps.LatLng(position.coords.latitude,
                                                     position.coords.longitude);

            infowindow = new google.maps.InfoWindow({
                map: map,
                position: pos,
                content: 'Você está aqui'
            });

            map.setCenter(pos);
        }, function () {
            handleNoGeolocation(true);
        });
    } else {
        // Browser doesn't support Geolocation
        handleNoGeolocation(false);
    }

    google.maps.event.addListener(map, 'click', function (event) {
        addMarker(event.latLng);
    });
}

// Adiciona um marcador ao mapa
function addMarker(location) {
    if (markers.length > 0) {
        clearMarkers(),
                    markers = [];
    }

    marker = new google.maps.Marker({
        position: location,
        map: map,
        draggable: true,
        animation: google.maps.Animation.DROP
    });

    markers.push(marker);

    google.maps.event.addDomListener(marker, 'dragend', function () {
        AtualizaCoordenadas(marker.getPosition());
    });
    AtualizaCoordenadas(marker.getPosition());
}

function AtualizaCoordenadas(mapa) {
    $("input[id$='txtLatitude']").val(mapa.lat().toString());
    $("input[id$='txtLongitude']").val(mapa.lng().toString());
}

function handleNoGeolocation(errorFlag) {
    if (errorFlag) {
        var content = 'Error: The Geolocation service failed.';
    } else {
        var content = 'Error: Your browser doesn\'t support geolocation.';
    }

    var options = {
        map: map,
        position: new google.maps.LatLng(60, 105),
        content: content
    };

    infowindow = new google.maps.InfoWindow(options);
    map.setCenter(options.position);
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
}
