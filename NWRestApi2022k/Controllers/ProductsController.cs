﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWRestApi2022k.Models;

namespace NWRestApi2022k.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        //private readonly northwindContext db = new northwindContext();

        //Dependency Injection toteutustapa
        private readonly northwindContext db;

        public ProductsController(northwindContext dbparam)
        {
            db = dbparam;
        }


        //[HttpGet]
        //public List<Product> GetAll()
        //{
        //    var p = db.Products;
        //    return p.ToList();
        //}


        [HttpGet]
        public ActionResult GetAllz()
        {
            var p = db.Products;

            return Ok(p.ToList());

        }


        [HttpGet]
        [Route("special/{productName}")]
        public ActionResult GetSpecialData(string productName)
        {

                            var prod = (from p in db.Products
                                        where p.ProductName.ToLower().Contains(productName.ToLower()) select p).FirstOrDefault();

                            var cat = (from c in db.Categories where c.CategoryId == prod.CategoryId select c).FirstOrDefault();

                            var sup = (from s in db.Suppliers where s.SupplierId == prod.SupplierId select s).FirstOrDefault();

                            List<ProductData> pdata = new List<ProductData>()
                             {
                                new ProductData
                                    {
                                            Id = prod.ProductId,
                                            ProductName = prod.ProductName,
                                            SupplierName = sup.CompanyName,
                                            CategoryName = cat.CategoryName,
                                    }
                             };

            return Ok(pdata);
        }



        [HttpGet]
        [Route("catid/{cid}")]
        public ActionResult GetByCatId(int cid)
        {
            var p = db.Products.Where(p => p.CategoryId == cid);
            return Ok(p);
        }


        // Haku categorian nimellä. esim. https://localhost:44327/api/Products/cname/seafood
        [HttpGet]
        [Route("cname/{cname}")]
        public ActionResult GetByCategoryName(string cname)
        {
           
            var products = (from p in db.Products join c in db.Categories on p.CategoryId equals c.CategoryId where c.CategoryName == cname
                    select p).ToList(); 

            return Ok(products);
        }


        // Haku hinnan mukaan
        [HttpGet]
        [Route("min-price/{min}/max-price/{max}")]
        public ActionResult GetByPrice(decimal min, decimal max)
        {
            var p = db.Products.Where(p => p.UnitPrice >= min && p.UnitPrice <= max);
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

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound("Tuotetta id:llä " + id + " ei löytynyt");
            }
            else
            {
                try
                {
                    db.Products.Remove(product);
                    db.SaveChanges();

                    return Ok("Removed product " + product.ProductName);
                }
                catch (Exception e)
                {
                    return BadRequest("Poisto ei onnistunut.");
                }
            }
        }

        [HttpPut]
        [Route("{key}")]
        public ActionResult PutEdit(int key, [FromBody] Product tuote)
        {

            if (tuote == null)
            {
                return BadRequest("Tuote puuttuu listalta.");
            }

            try
            {
                var product = db.Products.Find(key);

                if (product != null)
                {
                    product.ProductName = tuote.ProductName;
                    product.SupplierId = tuote.SupplierId;
                    product.CategoryId = tuote.CategoryId;
                    product.QuantityPerUnit = tuote.QuantityPerUnit;
                    product.UnitPrice = tuote.UnitPrice;
                    product.UnitsInStock = tuote.UnitsInStock;
                    product.Discontinued = tuote.Discontinued;

                    db.SaveChanges();
                    return Ok("Muokattu tuotetta: " + tuote.ProductName);
                }
                else
                {
                    return NotFound("Päivitettävää tuotetta ei löytynyt!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen tuotetta päivitettäessä. Alla lisätietoa" + e);
            }

        }
        //tämä luo uuden vaikka antaa id:n
        //[HttpPost]
        //public ActionResult PostCreateNew([FromBody] Employee tyontekija)
        //{
        //    try
        //    {
        //        using (var transaction = db.Database.BeginTransaction())
        //        {
        //            db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Employees ON;");
        //            db.Employees.Add(tyontekija);
        //            db.SaveChanges();
        //            db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Employees OFF;");
        //            transaction.Commit();
        //            return Ok("Lisättiin asiakas " + tyontekija.FirstName + " " + tyontekija.LastName);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest("Työntekijän lisääminen ei onnistunut. Tässä lisätietoa: " + e);
        //    }
        //}
    }
}
