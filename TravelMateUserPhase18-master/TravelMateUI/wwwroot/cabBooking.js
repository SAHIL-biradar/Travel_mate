/*let map;*/
let userMarker;
let cabMarkers = [];
let directionsService;
let directionsRenderer;

function initializeMap(userLat, userLng) {
    const mapOptions = {
        center: { lat: userLat, lng: userLng },
        zoom: 14,
        styles: [
            {
                featureType: "poi",
                elementType: "labels",
                stylers: [{ visibility: "off" }]
            }
        ]
    };

    map = new google.maps.Map(document.getElementById("map"), mapOptions);

    userMarker = new google.maps.Marker({
        position: { lat: parseFloat(userLat), lng: parseFloat(userLng) },
        map: map,
        icon: {
            url: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png"
        }
    });

    //nearbyCabs.forEach(cab => {
    //    const cabMarker = new google.maps.Marker({
    //        position: { lat: cab.Latitude, lng: cab.Longitude },
    //        map: map,
    //        icon: {
    //            url: "http://maps.google.com/mapfiles/ms/icons/car.png"
    //        }
    //    });
    //    cabMarkers.push(cabMarker);
    //});

    directionsService = new google.maps.DirectionsService();
    directionsRenderer = new google.maps.DirectionsRenderer({
        map: map,
        suppressMarkers: true
    });
}


function initializeMap2(userLat, userLng) {
    const mapOptions = {
        center: { lat: userLat, lng: userLng },
        zoom: 14,
        styles: [
            {
                featureType: "poi",
                elementType: "labels",
                stylers: [{ visibility: "off" }]
            }
        ]
    };

    map = new google.maps.Map(document.getElementById("map"), mapOptions);

    userMarker = new google.maps.Marker({
        position: { lat: userLat, lng: userLng },
        map: map,
        icon: {
            url: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png"
        }
    });

}

function showRoute(userLat, userLng, cabLat, cabLng) {
    const request = {
        origin: { lat: userLat, lng: userLng },
        destination: { lat: cabLat, lng: cabLng },
        travelMode: 'DRIVING'
    };

    directionsService.route(request, function (result, status) {
        if (status === 'OK') {
            directionsRenderer.setDirections(result);
        }
    });
}
function showRouteByAddress(fromLocation, toLocation) {
    return new Promise((resolve, reject) => {
        const directionsService = new google.maps.DirectionsService();
        const directionsRenderer = new google.maps.DirectionsRenderer();
        const map = new google.maps.Map(document.getElementById("map"), {
            zoom: 7,
            center: { lat: 41.85, lng: -87.65 }  // Default to Chicago
        });

        directionsRenderer.setMap(map);

        const request = {
            origin: fromLocation,
            destination: toLocation,
            travelMode: google.maps.TravelMode.DRIVING
        };

        directionsService.route(request, function (result, status) {
            if (status === 'OK') {
                directionsRenderer.setDirections(result);
                // Extract the total distance
                const totalDistance = result.routes[0].legs.reduce((sum, leg) => sum + leg.distance.value, 0);
                // Convert meters to kilometers
                const totalDistanceInKm = totalDistance / 1000;
                resolve({ distance: totalDistanceInKm });
            } else {
                reject(new Error('Directions request failed due to ' + status));
            }
        });
    });
}
function getCurrentLocation() {
    return new Promise((resolve, reject) => {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    resolve({
                        latitude: position.coords.latitude,
                        longitude: position.coords.longitude
                    });
                },
                (error) => {
                    reject(error);
                }
            );
        } else {
            reject("Geolocation is not supported by this browser.");
        }
    });
}

function initializeAutocomplete(elementId) {
    const input = document.getElementById(elementId);
    const autocomplete = new google.maps.places.Autocomplete(input);

    autocomplete.addListener('place_changed', function () {
        const place = autocomplete.getPlace();
        if (!place.geometry) {
            console.log("Autocomplete's returned place contains no geometry");
            return;
        }

        // If both locations are filled, display the route
        //if (elementId === "fromLocation" || elementId === "toLocation") {
        //    DotNet.invokeMethodAsync('TravelMate2', 'SetRouteSearched')
        //        .then(() => initializeMap());
        //}
    });
}
