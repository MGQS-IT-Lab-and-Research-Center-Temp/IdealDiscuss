namespace IdealDiscuss.Models.Comment;

public class CommentResponse
{
    public class CommentResponseModel : BaseResponseModel
    {
        public ViewCommentModel Data { get; set; }
    }

    public class CommentsResponseModel : BaseResponseModel
    {
        public  List<ViewCommentModel> Data { get; set; }
    }
}
