
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

     new google.maps.Marker({
        position: { lat: userLat, lng: userLng },
        map: map,
        icon: {
            url: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png"
        }
    });
}

function showLocation(destination) {
    return new Promise((resolve, reject) => {
        const geocoder = new google.maps.Geocoder();

        geocoder.geocode({ address: destination }, (results, status) => {
            if (status === "OK" && results[0]) {
                // Get the location from the geocoding result
                const location = results[0].geometry.location;
                const lat = location.lat();
                const lng = location.lng();

                // Center the map on the location
                const map = new google.maps.Map(document.getElementById("map"), {
                    center: location,
                    zoom: 14,
                    styles: [
                        {
                            featureType: "poi",
                            elementType: "labels",
                            stylers: [{ visibility: "off" }]
                        }
                    ]
                });

                // Create a marker at the location
                new google.maps.Marker({
                    map: map,
                    position: location,
                    title: destination,
                    icon: 'http://maps.google.com/mapfiles/ms/icons/blue-dot.png'
                });
                // Resolve the promise with latitude and longitude
                resolve({
                    latitude: lat,
                    longitude: lng
                });
                
            } else {
                // Reject the promise if geocoding fails
                reject(`Geocode was not successful for the following reason: ${status}`);
            }
        });
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
        if (status == 'OK') {
            directionsRenderer.setDirections(result);
        }
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
        if (elementId === "fromLocation" || elementId === "toLocation") {
            DotNet.invokeMethodAsync('TravelMate2', 'SetRouteSearched')
                .then(() => initializeMap());
        }
    });
}
 