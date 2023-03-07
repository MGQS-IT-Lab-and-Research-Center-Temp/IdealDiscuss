namespace IdealDiscuss.Dtos.QuestionDto
{
    public class CreateQuestionDto
    {
        public int UserId { get; set; }
        public List<int> CategoryIds { get; set; }
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }
    }
}
