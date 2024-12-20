using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAssignment1.BusinessLayer;
using WebApiAssignment1.Models.EntityLayer;

namespace WebApiAssignment1.Controllers
{
    public class CustomerController : ApiController
    {

        CustomerService customerService = new CustomerService();

        [HttpPost]
        public IHttpActionResult InsertCustomer([FromBody] Customer customer)
        {
            string res = customerService.InsertCust(customer);

            if (res == "Data has been successfully inserted")
            {
                return Ok(res);
            }
            else
            {
                var responce = Request.CreateErrorResponse(HttpStatusCode.NotFound,"Data cannot be inserted");
                return ResponseMessage(responce);
            }
        }

        [HttpGet]

        public IHttpActionResult GetAllCustomers() 
        {

            List<Customer> customers = customerService.GetAllCust();
            
            if(customers.Count>=1)
            {
                return Ok(customers);
            }
            else
            {
                var responce = Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Records Found");
                return ResponseMessage(responce);
            }
        }

        [HttpGet]

        public IHttpActionResult FetchCustomer(int id)
        {
            Customer customer = customerService.GetCustomerByID(id);

            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                var responce = Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Records Found");
                return ResponseMessage(responce);
            }
        }


        [HttpPut]

        public IHttpActionResult UpdateCustomerInfo([FromBody] Customer customer)
        {
            string res = customerService.UpdateCustInfo(customer);

            if (res == "Customer information updated successfully")
            {
                return Ok(res);
            }
            else
            {
                var responce = Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Data cannot be updated due to internal error");
                return ResponseMessage(responce);
            }
        }

        [HttpDelete]

        public IHttpActionResult DeleteCustomer(int id)
        {
            string res = customerService.DeleteCust(id);

            if (res == "Customer successfully removed")
            {
                return Ok(res);
            }
            else
            {
                var responce = Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Data cannot be deleted due to invalid id");
                return ResponseMessage(responce);
            }
        }
    }
}