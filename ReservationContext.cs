using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ParkApi;
using ParkApi.Models;

public class ReservationContext : IReservationContext
{
    private readonly IMongoDatabase _db;
    public ReservationContext(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("ParkDB"));
         _db = client.GetDatabase("ParkDB");
    }
    public IMongoCollection<Reservation> Reservations => _db.GetCollection<Reservation>("Reservation");
}