using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace App2.Views
{
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void Button_OnClicked(object sender, EventArgs e)
        {
           
          CallLoginAPI();
        }
        public async void CallLoginAPI()
        {
            LoginRequest loginRequest = new LoginRequest()
            {
                username = "skadiveti",
                password = "Welcome19"
            };

            HttpResponseMessage resultMessage = await RestClient<LoginRequest>.PostAsync(loginRequest);
            if (resultMessage!=null)
            {
                if (resultMessage.IsSuccessStatusCode)
                {

                }
            }
        }



        class LoginRequest
        {
            public string username = string.Empty;
            public string password = string.Empty;
        }


        public class RestClient<T>
        {
            private const string WebServiceUrl = "https://ppagadal-l-856.evoketechnologies.com/CatamaranWS/CatamaranService.svc/login";
            public async Task<List<T>> GetAsync()
            {
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(WebServiceUrl);
                var taskModels = JsonConvert.DeserializeObject<List<T>>(json);
                return taskModels;
            }

            public static async Task<HttpResponseMessage> PostAsync(T t)
            {
                var json = JsonConvert.SerializeObject(t);
                HttpResponseMessage result = null;
                using (CancellationTokenSource cancellationSource = new CancellationTokenSource())
                {
                    try
                    {
                        cancellationSource.CancelAfter(5000);
                        var cancellationToken = cancellationSource.Token;
                        using (HttpClient httpClient = new HttpClient())
                        {
                            using (HttpContent httpContent = new StringContent(json))
                            {
                                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                result = await httpClient.PostAsync(WebServiceUrl, httpContent, cancellationToken);
                            }
                        }
                    }
                    catch (TaskCanceledException ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    catch (AggregateException exception)
                    {
                        foreach (var VARIABLE in exception.InnerExceptions)
                        {
                          
                        }
                    }
                    return result;
                }
            }

            public async Task<bool> PutAsync(int id, T t)
            {
                var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(t);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await httpClient.PutAsync(WebServiceUrl + id, httpContent);
                return result.IsSuccessStatusCode;
            }

            public async Task<bool> DeleteAsync(int id, T t)
            {
                var httpClient = new HttpClient();
                var response = await httpClient.DeleteAsync(WebServiceUrl + id);
                return response.IsSuccessStatusCode;
            }
        }
    }
}



