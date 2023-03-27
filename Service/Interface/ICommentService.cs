using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CommentDto;
using static IdealDiscuss.Dtos.CommentDto.CommentResponse;

namespace IdealDiscuss.Service.Interface;

public interface ICommentService
{
    BaseResponseModel CreateComment(CreateCommentDto request);
    BaseResponseModel DeleteComment(string commentId);
    BaseResponseModel UpdateComment(string commentId, UpdateCommentDto request);
    CommentResponseModel GetComment(string commentId);
    CommentsResponseModel GetAllComment();
}
