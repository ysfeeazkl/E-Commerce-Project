using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.ReportValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.ReportDtos;
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
    public class ReportManager:ManagerBase,IReportService
    {
        IHttpContextAccessor _httpContextAccessor;

        public ReportManager(CommerceContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult> AddAsync(ReportAddDto reportAddDto)
        {
            ValidationTool.Validate(new ReportAddDtoValidator(), reportAddDto);
            var report = Mapper.Map<Report>(reportAddDto);
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == reportAddDto.CustomerID);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı bulunamadı");

            report.CreatedDate = DateTime.Now;
            report.CreatedByUserId = reportAddDto.CustomerID;
            report.Customer = customer;

            await DbContext.Reports.AddAsync(report);
            await DbContext.SaveChangesAsync();

            if (reportAddDto.File is not null)
            {
                var result = FileUpload.UploadAlternative(reportAddDto.File, "Reports");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    ReportPicture reportPicture = new ReportPicture()
                    {
                        ReportID = report.ID,
                        Report = report,
                        FileName = result.Message,
                        FilePath = result.Data.ToString(),
                        CreatedDate = DateTime.Now,
                    };
                    await DbContext.ReportPictures.AddAsync(reportPicture);
                    await DbContext.SaveChangesAsync();
                }
            }

            return new DataResult(ResultStatus.Success, "Rapor başarı ile eklendi", report);
        }

        public async Task<IDataResult> UpdateAsync(ReportUpdateDto reportUpdateDto)
        {
            ValidationTool.Validate(new ReportUpdateDtoValidator(), reportUpdateDto);

            var reportIsExist = await DbContext.Reports.SingleOrDefaultAsync(a => a.ID == reportUpdateDto.ID);
            if (reportIsExist is null)
                return new DataResult(ResultStatus.Error, "Böyle bir rapor bulunamadı.");
            var report = Mapper.Map<ReportUpdateDto, Report>(reportUpdateDto, reportIsExist);

            report.ModifiedDate = DateTime.Now;
            report.ModifiedByUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(a => a.Type == "UserId").Value);

            DbContext.Reports.Update(report);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Rapor başarıyla güncellendi.");
        }

       
        public async Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy)
        {
            IQueryable<Report> query = DbContext.Set<Report>().Include(a => a.Customer).AsNoTracking();
            if (isDeleted.HasValue)
                query = query.Where(a => a.IsActive == isDeleted);
            switch (orderBy)
            {
                case OrderBy.Id:
                    query = isAscending ? query.OrderBy(a => a.ID) : query.OrderByDescending(a => a.ID);
                    break;
                case OrderBy.Az:
                    query = isAscending ? query.OrderBy(a => a.Customer.FirstName) : query.OrderByDescending(a => a.Customer.FirstName);
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
                var filteredQuery = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(a => Mapper.Map<Report>(a)).ToListAsync();
                return new DataResult(ResultStatus.Success, filteredQuery);
            }
            return new DataResult(ResultStatus.Success, query);
        }


        public async Task<IDataResult> GetByID(int id)
        {
            var report = await DbContext.Reports.SingleOrDefaultAsync(a => a.ID == id);
            if (report is null)
                return new DataResult(ResultStatus.Error, "Böyle bir rapor bulunamadı.");
            return new DataResult(ResultStatus.Success, report);

        }

        public async Task<IDataResult> GetAllByBrandID(int brandId)
        {
            var brand = await DbContext.Brands.SingleOrDefaultAsync(a=>a.ID==brandId);
            if (brand is null)
                return new DataResult(ResultStatus.Error, "Böyle bir marka bulunamadı.");
            var report =  DbContext.Reports.Where(a => a.ID == brand.ID);
            if (report is null)
                return new DataResult(ResultStatus.Error, "Böyle bir rapor bulunamadı.");
            return new DataResult(ResultStatus.Success, report);


        }

        public async Task<IDataResult> GetAllByCommentID(int commentId)
        {
            var comment = await DbContext.Comments.SingleOrDefaultAsync(a => a.ID == commentId);
            if (comment is null)
                return new DataResult(ResultStatus.Error, "Böyle bir yorum bulunamadı.");
            var reports = DbContext.Reports.Where(a => a.ID == comment.ID);
            if (reports is null)
                return new DataResult(ResultStatus.Error, "Böyle bir rapor bulunamadı.");
            return new DataResult(ResultStatus.Success, reports);
        }

        public async Task<IDataResult> GetAllByCustomerID(int customerId)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == customerId);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı bulunamadı.");
            var reports = DbContext.Reports.Where(a => a.ID == customer.ID);
            if (reports is null)
                return new DataResult(ResultStatus.Error, "Böyle bir rapor bulunamadı.");
            return new DataResult(ResultStatus.Success, reports);
        }

      
        public async Task<IDataResult> GetAllByProductID(int productId)
        {
            var product = await DbContext.Products.SingleOrDefaultAsync(a => a.ID == productId);
            if (product is null)
                return new DataResult(ResultStatus.Error, "Böyle bir ürün bulunamadı.");
            var reports = DbContext.Reports.Where(a => a.ID == product.ID);
            if (reports is null)
                return new DataResult(ResultStatus.Error, "Böyle bir rapor bulunamadı.");
            return new DataResult(ResultStatus.Success, reports);
        }

        public async Task<IDataResult> GetAllBySellerID(int sellerId)
        {
            var seller = await DbContext.Sellers.SingleOrDefaultAsync(a => a.ID == sellerId);
            if (seller is null)
                return new DataResult(ResultStatus.Error, "Böyle bir satıcı bulunamadı.");
            var reports = DbContext.Reports.Where(a => a.ID == seller.ID);
            if (reports is null)
                return new DataResult(ResultStatus.Error, "Böyle bir rapor bulunamadı.");
            return new DataResult(ResultStatus.Success, reports);
        }

        public async Task<IDataResult> DeleteByIdAsync(int id)
        {
            var report = await DbContext.Reports.SingleOrDefaultAsync(a => a.ID == id);
            if (report is null)
                return new DataResult(ResultStatus.Error, "Böyle bir rapor bulunamadı.");
            report.IsDeleted = true;
            report.IsActive = false;
            DbContext.Reports.Update(report);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "rapor silindi.");
        }
        public async Task<IDataResult> HardDeleteByIdAsync(int id)
        {
            var report = await DbContext.Reports.SingleOrDefaultAsync(a => a.ID == id);
            if (report is null)
                return new DataResult(ResultStatus.Error, "Böyle bir rapor bulunamadı.");
            DbContext.Reports.Remove(report);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "rapor silindi.");
        }



    }
}
