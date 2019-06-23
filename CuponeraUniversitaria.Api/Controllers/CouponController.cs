using System;
using System.Threading.Tasks;
using CuponeraUniversitaria.Domain.Dtos.Coupon;
using CuponeraUniversitaria.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CuponeraUniversitaria.Api.Controllers
{
    [Route("api/coupons")]
    public class CouponController : Controller
    {
        private readonly ICouponService _CouponService;

        public CouponController(ICouponService _CouponService)
        {
            this._CouponService = _CouponService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CouponResponseDto>> Get(string id)
        {
            try
            {
                var couponResponseDto = await _CouponService.GetCoupon(id);

                if (couponResponseDto is null)
                {
                    return NotFound();
                }

                return couponResponseDto;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CouponResponseDto>> Post([FromBody] CouponDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var couponResponseDto = await _CouponService.SaveCoupon(model);

                if (couponResponseDto is null)
                {
                    return BadRequest();
                }

                return couponResponseDto;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
