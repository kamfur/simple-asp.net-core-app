using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using ParkApi.Models;

namespace ParkApi
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllReservationsAsync(ReservationQuery query);

        Task<List<Reservation>> GetAllArrivalsAsync(ReservationQuery query);

        Task<List<Reservation>> GetAllReturnsAsync(ReservationQuery query);
        Task<Reservation> GetReservation(string Id);

        Task<bool> UpdateArrival(Reservation reservation);

        Task<bool> UpdateReturn(Reservation reservation);
        
        Task Create(Reservation reservation);
        Task<bool> Update(Reservation reservation);
        Task<bool> Delete(string Id);

    }
}