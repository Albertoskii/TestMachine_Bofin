using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Client.Models;
using System.Text.Json;

using System.Text;
using Microsoft.Web.Administration;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        
        public HomeController(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<List<OrderDetails>> List()
        {
            
            using (var client = new HttpClient())
            {
                ConfigHttpClient(client);

                var response = client.GetAsync("OrderDetails").Result;

                // Si el servicio responde correctamente
                if (response.IsSuccessStatusCode)
                {
                    List<OrderDetails> result = JsonSerializer.Deserialize<List<OrderDetails>>(response.Content.ReadAsStringAsync().Result);
                    return result;
                }
                // Sino devuelve null
                return await Task.FromResult<List<OrderDetails>>(null);

            }
        }
        public async Task<OrderDetails> GetbyID(int id)
        {
            using (var client = new HttpClient())
            {
                ConfigHttpClient(client);

                var response = client.GetAsync("OrderDetails/" + id).Result;

                // Si el servicio responde correctamente
                if (response.IsSuccessStatusCode)
                {
                    OrderDetails result = JsonSerializer.Deserialize<OrderDetails>(response.Content.ReadAsStringAsync().Result);
                    return result;
                }
                // Sino devuelve null
                return await Task.FromResult<OrderDetails>(null);

            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(string data)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    ConfigHttpClient(client);
                    HttpResponseMessage response = await client.PostAsJsonAsync("OrderDetails", data);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }

                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                var a = e;
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ViewResult> Invoices(int id)
        {
            OrderDetails od = await GetbyID(id);
            int price = await GetPriceAsync(id);
            Dictionary<OrderDetails, int> orderPrice = new Dictionary<OrderDetails, int>();
            orderPrice.Add(od, price);

            return View("Invoices", orderPrice);
        }

        private async Task<int> GetPriceAsync(int id)
        {
            string functionsURI = _configuration.GetConnectionString("Functions");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(functionsURI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("ProductPrice?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    return int.Parse(response.Content.ReadAsStringAsync().Result);
                }
                return 0;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(string data)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    ConfigHttpClient(client);
                    HttpResponseMessage response = await client.PutAsJsonAsync("OrderDetails", data);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }

                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                var a = e;
                return BadRequest();
            }
        }
        public async Task<IActionResult> DeleteOrder(int id)
        {
            using (var client = new HttpClient())
            {
                ConfigHttpClient(client);
                HttpResponseMessage response = await client.DeleteAsync("OrderDetails/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }

                return BadRequest();
            }
        }

        private void ConfigHttpClient(HttpClient client)
        {

            client.BaseAddress = new Uri(_configuration.GetConnectionString("API"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
   
    }
}
