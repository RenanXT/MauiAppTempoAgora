using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(String cidade)
        {
            Tempo? t = null;
            String chave = "72664a6bb90979264d6f480a58c59243";
            String url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid={chave}";

            try
            {
                using (HttpClient Client = new HttpClient())
                {
                    HttpResponseMessage resp = await Client.GetAsync(url);

                    if (resp.IsSuccessStatusCode)
                    {
                        String json = await resp.Content.ReadAsStringAsync();
                        var rascunho = JObject.Parse(json);

                        DateTime sunrise = DateTimeOffset
                            .FromUnixTimeSeconds((long)rascunho["sys"]["sunrise"])
                            .ToLocalTime()
                            .DateTime;

                        DateTime sunset = DateTimeOffset
                            .FromUnixTimeSeconds((long)rascunho["sys"]["sunset"])
                            .ToLocalTime()
                            .DateTime;

                        t = new Tempo
                        {
                            lat = (double)rascunho["coord"]["lat"],
                            lon = (double)rascunho["coord"]["lon"],
                            description = (string)rascunho["weather"][0]["description"],
                            main = (string)rascunho["weather"][0]["main"],
                            temp_min = (double)rascunho["main"]["temp_min"],
                            temp_max = (double)rascunho["main"]["temp_max"],
                            speed = (double)rascunho["wind"]["speed"],
                            visibility = (int)rascunho["visibility"],
                            sunrise = sunrise.ToString(),
                            sunset = sunset.ToString(),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Algo deu errado, Verifique Sua Conexão Com a Internet!", ex);
            }

            return t;
        }
    }
}
