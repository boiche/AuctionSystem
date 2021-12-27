using AuctionSystem.Server.Models.Http.Requests;
using AuctionSystem.Server.Models.Http.Responses;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuctionSystem.Server.Services
{
    public class RecaptchaService
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task<HttpResponseMessage> RetrieveResponse(RecaptchaRequest recaptchaRequest, IConfiguration configuration)
        {
            var values = new Dictionary<string, string>
                {
                    { "secret", configuration.GetValue(typeof(string), "RECAPTCHA_SECRET_KEY").ToString() }, //ConfigurationManager.AppSettings["RECAPTCHA_SECRET_KEY"].ToString()},
                    { "response", recaptchaRequest.Response}
                };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(configuration.GetValue(typeof(string), "RECAPTCHA_GOOGLE_URL").ToString(), content);

            return new HttpResponseMessage()
            {
                StatusCode = response.StatusCode,
                Content = response.Content,
                ReasonPhrase = response.ReasonPhrase,
                RequestMessage = response.RequestMessage,
                Version = response.Version
            };
        }
    }
}
