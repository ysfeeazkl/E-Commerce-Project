using E_Commerce.Business.Abstract;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Dtos.BrandDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        //Task<IDataResult> AddAsync(BrandAddDto brandAddDto);
        //Task<IDataResult> UpdateAsync(BrandUpdateDto brandUpdateDto);
        //Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        //Task<IDataResult> GetByID(int id);
        //Task<IDataResult> GetByName(string Name);
        //Task<IDataResult> DeleteByIdAsync(int id);
        //Task<IDataResult> HardDeleteByIdAsync(int id);


        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] BrandAddDto brandAddDto)
        {
            var result = await _brandService.AddAsync(brandAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] BrandUpdateDto brandUpdateDto)
        {
            var result = await _brandService.UpdateAsync(brandUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            var result = await _brandService.GetAllAsync( isDeleted,  isAscending,  currentPage,  pageSize,  orderBy);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _brandService.GetByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByNameAsync(string Name)
        {
            var result = await _brandService.GetByNameAsync(Name);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var result = await _brandService.DeleteByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> HardDeleteByIdAsync(int id)
        {
            var result = await _brandService.HardDeleteByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
