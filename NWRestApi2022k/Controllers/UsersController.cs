using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWRestApi2022k.Models;

namespace NWRestApi2022k.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // private readonly northwindContext db = new northwindContext();

        // Dependency Injection tyyli
        private readonly northwindContext db;

        public UsersController(northwindContext dbparam)
        {
            db = dbparam;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var users = db.Users;


            foreach (var user in users)
            {
                user.Password = null;
            }
            return Ok(users);

        }

        // Uuden lisääminen
        [HttpPost]
        public ActionResult PostCreateNew([FromBody] User u)
        {
            try
            {
               
                db.Users.Add(u);
                db.SaveChanges();
                return Ok("Lisättiin käyttäjä " + u.Username);
            }
            catch (Exception e)
            {
                return BadRequest("Lisääminen ei onnistunut. Tässä lisätietoa: " + e);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound("Käyttäjää id:llä " + id + " ei löytynyt");
            }
            else
            {
                try
                {
                    db.Users.Remove(user);
                    db.SaveChanges();

                    return Ok("Removed user " + user.Username);
                }
                catch (Exception e)
                {
                    return BadRequest("Poisto ei onnistunut.");
                }
            }
        }

        //Muokkaus
        [HttpPut]
        [Route("{id}")]
        public ActionResult PutEdit(int id, [FromForm] User käyttäjä)
        {

            if (käyttäjä == null)
            {
                return BadRequest("Käyttäjä puuttuu pyynnön bodysta");
            }

            try
            {
                var user = db.Users.Find(id);

                if (user != null)
                {
                    user.Username = käyttäjä.Username;
                    user.AccesslevelId = käyttäjä.AccesslevelId;
                    user.Firstname = käyttäjä.Firstname;
                    user.Lastname = käyttäjä.Lastname;
                    user.Email = käyttäjä.Email;
                    
                    db.SaveChanges();
                    return Ok("Muokattu käyttäjää: " + käyttäjä.Username);
                }
                else
                {
                    return NotFound("Päivitettävää käyttäjää ei löytynyt!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen käyttäjää päivitettäessä. Alla lisätietoa" + e);
            }

        }
    }
}
