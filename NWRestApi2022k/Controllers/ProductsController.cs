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


        [HttpGet]
        [Route("catid/{cid}")]
        public ActionResult GetByCatId(int cid)
        {
            var p = db.Products.Where(p => p.CategoryId == cid);
            return Ok(p);
        }


        [HttpGet]
        [Route("cname/{cname}")]
        public ActionResult GetByCategoryName(string cname)
        {
           
            var products = (from p in db.Products join c in db.Categories on p.CategoryId equals c.CategoryId where c.CategoryName == cname
                    select p).ToList(); 

            return Ok(products);
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
