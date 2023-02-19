using IdealDiscuss.Dtos.CommentReport;
using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CommentDto;
using static IdealDiscuss.Dtos.CommentDto.CommentResponse;

namespace IdealDiscuss.Service.Interface
{
    public interface ICommentService
    {
        BaseResponseModel CreateComment(CreateCommentDto createCommentDto);
        BaseResponseModel DeleteComment(int commentId);
        BaseResponseModel UpdateComment(int commentId, UpdateCommentDto updateCommentDto);
        CommentResponseModel GetComment(int commentId);
        CommentsResponseModel GetAllComment();
    }
}
