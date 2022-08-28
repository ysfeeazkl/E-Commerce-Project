using E_Commerce.Entities.Dtos.ProductPictureDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IProductPictureService
    {
        Task<IDataResult> AddAsync(ProductPictureAddDto productPictureAddDto);
        Task<IDataResult> UpdateAsync(ProductPictureUpdateDto productPictureUpdateDto);
        Task<IDataResult> GetByIdAsync(int id);
        Task<IDataResult> GetAllByProductIdAsync(int productId);
        Task<IDataResult> DeleteByFileNameAsync(string fileName);
    }
}
