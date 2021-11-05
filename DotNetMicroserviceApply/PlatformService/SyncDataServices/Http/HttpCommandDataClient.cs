using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient http;
        private readonly IConfiguration config;
        public HttpCommandDataClient(HttpClient http,IConfiguration config)
        {
            this.http = http;
            this.config = config;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent = new StringContent
            (
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json"
            );

            var response = await http.PostAsync($"{config["CommandService"]}",httpContent);

            if (response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("--> Sync Post to Command Service was succesful!!!");
            }
            else
            {
                System.Console.WriteLine("--> Sync Post to Command Service was not succesful!!!");
            }
        }
    }
}
