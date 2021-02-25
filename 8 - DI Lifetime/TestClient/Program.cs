using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            InvokeWebService();
            Console.ReadLine();
        }


        private static async void InvokeWebService()
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://localhost:49333/api/values/1");
            string body = await response.Content.ReadAsStringAsync();

            Console.WriteLine(body);

            Console.WriteLine("---------------------------------");
            response = await httpClient.GetAsync("http://localhost:49333/api/values/1");
            body = await response.Content.ReadAsStringAsync();

            Console.WriteLine(body);
        }
    }
}
