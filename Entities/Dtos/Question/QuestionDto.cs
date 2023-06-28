using IdealDiscuss.Entities.Dtos.Comment;
using IdealDiscuss.Entities.Dtos.QuestionReport;

namespace IdealDiscuss.Entities.Dtos.Question;

public class QuestionDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string QuestionText { get; set; }
    public string ImageUrl { get; set; }
    public string UserName { get; set; }
    public List<CommentDto> Comments { get; set; }
    public List<QuestionReportDto> QuestionReports { get; set; }
}
