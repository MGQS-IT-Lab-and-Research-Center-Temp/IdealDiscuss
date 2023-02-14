namespace IdealDiscuss.Dtos.CommentReport
{
    public class EditCommentReportDto
    {
        public string AdditionalComment { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
