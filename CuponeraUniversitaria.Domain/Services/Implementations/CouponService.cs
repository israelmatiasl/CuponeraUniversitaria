using System;
using System.Threading.Tasks;
using AutoMapper;
using CuponeraUniversitaria.Domain.Dtos.Coupon;
using CuponeraUniversitaria.Domain.Entities;
using CuponeraUniversitaria.Domain.Resources;
using CuponeraUniversitaria.Domain.Services.Interfaces;

namespace CuponeraUniversitaria.Domain.Services.Implementations
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public CouponService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            this.UnitOfWork = UnitOfWork;
            this.Mapper = Mapper;
        }

        public async Task<CouponResponseDto> GetCoupon(string couponId)
        {
            if (string.IsNullOrEmpty(couponId))
            {
                throw new ArgumentNullException(nameof(couponId));
            }

            try
            {
                var coupon = await UnitOfWork.CouponRepository.FindById(couponId);

                return Mapper.Map<Coupon, CouponResponseDto>(coupon);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<CouponResponseDto> SaveCoupon(CouponDto couponDto)
        {
            if (couponDto is null)
            {
                throw new ArgumentNullException(nameof(CouponDto));
            }

            try
            {
                UnitOfWork.StartTransaction();

                var coupon = new Coupon(couponDto.AdvertisementId, couponDto.UserId);
                coupon.ExpiredAt = coupon.CreatedAt.AddHours(4);

                await UnitOfWork.CouponRepository.InsertOne(coupon);

                await UnitOfWork.CommitTransaction();

                return Mapper.Map<Coupon, CouponResponseDto>(coupon);
            }
            catch (Exception ex)
            {
                await UnitOfWork.RollBackTransaction();
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
