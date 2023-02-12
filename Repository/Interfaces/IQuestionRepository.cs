using IdealDiscuss.Dto;
using IdealDiscuss.Entities;
using System.Threading.Tasks;

namespace IdealDiscuss.Repository.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        List<Question> GetAllQuestionCount();
        //Question FindQuestionById(int id);
        List<QuestionDto> GetAllQuestion();
        QuestionDto? GetQuestionDetail(int id);
    }
}
