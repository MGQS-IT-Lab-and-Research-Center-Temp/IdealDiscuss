using IdealDiscuss.Models;
using IdealDiscuss.Models.Question;

namespace IdealDiscuss.Service.Interface
{
    public interface IQuestionService
    {
        Task<BaseResponseModel> Create(CreateQuestionViewModel createQuestionDto);
        Task<BaseResponseModel> Delete(string questionId);
        Task<BaseResponseModel> Update(string questionId, UpdateQuestionViewModel updatequestionDto);
        Task<QuestionResponseModel> GetQuestion(string questionId);
        Task<QuestionsResponseModel> GetAllQuestion();
        Task<QuestionsResponseModel> GetQuestionsByCategoryId(string categoryId);
        Task<QuestionsResponseModel> DisplayQuestion();
    }
}