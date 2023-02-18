using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CommentReport;

namespace IdealDiscuss.Service.Interface
{
    public interface ICommentReportService
    {
        BaseResponseModel CreateCommentReport(CreateCommentReportDto createCommentReportDto);
        BaseResponseModel DeleteCommentReport(int commentReportId);
        BaseResponseModel UpdateCommentReport(int commentReportId, UpdateCommentReportDto updateCommentReportDto);
        CommentReportReesponseModel GetCommentReport(int commentReportId);
        BaseResponseModel GetAllCommentReport();
    }
}
