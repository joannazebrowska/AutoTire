
const searchBtn = document.querySelector('#searchBtn');
const cityInput = document.querySelector('#cityInput')

searchBtn.addEventListener('click', () =>{
    const inputValue = cityInput.value;
    let url = `https://localhost:7214/api/location/${inputValue}/tire-status`;
    console.log(inputValue);

    fetch(url)
        .then(Response => {
            if (Response.ok) {
            return Response.json();   
        };
    }).then(data => {
        // console.log(html)
        const res = JSON.stringify(data.recommendation);
        document.getElementById('result').innerHTML = res;
        
        
    })

})





