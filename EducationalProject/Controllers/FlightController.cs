using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalProject.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
           _flightService = flightService;
        }

        [HttpGet]
        public string GetAsync()
        {
            return _flightService.Get();
        }

    }
}
