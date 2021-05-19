var marker;

function initMap() {
    const uom = { lat: 35.902303, lng: 14.483977 };

    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 16.5,
        center: uom,
    });
/*
    const marker = new google.maps.Marker({
        position: uom,
        map: map,
    });*/

    map.addListener("click", (mapsMouseEvent) => {
        addMarker(map, mapsMouseEvent);
    });
}

function addMarker(map, mapsMouseEvent) {
    if (!marker || !marker.setPosition) {
        marker = new google.maps.Marker({
            position: mapsMouseEvent.latLng,
            map
        });
    } else {
        marker.setPosition(mapsMouseEvent.latLng);
    }

    document.getElementById("hazardLat").value = marker.getPosition().lat();
    document.getElementById("hazardLng").value = marker.getPosition().lng();

    //console.log(typeof marker.getPosition().lat());
}