using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.QuestionDto;

namespace IdealDiscuss.Service.Interface
{
    public interface IQuestionService
    {
        BaseResponseModel Create(CreateQuestionDto createQuestionDto);
        BaseResponseModel Delete(int questionId);
        BaseResponseModel Update(int questionId, UpdateQuestionDto updatequestionDto);
        QuestionResponseModel GetQuestion(int questionId);
        QuestionsResponseModel GetAllQuestion();
        QuestionsResponseModel GetUserQuestions();
        QuestionsResponseModel GetQuestionsByCategoryId(int categoryId);
        QuestionsResponseModel DisplayQuestion();
    }
}