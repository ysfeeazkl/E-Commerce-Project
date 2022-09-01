using E_Commerce.Business.Abstract;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Dtos.CommentDtos;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APİ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        //Task<IDataResult> AddAsync(CommentAddDto commentAddDto);
        //Task<IDataResult> UpdateAsync(CommentUpdateDto commentUpdateDto);
        //Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        //Task<IDataResult> GetByIdAsync(int id);
        //Task<IDataResult> GetAllByCustomerIdAsync(int customerId);
        //Task<IDataResult> GetAllByProductIdAsync(int productId);
        //Task<IDataResult> GetAllBySellerIdAsync(int sellerId);
        //Task<IDataResult> GetAllByBaseCommentIdAsync(int baseCommentId);
        //Task<IDataResult> DeleteByIdAsync(int id);
        //Task<IDataResult> HardDeleteByIdAsync(int id);

        ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] CommentAddDto commentAddDto)
        {
            var result = await _commentService.AddAsync(commentAddDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] CommentUpdateDto commentUpdateDto)
        {
            var result = await _commentService.UpdateAsync(commentUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            var result = await _commentService.GetAllAsync(isDeleted, isAscending, currentPage, pageSize, orderBy);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _commentService.GetByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByCustomerIdAsync(int customerId)
        {
            var result = await _commentService.GetAllByCustomerIdAsync(customerId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        } 
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByProductIdAsync(int productId)
        {
            var result = await _commentService.GetAllByProductIdAsync(productId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBySellerIdAsync(int sellerId)
        {
            var result = await _commentService.GetAllBySellerIdAsync(sellerId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllByBaseCommentIdAsync(int baseCommentId)
        {
            var result = await _commentService.GetAllByBaseCommentIdAsync(baseCommentId);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> HardDeleteByIdAsync(int id)
        {
            var result = await _commentService.HardDeleteByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var result = await _commentService.DeleteByIdAsync(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
