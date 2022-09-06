using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.ProductValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.ProductDtos;
using E_Commerce.Shared.Utilities.FileUploads;
using E_Commerce.Shared.Utilities.Results.Abstract;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using E_Commerce.Shared.Utilities.Results.Concrete;
using E_Commerce.Shared.Utilities.Validation.FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Concrete
{
    public class ProductManager : ManagerBase, IProductService
    {
        IHttpContextAccessor _httpContextAccessor;

        public ProductManager(CommerceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult> AddAsync(ProductAddDto productAddDto)
        {
            ValidationTool.Validate(new ProductAddDtoValidator(), productAddDto);
            var product = Mapper.Map<Product>(productAddDto);
            product.CreatedDate = DateTime.Now;
            //product.CreatedByUserId = Convert.ToInt32(_httpContextAccessor.HttpContext!.User.Claims.SingleOrDefault(a => a.Type == "UserId").Value);
            product.Like = 0;

            //var seller = await DbContext.Sellers.SingleOrDefaultAsync(a => a.ID == productAddDto.SellerID);
            //product.Seller = seller!;

            if (productAddDto.BrandName is not null)
            {
                var brand = await DbContext.Brands.SingleOrDefaultAsync(a => a.Name.Contains(productAddDto.BrandName));
                if (brand is null)
                    return new DataResult(ResultStatus.Error, "Böyle bir şirket bulunamadı.");

                product.Brand = brand;
                product.BrandID = brand.ID;
            }

            await DbContext.Products.AddAsync(product);
            await DbContext.SaveChangesAsync();
            if (productAddDto.File is not null)
            {
                var result = FileUpload.UploadAlternative(productAddDto.File, "Product");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    ProductPicture productPicture = new ProductPicture()
                    {
                        ProductID = product.ID,
                        Product = product,
                        FileName = result.Message,
                        FilePath = result.Data.ToString(),
                        CreatedDate = DateTime.Now,
                    };
                    await DbContext.ProductPictures.AddAsync(productPicture);
                    await DbContext.SaveChangesAsync();
                }
            }
            return new DataResult(ResultStatus.Success, "Başarıyla Ürün Eklendi.");
        }
        public async Task<IDataResult> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            ValidationTool.Validate(new ProductUpdateDtoValidator(), productUpdateDto);

            var productIsExist = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == productUpdateDto.ID);
            if (productIsExist is null)
                return new DataResult(ResultStatus.Error, "Böyle bir Marka bulunamadı.");
            var product = Mapper.Map<ProductUpdateDto, Product>(productUpdateDto, productIsExist);

            product.ModifiedDate = DateTime.Now;
            //product.ModifiedByUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(a => a.Type == "UserId").Value);

            DbContext.Products.Update(product);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Ürün başarıyla güncellendi.");
        }

        public async Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            IQueryable<Product> query = DbContext.Set<Product>().AsNoTracking();//.Include(a=>a.Seller)
            if (isDeleted.HasValue)
                query = query.Where(a => a.IsActive == isDeleted);
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(a => a.ID) : query.OrderByDescending(a => a.ID);
                    break;
                case OrderBy.Az:
                    query = isAscending ? query.OrderBy(a => a.Name) : query.OrderByDescending(a => a.Name);
                    break;
                case OrderBy.CreatedDate:
                    query = isAscending ? query.OrderBy(a => a.CreatedDate) : query.OrderByDescending(a => a.CreatedDate);
                    break;
                default:
                    query = isAscending ? query.OrderBy(a => a.ID) : query.OrderByDescending(a => a.ID);
                    break;
            }

            if (currentPage != 0 && pageSize != 0)
            {
                var filteredQuery = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<Product>(a)).ToListAsync();
                return new DataResult(ResultStatus.Success, filteredQuery);
            }
            return new DataResult(ResultStatus.Success, query);
        }

        public async Task<IDataResult> LikeEventByIdAsync(int id,int likeAndDislike)
        {
            var product = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == id);
            if (product is null)
                return new DataResult(ResultStatus.Error, "Böyle bir ürün bulunamadı.");
            product.Like += likeAndDislike;

            DbContext.Products.Update(product);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, product);
        }
        public async Task<IDataResult> GetByIdAsync(int id)
        {
            var product = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == id);
            if (product is null)
                return new DataResult(ResultStatus.Error, "Böyle bir ürün bulunamadı.");
            return new DataResult(ResultStatus.Success, product);
        }
        public async Task<IDataResult> GetAllByBrandIdAsync(int brandId)
        {
            var brand = await DbContext.Brands.SingleOrDefaultAsync(a=>a.ID==brandId);
            if (brand is null)
                return new DataResult(ResultStatus.Error, "Böyle bir marka bulunamadı.");

            var products =  DbContext.Products.Where(a => a.ID == brand.ID);
            if (products is null)
                return new DataResult(ResultStatus.Error, "Bu markaya sahip bir ürün bulunamadı.");
            return new DataResult(ResultStatus.Success, products);

        }

        public async Task<IDataResult> GetAllByCategoryIdAsync(int categoryId)
        {
            var category = await DbContext.Categories.SingleOrDefaultAsync(a => a.ID == categoryId);
            if (category is null)
                return new DataResult(ResultStatus.Error, "Böyle bir categori bulunamadı.");

            var products = DbContext.Products.Where(a => a.ID == category.ID);
            if (products is null)
                return new DataResult(ResultStatus.Error, "Bu categoriye sahip bir ürün bulunamadı.");
            return new DataResult(ResultStatus.Success, products);
        }

        public async Task<IDataResult> GetAllBySellerIdAsync(int sellerId)
        {
            var seller = await DbContext.Sellers.SingleOrDefaultAsync(a => a.ID == sellerId);
            if (seller is null)
                return new DataResult(ResultStatus.Error, "Böyle bir satıcı bulunamadı.");

            var products = DbContext.Products.Where(a => a.ID == seller.ID);
            if (products is null)
                return new DataResult(ResultStatus.Error, "Bu satıcıya sahip bir ürün bulunamadı.");
            return new DataResult(ResultStatus.Success, products);
        }

        public async Task<IDataResult> HardDeleteByIdAsync(int id)
        {
            var product = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == id);
            if (product is null)
                return new DataResult(ResultStatus.Error, "Böyle bir ürün bulunamadı.");      
            DbContext.Products.Remove(product);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{product.Name}, ürünü silindi.");
        }
        public async Task<IDataResult> DeleteByIdAsync(int id)
        {
            var product = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == id);
            if (product is null)
                return new DataResult(ResultStatus.Error, "Böyle bir ürün bulunamadı.");
            product.IsDeleted = true;
            product.IsActive = false;
            DbContext.Products.Update(product);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"{product.Name}, ürünü silindi.");
        }


    }
}
