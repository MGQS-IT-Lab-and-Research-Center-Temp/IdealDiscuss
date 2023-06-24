using IdealDiscuss.Entities;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Interfaces;

public interface IQuestionRepository : IRepository<Question>
{
    Task<List<Question>> GetQuestions();
    Task<List<Question>> GetQuestions(Expression<Func<Question, bool>> expression);
    Task<Question> GetQuestion(Expression<Func<Question, bool>> expression);
    Task<List<CategoryQuestion>> GetQuestionByCategoryId(string id);
    Task<List<CategoryQuestion>> SelectQuestionByCategory();
}
