using E_Commerce.Business.Abstract;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Dtos.CustomerDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //Task<IDataResult> UpdateAsync(CustomerUpdateDto customerUpdateDto);
        //Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        //Task<IDataResult> GetByIdAsync(int id);
        //Task<IDataResult> GetByUserNameAsync(string userName);
        //Task<IDataResult> GetByEmailAddressAsync(string emailAddress);
        //Task<IDataResult> GetByPhoneNumberAsync(string phoneNumber);
        //Task<IDataResult> DeleteByIdAsync(int id);
        //Task<IDataResult> HardDeleteByIdAsync(int id);

        ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] CustomerUpdateDto customerUpdateDto)
        {
            var result = await _customerService.UpdateAsync(customerUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            var result = await _customerService.GetAllAsync( isDeleted,  isAscending,  currentPage,  pageSize,  orderBy);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _customerService.GetByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByUserNameAsync(string userName)
        {
            var result = await _customerService.GetByUserNameAsync(userName);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByEmailAddressAsync(string emailAddress)
        {
            var result = await _customerService.GetByEmailAddressAsync(emailAddress);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByPhoneNumberAsync(string phoneNumber)
        {
            var result = await _customerService.GetByPhoneNumberAsync(phoneNumber);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var result = await _customerService.DeleteByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> HardDeleteByIdAsync(int id)
        {
            var result = await _customerService.HardDeleteByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
