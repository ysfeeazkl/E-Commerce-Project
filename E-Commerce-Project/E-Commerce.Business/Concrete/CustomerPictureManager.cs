using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.CustomerPictureValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.CustomerPictureDtos;
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
    public class CustomerPictureManager : ManagerBase,ICustomerPictureService
    {
        public CustomerPictureManager(CommerceContext context, IMapper mapper) : base(mapper, context)
        {

        }

        public async Task<IDataResult> AddAsync(CustomerPictureAddDto customerPictureAddDto)
        {
            ValidationTool.Validate(new CustomerPictureAddDtoValidator(), customerPictureAddDto);
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == customerPictureAddDto.CustomerId);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı yok.");
            if (await DbContext.CustomerPictures.Where(a => a.CustomerID == customerPictureAddDto.CustomerId).CountAsync() == 1)
                return new DataResult(ResultStatus.Error, "Bir kullanıcı maksimum 1 adet fotoğraf eklenebilir.");
            var result = FileUpload.UploadAlternative(customerPictureAddDto.File, "Customers");
            if (result.ResultStatus == ResultStatus.Error)
                return result;
            CustomerPicture customerPicture = new CustomerPicture
            {
                FilePath = result.Data.ToString(),
                CustomerID = customer.ID,
                Customer = customer,
                FileName = result.Message,              
                CreatedDate = DateTime.Now,
            };
            await DbContext.CustomerPictures.AddAsync(customerPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Kullanıcı fotoğrafı başarıyla eklendi.");
        }

        public async Task<IDataResult> UpdateAsync(CustomerPictureUpdateDto customerPictureUpdateDto)
        {
            ValidationTool.Validate(new CustomerPictureUpdateDtoValidator(), customerPictureUpdateDto);

            
            var customerPicture = await DbContext.CustomerPictures.SingleOrDefaultAsync(a => a.ID == customerPictureUpdateDto.ID || a.FileName == customerPictureUpdateDto.File.FileName);
            if (customerPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir fotoğraf bulunamadı.");
            var updateFile = FileUpload.UploadAlternative(customerPictureUpdateDto.File, "Customers");
            if (updateFile.ResultStatus == ResultStatus.Error)
                return updateFile;

            CustomerPicture newPicture = new CustomerPicture
            {
                FileName = updateFile.Message,
                FilePath = updateFile.Data.ToString(),
                CustomerID = customerPictureUpdateDto.CustomerId,
                ID = customerPicture.ID,
                ModifiedDate = DateTime.Now,
            };
            var customerPictureMapped = Mapper.Map<CustomerPicture, CustomerPicture>(customerPicture, newPicture);
            DbContext.CustomerPictures.Update(customerPictureMapped);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, customerPictureMapped);
        }

        public async Task<IDataResult> DeleteByFileNameAsync(string fileName)
        {
            var customerPicture = await DbContext.CustomerPictures.SingleOrDefaultAsync(a => a.FileName == fileName);
            if (customerPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunmamakta.");
            var result = FileUpload.Delete(customerPicture.FilePath!);
            if (result.ResultStatus == ResultStatus.Error)
                return result;
            DbContext.CustomerPictures.Remove(customerPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "kullanıcı fotoğrafı başarıyla silindi.");
        }

        public async Task<IDataResult> GetByCustomerIdAsync(int customerId)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == customerId);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "böyle bir kullanıcı bulunamadı.");
            var customerPicture = await DbContext.CustomerPictures.SingleOrDefaultAsync(a => a.ID == customerId);
            if (customerPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunamadı.");
            return new DataResult(ResultStatus.Success, customerPicture);
        }

        public async Task<IDataResult> GetByIdAsync(int id)
        {
            var customerPicture = await DbContext.CustomerPictures.SingleOrDefaultAsync(a => a.ID == id);
            if (customerPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunamadı.");
            return new DataResult(ResultStatus.Success, customerPicture);
        }

       
    }
}
