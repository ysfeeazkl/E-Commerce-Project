using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.CategoryValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.CategoryDtos;
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
    public class CategoryManager : ManagerBase, ICategoryService
    {
        public CategoryManager(CommerceContext context, IMapper mapper) : base(mapper, context)
        {

        }

        public async Task<IDataResult> AddAsync(CategoryAddDto categoryAddDto)
        {
            ValidationTool.Validate(new CategoryAddDtoValidator(), categoryAddDto);
            if (await DbContext.Categories.AnyAsync(a => a.Name == categoryAddDto.Name))
                return new DataResult(ResultStatus.Error, "Böyle bir kategori zaten mevcut");
            var category = Mapper.Map<Category>(categoryAddDto);

            category.CreatedDate = DateTime.Now;
            await DbContext.Categories.AddAsync(category);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{categoryAddDto.Name} adlı Kategori başarıyla eklendi.");
        }

        public async Task<IDataResult> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            ValidationTool.Validate(new CategoryUpdateDtoValidator(), categoryUpdateDto);
            var oldCategory = await DbContext.Categories.SingleOrDefaultAsync(a => a.ID == categoryUpdateDto.ID);
            if (oldCategory is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kategori bulunmamakta.");
            var newCategory = Mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCategory);
            newCategory.ModifiedDate = DateTime.Now;
            DbContext.Categories.Update(newCategory);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{newCategory.Name} adlı kategori başarıyla güncellendi.");
        }
        public async Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            IQueryable<Category> categories = DbContext.Set<Category>().Include(a => a.CategoryAndProducts).ThenInclude(a => a.Product).AsNoTracking();
            if (isDeleted.HasValue) categories = categories.Where(a => a.IsDeleted == isDeleted);
            switch (orderBy)
            {
                case OrderBy.Id:
                    categories = isAscending ? categories.OrderBy(a => a.ID) : categories.OrderByDescending(a => a.ID);
                    break;
                case OrderBy.Az:
                    categories = isAscending ? categories.OrderBy(a => a.Name) : categories.OrderByDescending(a => a.Name);
                    break;                          
                case OrderBy.CreatedDate:
                    categories = isAscending ? categories.OrderBy(a => a.CreatedDate) : categories.OrderByDescending(a => a.CreatedDate);
                    break;
                default:
                    categories = isAscending ? categories.OrderBy(a => a.Name) : categories.OrderByDescending(a => a.Name);
                    break;
            }
            if (currentPage != 0 && pageSize !=0)
            {
                var filteredQuery = await categories.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<Category>(a)).ToListAsync();
                return new DataResult(ResultStatus.Success, filteredQuery);
            }

            return new DataResult(ResultStatus.Success, categories);
        }

        public async Task<IDataResult> GetByID(int id)
        {
            var category = await DbContext.Categories.SingleOrDefaultAsync(a => a.ID == id);
            if (category is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
            return new DataResult(ResultStatus.Success, category);
        }

        public async Task<IDataResult> DeleteByIdAsync(int id)
        {
            var category = await DbContext.Categories.SingleOrDefaultAsync(a => a.ID == id);
            if (category is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
            category.IsDeleted = true;
            category.IsActive = false;
            DbContext.Categories.Update(category);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{category.Name} adlı kategori silindi.");
        }
        public async Task<IDataResult> DeleteByNameAsync(string name)
        {
            var category = await DbContext.Categories.SingleOrDefaultAsync(a => a.Name == name);
            if (category is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
            category.IsDeleted = true;
            category.IsActive = false;
            DbContext.Categories.Update(category);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{category.Name} adlı kategori silindi.");
        }

        public async Task<IDataResult> HardDeleteByIdAsync(int id)
        {
            var category = await DbContext.Categories.SingleOrDefaultAsync(a => a.ID == id);
            if (category is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
            
            DbContext.Categories.Remove(category);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{category.Name} adlı kategori silindi.");
        }

        public async Task<IDataResult> HardDeleteByNameAsync(string name)
        {
            var category = await DbContext.Categories.SingleOrDefaultAsync(a => a.Name == name);
            if (category is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kategori bulunamadı.");

            DbContext.Categories.Remove(category);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{category.Name} adlı kategori silindi.");
        }
    }






}

