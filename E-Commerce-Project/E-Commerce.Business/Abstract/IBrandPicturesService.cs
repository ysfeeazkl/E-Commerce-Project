using E_Commerce.Entities.Dtos.BrandPictureDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IBrandPicturesService
    {
        Task<IDataResult> AddAsync(BrandPictureAddDto brandPictureAddDto);
        Task<IDataResult> UpdateAsync(BrandPictureUpdateDto brandPictureUpdateDto);
        Task<IDataResult> GetByIdAsync(int id);
        Task<IDataResult> GetAllByBrandIdAsync(int brandId);
        Task<IDataResult> DeleteByFileNameAsync(string fileName);
        Task<IDataResult> GetByFileNameAsync(string fileName);


    }
}
