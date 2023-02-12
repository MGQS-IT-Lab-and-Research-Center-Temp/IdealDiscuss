using IdealDiscuss.Entities;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        List<Question> GetAllQuestion();
        //Question FindQuestionById(int id);

    }
}
