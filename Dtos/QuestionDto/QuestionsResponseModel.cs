
namespace IdealDiscuss.Dtos.QuestionDto
{

    public class QuestionsResponseModel : BaseResponseModel
    {
        public List<ViewQuestionDto> Reports { get; set; }
    }
    
    public class QuestionResponseModel: BaseResponseModel
    {
        public ViewQuestionDto Report { get; set; }
    }
}