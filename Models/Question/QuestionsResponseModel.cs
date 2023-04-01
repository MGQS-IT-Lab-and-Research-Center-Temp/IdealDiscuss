namespace IdealDiscuss.Models.Question;

public class QuestionsResponseModel : BaseResponseModel
{
    public List<QuestionViewModel> Questions { get; set; }
}

public class QuestionResponseModel : BaseResponseModel
{
    public QuestionViewModel Question { get; set; }
}