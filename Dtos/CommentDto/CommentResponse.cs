namespace IdealDiscuss.Dtos.CommentDto
{
    public class CommentResponse
    {
        public class CommentResponseModel : BaseResponseModel
        {
            public ViewCommentDto Data { get; set; }
        }

        public class CommentsResponseModel : BaseResponseModel
        {
            public List<ViewCommentDto> Data { get; set; }
        }
    }
}
