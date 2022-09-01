using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Dtos.CategoryAndProductDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAndProductController : ControllerBase
    {
        //Task<IDataResult> AddAsync(CategoryAndProductAddDto categoryAndProductAddDto);
        //Task<IDataResult> UpdateAsync(CategoryAndProductUpdateDto categoryAndProductUpdateDto);
        //Task<IDataResult> DeleteByCategoryIdAndProductIdAsync(int categoryId, int productId);
        //Task<IDataResult> GetByProductIdAsync(int productId, bool includeProduct);
        //Task<IDataResult> GetByCategoryIdAsync(int categoryId, bool includeCategory);

        ICategoryAndProductService _categoryAndProductService;
        public CategoryAndProductController(ICategoryAndProductService categoryAndProductService)
        {
            _categoryAndProductService = categoryAndProductService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] CategoryAndProductAddDto categoryAndProductAddDto)
        {
            var result = await _categoryAndProductService.AddAsync(categoryAndProductAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryAndProductUpdateDto categoryAndProductUpdateDto)
        {
            var result = await _categoryAndProductService.UpdateAsync(categoryAndProductUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
      
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByProductIdAsync(int productId, bool includeProduct)
        {
            var result = await _categoryAndProductService.GetByProductIdAsync(productId, includeProduct);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByCategoryIdAsync(int categoryId, bool includeCategory)
        {
            var result = await _categoryAndProductService.GetByCategoryIdAsync(categoryId, includeCategory);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByCategoryIdAndProductIdAsync(int categoryId, int productId)
        {
            var result = await _categoryAndProductService.DeleteByCategoryIdAndProductIdAsync(categoryId, productId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
