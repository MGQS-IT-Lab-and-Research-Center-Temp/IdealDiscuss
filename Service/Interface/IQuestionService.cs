using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.QuestionDto;

namespace IdealDiscuss.Service.Interface
{
    public interface IQuestionService
    {
        BaseResponseModel Create(CreateQuestionDto createQuestionDto);
        BaseResponseModel Delete(string questionId);
        BaseResponseModel Update(string questionId, UpdateQuestionDto updatequestionDto);
        QuestionResponseModel GetQuestion(string questionId);
        QuestionsResponseModel GetAllQuestion();
        QuestionsResponseModel GetUserQuestions();
        QuestionsResponseModel GetQuestionsByCategoryId(string categoryId);
        QuestionsResponseModel DisplayQuestion();
    }
}