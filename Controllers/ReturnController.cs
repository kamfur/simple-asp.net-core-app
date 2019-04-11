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
    public class ReturnController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReturnController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> Get([FromQuery]ReservationQuery query) {
            return await _reservationRepository.GetAllReturnsAsync(query);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Reservation>> Get(string id)
        {
            var reservation = await _reservationRepository.GetReservation(id);

            if (reservation == null)
            {
                return NotFound();
            }
            return reservation;
        }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody]Reservation reservation)
    {
        var reservationFromDb = await _reservationRepository.GetReservation(id);
        if (reservationFromDb == null)
            return new NotFoundResult();
        reservation.Id = reservationFromDb.Id;
        await _reservationRepository.UpdateReturn(reservationFromDb);
        return new OkObjectResult(reservationFromDb);
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