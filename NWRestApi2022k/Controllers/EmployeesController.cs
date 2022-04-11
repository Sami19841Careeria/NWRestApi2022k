using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NWRestApi2022k.Models;

namespace NWRestApi2022k.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private static readonly northwindContext db = new northwindContext();

        [HttpGet]
        public ActionResult GetAll()
        {
            var employees = db.Employees;
            return Ok(employees);
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOneEmployee(int id)
        {
            try
            {
                var employee = db.Employees.Find(id);

                if (employee == null)
                {
                    return BadRequest("Työntekijää id:llä " + id + " ei löytynyt.");
                }
                return Ok(employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //tämä poistaa työntekijän id:n perusteella
        [HttpDelete]
        [Route("{id}")]
        public ActionResult RemoveEmployee(int id)
        {
            var tyontekija = db.Employees.Find(id);
            if (tyontekija == null)
            {
                return NotFound("Työntekijää id:llä " + id + " ei löytynyt.");
            }
            else
            {
                try
                {
                    db.Employees.Remove(tyontekija);
                    db.SaveChanges();
                    return Ok("Poistettiin asiakas " + tyontekija.FirstName + " " + tyontekija.LastName);
                }
                catch (Exception)
                {
                    return BadRequest("Poisto ei onnistunut. Työntekijällä voi olla tilauksia hoidettavana.");
                }
            }
        }


        [HttpPost]
        public ActionResult PostCreateNew([FromBody] Employee tyontekija)
        
        {
            try
            {
                    db.Employees.Add(tyontekija);
                    db.SaveChanges();
                  
                    return Ok("Lisättiin asiakas " + tyontekija.FirstName + " " + tyontekija.LastName);
                
            }
            catch (Exception e)
            {
                return BadRequest("Työntekijän lisääminen ei onnistunut. Tässä lisätietoa: " + e);
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



        // Muokkaus
        [HttpPut]
        [Route("{id}")]
        public ActionResult PutEdit(int id, [FromBody] Employee employee)
        {

            try
            {
                var tyontekija = db.Employees.Find(id);
                if (tyontekija != null)
                {
                    tyontekija.FirstName = employee.FirstName;
                    tyontekija.LastName = employee.LastName;
                    tyontekija.Title = employee.Title;
                    tyontekija.TitleOfCourtesy = employee.TitleOfCourtesy;
                    tyontekija.BirthDate = employee.BirthDate;
                    tyontekija.HireDate = employee.HireDate;
                    tyontekija.Address = employee.Address;
                    tyontekija.City = employee.City;
                    tyontekija.Region = employee.Region;
                    tyontekija.PostalCode = employee.PostalCode;
                    tyontekija.Country = employee.Country;
                    tyontekija.HomePhone = employee.HomePhone;
                    tyontekija.Extension = employee.Extension;
                    tyontekija.Photo = employee.Photo;
                    tyontekija.Notes = employee.Notes;
                    tyontekija.ReportsTo = employee.ReportsTo;
                    tyontekija.PhotoPath = employee.PhotoPath;

                    db.SaveChanges();
                    return Ok("Muokattu työntekijää: " + tyontekija.FirstName + " " + tyontekija.LastName + ".");
                }
                else
                {
                    return NotFound("Päivitettävää työntekijää ei löytynyt.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen. Alla lisätietoa" + e);
            }

        }

      
    }
}
