using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkApi.Models
{
    public class Reservation
    {
         public ObjectId Id;
        

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("price")]
        public decimal? Price { get; set; }

         [BsonElement("registrationNumber")]
        public string RegistrationNumber { get; set; }

        [BsonElement("dateFrom")]
          [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateFrom { get; set; }

         [BsonElement("dateTo")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? DateTo { get; set; }

        [BsonElement("dateCreation")]
         [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [BsonElement("dateModification")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateModification { get; set; } = DateTime.Now;

        [BsonElement("annotation")]
        public string Annotation {get; set;}

         [BsonElement("placeType")]
        public PlaceType PlaceType {get; set;}

         [BsonElement("destination")]
        public string Destination {get; set;}

        [BsonElement("dateArrival")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateArrival{ get; set; }

        [BsonElement("isArrived")]
        public bool IsArrived {get; set;}

        [BsonElement("dateReturns")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateReturns{ get; set; }

        [BsonElement("isReturned")]
        public bool IsReturned {get; set;}
    }
}