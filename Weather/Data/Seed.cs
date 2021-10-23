using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Entities;

namespace Weather.Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Weathers.Any()) return;

            var weathers = new List<WeatherEntity>
            {
                new WeatherEntity
                {
                    CityName = "Kharkov",
                    CurrentT = 20,
                    MinT = 5,
                    MaxT = 26,
                    AverageT = 15,
                    WeatherHistory = new List<WeatherHistory>
                    {
                        new WeatherHistory
                        {
                            TemperatureC = 14,
                            Date = DateTime.Now
                        },
                        new WeatherHistory
                        {
                            TemperatureC = 44,
                            Date = DateTime.Now
                        }
                    }
                },

                new WeatherEntity
                {
                    CityName = "Kyev",
                    CurrentT = 15,
                    MinT = 2,
                    MaxT = 16,
                    AverageT = 10,
                    WeatherHistory = new List<WeatherHistory>
                    {
                        new WeatherHistory
                        {
                            TemperatureC = 12,
                            Date = DateTime.Now
                        },
                        new WeatherHistory
                        {
                            TemperatureC = 32,
                            Date = DateTime.Now
                        }
                    }
                },

                new WeatherEntity
                {
                    CityName = "Lvov",
                    CurrentT = 22,
                    MinT = 10,
                    MaxT = 21,
                    AverageT = 18,
                    WeatherHistory = new List<WeatherHistory>
                    {
                        new WeatherHistory
                        {
                            TemperatureC = 20,
                            Date = DateTime.Now
                        }
                    }
                },

                new WeatherEntity
                {
                    CityName = "Odessa",
                    CurrentT = 25,
                    MinT = 12,
                    MaxT = 28,
                    AverageT = 15,
                    WeatherHistory = new List<WeatherHistory>
                    {
                        new WeatherHistory
                        {
                            TemperatureC = 18,
                            Date = DateTime.Now
                        }
                    }
                },
            };

            await context.Weathers.AddRangeAsync(weathers);
            await context.SaveChangesAsync();
        }
    }
}
