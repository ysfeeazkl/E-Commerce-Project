using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.ReportPictureValidators;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.ReportPictureDtos;
using E_Commerce.Shared.Utilities.FileUploads;
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
    public class ReportPictureManager : ManagerBase,IReportPictureService
    {
        public ReportPictureManager(CommerceContext context, IMapper mapper) : base(mapper, context)
        {

        }

        public async Task<IDataResult> AddAsync(ReportPictureAddDto reportPictureAddDto)
        {
            ValidationTool.Validate(new ReportPictureAddDtoValidator(), reportPictureAddDto);
            var report = await DbContext.Reports.SingleOrDefaultAsync(a => a.ID == reportPictureAddDto.ReportID);
            if (report is null)
                return new DataResult(ResultStatus.Error, "Böyle bir rapor yok.");
            if (await DbContext.ReportPictures.Where(a => a.ReportID == reportPictureAddDto.ReportID).CountAsync() == 3)
                return new DataResult(ResultStatus.Error, "Bir rapora maksimum 3 adet fotoğraf eklenebilir.");
            var result = FileUpload.UploadAlternative(reportPictureAddDto.File, "Reports");
            if (result.ResultStatus == ResultStatus.Error)
                return result;
            ReportPicture reportPicture = new ReportPicture
            {
                FilePath = result.Data.ToString(),
                ReportID = report.ID,
                Report = report,
                FileName = result.Message,
                CreatedDate = DateTime.Now,
            };
            await DbContext.ReportPictures.AddAsync(reportPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "Ürün fotoğrafı başarı ile eklendi");
        }

        public async Task<IDataResult> UpdateAsync(ReportPictureUpdateDto reportPictureUpdateDto)
        {
            ValidationTool.Validate(new ReportPictureUpdateDtoValidator(), reportPictureUpdateDto);
            var reportPicture = await DbContext.ReportPictures.SingleOrDefaultAsync(a => a.ID == reportPictureUpdateDto.ID || a.FileName == reportPictureUpdateDto.File.FileName);
            if (reportPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir fotoğraf bulunamadı.");
            
            var updateFile = FileUpload.UploadAlternative(reportPictureUpdateDto.File, "Reports");
            if (updateFile.ResultStatus == ResultStatus.Error)
                return updateFile;

            ReportPicture newPicture = new ReportPicture
            {
                FileName = updateFile.Message,
                FilePath = updateFile.Data.ToString(),
                ReportID = reportPictureUpdateDto.ReportID,
                ID = reportPicture.ID,
                ModifiedDate = DateTime.Now,
            };
            var reportPictureMapped = Mapper.Map<ReportPicture, ReportPicture>(reportPicture, newPicture);
            DbContext.ReportPictures.Update(reportPictureMapped);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, reportPictureMapped);
        }

        public async Task<IDataResult> DeleteByFileNameAsync(string fileName)
        {
            var reportPicture = await DbContext.ReportPictures.SingleOrDefaultAsync(a => a.FileName == fileName);
            if (reportPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunmamakta.");
            var result = FileUpload.Delete(reportPicture.FilePath!);
            if (result.ResultStatus == ResultStatus.Error)
                return result;
            DbContext.ReportPictures.Remove(reportPicture);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, "rapor fotoğrafı başarıyla silindi.");
        }

        public async Task<IDataResult> GetByIdAsync(int id)
        {
            var reportPicture = await DbContext.ReportPictures.SingleOrDefaultAsync(a => a.ID == id);
            if (reportPicture is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunamadı.");
            return new DataResult(ResultStatus.Success, reportPicture);
        }

        public async Task<IDataResult> GetAllByReportIdAsync(int reportId)
        {
            var report = await DbContext.Reports.SingleOrDefaultAsync(a => a.ID == reportId);
            if (report is null)
                return new DataResult(ResultStatus.Error, "Böyle bir rapor yok.");
            var reportPictures = DbContext.ReportPictures.Where(a => a.ID == reportId);
            if (reportPictures is null)
                return new DataResult(ResultStatus.Error, "Böyle bir resim bulunamadı.");
            return new DataResult(ResultStatus.Success, reportPictures);
        }

       
    }
}
