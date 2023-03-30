using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CommentReport;

namespace IdealDiscuss.Service.Interface
{
    public interface ICommentReportService
    {
        BaseResponseModel CreateCommentReport(CreateCommentReportDto request);
        BaseResponseModel DeleteCommentReport(string id);
        BaseResponseModel UpdateCommentReport(string id, UpdateCommentReportDto request);
        CommentReportResponseModel GetCommentReport(string id);
        CommentReportsResponseModel GetAllCommentReport();
    }
}
