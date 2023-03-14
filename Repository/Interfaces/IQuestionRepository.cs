using IdealDiscuss.Entities;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        List<Question> GetQuestions();
        Question GetQuestion(int id);
        List<CategoryQuestion> GetQuestionByCategoryId(int categoryId);


    }
}
