using System;
using CuponeraUniversitaria.Domain.Entities;
using CuponeraUniversitaria.Infrastructure;
using MongoDB.Driver;

namespace CuponeraUniversitaria.Domain.Resources
{
    public interface IUnitOfWork
    {
        //All Repositories Entities
        IRepository<Advertisement> AdvertisementRepository { get; }
        IRepository<Coupon> CouponRepository { get; }

        void StartTransaction();
        System.Threading.Tasks.Task CommitTransaction();
        System.Threading.Tasks.Task RollBackTransaction();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private IMongoDatabase database;
        private IClientSessionHandle session;

        private IRepository<Advertisement> _advertisementRepository;
        private IRepository<Coupon> _couponRepository;

        public UnitOfWork()
        {
            GetDatabase();
        }

        private void GetDatabase()
        {
            var client = new MongoClient("mongodb+srv://usradmin:98714731Iml7.@rumidevelopment-y9n6w.mongodb.net/test?retryWrites=true");

            if (session == null)
            {
                session = client.StartSession();
            }
            database = session.Client.GetDatabase("AdvertisementDb");
        }

        public IRepository<Advertisement> AdvertisementRepository { get { return _advertisementRepository = new Repository<Advertisement>(session, database); } }

        public IRepository<Coupon> CouponRepository { get { return _couponRepository = new Repository<Coupon>(session, database); } }


        public void StartTransaction()
        {
            session.StartTransaction();
        }

        public async System.Threading.Tasks.Task CommitTransaction()
        {
            await session.CommitTransactionAsync();
        }

        public async System.Threading.Tasks.Task RollBackTransaction()
        {
            await session.AbortTransactionAsync();
        }

    }
}
