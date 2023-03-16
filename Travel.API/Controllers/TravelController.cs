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
using Microsoft.Extensions.Caching.Memory;

namespace Travel.API.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TravelController : Controller
    {
        private const string journeyCacheKey = "journey";
        private readonly IFlightService _flightService;
        private readonly GeneralSettings _settings;
        private readonly IMapper _mapper;
        private IMemoryCache _cache;

        public TravelController(IFlightService flightService, IOptions<ApiSettings> settings, IMapper mapper, IMemoryCache cache)
        {
            _flightService = flightService;
            _settings = settings.Value.environmentVariables.GeneralSettings;
            _mapper = mapper;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpPost("travels")]
        public ActionResult<OperationResult> GetTravels([FromBody] OperationRequest Request, string typeRequest)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Consulta Viajes");

            if (Request != null)
            {
                if (_cache.TryGetValue(journeyCacheKey, out Journey journey))
                {
                    //Cache Exist journey
                }
                else
                {
                    //Create Cache  journey

                    journey = _flightService.GetFlight(Request, typeRequest, _settings.MaxJourneyFlights);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(journeyCacheKey, journey, cacheEntryOptions);
                }

                return Ok(journey);
            }
            else
            {
                return NoContent();
            }
        }

    }
}