using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CuponeraUniversitaria.Domain.Dtos.Advertisement;
using CuponeraUniversitaria.Domain.Entities;
using CuponeraUniversitaria.Domain.Resources;
using CuponeraUniversitaria.Domain.Services.Interfaces;

namespace CuponeraUniversitaria.Domain.Services.Implementations
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public AdvertisementService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            this.UnitOfWork = UnitOfWork;
            this.Mapper = Mapper;
        }

        public async Task<bool> DeleteAdvertisement(string advertisementId)
        {
            if (string.IsNullOrEmpty(advertisementId))
            {
                throw new ArgumentNullException(nameof(advertisementId));
            }

            try
            {
                UnitOfWork.StartTransaction();

                var result = await UnitOfWork.AdvertisementRepository.DeleteOne(x => x.Id == advertisementId);

                if (result)
                {
                    await UnitOfWork.CommitTransaction();
                }

                return result;
            }
            catch(Exception ex)
            {
                await UnitOfWork.RollBackTransaction();
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<AdvertisementResponseDto> GetAdvertisement(string advertisementId)
        {
            if (string.IsNullOrEmpty(advertisementId))
            {
                throw new ArgumentNullException(nameof(advertisementId));
            }

            try
            {
                var advertisement = await UnitOfWork.AdvertisementRepository.FindById(advertisementId);

                return Mapper.Map<Advertisement, AdvertisementResponseDto>(advertisement);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<AdvertisementResponseDto>> GetAdvertisements()
        {
            try
            {
                var advertisements = await UnitOfWork.AdvertisementRepository.FindAll();

                return Mapper.Map<IEnumerable<Advertisement>, IEnumerable<AdvertisementResponseDto>>(advertisements);
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<AdvertisementResponseDto> SaveAdvertisement(AdvertisementDto advertisementDto)
        {
            if (advertisementDto is null)
            {
                throw new ArgumentNullException(nameof(AdvertisementDto));
            }

            try
            {
                UnitOfWork.StartTransaction();

                var advertisement = new Advertisement(advertisementDto.UniversityId, advertisementDto.CompanyId,
                                                      advertisementDto.Max, advertisementDto.Unlimited);

                await UnitOfWork.AdvertisementRepository.InsertOne(advertisement);

                await UnitOfWork.CommitTransaction();

                return Mapper.Map<Advertisement, AdvertisementResponseDto>(advertisement);
            }
            catch(Exception ex)
            {
                await UnitOfWork.RollBackTransaction();
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task<AdvertisementResponseDto> UpdateAdvertisement(string advertisementId, AdvertisementDto advertisementDto)
        {
            throw new NotImplementedException();
        }
    }
}
