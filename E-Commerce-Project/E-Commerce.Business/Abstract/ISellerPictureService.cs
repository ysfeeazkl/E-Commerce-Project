using E_Commerce.Entities.Dtos.SellerPictureDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface ISellerPictureService
    {
        Task<IDataResult> AddAsync(SellerPictureAddDto sellerPictureAddDto);
        Task<IDataResult> UpdateAsync(SellerPictureUpdateDto sellerPictureUpdateDto);
        Task<IDataResult> GetByIdAsync(int id);
        Task<IDataResult> GetBySellerIdAsync(int sellerId);
        Task<IDataResult> DeleteByFileNameAsync(string fileName);
    }
}
