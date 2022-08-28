using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.CustomerValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.CustomerDtos;
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
    public class CustomerManager : ManagerBase, ICustomerService
    {

        IHttpContextAccessor _httpContextAccessor;
        public CustomerManager(CommerceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, context)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<IDataResult> UpdateAsync(CustomerUpdateDto customerUpdateDto)
        {
            ValidationTool.Validate(new CustomerUpdateDtoValidator(), customerUpdateDto);

            var OldCustomer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == customerUpdateDto.ID);
            if (OldCustomer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı bulunamadı.");
            var customer = Mapper.Map<CustomerUpdateDto, Customer>(customerUpdateDto, OldCustomer);

            customer.ModifiedDate = DateTime.Now;
            customer.ModifiedByUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(a => a.Type == "UserId").Value);

            DbContext.Customers.Update(customer);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "kullanıcı başarıyla güncellendi.");



        }

        public async Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            IQueryable<Customer> query = DbContext.Set<Customer>().AsNoTracking();
            if (isDeleted.HasValue)
                query = query.Where(a => a.IsActive == isDeleted);
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(a => a.ID) : query.OrderByDescending(a => a.ID);
                    break;
                case OrderBy.Az:
                    query = isAscending ? query.OrderBy(a => a.FirstName) : query.OrderByDescending(a => a.FirstName);
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
                var filteredQuery = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<Brand>(a)).ToListAsync();
                return new DataResult(ResultStatus.Success, filteredQuery);
            }
            return new DataResult(ResultStatus.Success, query);
        }

        public async Task<IDataResult> GetByEmailAddressAsync(string emailAddress)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.EmailAddress == emailAddress);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı yok");
            return new DataResult(ResultStatus.Success, customer);

        }

        public async Task<IDataResult> GetByIdAsync(int id)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == id);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı yok");
            return new DataResult(ResultStatus.Success, customer);
        }

        public async Task<IDataResult> GetByPhoneNumberAsync(string phoneNumber)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.PhoneNumber == phoneNumber);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı yok");
            return new DataResult(ResultStatus.Success, customer);
        }

        public async Task<IDataResult> GetByUserNameAsync(string userName)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.UserName == userName);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı yok");
            return new DataResult(ResultStatus.Success, customer);
        }

        public async Task<IDataResult> DeleteByIdAsync(int id)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == id);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı yok");

            customer.ModifiedDate = DateTime.Now;
            customer.IsDeleted = true;
            customer.IsActive = false;
            DbContext.Customers.Update(customer);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Error, "başarı ile silindi");
        }
        public async Task<IDataResult> HardDeleteByIdAsync(int id)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == id);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı yok");

            DbContext.Customers.Remove(customer);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Error, "başarı ile silindi");
        }



    }
}
