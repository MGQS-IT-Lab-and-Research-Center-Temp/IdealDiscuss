using IdealDiscuss.Models.Comment;
using IdealDiscuss.Models.QuestionReport;

namespace IdealDiscuss.Models.Question;

public class QuestionViewModel
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string QuestionText { get; set; }
    public string ImageUrl { get; set; }
    public string UserName { get; set; }
    public List<CommentViewModel> Comments { get; set; }
    public List<QuestionReportViewModel> QuestionReports { get; set; }
}
