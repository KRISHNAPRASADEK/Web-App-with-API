using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class DirectorController : Controller
    {
        Uri baseaddress =new Uri("https://localhost:7199/api");
        HttpClient client;

        public DirectorController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;
        }
        public IActionResult DirectorView()
        {
            List <DirectorDto> modellist = new List<DirectorDto>();
            HttpResponseMessage response=client.GetAsync(client.BaseAddress+"/Directors").Result;

            if (response.IsSuccessStatusCode)
            {
                string data=response.Content.ReadAsStringAsync().Result;
                modellist=JsonConvert.DeserializeObject<List<DirectorDto>>(data);
                
            }
            return View(modellist);
        }
    }
}
