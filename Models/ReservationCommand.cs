using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkApi.Models
{
    public class ReservationCommand
    {
         public ObjectId? Id;
        

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("price")]
        public decimal? Price { get; set; }

         [BsonElement("registrationNumber")]
        public string RegistrationNumber { get; set; }

        [BsonElement("dateFrom")]
          [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateFrom { get; set; }

         [BsonElement("dateTo")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DateTo { get; set; }

        [BsonElement("dateCreation")]
         [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [BsonElement("dateModification")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateModification { get; set; } = DateTime.Now;

        [BsonElement("annotation")]
        public string Annotation {get; set;}

         [BsonElement("placeType")]
        public PlaceType PlaceType {get; set;}

         [BsonElement("destination")]
        public string Destination {get; set;}

       
    }
}