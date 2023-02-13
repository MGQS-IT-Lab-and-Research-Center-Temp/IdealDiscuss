namespace IdealDiscuss.Dtos.CommentReport
{
    public class EditCommentReport
    {
        public string AdditionalComment { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
