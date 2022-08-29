using E_Commerce.Entities.Dtos.ReportPictureDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IReportPictureService
    {
        Task<IDataResult> AddAsync(ReportPictureAddDto reportPictureAddDto);
        Task<IDataResult> UpdateAsync(ReportPictureUpdateDto reportPictureUpdateDto);
        Task<IDataResult> GetByIdAsync(int id);
        Task<IDataResult> GetAllByReportIdAsync(int reportId);
        Task<IDataResult> DeleteByFileNameAsync(string fileName);
    }
}
