using System;
using System.Threading.Tasks;
using CuponeraUniversitaria.Domain.Dtos.Advertisement;
using CuponeraUniversitaria.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CuponeraUniversitaria.Api.Controllers
{
    [Route("api/advertisements")]
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _AdvertisementService;

        public AdvertisementController(IAdvertisementService _AdvertisementService)
        {
            this._AdvertisementService = _AdvertisementService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AdvertisementResponseDto>> Get(string id)
        {
            try
            {
                var advertisementResponseDto = await _AdvertisementService.GetAdvertisement(id);

                if (advertisementResponseDto is null)
                {
                    return NotFound();
                }

                return advertisementResponseDto;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AdvertisementResponseDto>> Post([FromBody] AdvertisementDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var advertisementResponseDto = await _AdvertisementService.SaveAdvertisement(model);

                if (advertisementResponseDto is null)
                {
                    return BadRequest();
                }

                return advertisementResponseDto;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<AdvertisementResponseDto>> Patch([FromBody] AdvertisementDto model, string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var advertisementResponseDto = await _AdvertisementService.UpdateAdvertisement(id, model);

                if (advertisementResponseDto is null)
                {
                    return BadRequest();
                }

                return advertisementResponseDto;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
