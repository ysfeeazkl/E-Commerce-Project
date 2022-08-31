using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.AuthDtos;
using E_Commerce.Entities.Dtos.CustomerDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        //Task<IDataResult> CreateAccessTokenByCustomerIdAsync(int customerId, bool isRefresh);

        //Task<IDataResult> RegisterAsync(CustomerRegisterDto customerRegisterDto);
        //Task<IDataResult> LoginWithPhoneAsync(CustomerLoginWithPhoneDto customerLoginWithPhoneDto);
        //Task<IDataResult> LoginWithEmailAsync(CustomerLoginWithEmailDto customerLoginWithEmailDto);

        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

    

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAccessTokenByCustomerIdAsync(int customerId, bool isRefresh)
        {
            var result = await _authService.CreateAccessTokenByCustomerIdAsync(customerId, isRefresh);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAsync([FromBody] CustomerRegisterDto customerRegisterDto)
        {
            var result = await _authService.RegisterAsync(customerRegisterDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginWithPhoneAsync([FromBody] CustomerLoginWithPhoneDto customerLoginWithPhoneDto)
        {
            var result = await _authService.LoginWithPhoneAsync(customerLoginWithPhoneDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginWithEmailAsync([FromBody] CustomerLoginWithEmailDto customerLoginWithEmailDto)
        {
            var result = await _authService.LoginWithEmailAsync(customerLoginWithEmailDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
