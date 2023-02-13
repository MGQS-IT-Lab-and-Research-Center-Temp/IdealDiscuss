
namespace IdealDiscuss.Dtos.CommentDto
{
    public class ViewCommentDetail
    {
        public string CommentText { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }
}
