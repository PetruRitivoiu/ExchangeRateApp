using ExchangeRate.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRate.Services
{
    public class ExchangeRateService
    {
        public async Task<ExchangeRateModel> GetAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("access-control-allow-methods", "[GET]");

                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = JsonConvert.DeserializeObject<ExchangeRateModel>(await response.Content.ReadAsStringAsync());

                        return content;
                    }
                    else
                    {
                        throw new HttpRequestException("Something went wrong when trying to download the latest exchange rates");
                    }
                }
            }

        }
    }
}
