using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ParkApi.Models;
using ParkApi.Services;

namespace ParkApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(ReservationService reservationService, IReservationRepository reservationRepository)
        {
            _reservationService = reservationService;
            _reservationRepository = reservationRepository;
        }

        [Route("async")] 
        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetAsync([FromQuery]ReservationQuery query) {
            return await _reservationRepository.GetAllReservationsAsync(query);
        }


        [HttpGet]
        public  ActionResult<List<Reservation>> Get() {
            return _reservationService.Get();
        }


        [HttpGet("{id:length(24)}")]
        public ActionResult<Reservation> Get(string id)
        {
            var reservation = _reservationService.Get(id);

            if (reservation == null)
            {
                return NotFound();
            }
            return reservation;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Reservation reservation)
        {
            await _reservationRepository.Create(reservation);
            return new OkObjectResult(reservation);
        }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody]Reservation reservation)
    {
        var reservationFromDb = await _reservationRepository.GetReservation(id);
        if (reservationFromDb == null)
            return new NotFoundResult();
        reservation.Id = reservationFromDb.Id;
        await _reservationRepository.Update(reservation);
        return new OkObjectResult(reservation);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var reservationFromDb = await _reservationRepository.GetReservation(id);
        if (reservationFromDb == null)
            return new NotFoundResult();
        await _reservationRepository.Delete(id);
        return new OkResult();
    }

    }
}