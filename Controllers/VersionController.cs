using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApiAssignment1.BusinessLayer;
using WebApiAssignment1.Models.EntityLayer;

namespace WebApiAssignment1.Controllers
{
    public class VersionController : Controller
    {
        public async Task<ActionResult> Index()
        {
            Customer customer = new Customer();
            IEnumerable<Customer> cust = null;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:61680");
            try
            {
                var consumeData = await client.GetAsync("Api/Version1Api");
                if (consumeData.IsSuccessStatusCode)
                {
                    var results = await consumeData.Content.ReadAsAsync<IList<Customer>>();
                    cust = results;
                }
                else
                {
                    cust = Enumerable.Empty<Customer>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            catch (Exception)
            {
                cust = Enumerable.Empty<Customer>();
                ModelState.AddModelError(string.Empty, "Error connecting to the server.");
            }


            ViewBag.res = TempData["Message"];
            return View(cust);
        }

        public ActionResult InsertCust()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> InsertCust(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:61680");


            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

            HttpResponseMessage consumeData = await client.PostAsync("Api/Version1Api", content);

            if (consumeData.IsSuccessStatusCode)
            {
                string Message = await consumeData.Content.ReadAsStringAsync();
                TempData["Message"] = Message;
                return RedirectToAction("Index");
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Server error. Please contact the administrator.");
                return RedirectToAction("Index");
            }

        }

        public ActionResult UpdateCust(int id)
        {
            CustomerService customerService = new CustomerService();

            Customer cust = customerService.GetCustomerByID(id);

            TempData["ID"] = id;
            return View(cust);
        }

        [HttpPost]

        public async Task<ActionResult> UpdateCust(Customer customer)
        {
            customer.Id = Convert.ToInt32(TempData["ID"]);
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:61680");

            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

            HttpResponseMessage consumeData = await httpClient.PutAsync("Api/Version1Api", content);

            if (consumeData.IsSuccessStatusCode)
            {
                string Message = await consumeData.Content.ReadAsStringAsync();
                TempData["Message"] = Message;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact the administrator.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCust(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:61680/");
            HttpResponseMessage consumeData = await httpClient.DeleteAsync($"Api/Version2Api/{id}");

            if (consumeData.IsSuccessStatusCode)
            {
                string Message = await consumeData.Content.ReadAsStringAsync();
                TempData["Message"] = Message;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact the administrator.");
                return RedirectToAction("Index");
            }
        }
    }
}