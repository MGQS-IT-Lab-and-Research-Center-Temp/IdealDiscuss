using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdealDiscuss.Repository.Implementations;

public class QuestionReportRepository : BaseRepository<QuestionReport> , IQuestionReportRepository
{
    public QuestionReportRepository(IdealDiscussContext context) : base(context)
    {
    }

    public async Task<QuestionReport> GetQuestionReport(string id)
    {
        var questionReport = await _context.QuestionReports
            .Include(u => u.User)
            .Include(c => c.Question)
            .Include(crf => crf.QuestionReportFlags)
            .ThenInclude(f => f.Flag)
            .Where(cr => cr.Id.Equals(id))
            .FirstOrDefaultAsync();

        return questionReport;
    }

    public async Task<List<QuestionReport>> GetQuestionReports(string questionId)
    {
        var questionWithReports = await _context.QuestionReports
                    .Where(qr => qr.QuestionId.Equals(questionId))
                    .Include(qr => qr.User)
                    .Include(qr => qr.QuestionReportFlags)
                        .ThenInclude(f => f.Flag)
                    .ToListAsync();

        return questionWithReports;
    }
}
