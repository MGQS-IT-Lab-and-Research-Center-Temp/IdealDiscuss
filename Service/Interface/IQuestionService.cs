using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.QuestionDto;
using IdealDiscuss.Models.Question;

namespace IdealDiscuss.Service.Interface
{
    public interface IQuestionService
    {
        BaseResponseModel Create(CreateQuestionViewModel createQuestionDto);
        BaseResponseModel Delete(string questionId);
        BaseResponseModel Update(string questionId, UpdateQuestionDto updatequestionDto);
        QuestionResponseModel GetQuestion(string questionId);
        QuestionsResponseModel GetAllQuestion();
        QuestionsResponseModel GetUserQuestions();
        QuestionsResponseModel GetQuestionsByCategoryId(string categoryId);
        QuestionsResponseModel DisplayQuestion();
    }
}