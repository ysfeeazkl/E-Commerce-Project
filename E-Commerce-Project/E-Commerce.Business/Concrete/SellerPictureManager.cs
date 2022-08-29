using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.SellerPictureValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.SellerPictureDtos;
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
    public class SellerPictureManager: ManagerBase,ISellerPictureService
    {
        public SellerPictureManager(CommerceContext context, IMapper mapper) : base(mapper, context)
        {

        }

        public async Task<IDataResult> AddAsync(SellerPictureAddDto sellerPictureAddDto)
        {
            ValidationTool.Validate(new SellerPictureAddDtoValidator(), sellerPictureAddDto);
            var seller = await DbContext.Sellers.SingleOrDefaultAsync(a => a.ID == sellerPictureAddDto.SellerID);
            if (seller is null)
                return new DataResult(ResultStatus.Error, "Böyle bir satıcı yok.");
            if (await DbContext.SellerPictures.Where(a => a.SellerID == sellerPictureAddDto.SellerID).CountAsync() == 2)
                return new DataResult(ResultStatus.Error, "Bir satıcıya maksimum 2 adet fotoğraf eklenebilir.");
            var result = FileUpload.UploadAlternative(sellerPictureAddDto.File, "Sellers");
            if (result.ResultStatus == ResultStatus.Error)
                return result;
            SellerPicture sellerPicture = new SellerPicture
            {
                FilePath = result.Data.ToString(),
                SellerID = seller.ID,
                Seller = seller,
                FileName = result.Message,
                CreatedDate = DateTime.Now,
            };
            await DbContext.SellerPictures.AddAsync(sellerPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Satıcı fotoğrafı başarı ile eklendi");
        }

        public async Task<IDataResult> UpdateAsync(SellerPictureUpdateDto sellerPictureUpdateDto)
        {
            ValidationTool.Validate(new SellerPictureUpdateDtoValidator(), sellerPictureUpdateDto);
            var sellerPicture = await DbContext.SellerPictures.SingleOrDefaultAsync(a => a.ID == sellerPictureUpdateDto.ID || a.FileName == sellerPictureUpdateDto.File.FileName);
            if (sellerPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir fotoğraf bulunamadı.");
            var seller = await DbContext.Sellers.SingleOrDefaultAsync(a => a.ID == sellerPicture.ID);

            var updateFile = FileUpload.UploadAlternative(sellerPictureUpdateDto.File, "Sellers");
            if (updateFile.ResultStatus == ResultStatus.Error)
                return updateFile;

            SellerPicture newPicture = new SellerPicture
            {
                FileName = updateFile.Message,
                FilePath = updateFile.Data.ToString(),
                SellerID= sellerPictureUpdateDto.SellerID,
                Seller = seller!,
                ID = sellerPicture.ID,
                ModifiedDate = DateTime.Now,
            };
            var sellerPictureMapped = Mapper.Map<SellerPicture, SellerPicture>(sellerPicture, newPicture);
            DbContext.SellerPictures.Update(sellerPictureMapped);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, sellerPictureMapped);
        }

        public async Task<IDataResult> DeleteByFileNameAsync(string fileName)
        {
            var sellerPicture = await DbContext.SellerPictures.SingleOrDefaultAsync(a => a.FileName == fileName);
            if (sellerPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunmamakta.");
            var result = FileUpload.Delete(sellerPicture.FilePath!);
            if (result.ResultStatus == ResultStatus.Error)
                return result;
            DbContext.SellerPictures.Remove(sellerPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "satıcı fotoğrafı başarıyla silindi.");
        }

        public async Task<IDataResult> GetByIdAsync(int id)
        {
            var sellerPicture = await DbContext.SellerPictures.SingleOrDefaultAsync(a => a.ID == id);
            if (sellerPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunmamakta.");
            return new DataResult(ResultStatus.Error, sellerPicture);

        }

        public async Task<IDataResult> GetAllBySellerIdAsync(int sellerId)
        {
            var seller= await DbContext.Sellers.SingleOrDefaultAsync(a => a.ID == sellerId);
            if (seller is null)
                return new DataResult(ResultStatus.Error, "Böyle bir satıcı bulunmamakta.");

            var sellerPicture = DbContext.SellerPictures.Where(a => a.ID == sellerId);
            if (sellerPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunmamakta.");
            return new DataResult(ResultStatus.Error, sellerPicture);
        }

       
    }
}
