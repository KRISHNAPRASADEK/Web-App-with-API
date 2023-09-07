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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DirectorDto model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Directors", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DirectorView");
            }
            return View();
        }

        public ActionResult Edit(int Id)
        {
            DirectorDto model = new DirectorDto();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Directors/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<DirectorDto>(data);

            }
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(DirectorDto model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Directors/" + model.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("DirectorView");
            }
            return View();
        }
        
        public ActionResult Delete(int Id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Directors/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("DirectorView");
            }
            return View();
        }


    }
}
