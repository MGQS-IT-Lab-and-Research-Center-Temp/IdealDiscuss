using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdealDiscuss.Repository.Implementations;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(IdealDiscussContext context) : base(context)
    {
    }

    public async Task<Question> GetQuestion(Expression<Func<Question, bool>> expression)
    {
        var question = await _context.Questions
            .Include(c => c.User)
            .Include(c => c.Comments)
            .ThenInclude(u => u.User)
            .SingleOrDefaultAsync(expression);

        return question;
    }

    public async Task<List<Question>> GetQuestions()
    {
        var questions = await _context.Questions
            .Include(uq => uq.User)
            .Include(c => c.Comments)
            .ThenInclude(u => u.User)
            .Include(qr => qr.QuestionReports)
            .ToListAsync();

        return questions;
    }

    public async Task<List<Question>> GetQuestions(Expression<Func<Question, bool>> expression)
    {
        var questions = await _context.Questions
            .Where(expression)
            .Include(u => u.User)
            .Include(c => c.Comments)
            .ThenInclude(u => u.User)
            .Include(qr => qr.QuestionReports)
            .ToListAsync();

        return questions;
    }

    public async Task<List<CategoryQuestion>> GetQuestionByCategoryId(string categoryId)
    {
        var questions = await _context.CategoryQuestions
            .Include(c => c.Category)
            .Include(c => c.Question)
            .ThenInclude(c => c.User)
            .Where(c => c.CategoryId.Equals(categoryId))
            .ToListAsync();

        return questions;
    }

    public async Task<List<CategoryQuestion>> SelectQuestionByCategory()
    {
        var questions = await _context.CategoryQuestions
            .Include(c => c.Category)
            .Include(c => c.Question)
            .ThenInclude(c => c.User)
            .ToListAsync();

        return questions;
    }
}
