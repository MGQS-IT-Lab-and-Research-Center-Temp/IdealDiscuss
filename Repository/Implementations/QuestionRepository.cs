using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Implementations
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IdealDiscussContext context)
        {
            _context = context;
        }

        public Question GetQuestion(int id)
        {
            var question = _context.Questions.Include(c => c.User).SingleOrDefault();
            return question;

        }

        public List<Question> GetQuestions()
        {
            var questions = _context.Questions.Include(c => c.User).ToList();

            return questions;
        }
    }
}
