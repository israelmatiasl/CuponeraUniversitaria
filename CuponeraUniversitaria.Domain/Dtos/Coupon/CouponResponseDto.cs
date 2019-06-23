using System;
namespace CuponeraUniversitaria.Domain.Dtos.Coupon
{
    public class CouponResponseDto
    {
        public string CouponId { get; set; }

        public string Advertisement { get; set; }

        public string Code { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiredAt { get; set; }

        public string Status { get; set; }
    }
}
