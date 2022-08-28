using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.CategoryAndProductValidator;
using E_Commerce.Business.ValidationRules.FluentValidation.CategoryAndProductValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.CategoryAndProductDtos;
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
    public class CategoryAndProductManager : ManagerBase,ICategoryAndProductService
    {
        public CategoryAndProductManager(CommerceContext context, IMapper mapper) : base(mapper, context)
        {

        }

        public async Task<IDataResult> AddAsync(CategoryAndProductAddDto categoryAndProductAddDto)
        {
            ValidationTool.Validate(new CategoryAndProductAddDtoValidator(), categoryAndProductAddDto);

            var categoryAndProductIsExist = await DbContext.CategoryAndProducts.SingleOrDefaultAsync(a => a.CategoryID == categoryAndProductAddDto.CategoryID && a.ProductID == categoryAndProductAddDto.ProductID);
            if (categoryAndProductIsExist is not null)
                return new DataResult(ResultStatus.Error,"Bu kategori ve ürün daha önce eşleşmiş durumda");

            var product = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == categoryAndProductAddDto.ProductID);
            if (product is null)
                return new DataResult(ResultStatus.Error, "Böyle bir ürün bulunamadı");

            var category = await DbContext.Categories.SingleOrDefaultAsync(a => a.ID == categoryAndProductAddDto.CategoryID);
            if (category is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kategori bulunamadı");

            var categoryAndProduct = Mapper.Map<CategoryAndProduct>(categoryAndProductAddDto);
            categoryAndProduct.Product = product;
            categoryAndProduct.Category = category;

            category.CategoryAndProducts.Add(categoryAndProduct);
            product.CategoryAndProducts.Add(categoryAndProduct);

            await DbContext.CategoryAndProducts.AddAsync(categoryAndProduct);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, categoryAndProduct);
        }

        public async Task<IDataResult> DeleteByCategoryIdAndProductIdAsync(int categoryId, int productId)
        {
            var categoryAndProduct = await DbContext.CategoryAndProducts.SingleOrDefaultAsync(a => a.ProductID == productId&& a.CategoryID == categoryId);
            if (categoryAndProduct is null)
                return new DataResult(ResultStatus.Error, "Böyle bir ürün ve kategori bulunamadı.");
            DbContext.CategoryAndProducts.Remove(categoryAndProduct);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "başarıyla silindi.");
        }

        public async Task<IDataResult> GetByCategoryIdAsync(int categoryId, bool includeCategory)
        {
            if (categoryId < 1)
                return new DataResult(ResultStatus.Error, "Geçerli bir veri giriniz");
            IQueryable<CategoryAndProduct> query = DbContext.Set<CategoryAndProduct>().Include(a => a.Product).Where(a => a.CategoryID == categoryId);
            if (includeCategory) query = query.Include(a => a.Category);

            return new DataResult(ResultStatus.Success, query);
        }

        public async Task<IDataResult> GetByProductIdAsync(int productId, bool includeProduct)
        {
            if (productId < 1)
                return new DataResult(ResultStatus.Error, "Geçerli bir veri giriniz");
            IQueryable<CategoryAndProduct> query = DbContext.Set<CategoryAndProduct>().Include(a => a.Category).Where(a => a.ProductID == productId);
            if (includeProduct) query = query.Include(a => a.Product);

            return new DataResult(ResultStatus.Success, query);
        }
        public async Task<IDataResult> UpdateAsync(CategoryAndProductUpdateDto categoryAndProductUpdateDto)
        {
            ValidationTool.Validate(new CategoryAndProductUpdateDtoValidator(), categoryAndProductUpdateDto);

            var categoryAndProduct = await DbContext.CategoryAndProducts.Include(a => a.Product).SingleOrDefaultAsync(a => a.ProductID == categoryAndProductUpdateDto.ProductID && a.CategoryID == categoryAndProductUpdateDto.CategoryID);
            if (categoryAndProduct is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kategori ve ürün bulunamadı.");

            if (categoryAndProductUpdateDto.NewProductID.HasValue)
                categoryAndProduct.ProductID = categoryAndProductUpdateDto.NewProductID.Value;
            if (categoryAndProductUpdateDto.NewCategoryID.HasValue)
                categoryAndProduct.CategoryID = categoryAndProductUpdateDto.NewCategoryID.Value;
            categoryAndProduct.Product.ModifiedDate = DateTime.Now;

            var newCategoryAndProduct = Mapper.Map<CategoryAndProductUpdateDto, CategoryAndProduct>(categoryAndProductUpdateDto, categoryAndProduct);
            DbContext.CategoryAndProducts.Update(newCategoryAndProduct);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, categoryAndProduct);
        }
    }    
}
