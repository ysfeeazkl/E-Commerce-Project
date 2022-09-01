using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.BrandPictureDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandPictureController : ControllerBase
    {
        IBrandPicturesService _brandPictureService;
        public BrandPictureController(IBrandPicturesService brandPicture)
        {
            _brandPictureService = brandPicture;
        }
        //Task<IDataResult> AddAsync(BrandPictureAddDto brandPictureAddDto);
        //Task<IDataResult> UpdateAsync(BrandPictureUpdateDto brandPictureUpdateDto);
        //Task<IDataResult> GetByIdAsync(int id);
        //Task<IDataResult> GetAllByBrandIdAsync(int brandId);
        //Task<IDataResult> DeleteByFileNameAsync(string fileName);
        //Task<IDataResult> GetByFileNameAsync(string fileName);


        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] BrandPictureAddDto brandPictureAddDto)
        {
            var result = await _brandPictureService.AddAsync(brandPictureAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] BrandPictureUpdateDto brandPictureUpdateDto)
        {
            var result = await _brandPictureService.UpdateAsync(brandPictureUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _brandPictureService.GetByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByBrandIdAsync(int brandId)
        {
            var result = await _brandPictureService.GetAllByBrandIdAsync(brandId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByFileNameAsync(string fileName)
        {
            var result = await _brandPictureService.GetByFileNameAsync(fileName);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByFileNameAsync(string fileName)
        {
            var result = await _brandPictureService.DeleteByFileNameAsync(fileName);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
      
    }
}
