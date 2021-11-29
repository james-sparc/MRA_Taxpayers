using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxPayers.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace TaxPayers.Controllers
{
    public class HomeController : Controller
    {
       

        private JsonSerializer _serialiser = new JsonSerializer();
        private HttpClient httpClient;

        private const string Url = "https://www.mra.mw/sandbox/programming/challenge/webservice/Taxpayers/";

        public HttpClient HttpClient => httpClient ?? (httpClient = new HttpClient());


        public IActionResult Index()
        {

            return View("Login"); ;
        }

        public async Task<IActionResult> Home()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
                return RedirectToAction("Index", "Home");

            string username = HttpContext.Session.GetString("Username");

            HttpClient.DefaultRequestHeaders.Add("candidateid", username);

            HttpClient.DefaultRequestHeaders.Add("apikey", "3fdb48c5-336b-47f9-87e4-ae73b8036a1c");

            var responseData = await HttpClient.GetStringAsync(Url + "getAll");

            List<Taxpayer> taxpayers = JsonConvert.DeserializeObject<List<Taxpayer>>(responseData);          
          
            ViewBag.taxpayers = taxpayers.Count;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            string Url = "https://www.mra.mw/sandbox/programming/challenge/webservice/auth/logout";
            var taxpayer = new Taxpayer()
            {
                Email = HttpContext.Session.GetString("Username")
            };
            string username = HttpContext.Session.GetString("Username");

            HttpClient.DefaultRequestHeaders.Add("candidateid", username);
            HttpClient.DefaultRequestHeaders.Add("apikey", "3fdb48c5-336b-47f9-87e4-ae73b8036a1c");

            var request = new StringContent(JsonConvert.SerializeObject(taxpayer), Encoding.UTF8, "application/json");
            var result = await HttpClient.PostAsync(Url, request);

            string data = await result.Content.ReadAsStringAsync();

            dynamic parsedData = JObject.Parse(data);

            int resultCode = parsedData["ResultCode"];

            if (result.IsSuccessStatusCode && resultCode == 1)
            {

                // serializing the data received               
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
            
        }
    }
       
}
