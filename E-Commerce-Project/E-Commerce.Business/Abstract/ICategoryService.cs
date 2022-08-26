using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Dtos.CategoryDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult> AddAsync(CategoryAddDto categoryAddDto);
        Task<IDataResult> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        Task<IDataResult> GetByID(int id);
        Task<IDataResult> DeleteByIdAsync(int id);
        Task<IDataResult> HardDeleteByIdAsync(int id);
        Task<IDataResult> HardDeleteByNameAsync(string name);
        Task<IDataResult> DeleteByNameAsync(string name);
    }
}
