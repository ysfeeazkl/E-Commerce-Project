using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.ProductPictureValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.ProductPictureDtos;
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
    public class ProductPictureManager : ManagerBase,IProductPictureService
    {
        public ProductPictureManager(CommerceContext context, IMapper mapper) : base(mapper, context)
        {

        }

        public async Task<IDataResult> AddAsync(ProductPictureAddDto productPictureAddDto)
        {
            ValidationTool.Validate(new ProductPictureAddDtoValidator(), productPictureAddDto);
            var product = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == productPictureAddDto.ProductId);
            if (product is null)
                return new DataResult(ResultStatus.Error, "Böyle bir ürün yok.");
            if (await DbContext.ProductPictures.Where(a => a.ProductID == productPictureAddDto.ProductId).CountAsync() == 5)
                return new DataResult(ResultStatus.Error, "Bir ürüne maksimum 5 adet fotoğraf eklenebilir.");
            var result = FileUpload.UploadAlternative(productPictureAddDto.File, "Products");
            if (result.ResultStatus == ResultStatus.Error)
                return result;
            ProductPicture productPicture = new ProductPicture
            {
                FilePath = result.Data.ToString(),
                ProductID = product.ID,
                Product = product,
                FileName = result.Message,               
                CreatedDate = DateTime.Now,
            };
            await DbContext.ProductPictures.AddAsync(productPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Ürün fotoğrafı başarı ile eklendi");
        }

        public async Task<IDataResult> UpdateAsync(ProductPictureUpdateDto productPictureUpdateDto)
        {
            ValidationTool.Validate(new ProductPictureUpdateDtoValidator(), productPictureUpdateDto);
            var productPicture = await DbContext.ProductPictures.SingleOrDefaultAsync(a => a.ID == productPictureUpdateDto.ID || a.FileName == productPictureUpdateDto.File.FileName);
            if (productPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir fotoğraf bulunamadı.");
            var updateFile = FileUpload.UploadAlternative(productPictureUpdateDto.File, "Products");
            if (updateFile.ResultStatus == ResultStatus.Error)
                return updateFile;

            ProductPicture newPicture = new ProductPicture
            {
                FileName = updateFile.Message,
                FilePath = updateFile.Data.ToString(),
                ProductID = productPictureUpdateDto.ProductId,
                ID = productPicture.ID,
                ModifiedDate = DateTime.Now,
            };
            var productPictureMapped = Mapper.Map<ProductPicture, ProductPicture>(productPicture, newPicture);
            DbContext.ProductPictures.Update(productPictureMapped);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, productPictureMapped);
        }

        public async Task<IDataResult> DeleteByFileNameAsync(string fileName)
        {
            var productPicture = await DbContext.ProductPictures.SingleOrDefaultAsync(a => a.FileName == fileName);
            if (productPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunmamakta.");
            var result = FileUpload.Delete(productPicture.FilePath!);
            if (result.ResultStatus == ResultStatus.Error)
                return result;
            DbContext.ProductPictures.Remove(productPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "ürün fotoğrafı başarıyla silindi.");
        }

        public async Task<IDataResult> GetByIdAsync(int id)
        {
            var productPicture = await DbContext.ProductPictures.SingleOrDefaultAsync(a => a.ID == id);
            if (productPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunamadı.");
            return new DataResult(ResultStatus.Success, productPicture);
        }

        public async Task<IDataResult> GetAllByProductIdAsync(int productId)
        {
            var product = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == productId);
            if (product is null)
                return new DataResult(ResultStatus.Error, "böyle bir kullanıcı bulunamadı.");
            var productPicture = DbContext.ProductPictures.Where(a => a.ProductID == productId);
            if (productPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunamadı.");
            return new DataResult(ResultStatus.Success, productPicture);
        }

    
    }
}
