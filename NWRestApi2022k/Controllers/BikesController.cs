using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace NWRestApi2022k.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class BikesController : ControllerBase
    {

        [HttpGet]
        public ActionResult getBikes()
        {

            List<Bike> bikes = new List<Bike>()
        {
        new Bike()
        {
        Id = 1,
        Make = "Yamaha",
        Yearmodel = 2019
        },
        new Bike()
        {
        Id = 2,
        Make = "Suzuki",
        Yearmodel = 2019
        },
        new Bike()
        {
        Id = 3,
        Make = "Kawasaki",
        Yearmodel = 2020
        }
        };

            return Ok(bikes);

        }
    }
}
