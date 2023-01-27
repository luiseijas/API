using API.Models;
using Newtonsoft.Json;

namespace API.Utils
{
    public static class Utiles
    {
        public static async Task<Clima> ObtenerClimalocalidad(string localidad)
        {
            //localidad = "A Coruna, Spain";
            var client = new HttpClient();
            var apiKey = "57a8e77708d634d5a0891070d3f46cd7";
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://api.weatherstack.com/current?access_key={apiKey}&query={localidad}");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>(json);
                var temperatura = data.current.temperature;
                var humedad = data.current.humidity;

                var clima = new Clima { Temperatura = temperatura, Humedad = humedad };
                return clima;
            }
            else
            {
                return null;
            }

        }
    }
}
