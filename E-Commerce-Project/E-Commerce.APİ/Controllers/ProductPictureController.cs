using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Dtos.ProductPictureDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPictureController : ControllerBase
    {
        //Task<IDataResult> AddAsync(ProductPictureAddDto productPictureAddDto);
        //Task<IDataResult> UpdateAsync(ProductPictureUpdateDto productPictureUpdateDto);
        //Task<IDataResult> GetByIdAsync(int id);
        //Task<IDataResult> GetAllByProductIdAsync(int productId);
        //Task<IDataResult> DeleteByFileNameAsync(string fileName);


        IProductPictureService _productPictureService;
        public ProductPictureController(  IProductPictureService productPictureService)
        {
           
            _productPictureService = productPictureService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] ProductPictureAddDto productPictureAddDto)
        {
            var result = await _productPictureService.AddAsync(productPictureAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductPictureUpdateDto productPictureUpdateDto)
        {
            var result = await _productPictureService.UpdateAsync(productPictureUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _productPictureService.GetByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByProductIdAsync(int productId)
        {
            var result = await _productPictureService.GetAllByProductIdAsync(productId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByFileNameAsync(string fileName)
        {
            var result = await _productPictureService.DeleteByFileNameAsync(fileName);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
