using Microsoft.AspNetCore.Mvc;
using Travel.Business.Entities;
using Travel.Business.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Options;
using Travel.Business.Configuration;
using System.Threading.Tasks;
using Travel.DataAccess.DTO;
using AutoMapper;
using System.Linq;

namespace Travel.API.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TravelController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly GeneralSettings _settings;
        private readonly IMapper _mapper;

        public TravelController(IFlightService flightService, IOptions<ApiSettings> settings, IMapper mapper)
        {
            _flightService = flightService;
            _settings = settings.Value.environmentVariables.GeneralSettings;
            _mapper = mapper;
        }

        [HttpPost("travels")]
        public ActionResult<OperationResult> GetTravels([FromBody] OperationRequest Request, string type)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Consulta Viajes");

            if (Request != null)
            {
                return Ok(_flightService.GetFlight(Request, type, _settings.MaxJourneyFlights));
            }
            else
            {
                return NoContent();
            }
        }

    }
}