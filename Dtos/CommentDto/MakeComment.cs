namespace IdealDiscuss.Dtos.CommentDto
{
    public class MakeComment
    {

        public string CommentText { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
