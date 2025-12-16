
const searchBtn = document.querySelector('#searchBtn');
const cityInput = document.querySelector('#cityInput');

searchBtn.addEventListener('click', () =>{
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
            document.getElementById('recommendation').innerText = 'zmien na zimowe';
        } else if(data.recommendation === 'ChangeToSummer') {
            document.getElementById('recommendation').innerText = 'zmien na letnie';
        }

        const averageTemp = (data.averageTemperature);
        document.getElementById('averageTemp').innerHTML = averageTemp;

        const daysBelow = (data.daysBelowTreshold);
        document.getElementById('daysBelow').innerHTML = daysBelow;
    
    })
})





