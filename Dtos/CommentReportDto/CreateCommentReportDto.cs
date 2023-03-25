namespace IdealDiscuss.Dtos.CommentReport
{
    public class CreateCommentReportDto
    {
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public List<string> FlagIds { get; set; } = new List<string>();
        public string AdditionalComment { get; set; }
    }
}
