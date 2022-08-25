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
        Task<IDataResult> AddAsync();
        Task<IDataResult> UpdateAsync();
        Task<IDataResult> GetByIdAsync(int id);
        Task<IDataResult> GetByBrandIdAsync(int brandId);
        Task<IDataResult> DeleteByFileNameAsync(string fileName);

    }
}
