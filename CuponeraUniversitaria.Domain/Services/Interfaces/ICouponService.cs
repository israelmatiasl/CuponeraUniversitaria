using System;
using System.Threading.Tasks;
using CuponeraUniversitaria.Domain.Dtos.Coupon;

namespace CuponeraUniversitaria.Domain.Services.Interfaces
{
    public interface ICouponService
    {
        Task<CouponResponseDto> GetCoupon(string couponId);
        Task<CouponResponseDto> SaveCoupon(CouponDto couponDto);
    }
}
