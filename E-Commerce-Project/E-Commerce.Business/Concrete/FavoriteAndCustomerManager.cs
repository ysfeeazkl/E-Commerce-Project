using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.FavoriteAndCustomerValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.FavoriteAndCustomerDtos;
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
    public class FavoriteAndCustomerManager : ManagerBase,IFavoriteAndCustomerService
    {
        public FavoriteAndCustomerManager(CommerceContext context, IMapper mapper) : base(mapper, context)
        {

        }

        public async Task<IDataResult> AddAsync(FavoriteAndCustomerAddDto favoriteAndCustomerAddDto)
        {
            ValidationTool.Validate(new FavoriteAndCustomerAddDtoValidator(), favoriteAndCustomerAddDto);

            var categoryAndProductIsExist = await DbContext.FavoriteAndCustomers.SingleOrDefaultAsync(a => a.CustomerID == favoriteAndCustomerAddDto.CustomerID && a.ProductID == favoriteAndCustomerAddDto.ProductID);
            if (categoryAndProductIsExist is not null)
                return new DataResult(ResultStatus.Error, "Bu kullanıcı ve ürün daha önce eşleşmiş durumda");

            var product = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == favoriteAndCustomerAddDto.ProductID);
            if (product is null)
                return new DataResult(ResultStatus.Error, "Böyle bir ürün bulunamadı");

            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == favoriteAndCustomerAddDto.CustomerID);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı bulunamadı");

            var favoriteAndCustomer = Mapper.Map<FavoriteAndCustomer>(favoriteAndCustomerAddDto);
            favoriteAndCustomer.Product = product;
            favoriteAndCustomer.Customer = customer;

            customer.Favorites.Add(favoriteAndCustomer);
            product.Favorites.Add(favoriteAndCustomer);

            await DbContext.FavoriteAndCustomers.AddAsync(favoriteAndCustomer);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, favoriteAndCustomer);
        }

        public async Task<IDataResult> UpdateAsync(FavoriteAndCustomerUpdateDto favoriteAndCustomerUpdate)
        {
            ValidationTool.Validate(new FavoriteAndCustomerUpdateDtoValidator(), favoriteAndCustomerUpdate);

            var favoriteAndCustomer = await DbContext.FavoriteAndCustomers.Include(a => a.Product).SingleOrDefaultAsync(a => a.ProductID == favoriteAndCustomerUpdate.ProductID && a.CustomerID == favoriteAndCustomerUpdate.CustomerID);
            if (favoriteAndCustomer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı ve ürün bulunamadı.");

            if (favoriteAndCustomerUpdate.NewProductID.HasValue)
                favoriteAndCustomer.ProductID = favoriteAndCustomerUpdate.NewProductID.Value;
            if (favoriteAndCustomerUpdate.NewCustomerID.HasValue)
                favoriteAndCustomer.CustomerID = favoriteAndCustomerUpdate.NewCustomerID.Value;

            var newFavoriteAndCustomer = Mapper.Map<FavoriteAndCustomerUpdateDto, FavoriteAndCustomer>(favoriteAndCustomerUpdate, favoriteAndCustomer);
            DbContext.FavoriteAndCustomers.Update(newFavoriteAndCustomer);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, newFavoriteAndCustomer);
        }

        public async Task<IDataResult> DeleteByFavoriteIdAndCustomerIdAsync(int customerId, int productId)
        {
            var favoriteAndProduct = await DbContext.FavoriteAndCustomers.SingleOrDefaultAsync(a => a.ProductID == productId && a.CustomerID == customerId);
            if (favoriteAndProduct is null)
                return new DataResult(ResultStatus.Error, "Böyle bir ürün ve kullanıcı bulunamadı.");
            DbContext.FavoriteAndCustomers.Remove(favoriteAndProduct);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "başarıyla silindi.");
        }

        public async Task<IDataResult> GetByCustomerIdAsync(int customerId, bool includeCustomer)
        {
            if (customerId < 1)
                return new DataResult(ResultStatus.Error, "Geçerli bir veri giriniz");
            IQueryable<FavoriteAndCustomer> query = DbContext.Set<FavoriteAndCustomer>().Include(a => a.Product).Where(a => a.CustomerID == customerId);
            if (includeCustomer) query = query.Include(a => a.Customer);

            return new DataResult(ResultStatus.Success, query);
        }

        public async Task<IDataResult> GetByFavoriteIdAsync(int productId, bool includeProduct)
        {
            if (productId < 1)
                return new DataResult(ResultStatus.Error, "Geçerli bir veri giriniz");
            IQueryable<FavoriteAndCustomer> query = DbContext.Set<FavoriteAndCustomer>().Include(a => a.Customer).Where(a => a.ProductID == productId);
            if (includeProduct) query = query.Include(a => a.Product);
            return new DataResult(ResultStatus.Success, query);
        }

   
    }
}
