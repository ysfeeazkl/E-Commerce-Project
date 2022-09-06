using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.ShoppingCartValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.ShoppingCartDtos;
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
    public class ShoppingCartManager : ManagerBase, IShoppingCartService
    {
        IHttpContextAccessor _httpContextAccessor;
        public ShoppingCartManager(CommerceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult> AddAsync(ShoppingCartAddDto shoppingCartAddDto)
        {
            ValidationTool.Validate(new ShoppingCartAddDtoValidator(), shoppingCartAddDto);

            var shoppingCartIsExist = await DbContext.ShoppingCarts.SingleOrDefaultAsync(a => a.CustomerID == shoppingCartAddDto.CustomerID);
            if (shoppingCartIsExist is not null)
                return new DataResult(ResultStatus.Error, "Bu kullanıcıya ait alışveriş sepeti zaten mevcut");

            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == shoppingCartAddDto.CustomerID);
            if (customer is null)
            {
                return new DataResult(ResultStatus.Success, "Böyle bir kullanıcı bulunamadı");
            }


            var shoppingCart = Mapper.Map<ShoppingCart>(shoppingCartAddDto);
            shoppingCart.CreatedDate = DateTime.Now;
           // shoppingCart.CreatedByUserId = Convert.ToInt32(_httpContextAccessor.HttpContext!.User.Claims.SingleOrDefault(a => a.Type == "UserId")!.Value);
            shoppingCart.Customer = customer;

            customer.ShoppingCart = shoppingCart;
            customer.ShoppingCartID = shoppingCart.ID;

            await DbContext.ShoppingCarts.AddAsync(shoppingCart);
            await DbContext.SaveChangesAsync();
            DbContext.Customers.Update(customer);
            await DbContext.SaveChangesAsync();


            return new DataResult(ResultStatus.Success, "Alışveriş sepeti başarıyla oluşturuldu", shoppingCart);
        }

        public async Task<IDataResult> UpdateAsync(ShoppingCartUpdateDto shoppingCartUpdateDto)
        {
            ValidationTool.Validate(new ShoppingCartUpdateDtoValidator(), shoppingCartUpdateDto);

            var shoppingCart = await DbContext.ShoppingCarts.SingleOrDefaultAsync(a => a.ID == shoppingCartUpdateDto.ID);
            if (shoppingCart is null)
                return new DataResult(ResultStatus.Error, "böyle bir alışveriş sepeti bulunmadı");
            var product = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == shoppingCartUpdateDto.ProductID);
            if (product is null)
                return new DataResult(ResultStatus.Error, "böyle bir ürün bulunmadı");

            shoppingCart.Products.Add(product);
            shoppingCart.ModifiedDate = DateTime.Now;
            //shoppingCart.ModifiedByUserId = Convert.ToInt32(_httpContextAccessor.HttpContext!.User.Claims.SingleOrDefault(a => a.Type == "UserId")!.Value);

            DbContext.ShoppingCarts.Update(shoppingCart);
            await DbContext.SaveChangesAsync();

            return new DataResult(ResultStatus.Success, "Ürün başarı ile eklendi", shoppingCart);
        }

       

        public async Task<IDataResult> GetAllAsync()
        {
            var shoppingCarts = await DbContext.ShoppingCarts.ToListAsync();
            if (shoppingCarts is null)
                return new DataResult(ResultStatus.Error, "böyle bir alışveriş sepeti bulunmadı");
            return new DataResult(ResultStatus.Error, shoppingCarts);
        }

        public async Task<IDataResult> GetByCustomerIdAsync(int customerId)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == customerId);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "böyle bir kullanıcı bulunmadı");
            var shoppingCart = await DbContext.ShoppingCarts.Include(a => a.Products).SingleOrDefaultAsync(a => a.CustomerID == customer.ID);//.Include(a=>a.Products)
            //var asd = DbContext.Set<ShoppingCart>().Include(a=>a.Products).AsNoTracking();
            if (shoppingCart is null)
                return new DataResult(ResultStatus.Error, "böyle bir alışveriş sepeti bulunmadı");
            return new DataResult(ResultStatus.Success, shoppingCart);
        }

        public async Task<IDataResult> GetByIdAsync(int id)
        {
            var shoppingCart = await DbContext.ShoppingCarts.Include(a => a.Products).SingleOrDefaultAsync(a => a.ID == id);
            if (shoppingCart is null)
                return new DataResult(ResultStatus.Error, "böyle bir alışveriş sepeti bulunmadı");
            return new DataResult(ResultStatus.Success, shoppingCart);
        }

        public async Task<IDataResult> HardDeleteByIdAsync(int id)
        {
            var shoppingCart = await DbContext.ShoppingCarts.SingleOrDefaultAsync(a => a.ID == id);
            if (shoppingCart is null)
                return new DataResult(ResultStatus.Error, "böyle bir alışveriş sepeti bulunmadı");
            DbContext.ShoppingCarts.Remove(shoppingCart);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "sepet silindi");
        }

        public async Task<IDataResult> DeleteByIdAsync(int id)
        {
            var shoppingCart = await DbContext.ShoppingCarts.SingleOrDefaultAsync(a => a.ID == id);
            if (shoppingCart is null)
                return new DataResult(ResultStatus.Error, "böyle bir alışveriş sepeti bulunmadı");

            shoppingCart.IsActive = false;
            shoppingCart.IsDeleted = true;
            DbContext.ShoppingCarts.Update(shoppingCart);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "sepet arşivlendi");

        }


    }
}
