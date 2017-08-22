using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using SampleApi.Helper;
using SampleApi.WeatherServiceReference;

namespace SampleApi.Controllers.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        public WeatherController WeatherController { get; set; }
        public UnitTest1()
        {
            WeatherController = new WeatherController();
        }

        [TestMethod()]
        public void GetWeatherDetailsForCityTest()
        {
            var testDataCountry = "China";
            IHttpActionResult result = WeatherController.GetCitiesFromCountry(testDataCountry);
            Assert.IsNotNull(result);

            var response = result as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(response);

            var content = response.Content;
            Assert.IsNotNull(content);

            // check if Beijing city is in China and the content is having the details
            Assert.IsTrue(content.Contains("Beijing"));
        }

        [TestMethod()]
        public void GetWeatherDetailsForCityTest1()
        {
            var testDataCity = "Beijing";
            IHttpActionResult result = WeatherController.GetWeatherDetailsForCity(testDataCity);
            Assert.IsNotNull(result);

            var response = result as OkNegotiatedContentResult<WeatherResponse.ResponseWeather>;
            Assert.IsNotNull(response);

            var content = response.Content;
            Assert.IsNotNull(content);

            // check if Beijing city has weather details
            Assert.IsNotNull(content.name);
            Assert.AreEqual(content.name, "Beijing");

            // check for all fields 
            Assert.IsNotNull(content.clouds);
            Assert.IsNotNull(content.coord);
            Assert.IsNotNull(content.dt);
            Assert.IsNotNull(content.main);
            Assert.IsNotNull(content.sys);
            Assert.IsNotNull(content.visibility);
            Assert.IsNotNull(content.weather);

        }
    }
}