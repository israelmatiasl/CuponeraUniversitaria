using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CuponeraUniversitaria.Infrastructure
{
    public interface IRepository<T>
    {
        Task<IList<T>> FindAll();
        Task<IList<T>> FindAll(Expression<Func<T, bool>> predicate);
        Task<IList<T>> FindAll(Expression<Func<T, bool>> predicate, PaginationOptions paginationOptions);
        Task<IList<T>> FindAll(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderby, PaginationOptions paginationOptions);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> FindById(string id);
        Task<T> FindOne(Expression<Func<T, bool>> predicate);
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task<long> Count(Expression<Func<T, bool>> predicate);
        Task<T> InsertOne(T entity);
        Task<T> UpdateOne(T entity);
        Task Delete(string id);
        Task<bool> DeleteOne(Expression<Func<T, bool>> predicate);
    }

    public class Repository<T> : IRepository<T> where T : IDocument
    {
        private IMongoDatabase database;
        private IMongoCollection<T> collection;
        public IClientSessionHandle session;

        public Repository(IClientSessionHandle session, IMongoDatabase database)
        {
            this.session = session;
            this.database = database;
            GetCollection();
        }

        private void GetCollection()
        {
            var entityType = typeof(T);
            var collectionNameAttribute = entityType.GetCustomAttributes(typeof(MongoCollectionAttribute), true).FirstOrDefault();

            if (collectionNameAttribute == null)
            {
                throw new InvalidOperationException();
            }

            var attribute = collectionNameAttribute as MongoCollectionAttribute;

            if (attribute == null)
            {
                throw new InvalidOperationException();
            }

            collection = database.GetCollection<T>(attribute.Name);
        }

        public async Task<IList<T>> FindAll()
        {
            return await collection.Find(session, _ => true).ToListAsync();
        }

        public async Task<IList<T>> FindAll(Expression<Func<T, bool>> predicate)
        {
            return await Find(predicate, null, null);
        }

        public async Task<IList<T>> FindAll(Expression<Func<T, bool>> predicate, PaginationOptions paginationOptions)
        {
            return await Find(predicate, null, paginationOptions);
        }

        public async Task<IList<T>> FindAll(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderby, PaginationOptions paginationOptions)
        {
            return await Find(predicate, orderby, paginationOptions);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await collection.Find(session, Builders<T>.Filter.Where(predicate)).FirstOrDefaultAsync();
        }

        public async Task<T> FindById(string id)
        {
            return await collection.Find(session, FilterDefinitionId(id)).FirstOrDefaultAsync();
        }

        public async Task<T> FindOne(Expression<Func<T, bool>> predicate)
        {
            return await collection.Find(session, Builders<T>.Filter.Where(predicate)).FirstOrDefaultAsync();
        }

        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await collection.Find(session, Builders<T>.Filter.Where(predicate)).AnyAsync();
        }

        public async Task<long> Count(Expression<Func<T, bool>> predicate)
        {
            return await collection.CountDocumentsAsync(session, Builders<T>.Filter.Where(predicate));
        }

        public async Task<T> InsertOne(T entity)
        {
            await collection.InsertOneAsync(session, entity);
            return entity;
        }

        public async Task<T> UpdateOne(T entity)
        {
            var filter = Builders<T>.Filter.Where(x => x.Id == entity.Id);

            var result = await collection.ReplaceOneAsync(session, filter, entity, new UpdateOptions() { IsUpsert = false });

            if (!result.IsAcknowledged)
            {
                throw new InvalidOperationException();
            }

            return entity;
        }

        public async Task Delete(string id)
        {
            var filter = Builders<T>.Filter.Where(x => x.Id == id);
            await collection.DeleteOneAsync(session, filter);
        }

        public async Task<bool> DeleteOne(Expression<Func<T, bool>> predicate)
        {
            var filter = Builders<T>.Filter.Where(predicate);
            var deleted = await collection.DeleteOneAsync(session, filter);

            return deleted.DeletedCount > 0;
        }

        private async Task<IList<T>> Find(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderby, PaginationOptions paginationOptions)
        {
            var query = collection.Find(session, Builders<T>.Filter.Where(predicate));

            if (orderby != null)
            {
                query = query.SortBy(orderby);
            }

            if (paginationOptions != null)
            {
                query = query.Skip(paginationOptions.Page * paginationOptions.PageSize).Limit(paginationOptions.PageSize);
            }

            return await query.ToListAsync();
        }

        private FilterDefinition<T> FilterDefinitionId(string id)
        {
            return Builders<T>.Filter.Where(x => x.Id == id);
        }
    }
}