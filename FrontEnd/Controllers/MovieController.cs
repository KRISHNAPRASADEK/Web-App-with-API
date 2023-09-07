using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class MovieController : Controller
    {
        Uri baseaddress =new Uri("https://localhost:7199/api");
        HttpClient client;

        public MovieController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;
        }
        public IActionResult MovieView()
        {
            List <MovieDto> modellist = new List<MovieDto>();
            HttpResponseMessage response=client.GetAsync(client.BaseAddress+"/Movies").Result;

            if (response.IsSuccessStatusCode)
            {
                string data=response.Content.ReadAsStringAsync().Result;
                modellist=JsonConvert.DeserializeObject<List<MovieDto>>(data);
                
            }
            return View(modellist);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MovieDto model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Movies", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MovieView");
            }
            return View();
        }

        public ActionResult Edit(int Id)
        {
            MovieDto model = new MovieDto();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Movies/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<MovieDto>(data);

            }
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(MovieDto model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Movies/" + model.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MovieView");
            }
            return View();
        }
        
        public ActionResult Delete(int Id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Movies/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("MovieView");
            }
            return View();
        }


    }
}
