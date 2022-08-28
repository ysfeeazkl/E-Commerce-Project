using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.CommentValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.CommentDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using E_Commerce.Shared.Utilities.Results.Concrete;
using E_Commerce.Shared.Utilities.Validation.FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Concrete
{
    public class CommentManager : ManagerBase,ICommentService
    {
        public CommentManager(CommerceContext context, IMapper mapper) : base(mapper, context)
        {

        }

        public async Task<IDataResult> AddAsync(CommentAddDto commentAddDto)
        {
            ValidationTool.Validate(new CommentAddDtoValidator(),commentAddDto);

            var comment = Mapper.Map<Comment>(commentAddDto);

            comment.CreatedDate = DateTime.Now;
            comment.CreatedByUserId = commentAddDto.CustomerID;

            await DbContext.Comments.AddAsync(comment);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success,"Yorum başarı ile eklendi",comment);
            
           
        }
        public Task<IDataResult> UpdateAsync(CommentUpdateDto commentUpdateDto)
        {
            throw new NotImplementedException();
        }
        public async Task<IDataResult> DeleteByIdAsync(int id)
        {
            var comment = await DbContext.Comments.SingleOrDefaultAsync(a => a.ID == id);
            if (comment is null)
                return new DataResult(ResultStatus.Error, "Böyle bir yorum bulunamadı.");
            comment.IsDeleted = true;
            comment.IsActive = false;
            DbContext.Comments.Update(comment);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{comment.Content}, yorumu silindi.");
        }

        public async Task<IDataResult> HardDeleteByIdAsync(int id)
        {
            var comment = await DbContext.Comments.SingleOrDefaultAsync(a => a.ID == id);
            if (comment is null)
                return new DataResult(ResultStatus.Error, "Böyle bir yorum bulunamadı.");

            DbContext.Comments.Remove(comment);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, @$"{comment.Comments}, yorumu silindi.");
        }

        public async Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            IQueryable<Comment> comments = DbContext.Set<Comment>().Include(a => a.Customer).AsNoTracking();
            if (isDeleted.HasValue) comments = comments.Where(a => a.IsDeleted == isDeleted);
            switch (orderBy)
            {
                case OrderBy.Id:
                    comments = isAscending ? comments.OrderBy(a => a.ID) : comments.OrderByDescending(a => a.ID);
                    break;
                case OrderBy.Az:
                    comments = isAscending ? comments.OrderBy(a => a.Customer.FirstName) : comments.OrderByDescending(a => a.Customer.FirstName);
                    break;
                case OrderBy.CreatedDate:
                    comments = isAscending ? comments.OrderBy(a => a.CreatedDate) : comments.OrderByDescending(a => a.CreatedDate);
                    break;
                default:
                    comments = isAscending ? comments.OrderBy(a => a.ID) : comments.OrderByDescending(a => a.ID);
                    break;
            }
            if (currentPage != 0 && pageSize != 0)
            {
                var filteredQuery = await comments.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<Category>(a)).ToListAsync();
                return new DataResult(ResultStatus.Success, filteredQuery);
            }

            return new DataResult(ResultStatus.Success, comments);
        }

        public async Task<IDataResult> GetByID(int id)
        {
            var comment = await DbContext.Comments.SingleOrDefaultAsync(a => a.ID == id);
            if (comment is null)
                return new DataResult(ResultStatus.Error, "Böyle bir yorum bulunamadı.");
            return new DataResult(ResultStatus.Success, comment);
        }

        public async Task<IDataResult> GetAllByBaseCommentID(int baseCommentId)
        {
            var baseComment = await DbContext.Comments.SingleOrDefaultAsync(a => a.ID == baseCommentId);
            if (baseComment is null)
                return new DataResult(ResultStatus.Error, "Böyle bir yorum bulanamadı");
            var comment =  DbContext.Comments.Where(a => a.BaseCommentID == baseCommentId);
            if (comment is null)
                return new DataResult(ResultStatus.Error, "Böyle bir yorum bulunamadı.");
            return new DataResult(ResultStatus.Success, comment);
        }

        public async Task<IDataResult> GetAllByCustomerID(int customerId)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == customerId);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı bulanamadı");
            var comment = DbContext.Comments.Where(a => a.CustomerID == customerId);
            if (comment is null)
                return new DataResult(ResultStatus.Error, "Böyle bir yorum bulunamadı.");
            return new DataResult(ResultStatus.Success, comment);
        }

     

        public async Task<IDataResult> GetAllByProductID(int productId)
        {
            var product = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == productId);
            if (product is null)
                return new DataResult(ResultStatus.Error, "Böyle bir ürün bulanamadı");
            var comment = DbContext.Comments.Where(a => a.ProductID == productId);
            if (comment is null)
                return new DataResult(ResultStatus.Error, "Böyle bir yorum bulunamadı.");
            return new DataResult(ResultStatus.Success, comment);
        }

        public async Task<IDataResult> GetAllBySellerID(int sellerId)
        {
            var seller = await DbContext.Sellers.SingleOrDefaultAsync(a => a.ID == sellerId);
            if (seller is null)
                return new DataResult(ResultStatus.Error, "Böyle bir satıcı bulanamadı");
            var comment = DbContext.Comments.Where(a => a.SellerID == sellerId);
            if (comment is null)
                return new DataResult(ResultStatus.Error, "Böyle bir yorum bulunamadı.");
            return new DataResult(ResultStatus.Success, comment);
        }

      
    }
}
