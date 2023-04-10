using IdealDiscuss.Entities;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        List<Question> GetQuestions();
        List<Question> GetQuestions(Expression<Func<Question, bool>> expression);
        Question GetQuestion(Expression<Func<Question, bool>> expression);
        List<CategoryQuestion> GetQuestionByCategoryId(string id);
        List<CategoryQuestion> SelectQuestionByCategory();
    }
}
