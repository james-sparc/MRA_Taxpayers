using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxPayers.Core.Models;
using TaxPayers.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace TaxPayers.Controllers
{
    public class AccountController : Controller
    {       
        private JsonSerializer _serialiser = new JsonSerializer();
        private HttpClient httpClient;

        private const string Url = "https://www.mra.mw/sandbox/programming/challenge/webservice/auth/";

        public HttpClient HttpClient => httpClient ?? (httpClient = new HttpClient());


        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)

        {

            var keyValues = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Email",email),
                    new KeyValuePair<string, string>("Password",password)
                };
            var appUser = new ApplicationUser()
            {
                Email = email,
                Password = password
            };

            var request = new StringContent(JsonConvert.SerializeObject(appUser), Encoding.UTF8, "application/json");
            var result = await HttpClient.PostAsync(Url + "login", request);

            var data = await result.Content.ReadAsStringAsync();

            dynamic parsedData = JObject.Parse(data);

            var resultCode = parsedData["Token"];

            if (result.IsSuccessStatusCode && resultCode != null)
            {
                
                
                var UserData = parsedData.UserDetails;
                string username = UserData.Username;
                string lastname = UserData.LastName;
                string firstname = UserData.FirstName;
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("Firstname", firstname);
                HttpContext.Session.SetString("Lastname", lastname);

                return RedirectToAction("Home", "Home");
            }
            else
            {
                //Redirect to login page                
                return RedirectToAction("Index", "Home");
            }
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
