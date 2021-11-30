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
            // show login page
            return View("Login"); ;
        }

        public async Task<IActionResult> Home()
        {
            // check if session is set, if not, show thw login page again
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
                return RedirectToAction("Index", "Home");

            string username = HttpContext.Session.GetString("Username");

            HttpClient.DefaultRequestHeaders.Add("candidateid", username);

            HttpClient.DefaultRequestHeaders.Add("apikey", "3fdb48c5-336b-47f9-87e4-ae73b8036a1c");

            var responseData = await HttpClient.GetStringAsync(Url + "getAll");

            List<Taxpayer> taxpayers = JsonConvert.DeserializeObject<List<Taxpayer>>(responseData);          
          
            // gatting taxpayers total count
            ViewBag.taxpayers = taxpayers.Count;
            return View();
        }
      
    }
       
}
