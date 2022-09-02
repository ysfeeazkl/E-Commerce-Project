using E_Commerce.Business.Abstract;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Dtos.ReportDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        //Task<IDataResult> AddAsync(ReportAddDto reportAddDto);
        //Task<IDataResult> UpdateAsync(ReportUpdateDto reportUpdateDto);
        //Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        //Task<IDataResult> GetByIdAsync(int id);
        //Task<IDataResult> GetAllBySellerIdAsync(int sellerId);
        //Task<IDataResult> GetAllByBrandIdAsync(int brandId);
        //Task<IDataResult> GetAllByCustomerIdAsync(int customerId);
        //Task<IDataResult> GetAllByProductIdAsync(int productId);
        //Task<IDataResult> GetAllByCommentIdAsync(int commentId);
        //Task<IDataResult> DeleteByIdAsync(int id);
        //Task<IDataResult> HardDeleteByIdAsync(int id);

        IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] ReportAddDto reportAddDto)
        {
            var result = await _reportService.AddAsync(reportAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] ReportUpdateDto reportUpdateDto)
        {
            var result = await _reportService.UpdateAsync(reportUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            var result = await _reportService.GetAllAsync(isDeleted, isAscending, currentPage, pageSize, orderBy);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _reportService.GetByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByBrandIdAsync(int brandId)
        {
            var result = await _reportService.GetAllByBrandIdAsync(brandId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBySellerIdAsync(int sellerId)
        {
            var result = await _reportService.GetAllBySellerIdAsync(sellerId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByCustomerIdAsync(int customerId)
        {
            var result = await _reportService.GetAllByCustomerIdAsync(customerId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByProductIdAsync(int productId)
        {
            var result = await _reportService.GetAllByProductIdAsync(productId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByCommentIdAsync(int commentId)
        {
            var result = await _reportService.GetAllByCommentIdAsync(commentId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var result = await _reportService.DeleteByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> HardDeleteByIdAsync(int id)
        {
            var result = await _reportService.HardDeleteByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
