using IdealDiscuss.Models;
using IdealDiscuss.Models.Comment;
using static IdealDiscuss.Models.Comment.CommentResponse;

namespace IdealDiscuss.Service.Interface;

public interface ICommentService
{
    Task<BaseResponseModel> CreateComment(CreateCommentViewModel request);
    Task<BaseResponseModel> DeleteComment(string commentId);
    Task<BaseResponseModel> UpdateComment(string commentId, UpdateCommentViewModel request);
    Task<CommentResponseModel> GetComment(string commentId);
    Task<CommentsResponseModel> GetAllComment();
}
