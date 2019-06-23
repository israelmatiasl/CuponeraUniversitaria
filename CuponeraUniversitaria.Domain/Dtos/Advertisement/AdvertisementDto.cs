using System;
namespace CuponeraUniversitaria.Domain.Dtos.Advertisement
{
    public class AdvertisementDto
    {
        public string UniversityId { get; set; }
        public string CompanyId { get; set; }
        public int Max { get; set; }
        public bool Unlimited { get; set; }
        public int Current { get; set; }
    }
}
