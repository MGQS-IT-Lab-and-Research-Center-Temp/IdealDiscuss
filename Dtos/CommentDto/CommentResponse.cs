using IdealDiscuss.Dtos.CommentReport;

namespace IdealDiscuss.Dtos.CommentDto
{
    public class CommentResponse
    {
        public class CommentResponseModel : BaseResponseModel
        {
            public ViewCommentDto Comment{get; set; }
        }

        public class CommentsResponseModel : BaseResponseModel
        {
            public List<ViewCommentDto> Comments { get; set; }
        }
    }
}
