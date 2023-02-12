using IdealDiscuss.Dto;
using IdealDiscuss.Entities;
using IdealDiscuss.ResponseModel;

namespace IdealDiscuss.Service.Interfaces
{
    public interface IQuestionService
    {
        CreateResponseModel CreateQuestion(Question request);
        List<QuestionDto> PrintAllQuestion();
        QuestionDto PrintQuestionDetail(int id);
        UpdateResponseModel UpdateQuestion(int id, Question updateMemberDto);
        CreateResponseModel CloseQuestion(int id);
    } 
}
