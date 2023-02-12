using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace IdealDiscuss.Repository.Implementations
{
    public class QuestionReposiotry : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionReposiotry(IdealDiscussContext context)
        {
            _context = context;
        }

        public List<Question> GetAllQuestion()
        {
            return _context.Questions.ToList();
        }

        //public Question FindQuestionById(int id)
        //{
        //    return _context.Questions.Where(s => s.Id == id).FirstOrDefaultAsync();
        //}
    }
}
