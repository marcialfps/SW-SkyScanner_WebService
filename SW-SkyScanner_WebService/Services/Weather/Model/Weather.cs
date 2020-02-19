using System;

namespace SW_SkyScanner_WebService.Services.Weather.Model
{
    public class Weather
    {
        public Weather()
        {}
        public Weather(dynamic dynWeather)
        {
            // Fill weather object properties
            Time = (int)dynWeather.dt;
            Main = dynWeather.weather[0].main;
            Description = dynWeather.weather[0].description;
            Temperature = (double)dynWeather.main.temp;
            TemperatureMax = (double)dynWeather.main.temp_max;
            TemperatureMin = (double)dynWeather.main.temp_min;
            Pressure = (int)dynWeather.main.pressure;
            Humidity = (int)dynWeather.main.humidity;
            WindSpeed = (double)dynWeather.wind.speed;
            WindDirection = (double)dynWeather.wind.speed;
            Cloudiness = (int)dynWeather.clouds.all;
        }
        
        /// <summary>
        /// Timestamp (secs since Unix epoch) 
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Text description of the climate condition
        /// </summary>
        public string Main { get; set; }
        
        /// <summary>
        /// Detailed text description of the climate condition
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Temperature (celsius)
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Max temperature (celsius)
        /// </summary>
        public double TemperatureMax { get; set; }
        /// <summary>
        /// Min temperature (celsius)
        /// </summary>
        
        public double TemperatureMin { get; set; }

        /// <summary>
        /// Pressure (hPa)
        /// </summary>
        public int Pressure { get; set; }
        
        /// <summary>
        /// Humidity (%)
        /// </summary>
        public int Humidity { get; set; }
        
        /// <summary>
        /// Wind speed (m/s)
        /// </summary>
        public double WindSpeed{ get; set; }

            /// <summary>
        /// Wind direction (degrees(?))
        /// </summary>
        public double WindDirection { get; set; }

        /// <summary>
        /// Cloudiness (%)
        /// </summary>
        public int Cloudiness { get; set; }
    }
}