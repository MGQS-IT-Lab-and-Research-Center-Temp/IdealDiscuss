namespace IdealDiscuss.Dtos.QuestionDto
{
    public class CreateQuestionDto
    {
        public string UserId { get; set; }
        public List<string> CategoryIds { get; set; }
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }
    }
}
