// Example site JavaScript using axios
document.addEventListener('DOMContentLoaded', function() {
    // Add footer
    const footer = document.createElement('div');
    footer.className = 'footer';
    footer.innerHTML = '&copy; ' + new Date().getFullYear() + ' - ASP.NET Core Static Site Example';
    document.body.appendChild(footer);
    
    console.log('Static site JavaScript loaded successfully');
    console.log('Axios version:', axios.VERSION);
    
    // Set up weather data fetching with axios
    const fetchWeatherButton = document.getElementById('fetch-weather');
    const weatherDataDiv = document.getElementById('weather-data');
    
    if (fetchWeatherButton) {
        fetchWeatherButton.addEventListener('click', function() {
            // Show loading state
            weatherDataDiv.style.display = 'block';
            weatherDataDiv.innerHTML = 'Loading weather data...';
            
            // Make API request using axios
            axios.get('/weatherforecast', {
                timeout: 3000
            })
                .then(function(response) {
                    // Handle successful response
                    const weatherData = response.data;
                    let html = '<h3>Weather Forecast</h3><ul>';
                    
                    weatherData.forEach(function(forecast) {
                        html += `<li>
                            <strong>Date:</strong> ${forecast.date}, 
                            <strong>Temp:</strong> ${forecast.temperatureC}°C (${forecast.temperatureF}°F), 
                            <strong>Summary:</strong> ${forecast.summary}
                        </li>`;
                    });
                    
                    html += '</ul>';
                    weatherDataDiv.innerHTML = html;
                })
                .catch(function(error) {
                    // Handle error
                    console.error('Error fetching weather data:', error);
                    weatherDataDiv.innerHTML = 'Error loading weather data. See console for details.';
                });
        });
    }
});