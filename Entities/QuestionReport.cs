namespace IdealDiscuss.Entities
{
    public class QuestionReport : BaseEntity
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; } 
        public string AdditionalComment { get; set; }
    }
}
