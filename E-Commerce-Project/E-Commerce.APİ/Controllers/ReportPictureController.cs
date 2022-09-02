using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Dtos.ReportPictureDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportPictureController : ControllerBase
    {
        //Task<IDataResult> AddAsync(ReportPictureAddDto reportPictureAddDto);
        //Task<IDataResult> UpdateAsync(ReportPictureUpdateDto reportPictureUpdateDto);
        //Task<IDataResult> GetByIdAsync(int id);
        //Task<IDataResult> GetAllByReportIdAsync(int reportId);
        //Task<IDataResult> DeleteByFileNameAsync(string fileName);

        IReportPictureService _reportPictureService;
        public ReportPictureController(IReportPictureService reportPictureService)
        {
            _reportPictureService = reportPictureService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] ReportPictureAddDto reportPictureAddDto)
        {
            var result = await _reportPictureService.AddAsync(reportPictureAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] ReportPictureUpdateDto reportPictureUpdateDto)
        {
            var result = await _reportPictureService.UpdateAsync(reportPictureUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _reportPictureService.GetByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByReportIdAsync(int reportId)
        {
            var result = await _reportPictureService.GetAllByReportIdAsync(reportId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByFileNameAsync(string fileName)
        {
            var result = await _reportPictureService.DeleteByFileNameAsync(fileName);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
