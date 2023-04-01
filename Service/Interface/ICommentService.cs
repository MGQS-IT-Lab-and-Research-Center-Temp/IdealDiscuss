using IdealDiscuss.Models;
using IdealDiscuss.Models.Comment;
using static IdealDiscuss.Models.Comment.CommentResponse;

namespace IdealDiscuss.Service.Interface;

public interface ICommentService
{
    BaseResponseModel CreateComment(CreateCommentViewModel request);
    BaseResponseModel DeleteComment(string commentId);
    BaseResponseModel UpdateComment(string commentId, UpdateCommentViewModel request);
    CommentResponseModel GetComment(string commentId);
    CommentsResponseModel GetAllComment();
}
