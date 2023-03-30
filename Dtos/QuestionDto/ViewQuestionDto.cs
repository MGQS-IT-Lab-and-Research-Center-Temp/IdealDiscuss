using IdealDiscuss.Dtos.CommentDto;

namespace IdealDiscuss.Dtos.QuestionDto
{
    public class ViewQuestionDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public List<ListCommentDto> Comments { get; set; }
    }
}
