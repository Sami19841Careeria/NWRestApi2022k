using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWRestApi2022k.Services.Interfaces;

namespace NWRestApi2022k.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticateService _authenticateService;

        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Models.User model)
        {
            var user = _authenticateService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Käyttäjätunus tai salasana on virheellinen" });

            return Ok(user); // Palautus front endiin
        }
    }
}
