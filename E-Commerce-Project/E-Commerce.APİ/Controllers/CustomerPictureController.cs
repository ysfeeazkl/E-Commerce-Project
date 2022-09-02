using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Dtos.CustomerPictureDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPictureController : ControllerBase
    {
        //Task<IDataResult> AddAsync(CustomerPictureAddDto customerPictureAddDto);
        //Task<IDataResult> UpdateAsync(CustomerPictureUpdateDto customerPictureUpdateDto);
        //Task<IDataResult> GetByIdAsync(int id);
        //Task<IDataResult> GetByCustomerIdAsync(int customerId);
        //Task<IDataResult> DeleteByFileNameAsync(string fileName);


        ICustomerPictureService _customerPictureService;
        public CustomerPictureController(ICustomerPictureService customerPictureService)
        {
            _customerPictureService = customerPictureService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] CustomerPictureAddDto customerPictureAddDto)
        {
            var result = await _customerPictureService.AddAsync(customerPictureAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] CustomerPictureUpdateDto customerPictureUpdateDto)
        {
            var result = await _customerPictureService.UpdateAsync(customerPictureUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _customerPictureService.GetByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByCustomerIdAsync(int customerId)
        {
            var result = await _customerPictureService.GetByCustomerIdAsync(customerId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByFileNameAsync(string fileName)
        {
            var result = await _customerPictureService.DeleteByFileNameAsync(fileName);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
