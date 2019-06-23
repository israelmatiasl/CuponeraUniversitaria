using System;
using AutoMapper;
using CuponeraUniversitaria.Domain.Dtos.Advertisement;
using CuponeraUniversitaria.Domain.Dtos.Coupon;
using CuponeraUniversitaria.Domain.Entities;

namespace CuponeraUniversitaria.Domain.Resources
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Advertisement, AdvertisementResponseDto>()
                .ForMember(a => a.AdvertisementId, ar => ar.MapFrom(a => a.Id));

            CreateMap<Coupon, CouponResponseDto>()
                .ForMember(c => c.CouponId, cr => cr.MapFrom(c => c.Id));
        }
    }
}
