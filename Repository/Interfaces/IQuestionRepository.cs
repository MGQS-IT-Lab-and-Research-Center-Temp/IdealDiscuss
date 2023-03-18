using IdealDiscuss.Entities;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        List<Question> GetQuestions();
        Question GetQuestion(Expression<Func<Question, bool>> expression);
        List<CategoryQuestion> GetQuestionByCategoryId(int categoryId);

        List<CategoryQuestion> SelectQuestionByCategory();
    }
}
