using MongoDB.Driver;
using ParkApi.Models;

namespace ParkApi
{
   public interface IReservationContext
{
    IMongoCollection<Reservation> Reservations { get; }
}
}