using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceProjector.Interfaces;
using System.Linq.Expressions;

namespace FinanceProjector.Repository
{
    public class MongoRepository<T> : IQueryable<T> where T : IMongoEntity
    {
        private MongoCollection<T> _collection;
        private string _connectionString = "mongodb://localhost"; //move to config
        private MongoClient _client;
        private MongoServer _server;
        private MongoDatabase _database;
        private string _collectionName;

        public MongoRepository()
        {
            _client = new MongoClient(_connectionString);
            _server = _client.GetServer();
            _database = _server.GetDatabase("FinanceProjector"); // how to get?
            _collectionName = ElementType.Name;
            _collection = _database.GetCollection<T>(_collectionName);
        }

        public T GetById(ObjectId id)
        {
            return _collection.FindOneByIdAs<T>(id);
        }

        public void Save(T item)
        {
            _collection.Save(item);
        }

        public void InsertBatch(IEnumerable<T> items)
        {
            _collection.InsertBatch<T>(items);
        }

        public void Delete(ObjectId id)
        {
            var query = Query<T>.EQ(e => e.Id, id);
            _collection.Remove(query);
        }

        public void Delete(T item)
        {
            Delete(item.Id);
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            foreach (T item in _collection.AsQueryable<T>().Where(predicate))
            {
                Delete(item);
            }
        }

        public void DeleteAll()
        {
            _collection.RemoveAll();
        }

        public long Count()
        {
            return _collection.Count();
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _collection.AsQueryable<T>().Any(predicate);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.AsQueryable<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _collection.AsQueryable<T>().GetEnumerator();
        }

        public Expression Expression
        {
            get { return _collection.AsQueryable<T>().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _collection.AsQueryable<T>().Provider; }
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }
    }
}
