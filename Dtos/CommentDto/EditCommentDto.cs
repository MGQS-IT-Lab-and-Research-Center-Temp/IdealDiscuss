namespace IdealDiscuss.Dtos.CommentDto
{
    public class EditCommentDto
    {
        public string CommentText { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
        public int Id { get; set; }
    }
}
