using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Dtos.SellerPictureDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerPictureController : ControllerBase
    {
        //Task<IDataResult> AddAsync(SellerPictureAddDto sellerPictureAddDto);
        //Task<IDataResult> UpdateAsync(SellerPictureUpdateDto sellerPictureUpdateDto);
        //Task<IDataResult> GetByIdAsync(int id);
        //Task<IDataResult> GetAllBySellerIdAsync(int sellerId);
        //Task<IDataResult> DeleteByFileNameAsync(string fileName);

        ISellerPictureService _sellerPictureService;
        public SellerPictureController(ISellerPictureService sellerPictureService)
        {
            _sellerPictureService = sellerPictureService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync( SellerPictureAddDto sellerPictureAddDto)
        {
            var result = await _sellerPictureService.AddAsync(sellerPictureAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync( SellerPictureUpdateDto sellerPictureUpdateDto)
        {
            var result = await _sellerPictureService.UpdateAsync(sellerPictureUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _sellerPictureService.GetByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBySellerIdAsync(int sellerId)
        {
            var result = await _sellerPictureService.GetAllBySellerIdAsync(sellerId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByFileNameAsync(string fileName)
        {
            var result = await _sellerPictureService.DeleteByFileNameAsync(fileName);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
