using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWRestApi2022k.Models;

namespace NWRestApi2022k.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {
        // private readonly northwindContext db = new northwindContext();

        // Dependency Injection tyyli
        private readonly northwindContext db;

        public DocumentationController(northwindContext dbparam)
        {
            db = dbparam;
        }

        [HttpGet]
        [Route("{keycode}")]
        public ActionResult GetAll(string keycode)
        {
            var paths = db.Documentations.Where(d => d.Keycode == keycode);
            return Ok(paths);

        }

    }
}
