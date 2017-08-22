using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Xml;
using Newtonsoft.Json;
using SampleApi.Helper;

namespace SampleApi.Controllers
{
    public class WeatherController : ApiController
    {
        /// <summary>
        /// This GetWeatherDetailsForCity Action is called to get the weather details of the particular city.
        /// It calls a external third party service from http://api.openweathermap.org/data/2.5/weather?q=London
        /// The result is desirialized into a helper class which is constructed based on the json return type
        /// This helps us to map the json resutl to a json object which we can send to the calling client 
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        // GET api/Weather/City/Bejing
        [HttpGet]
        [Route("api/Weather/City/{cityName}")]
        public IHttpActionResult GetWeatherDetailsForCity(string cityName)
        {
            /*Calling API http://openweathermap.org/api */
            // call configuration manager to get the api key stored
            string apiKey = ConfigurationManager.AppSettings["OpenWeatherMapApiKey"];
            // build the url to call the api with city name and appkey id
            var url = "http://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&appid=" + apiKey;
            
            // make a web request to that url
            HttpWebRequest apiRequest =
                WebRequest.Create(url) as HttpWebRequest;

            // capture the http response 
            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }
            
            // convert json object by desirializing it to helper class and return
            WeatherResponse.ResponseWeather responseWeather =
                JsonConvert.DeserializeObject<WeatherResponse.ResponseWeather>(apiResponse);

            return Ok(responseWeather);
        }

        /// <summary>
        /// This GetCitiesFromCountry Action is called to get the list of countries by calling 
        /// a WCF service from http://www.webservicex.net/globalweather.asmx/GetCitiesByCountry
        /// The result is parsed to just send the cities in json format 
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns>Cities in Json format</returns>
        // GET api/Weather/Country/China
        [HttpGet]
        [Route("api/Weather/Country/{countryName}")]
        public IHttpActionResult GetCitiesFromCountry(string countryName)
        {
            // Call the proxy of the service client
            WeatherServiceReference.GlobalWeatherSoapClient client = new SampleApi.WeatherServiceReference.GlobalWeatherSoapClient();
            var result = client.GetCitiesByCountry(countryName);
            // Load the values in XML as the results we get from the WCF service is in the xml format
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(result);
            var txt = "";
            // select all the city element and put it in a string separated by comma delimeter
            var x = xdoc.GetElementsByTagName("City");
            for (var i = 0; i < x.Count; i++)
            {
                txt += x[i].ChildNodes[0].Value;
                if (i < x.Count-1)
                    txt += ',';
            }
            // create a array object from the string separated by comma delimeter
            string[] values = txt.Split(',').Select(sValue => sValue.Trim()).ToArray();
            // convert the object array into json and return
            result = JsonConvert.SerializeObject(values);
            return Ok(result);
        }
    }
}
