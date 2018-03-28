using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace budget.DB
{
    public class DBManager<T>
    {
        private MongoClient _client;
        private IMongoDatabase _db;
        private string _collectionName;

        public DBManager(string connectionString, string databaseName, string collectionName)
        {
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase(databaseName);
            _collectionName = collectionName;
        }

        public T Create(T entity)
        {
            _db.GetCollection<T>(_collectionName).InsertOne(entity);

            return entity;
        }

        public List<T> Create(List<T> entities)
        {
            _db.GetCollection<T>(_collectionName).InsertMany(entities);

            return entities;
        }

        public IEnumerable<T> RetrieveAll(Expression<Func<T, bool>> filterDefinition)
        {
            return _db.GetCollection<T>(_collectionName).Find(filterDefinition).ToEnumerable();
        }

        public T RetrieveOne(Expression<Func<T, bool>> filterDefinition)
        {
            return _db.GetCollection<T>(_collectionName).Find(filterDefinition).FirstOrDefault();
        }

        public T Update(Expression<Func<T, bool>> filterDefinition, T entity)
        {
            _db.GetCollection<T>(_collectionName).ReplaceOne(filterDefinition, entity);

            return entity;
        }

        public void Delete(Expression<Func<T, bool>> filterDefinition)
        {
            _db.GetCollection<T>(_collectionName).DeleteOne(filterDefinition);
        }

        public void DeleteMany(Expression<Func<T, bool>> filterDefinition)
        {
            _db.GetCollection<T>(_collectionName).DeleteMany(filterDefinition);
        }
    }
}