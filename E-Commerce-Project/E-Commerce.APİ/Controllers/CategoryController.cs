using E_Commerce.Business.Abstract;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Dtos.CategoryDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //Task<IDataResult> AddAsync(CategoryAddDto categoryAddDto);
        //Task<IDataResult> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        //Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        //Task<IDataResult> GetByIdAsync(int id);
        //Task<IDataResult> DeleteByIdAsync(int id);
        //Task<IDataResult> HardDeleteByIdAsync(int id);
        //Task<IDataResult> HardDeleteByNameAsync(string name);
        //Task<IDataResult> DeleteByNameAsync(string name);

        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] CategoryAddDto categoryAddDto)
        {
            var result = await _categoryService.AddAsync(categoryAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            var result = await _categoryService.UpdateAsync(categoryUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            var result = await _categoryService.GetAllAsync(isDeleted, isAscending, currentPage, pageSize, orderBy);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var result = await _categoryService.DeleteByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByNameAsync(string name)
        {
            var result = await _categoryService.DeleteByNameAsync(name);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> HardDeleteByIdAsync(int id)
        {
            var result = await _categoryService.HardDeleteByIdAsync(id );
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> HardDeleteByNameAsync(string name)
        {
            var result = await _categoryService.HardDeleteByNameAsync(name);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);

        }
        
    }
}
