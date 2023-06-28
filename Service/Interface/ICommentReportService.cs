using IdealDiscuss.Models;
using IdealDiscuss.Models.CommentReport;

namespace IdealDiscuss.Service.Interface;

public interface ICommentReportService
{
    Task<BaseResponseModel> CreateCommentReport(CreateCommentReportViewModel request);
    Task<BaseResponseModel> DeleteCommentReport(string id);
    Task<BaseResponseModel> UpdateCommentReport(string id, UpdateCommentReportViewModel request);
    Task<CommentReportResponseModel> GetCommentReport(string id);
    Task<CommentReportsResponseModel> GetAllCommentReport();
}
