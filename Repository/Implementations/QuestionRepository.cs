using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Implementations
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IdealDiscussContext context)
        {
            _context = context;
        }

        public Question GetQuestion(Expression<Func<Question, bool>> expression)
        {
            var question = _context.Questions
                .Include(c => c.User)
                .Include(c => c.Comments)
                .ThenInclude(u => u.User)
                .SingleOrDefault(expression);

            return question;
        }

        public List<Question> GetQuestions()
        {
            var questions = _context.Questions
                .Include(u => u.User)
                .Include(c => c.Comments)
                .ThenInclude(u => u.User)
                .ToList();

            return questions;
        }

        public List<Question> GetQuestions(Expression<Func<Question, bool>> expression)
        {
            var questions = _context.Questions
                .Where(expression)
                .Include(u => u.User)
                .Include(c => c.Comments)
                .ThenInclude(u => u.User)
                .ToList();

            return questions;
        }

        public List<CategoryQuestion> GetQuestionByCategoryId(string categoryId)
        {
            var questions = _context.CategoryQuestions
                .Include(c => c.Category)
                .Include(c => c.Question)
                .ThenInclude(c => c.User)
                .Where(c => c.CategoryId.Equals(categoryId))
                .ToList();

            return questions;
        }

        public List<CategoryQuestion> SelectQuestionByCategory()
        {
            var questions = _context.CategoryQuestions
                .Include(c => c.Category)
                .Include(c => c.Question)
                .ThenInclude(c => c.User)
                .ToList();

            return questions;
        }
    }
}
