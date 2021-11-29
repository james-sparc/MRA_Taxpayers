using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaxPayers.Core.Helpers;
using TaxPayers.Core.Models;
using TaxPayers.Core.ViewModels;
using TaxPayers.Data;

namespace TaxPayers.Controllers
{
    public class TaxpayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private JsonSerializer _serialiser = new JsonSerializer();
        private HttpClient httpClient;
       
        //base url
        private const string Url = "https://www.mra.mw/sandbox/programming/challenge/webservice/Taxpayers/";

       public HttpClient HttpClient => httpClient ?? (httpClient = new HttpClient());


        
        // GET: Taxpayers
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
                return RedirectToAction("Index", "Home");

            string username = HttpContext.Session.GetString("Username");
        
        //http headers
        HttpClient.DefaultRequestHeaders.Add("candidateid", username);
            HttpClient.DefaultRequestHeaders.Add("apikey", "3fdb48c5-336b-47f9-87e4-ae73b8036a1c");

            // getting data
            string responseData = await HttpClient.GetStringAsync(Url + "getAll");

            List<Taxpayer> taxpayers = JsonConvert.DeserializeObject<List<Taxpayer>>(responseData);
            var taxpayer = new Taxpayer();

            // viewbag to be used in vies
            ViewBag.taxpayers = taxpayers;

            return View(taxpayer);
        }
    }
}