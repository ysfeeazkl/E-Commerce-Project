using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.BrandValidator;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.BrandDtos;
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
    public class BrandManager : ManagerBase,IBrandService
    {
        IHttpContextAccessor _httpContextAccessor;
        public BrandManager(CommerceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult> AddAsync(BrandAddDto brandAddDto)
        {
            ValidationTool.Validate(new BrandAddDtoValidator(), brandAddDto);
            var brandIsExist = await DbContext.Brands.SingleOrDefaultAsync(a => a.Name == brandAddDto.Name);
            if (brandIsExist is not null)
                return new DataResult(ResultStatus.Error, "Böyle bir Marka zaten mevcut");
            var brand = Mapper.Map<Brand>(brandAddDto);
            brand.CreatedDate = DateTime.Now;
            brand.CreatedByUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(a => a.Type == "UserId").Value);

            await DbContext.Brands.AddAsync(brand);
            await DbContext.SaveChangesAsync();
            if (brandAddDto.File is not null)
            {
                var result = FileUpload.UploadAlternative(brandAddDto.File, "Brands");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    BrandPicture brandPicture = new BrandPicture()
                    {
                        BrandID = brand.ID,
                        Brand = brand,
                        FileName = result.Message,
                        FilePath = result.Data.ToString(),
                        CreatedDate = DateTime.Now,
                    };
                    await DbContext.BrandPictures.AddAsync(brandPicture);
                    await DbContext.SaveChangesAsync();
                }
            }
            return new DataResult(ResultStatus.Success, "Başarıyla Marka Eklendi.");
        }

        public async Task<IDataResult> UpdateAsync(BrandUpdateDto brandUpdateDto)
        {
            ValidationTool.Validate(new BrandUpdateDtoValidator(), brandUpdateDto);

            var brandIsExist = await DbContext.Brands.SingleOrDefaultAsync(a => a.ID == brandUpdateDto.ID);
            if (brandIsExist is null)
                return new DataResult(ResultStatus.Error, "Böyle bir Marka bulunamadı.");
            var brand = Mapper.Map<BrandUpdateDto, Brand>(brandUpdateDto, brandIsExist);

            brand.ModifiedDate = DateTime.Now;
            brand.ModifiedByUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(a => a.Type == "UserId").Value);

            DbContext.Brands.Update(brand);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Grup başarıyla güncellendi.");
        }

        public async Task<IDataResult> DeleteByIdAsync(int id)
        {
            var brand = await DbContext.Brands.SingleOrDefaultAsync(a => a.ID == id);
            if (brand is null)
                return new DataResult(ResultStatus.Error, "Böyle bir Marka bulunmuyor");

            brand.ModifiedDate = DateTime.Now;
            brand.IsActive = false;
            brand.IsDeleted = true;

            DbContext.Update(brand);
            await DbContext.SaveChangesAsync();

            return new DataResult(ResultStatus.Success, "Marka başarı ile silindi");
        }

        public async Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
      
            IQueryable<Brand> query = DbContext.Set<Brand>().AsNoTracking();
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
                    query = isAscending ? query.OrderBy(a => a.CreatedDate) : query.OrderByDescending(a => a.Name);
                    break;
            }

            if (currentPage!=0 && pageSize != 0)
            {
                var filteredQuery = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<Brand>(a)).ToListAsync();
                return new DataResult(ResultStatus.Success, filteredQuery);
            }
            return new DataResult(ResultStatus.Success, query);


        }

        public async Task<IDataResult> GetByID(int id)
        {
            var brand = await DbContext.Brands.SingleOrDefaultAsync(a=>a.Equals(id));
            if (brand is null)
                return new DataResult(ResultStatus.Error,"Böyle bir Marka bulunamadı");
            return new DataResult(ResultStatus.Success,brand);
        }

        public async Task<IDataResult> GetByName(string name)
        {
            var brand = await DbContext.Brands.SingleOrDefaultAsync(a => a.Name == name);
            if (brand is null)
                return new DataResult(ResultStatus.Error, "Böyle bir Marka bulunamadı");
            return new DataResult(ResultStatus.Success, brand);
        }

        public async Task<IDataResult> HardDeleteByIdAsync(int id)
        {
            var brand = await DbContext.Brands.SingleOrDefaultAsync(a => a.ID == id);
            if (brand is null)
                return new DataResult(ResultStatus.Error, "Böyle bir Marka bulunamadı");
            DbContext.Brands.Remove(brand);

            return new DataResult(ResultStatus.Success, "Marka başarılı bir şekilde silindi");
        }

   
    }
}
