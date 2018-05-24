using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using App1.Extenssions;
using Newtonsoft.Json;
using Application = Xamarin.Forms.Application;

namespace App1.Services {
    public class Http {
        private const string BaseUri = "http://95.59.125.132:1133";
        private readonly JsonSerializerSettings _serializerSettings;
        public TimeSpan Timeout { get; set; }

        public Http() {
            Timeout = TimeSpan.FromSeconds(20);
            _serializerSettings = new JsonSerializerSettings();
        }
        public async Task<TResponse> GetAsync<TResponse>(string uri) where TResponse : class {
            var token = SettingsManager.Instance.AuthToken;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var res = await httpClient.GetAsync(BaseUri + "/" + uri);
            var ms = new MemoryStream();
            await res.Content.CopyToAsync(ms);
            var jbo = JsonConvert.DeserializeObject<TResponse>(Encoding.UTF8.GetString(ms.GetBuffer()));
            return jbo;
        }
        public async Task<TResponse> PostAsJson<TResponse>(string uri, object request, bool defaultJsonSettings = false) {
            var jsonRequest = JsonConvert.SerializeObject(request, _serializerSettings);
            var client = new HttpClient { Timeout = Timeout };
            //client.DefaultRequestHeaders.Add("culture", "RU");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(BaseUri + "/" + uri));

            using (var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json")) {
                var token = SettingsManager.Instance.AuthToken;
                requestMessage.Content = content;
                requestMessage.Headers.Add("Authorization", "Bearer " + token);
                var responseStream = await client.SendAsync(requestMessage).ConfigureAwait(false);
                responseStream.EnsureSuccessStatusCode();
                var jsonResponse = await responseStream.Content.ReadAsStringAsync().ConfigureAwait(false);
                TResponse response;
                if (defaultJsonSettings) {
                    response = JsonConvert.DeserializeObject<TResponse>(jsonResponse);
                } else {
                    response = JsonConvert.DeserializeObject<TResponse>(jsonResponse, _serializerSettings);
                }
                return response;
            }
        }

    }
}
