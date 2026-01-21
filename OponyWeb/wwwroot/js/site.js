const searchBtn = document.querySelector('#searchBtn');
const cityInput = document.querySelector('#cityInput');
const form = document.querySelector('#search-section');

function round(x) {
    return Number.parseFloat(x).toPrecision(2);
}

form.addEventListener('submit', (event) =>{
    event.preventDefault();
    searchCity();
})

function searchCity() {
    const inputValue = cityInput.value;
    let url = `https://localhost:7214/api/location/${inputValue}/tire-status`;

    fetch(url)
            .then(response => {
                return response.json();   
    }).then(data => {
            if(data.recommendation === 'ChangeToWinter') {
                document.getElementById('recommendation').innerText = 'Opony zimowe';
            } else if(data.recommendation === 'ChangeToSummer') {
                document.getElementById('recommendation').innerText = 'Opony letnie';
            }

            const averageTemp = (data.averageTemperature);
            document.getElementById('averageTemp').innerHTML = (round(averageTemp));

            const daysBelow = (data.daysBelowTreshold);
            document.getElementById('daysBelow').innerHTML = daysBelow;

            var lat = (data.latitude)
            var lon = (data.longitude)
            
            function search() {
                fetch('https://places.googleapis.com/v1/places:searchNearby', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'X-Goog-Api-Key': 'AIzaSyAgpXHQdJCRLtrb0YMZEBXaJFhjEMC3NPk',
                    'X-Goog-FieldMask': 'places.displayName,places.formatted_address,places.rating,places.nationalPhoneNumber,places.regularOpeningHours'
                },
                body: JSON.stringify({
                    'includedTypes': [
                    'car_repair'
                    ],
                    'maxResultCount': 5,
                    'locationRestriction': {
                    'circle': {
                        'center': {
                        'latitude': lat,
                        'longitude': lon
                        },
                        'radius': 5000
                    }
                    }
                })
                }).then(response => {
                    return response.json();
                }).then(d => {

                    var container = document.getElementById('nerbayCarRepair');
                    
                    document.getElementById('nerbayCarRepair').innerHTML = "";
                    
                    d.places.forEach(place => {
                        const div = document.createElement("div");
                        div.id = "karta-warsztatu";

                        const name = document.createElement("p");
                        name.textContent = place.displayName.text;

                        const rating = document.createElement("p");
                        rating.textContent = `⭐ ${place.rating}`;

                        const address = document.createElement("p");
                        address.textContent = `📌 ${place.formattedAddress}`;

                        const phoneNumber = document.createElement("p");
                        phoneNumber.textContent = `📞 ${place.nationalPhoneNumber}`;

                        // const openNow = document.createElement("p");
                        // openNow.textContent = place.regularOpeningHours.openNow;

                        // if(openNow === true) {
                        //     textContent = "Otwarte"
                        // } else {
                        //     textContent = "Zamknięte"
                        // }

                        div.appendChild(name);
                        div.appendChild(rating);
                        div.appendChild(address);
                        div.appendChild(phoneNumber);
                        // div.appendChild(openNow);
                        container.appendChild(div);
                    })
                })
            }
            search();
    })
}

function ShowSummary() {
    var rows = document.getElementById("rows");
    rows.style.display = "flex";
    var summary = document.getElementById("summary");
    summary.style.display = "flex";
    var carRepair = document.getElementById("carRepair");
    carRepair.style.display = "flex";
}