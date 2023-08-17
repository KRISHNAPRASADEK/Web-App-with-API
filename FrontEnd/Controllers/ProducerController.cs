using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class ProducerController : Controller
    {
        Uri baseaddress =new Uri("https://localhost:7199/api");
        HttpClient client;

        public ProducerController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;
        }
        public IActionResult ProducerView()
        {
            List <ProducerDto> modellist = new List<ProducerDto>();
            HttpResponseMessage response=client.GetAsync(client.BaseAddress+"/Producers").Result;

            if (response.IsSuccessStatusCode)
            {
                string data=response.Content.ReadAsStringAsync().Result;
                modellist=JsonConvert.DeserializeObject<List<ProducerDto>>(data);
                
            }
            return View(modellist);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Edit(ProducerDto model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Producers/" + model.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProducerView");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProducerDto model)
                                      {
            string data=JsonConvert.SerializeObject(model);
            StringContent content=new StringContent(data,System.Text.Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Producers", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProducerView");
            }
                return View();
        }

        public ActionResult Edit(int Id)
        {
            ProducerDto model = new ProducerDto();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Producers/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProducerDto>(data);

            }
            return View("Create",model);
        }

        
        public ActionResult Delete(int Id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Producers/" + Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("ProducerView");
            }
            return View();
        }


    }
}
