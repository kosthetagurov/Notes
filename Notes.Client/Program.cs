using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Xml.Linq;

namespace Notes.Client
{
    internal class Program
    {
        const string baseUrl = "";

        static void Main(string[] args)
        {
            GetToken();
        }


        static async Task GetToken()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            var response = await httpClient.GetAsync("api/TokenGenerator/generate");
        }

        static async Task Create(string token)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Add("UserToken", token);

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Title", "New note"),
                new KeyValuePair<string, string>("Content", "New note content"),
            });

            var response = await httpClient.PutAsync("api/Notes/create", formContent);
        }

        static async Task Read()
        {

        }

        static async Task Update()
        {

        }

        static async Task Delete()
        {

        }
    }
}