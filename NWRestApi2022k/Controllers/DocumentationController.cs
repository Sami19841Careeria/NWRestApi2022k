using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWRestApi2022k.Models;

namespace NWRestApi2022k.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {
        private static readonly northwindContext db = new northwindContext();

        [HttpGet]
        [Route("{keycode}")]
        public ActionResult GetAll(string keycode)
        {
            var paths = db.Documentations.Where(d => d.Keycode == keycode);
            return Ok(paths);

        }

    }
}
