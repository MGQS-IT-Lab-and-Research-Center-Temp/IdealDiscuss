
namespace IdealDiscuss.Dtos.QuestionDto
{

    public class QuestionsResponseModel : BaseResponseModel
    {
        public List<ViewQuestionDto> questions { get; set; }
    }
    
    public class QuestionResponseModel: BaseResponseModel
    {
        public ViewQuestionDto question { get; set; }
    }
}