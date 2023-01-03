using ApiGeo.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace ApiGeo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GeolocatorController : ControllerBase
    {
        private readonly IGeolocatorManager services;
        public GeolocatorController(IGeolocatorManager services)
        {
            this.services = services;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var status = new { status = "OK todos los container UP" };
            return Ok(status);
        }

        [HttpPost, Route("geolocalizar")]
        public async Task<ActionResult> Geolocate(GeolocateRequest request)
        {
            var response = await services.Geolocate(request);
            return response.Code != 500 ? StatusCode(StatusCodes.Status202Accepted, response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpGet, Route("geocodificar")]
        public async Task<ActionResult> Geocode(int id)
        {
            var response = await services.Geocode(id);
            return response.Code != 500 ? StatusCode(StatusCodes.Status202Accepted, response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
