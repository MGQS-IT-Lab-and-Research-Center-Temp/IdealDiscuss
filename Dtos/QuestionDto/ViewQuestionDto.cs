using IdealDiscuss.Dtos.CommentDto;
using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.QuestionDto
{
    public class ViewQuestionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public List<ListCommentDto> Comments { get; set; }
    }
}
