using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.BrandPictureValidator;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.BrandPictureDtos;
using E_Commerce.Shared.Utilities.FileUploads;
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
    public class BrandPicturesManager : ManagerBase,IBrandPicturesService
    {
        public BrandPicturesManager(CommerceContext context, IMapper mapper) : base(mapper, context)
        {

        }

        public async Task<IDataResult> AddAsync(BrandPictureAddDto brandPictureAddDto)
        {
            ValidationTool.Validate(new BrandPictureAddDtoValidator(), brandPictureAddDto);
            var brand = await DbContext.Brands.SingleOrDefaultAsync(a => a.ID == brandPictureAddDto.BrandId);
            if (brand is null)
                return new DataResult(ResultStatus.Error, "Böyle bir marka yok.");
            if (await DbContext.BrandPictures.Where(a => a.BrandID == brandPictureAddDto.BrandId).CountAsync() == 2)
                return new DataResult(ResultStatus.Error, "Bir marka maksimum 2 adet fotoğraf eklenebilir.");
            var result = FileUpload.UploadAlternative(brandPictureAddDto.File, "Brands");
            if (result.ResultStatus == ResultStatus.Error)
                return result;
            BrandPicture brandPicture = new BrandPicture
            {
                FilePath = result.Data.ToString(),
                BrandID = brand.ID,
                FileName = result.Message,
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
            };
            await DbContext.BrandPictures.AddAsync(brandPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Marka fotoğrafı başarıyla eklendi.");
        }

        public async Task<IDataResult> UpdateAsync(BrandPictureUpdateDto brandPictureUpdateDto)
        {
            ValidationTool.Validate(new BrandPictureUpdateDtoValidator(), brandPictureUpdateDto);
            var brandPicture = await DbContext.BrandPictures.SingleOrDefaultAsync(a => a.ID == brandPictureUpdateDto.ID || a.FileName == brandPictureUpdateDto.File.FileName);
            if (brandPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir fotoğraf bulunamadı.");
            var updateFile = FileUpload.UploadAlternative(brandPictureUpdateDto.File, "Brands");
            if (updateFile.ResultStatus == ResultStatus.Error)
                return updateFile;
          
            BrandPicture newPicture = new BrandPicture
            {
                FileName = updateFile.Message,
                FilePath = updateFile.Data.ToString(),
                BrandID = brandPictureUpdateDto.BrandId,
                ID = brandPicture.ID,
                ModifiedDate = DateTime.Now,
            };
            var brandPictureMapped = Mapper.Map<BrandPicture, BrandPicture>(brandPicture, newPicture);
            DbContext.BrandPictures.Update(brandPictureMapped);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, brandPictureMapped);
        }

        public async Task<IDataResult> DeleteByFileNameAsync(string fileName)
        {
            var brandPicture = await DbContext.BrandPictures.SingleOrDefaultAsync(a => a.FileName == fileName);
            if (brandPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunmamakta.");

            var result = FileUpload.Delete(brandPicture.FilePath!);
            if (result.ResultStatus == ResultStatus.Error)
                return result;
            DbContext.BrandPictures.Remove(brandPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Marka fotoğrafı başarıyla silindi.");
        }

        public async Task<IDataResult> GetAllByBrandIdAsync(int brandId)
        {
            var brandPicture = DbContext.BrandPictures.Where(a => a.BrandID == brandId);
            if (brandPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunamadı.");
            return new DataResult(ResultStatus.Success, brandPicture);
        }

        public async Task<IDataResult> GetByIdAsync(int id)
        {
            var brandPicture = await DbContext.BrandPictures.SingleOrDefaultAsync(a => a.ID == id);
            if (brandPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunamadı.");
            return new DataResult(ResultStatus.Success, brandPicture);
        }

        public async Task<IDataResult> GetByFileNameAsync(string fileName)
        {
            var brandPicture = DbContext.BrandPictures.SingleOrDefaultAsync(a => a.FileName == fileName);
            if (brandPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunamadı.");
            return new DataResult(ResultStatus.Success, brandPicture);
        }

       
    }
}
