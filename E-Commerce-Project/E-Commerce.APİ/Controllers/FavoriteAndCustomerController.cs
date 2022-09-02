using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Dtos.FavoriteAndCustomerDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteAndCustomerController : ControllerBase
    {
        //Task<IDataResult> AddAsync(FavoriteAndCustomerAddDto favoriteAndCustomerAddDto);
        //Task<IDataResult> UpdateAsync(FavoriteAndCustomerUpdateDto favoriteAndCustomerUpdate);
        //Task<IDataResult> DeleteByFavoriteIdAndCustomerIdAsync(int customerId, int productId);
        //Task<IDataResult> GetByFavoriteIdAsync(int productId, bool includeProduct);
        //Task<IDataResult> GetByCustomerIdAsync(int customerId, bool includeCustomer);

        IFavoriteAndCustomerService _favoriteAndCustomerService;
        public FavoriteAndCustomerController(IFavoriteAndCustomerService favoriteAndCustomerService)
        {
            _favoriteAndCustomerService = favoriteAndCustomerService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync(FavoriteAndCustomerAddDto favoriteAndCustomerAddDto)
        {
            var result = await _favoriteAndCustomerService.AddAsync(favoriteAndCustomerAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> UpdateAsync(FavoriteAndCustomerUpdateDto favoriteAndCustomerUpdate)
        {
            var result = await _favoriteAndCustomerService.UpdateAsync(favoriteAndCustomerUpdate);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetByFavoriteIdAsync(int productId, bool includeProduct)
        {
            var result = await _favoriteAndCustomerService.GetByFavoriteIdAsync(productId, includeProduct);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByCustomerIdAsync(int customerId, bool includeCustomer)
        {
            var result = await _favoriteAndCustomerService.GetByCustomerIdAsync(customerId, includeCustomer);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteByFavoriteIdAndCustomerIdAsync(int customerId, int productId)
        {
            var result = await _favoriteAndCustomerService.DeleteByFavoriteIdAndCustomerIdAsync(customerId, productId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
