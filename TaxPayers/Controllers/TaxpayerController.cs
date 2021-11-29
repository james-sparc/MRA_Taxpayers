using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaxPayers.Core.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaxPayers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxpayerController : ControllerBase
    {

        private JsonSerializer _serialiser = new JsonSerializer();
        private HttpClient httpClient;

        private const string Url = "https://www.mra.mw/sandbox/programming/challenge/webservice/Taxpayers/";

        public HttpClient HttpClient => httpClient ?? (httpClient = new HttpClient());      


        // GET: api/<TaxpayerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string username = HttpContext.Session.GetString("Username");

            HttpClient.DefaultRequestHeaders.Add("candidateid", username);

            httpClient.DefaultRequestHeaders.Add("apikey", "3fdb48c5-336b-47f9-87e4-ae73b8036a1c");

            string responseData = await HttpClient.GetStringAsync(Url + "getAll");

            List<Taxpayer> taxpayers = JsonConvert.DeserializeObject<List<Taxpayer>>(responseData);

            return Ok(taxpayers);
        }

        // GET api/<TaxpayerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TaxpayerController>
        [HttpPost]        
        public async Task<IActionResult> Post(Taxpayer taxpayer)
        {
            //checking if form is filled
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return BadRequest(messages);
            }

            try
            {
                string username = HttpContext.Session.GetString("Username");

                HttpClient.DefaultRequestHeaders.Add("candidateid", username);

                HttpClient.DefaultRequestHeaders.Add("apikey", "3fdb48c5-336b-47f9-87e4-ae73b8036a1c");

                taxpayer.Username = "mjuweni@outlook.com";
                var request = new StringContent(JsonConvert.SerializeObject(taxpayer), Encoding.UTF8, "application/json");
                var result = await HttpClient.PostAsync(Url+"add", request);

                string data = await result.Content.ReadAsStringAsync();

                var payer = new Taxpayer();

                if (result.IsSuccessStatusCode)
                {
                    // serilizing the data receied
                    payer = JsonConvert.DeserializeObject<Taxpayer>(data);

                    return Ok(payer);
                }
                else
                {
                    return BadRequest(data);
                }
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }

        // PUT api/<TaxpayerController>/5       
        [HttpPut("{tpin}")]
        public async Task<IActionResult> Put(string tpin)
        {

            /*/checking if form is filled
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return BadRequest(messages);
            } */
            try
            {
                var forData = HttpContext.Request.Form;
                // header
                string username = HttpContext.Session.GetString("Username");

                var taxpayer = new Taxpayer();
                //Taxpayer taxpayey
                HttpClient.DefaultRequestHeaders.Add("candidateid", username);
                HttpClient.DefaultRequestHeaders.Add("apikey", "3fdb48c5-336b-47f9-87e4-ae73b8036a1c");

                taxpayer.Username = username;
                var request = new StringContent(JsonConvert.SerializeObject(taxpayer), Encoding.UTF8, "application/json");
                var result = await HttpClient.PostAsync(Url + "edit", request);

                string data = await result.Content.ReadAsStringAsync();

                var payer = new Taxpayer();

                if (result.IsSuccessStatusCode)
                {
                    // serializing the data receied
                    payer = JsonConvert.DeserializeObject<Taxpayer>(data);

                    return Ok(payer);
                }
                else
                {
                    return NotFound(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE api/<TaxpayerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string TPIN)
        {
            var taxpayer = new Taxpayer()
            {
                TPIN = TPIN
            };
            string username = HttpContext.Session.GetString("Username");

            HttpClient.DefaultRequestHeaders.Add("candidateid", username);
            HttpClient.DefaultRequestHeaders.Add("apikey", "3fdb48c5-336b-47f9-87e4-ae73b8036a1c");

            var request = new StringContent(JsonConvert.SerializeObject(taxpayer), Encoding.UTF8, "application/json");
            var result = await HttpClient.PostAsync(Url + "delete", request);

            string data = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                // serializing the data receied               

                return Ok(data);
            }
            else
            {
                return NotFound(data);
            }

        }
    }
}
