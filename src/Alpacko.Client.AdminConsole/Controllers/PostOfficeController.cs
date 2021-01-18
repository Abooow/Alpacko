using Alpacko.Client.AdminConsole.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Alpacko.Client.AdminConsole.Controllers
{
    public class PostOfficeController
    {
        private HttpClient httpClient;

        public PostOfficeController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PostOfficeModel[]> GetPostOffices(int skip, int? take)
        {
            string requesUrl = $"api/postoffices?skip={skip}" + (take != null ? $"&take={take.Value}" : "");
            HttpResponseMessage response = await httpClient.GetAsync(requesUrl);
            PostOfficeModel[] postOffices = JsonSerializer.Deserialize<PostOfficeModel[]>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return postOffices;
        }

        public async Task<PostOfficeModel> GetPostOffice(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"api/postoffices/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);

            PostOfficeModel postOffice = JsonSerializer.Deserialize<PostOfficeModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return postOffice;
        }
    }
}