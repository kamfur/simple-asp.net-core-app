using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ParkApi.Models;

namespace ParkApi
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IReservationContext _context;
        public ReservationRepository(IReservationContext context)
        {
            _context = context;
        }
        public Task Create(Reservation reservation)
        {
            return _context.Reservations.InsertOneAsync(reservation);
        }

        public async Task<bool> Delete(string Id)
        {
             var objectId  = new ObjectId(Id);
            FilterDefinition<Reservation> filter = Builders<Reservation>.Filter.Eq(m => m.Id, objectId);
            DeleteResult deleteResult = await _context
                                                .Reservations
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync(ReservationQuery query)
        {
            var bson =  query.name != null ? BsonDocument.Parse(query.name) : new BsonDocument();
            return await _context.Reservations
                        .Find(bson)
                        .SortByDescending(res => res.Id)
                        .ToListAsync();
        }
        public async Task<List<Reservation>> GetAllArrivalsAsync(ReservationQuery query)
        {
            var bson = query.name != null ? BsonDocument.Parse(query.name) : new BsonDocument();
            return await _context.Reservations
                        .Find(ArrivedCars())
                        .SortBy(res => res.DateFrom)
                        .ToListAsync();
        }

        private static System.Linq.Expressions.Expression<Func<Reservation, bool>> ArrivedCars() 
        => x => x.DateFrom > DateTime.Now.AddDays(-1) && x.IsArrived == false;

        private static System.Linq.Expressions.Expression<Func<Reservation, bool>> ReturnsCar() 
        => x => x.DateTo > DateTime.Now.AddDays(-1) && x.IsArrived == true && x.IsReturned==false;


        public async Task<List<Reservation>> GetAllReturnsAsync(ReservationQuery query)
        {
              var bson = query.name != null ? BsonDocument.Parse(query.name) : new BsonDocument();
            return await _context.Reservations
                        .Find(ReturnsCar())
                        .SortBy(res => res.DateTo)
                        .ToListAsync();
        }
        public async Task<Reservation> GetReservation(string Id)
        {
            var objectId  = new ObjectId(Id);
            FilterDefinition<Reservation> filter = Builders<Reservation>.Filter.Eq(m => m.Id, objectId);
            return await _context
                    .Reservations
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Reservation reservation)
        {
            ReplaceOneResult updateResult =
            await _context
                    .Reservations
                    .ReplaceOneAsync(
                        filter: g => g.Id == reservation.Id,
                        replacement: reservation);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> UpdateArrival(Reservation reservation)
        {
            SetArrival(reservation);
            ReplaceOneResult updateResult =
            await _context
                    .Reservations
                    .ReplaceOneAsync(
                        filter: g => g.Id == reservation.Id,
                        replacement: reservation);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> UpdateReturn(Reservation reservation)
        {
            SetReturn(reservation);
            ReplaceOneResult updateResult =
            await _context
                    .Reservations
                    .ReplaceOneAsync(
                        filter: g => g.Id == reservation.Id,
                        replacement: reservation);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        private static void SetArrival(Reservation reservation)
        {
            reservation.IsArrived = true;
            reservation.DateArrival = DateTime.Now;
        }

        private static void SetReturn(Reservation reservation)
        {
            reservation.IsReturned = true;
            reservation.DateReturns = DateTime.Now;
        }

      
    }
}