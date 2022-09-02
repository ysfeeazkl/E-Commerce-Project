using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Dtos.ShoppingCartDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        //Task<IDataResult> AddAsync(ShoppingCartAddDto shoppingCartAddDto);
        //Task<IDataResult> UpdateAsync(ShoppingCartUpdateDto shoppingCartUpdateDto);
        //Task<IDataResult> GetAllAsync();
        //Task<IDataResult> GetByIdAsync(int id);
        //Task<IDataResult> GetByCustomerIdAsync(int customerId);
        //Task<IDataResult> DeleteByIdAsync(int id);
        //Task<IDataResult> HardDeleteByIdAsync(int id);

        IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync( [FromBody] ShoppingCartAddDto shoppingCartAddDto)
        {
            var result = await _shoppingCartService.AddAsync(shoppingCartAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] ShoppingCartUpdateDto shoppingCartUpdateDto)
        {
            var result = await _shoppingCartService.UpdateAsync(shoppingCartUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _shoppingCartService.GetAllAsync();
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _shoppingCartService.GetByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByCustomerIdAsync(int customerId)
        {
            var result = await _shoppingCartService.GetByCustomerIdAsync(customerId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var result = await _shoppingCartService.DeleteByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> HardDeleteByIdAsync(int id)
        {
            var result = await _shoppingCartService.HardDeleteByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
