using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CuponeraUniversitaria.Domain.Dtos.Advertisement;

namespace CuponeraUniversitaria.Domain.Services.Interfaces
{
    public interface IAdvertisementService
    {
        Task<IEnumerable<AdvertisementResponseDto>> GetAdvertisements();
        Task<AdvertisementResponseDto> GetAdvertisement(string advertisementId);
        Task<AdvertisementResponseDto> SaveAdvertisement(AdvertisementDto advertisementDto);
        Task<AdvertisementResponseDto> UpdateAdvertisement(string advertisementId, AdvertisementDto advertisementDto);
        Task<bool> DeleteAdvertisement(string advertisementId);
    }
}