using IdealDiscuss.Entities.Dtos.CommentReport;

namespace IdealDiscuss.Entities.Dtos.Comment;

public class CommentDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string QuestionId { get; set; }
    public string CommentText { get; set; }
    public string UserName { get; set; }
    public List<CommentReportDto> CommentReports = new();
}