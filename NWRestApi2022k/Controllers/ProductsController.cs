using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWRestApi2022k.Models;

namespace NWRestApi2022k.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private static readonly northwindContext db = new northwindContext();

        [HttpGet]
        public ActionResult GetAll()
        {
            var p = db.Products;
            return Ok(p);
        }

        // Uuden lisääminen
        [HttpPost]
        public ActionResult PostCreateNew([FromBody] Product prod)
        {
            try
            {
                db.Products.Add(prod);
                db.SaveChanges();
                return Ok("Lisättiin tuote " + prod.ProductName);
            }
            catch (Exception e)
            {
                return BadRequest("Virhe, tässä lisätietoa: " + e);
            }
        }




    }
}
