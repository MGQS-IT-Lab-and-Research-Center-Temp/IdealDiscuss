namespace IdealDiscuss.Dtos.QuestionDto
{
    public class EditQuestion
    {
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }
        public bool IsClosed { get; set; }
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }
}
