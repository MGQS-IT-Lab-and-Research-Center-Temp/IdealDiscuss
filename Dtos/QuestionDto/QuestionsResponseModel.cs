
namespace IdealDiscuss.Dtos.QuestionDto
{

    public class QuestionsResponseModel : BaseResponseModel
    {
        public List<ViewQuestionDto> Questions { get; set; }
    }
    
    public class QuestionResponseModel: BaseResponseModel
    {
        public ViewQuestionDto Question { get; set; }
    }
}