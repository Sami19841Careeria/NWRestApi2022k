using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWRestApi2022k.Models;

namespace NWRestApi2022k.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private static readonly northwindContext db = new northwindContext();

        [HttpGet]
        public ActionResult GetAll()
        {
            var customers = db.Customers;
            return Ok(customers);
        }

        // Get 1 customer by id
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOneCustomer(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);

                if (asiakas == null) {
                    return NotFound("Asiakasta id:llä " + id + " ei löytynyt.");
                  }

                return Ok(asiakas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(string id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound("Asiakasta id:llä " + id + " ei löytynyt");
            }
            else
            {
                try
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();

                    return Ok("Poistettiin asiakas " + customer.CompanyName);
                }
                catch (Exception e)
                {
                    return BadRequest("Poisto ei onnistunut. Ongelma saattaa johtua siitä, jos asiakkaalla on tilauksia?");
                }
            }  
        }



    }
}
