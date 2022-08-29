using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.SellerValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.SellerDtos;
using E_Commerce.Shared.Utilities.FileUploads;
using E_Commerce.Shared.Utilities.Results.Abstract;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using E_Commerce.Shared.Utilities.Results.Concrete;
using E_Commerce.Shared.Utilities.Validation.FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Concrete
{
    public class SellerManager : ManagerBase, ISellerService
    {
        IHttpContextAccessor _httpContextAccessor;

        public SellerManager(CommerceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult> AddAsync(SellerAddDto sellerAddDto)
        {
            ValidationTool.Validate(new SellerAddDtoValidator(), sellerAddDto);

            var sellerIsExist = await DbContext.Sellers.SingleOrDefaultAsync(a => a.Name == sellerAddDto.Name);
            if (sellerIsExist == null)
                return new DataResult(ResultStatus.Error, "Zaten bu isimde bir satıcı bulunuyor");

            var seller = Mapper.Map<Seller>(sellerAddDto);
            seller.CreatedDate = DateTime.Now;
            seller.CreatedByUserId = Convert.ToInt32(_httpContextAccessor.HttpContext!.User.Claims.SingleOrDefault(a => a.Type == "UserId")!.Value);

            await DbContext.Sellers.AddAsync(seller);
            await DbContext.SaveChangesAsync();
            if (sellerAddDto.File is not null)
            {
                var result = FileUpload.UploadAlternative(sellerAddDto.File, "Sellers");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    SellerPicture sellerPicture = new SellerPicture()
                    {
                        SellerID = seller.ID,
                        Seller = seller,
                        FileName = result.Message,
                        FilePath = result.Data.ToString(),
                        CreatedDate = DateTime.Now,
                    };
                    await DbContext.SellerPictures.AddAsync(sellerPicture);
                    await DbContext.SaveChangesAsync();
                }
            }
            return new DataResult(ResultStatus.Success, "Başarıyla Satıcı Eklendi.");
        }

        public async Task<IDataResult> UpdateAsync(SellerUpdateDto sellerUpdateDto)
        {
            ValidationTool.Validate(new SellerUpdateDtoValidator(), sellerUpdateDto);

            var sellerIsExist = await DbContext.Sellers.SingleOrDefaultAsync(a => a.ID == sellerUpdateDto.ID);
            if (sellerIsExist is null)
                return new DataResult(ResultStatus.Error, "Böyle bir Marka bulunamadı.");
            var seller = Mapper.Map<SellerUpdateDto, Seller>(sellerUpdateDto, sellerIsExist);

            seller.ModifiedDate = DateTime.Now;
            seller.ModifiedByUserId = Convert.ToInt32(_httpContextAccessor.HttpContext!.User.Claims.SingleOrDefault(a => a.Type == "UserId")!.Value);

            DbContext.Sellers.Update(seller);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Satıcı başarıyla güncellendi.");
        }



        public async Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            IQueryable<Seller> query = DbContext.Set<Seller>().AsNoTracking();
            if (isDeleted.HasValue)
                query = query.Where(a => a.IsActive == isDeleted);
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(a => a.ID) : query.OrderByDescending(a => a.ID);
                    break;
                case OrderBy.Az:
                    query = isAscending ? query.OrderBy(a => a.Name) : query.OrderByDescending(a => a.Name);
                    break;
                case OrderBy.CreatedDate:
                    query = isAscending ? query.OrderBy(a => a.CreatedDate) : query.OrderByDescending(a => a.CreatedDate);
                    break;
                default:
                    query = isAscending ? query.OrderBy(a => a.Name) : query.OrderByDescending(a => a.Name);
                    break;
            }

            if (currentPage != 0 && pageSize != 0)
            {
                var filteredQuery = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<Seller>(a)).ToListAsync();
                return new DataResult(ResultStatus.Success, filteredQuery);
            }
            return new DataResult(ResultStatus.Success, query);
        }

        public async Task<IDataResult> GetByID(int id)
        {
            var seller = await DbContext.Sellers.SingleOrDefaultAsync(a => a.ID == id);
            if (seller is null)
                return new DataResult(ResultStatus.Error, "Böyle bir satıcı bulunamadı.");
            return new DataResult(ResultStatus.Success, seller);
        }

        public async Task<IDataResult> HardDeleteByIdAsync(int id)
        {
            var seller = await DbContext.Sellers.SingleOrDefaultAsync(a => a.ID == id);
            if (seller is null)
                return new DataResult(ResultStatus.Error, "Böyle bir satıcı bulunamadı.");
            DbContext.Sellers.Remove(seller);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{seller.Name},isimli satıcı silindi.");
        }

        public async Task<IDataResult> DeleteByIdAsync(int id)
        {
            var seller = await DbContext.Sellers.SingleOrDefaultAsync(a => a.ID == id);
            if (seller is null)
                return new DataResult(ResultStatus.Error, "Böyle bir satıcı bulunamadı.");
            seller.IsDeleted = true;
            seller.IsActive = false;
            DbContext.Sellers.Update(seller);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{seller.Name},isimli satıcı silindi.");
        }

    }
}
