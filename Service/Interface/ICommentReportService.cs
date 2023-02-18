using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CommentReport;

namespace IdealDiscuss.Service.Interface
{
    public interface ICommentReportService
    {
        BaseResponseModel CreateCommentReport(CreateCommentReportDto createCommentReportDto);
        BaseResponseModel DeleteCommentReport(string commentReportId);
        BaseResponseModel UpdateCommentReport(string commentReportId, UpdateCommentReportDto updateCommentReportDto);
        BaseResponseModel GetCommentReport(string commentReportId);
        BaseResponseModel GetAllCommentReport();
    }
}
