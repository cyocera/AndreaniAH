using Geocodificador.Managers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Plain.RabbitMQ;
using System.Threading.Tasks;

namespace Geocodificador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirecctionProcessController : ControllerBase
    {
        private readonly IGeoProcessorManager services;
        private readonly IPublisher publisher;
        public DirecctionProcessController(IGeoProcessorManager services, IPublisher publisher)
        {
            this.services = services;
            this.publisher = publisher;
        }

        [HttpPost, Route("process")]
        public async Task<ActionResult> ProcessData(GeoProcesorResquest request)
        {
            var response = await services.ProcessData(request);
            publisher.Publish(JsonConvert.SerializeObject(response), "report.direction", null);
            return Ok();
        }
    }
}
