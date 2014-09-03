using MongoDB.Bson;
using System;

namespace FinanceProjector.Interfaces
{
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}
