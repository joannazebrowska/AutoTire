const searchBtn = document.querySelector('#searchBtn');
const cityInput = document.querySelector('#cityInput');
const form = document.querySelector('#search-section');


// cityInput.addEventListener('keypress', (e) =>{
//     if(e.key === "Enter") {
//         e.preventDefault();
//         searchCity();
//     }
// })

form.addEventListener('submit', (event) =>{
    event.preventDefault();
    searchCity();
})

function searchCity() {
    const inputValue = cityInput.value;
    let url = `https://localhost:7214/api/location/${inputValue}/tire-status`;
    console.log(inputValue);

    fetch(url)
            .then(response => {
                return response.json();   
    }).then(data => {
            // console.log(html)
            // console.log("gowno")
            // const res = JSON(data.recommendation);
            // document.getElementById('recommendation').innerHTML = rec;

            if(data.recommendation === 'ChangeToWinter') {
                document.getElementById('recommendation').innerText = 'powinieneś zmienić opony na zimowe';
            } else if(data.recommendation === 'ChangeToSummer') {
                document.getElementById('recommendation').innerText = 'powinieneś zmienić opony na letnie';
            }

            const averageTemp = (data.averageTemperature);
            document.getElementById('averageTemp').innerHTML = averageTemp;

            const daysBelow = (data.daysBelowTreshold);
            document.getElementById('daysBelow').innerHTML = daysBelow;
    })
}

// async function searchCity() {
//     const inputValue = cityInput.value;

//     try {
//         const response = await fetch(`https://localhost:7214/api/location/${inputValue}/tire-status`)
//         const data = await response.json();
//         return data;
//     } catch (error) {
//         console.error('Error fetching data:', error)
//     }
// }

// async function renderData() {
//     const container = document.querySelector('.container');
//     const data = await searchCity();

//     if(!data) {
//         return;
//     }

//     data.array.forEach(item => {
//         const card = document.createElement('div');
//         card.classList.add('card')

//         const averageTemperature = document.createElement('h2');
//         averageTemperature = data.averageTemperature;

//         container.appendChild(card);
//         card.appendChild(averageTemperature)
//     });
// }

// renderData();