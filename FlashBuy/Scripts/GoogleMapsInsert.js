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
    google.maps.event.addListener(marker, 'dragend', function () { alert(this.location); });
    //google.maps.event.addListener(marker, 'dragend', function () { enviarParaASP(); });
    enviarParaASP();

}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
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


// ENVIAR VALORES PARA UM WEBMETHOD
function enviarParaASP() {
    coordenadaASalvar = { latitude: marker.position.k, longitude: marker.position.A };
    var endereco = codeLatLng(coordenadaASalvar);
    endereco = endereco == undefined ? "indefinido" : endereco;

}

function codeLatLng(valor) {
    var latlng = new google.maps.LatLng(valor.latitude, valor.longitude);
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[0].address_components) {
                $("#address_components").val(results[0].address_components);
                $("#latitude").val(valor.latitude);



                //var endereco = results[0].address_components[1].short_name + ',' + results[0].address_components[0].short_name;
                //var cidade = results[0].address_components[3].short_name;
                //var estado = results[0].address_components[5].short_name;
                //var bairro = results[0].address_components[2].short_name;
                ////var parametros = { coord: valor, endereco: retorno };

                //jQuery.ajax({
                //    url: 'InserirBuracos.aspx/SalvaCoordenadas',  //eh soh alterar esse endereço do metodo pra onde vao as coordenadas
                //    type: "POST",
                //    //data: parametros,
                //    data: JSON.stringify({ 'vet': results[0].address_components, 'lt': valor.latitude, 'lg': valor.longitude }),
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",

                //    // success: function (msg) { alert("Buraco inserido  "); },
                //    //failure: function (msg) { alert("Sorry!!! "); }
                //});


                //return retorno;

            }
        } else {
            alert("Geocoder failed due to: " + status);
        }

    });
    // return def.promise(); //testar
}
