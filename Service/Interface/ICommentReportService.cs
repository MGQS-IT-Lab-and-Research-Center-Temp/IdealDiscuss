using IdealDiscuss.Models;
using IdealDiscuss.Models.CommentReport;

namespace IdealDiscuss.Service.Interface
{
    public interface ICommentReportService
    {
        BaseResponseModel CreateCommentReport(CreateCommentReportViewModel request);
        BaseResponseModel DeleteCommentReport(string id);
        BaseResponseModel UpdateCommentReport(string id, UpdateCommentReportViewModel request);
        CommentReportResponseModel GetCommentReport(string id);
        CommentReportsResponseModel GetAllCommentReport();
    }
}
