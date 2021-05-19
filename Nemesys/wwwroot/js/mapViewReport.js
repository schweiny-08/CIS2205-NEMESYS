var marker;

function initMap() {
    const uom = { lat: 35.902303, lng: 14.483977 };

    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 18,
        center: uom,
    });

    const marker = new google.maps.Marker({
        position: {lat, lng},
        map: map,
    });
}