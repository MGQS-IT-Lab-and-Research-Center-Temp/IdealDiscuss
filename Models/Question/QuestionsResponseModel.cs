namespace IdealDiscuss.Models.Question;

public class QuestionsResponseModel : BaseResponseModel
{
    public List<QuestionViewModel> Data { get; set; }
}

public class QuestionResponseModel : BaseResponseModel
{
    public QuestionViewModel Data { get; set; }
}