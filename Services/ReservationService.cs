using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using ParkApi.Models;

namespace ParkApi.Services
{
    public class ReservationService
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public ReservationService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("ParkDB"));
            var database = client.GetDatabase("ParkDB");
            _reservations = database.GetCollection<Reservation>("Reservation");
        }

        public List<Reservation> Get()
        {
            var x =  _reservations.Find(reservation => true).ToList();
            return x;
        }
        

        public Reservation Get(string id)
        {
            var docId = new ObjectId(id);

            return _reservations.Find<Reservation>(reservation => reservation.Id == docId).FirstOrDefault();
        }

        public Reservation Create(Reservation reservation)
        {
            _reservations.InsertOne(reservation);
            return reservation;
        }

        public void Update(string id, Reservation reservationIn)
        {
            var docId = new ObjectId(id);

            _reservations.ReplaceOne(reservation => reservation.Id == docId, reservationIn);
        }

        public void Remove(Reservation reservationIn) => 
        _reservations.DeleteOne(reservation => reservation.Id == reservationIn.Id);

        public void Remove(ObjectId id) => 
        _reservations.DeleteOne(reservation => reservation.Id == id);
    }
}