using IdealDiscuss.Context;
using IdealDiscuss.Dto;
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

        public List<Question> GetAllQuestionCount()
        {
            return _context.Questions.ToList();
        }

        public List<QuestionDto> GetAllQuestion()
        {
            return _context.Questions.Select(x => new QuestionDto
            {
                Id = x.Id,
                QuestionText = x.QuestionText
            }).ToList();
        }

        public QuestionDto GetQuestionDetail(int id) =>
             _context.Questions
                            .Where(x => x.Id == id)
                            .Select(x => new QuestionDto
                            {
                                Id = x.Id,
                                QuestionText = x.QuestionText,
                                ImageUrl = x.ImageUrl,
                                UserId = x.UserId,
                                User = x.User
                            })
                            .FirstOrDefault();
    }
}
