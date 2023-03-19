using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CommentReport;
using IdealDiscuss.Dtos.QuestionReportDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Implementations;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;

namespace IdealDiscuss.Service.Implementations
{
    public class QuestionReportService : IQuestionReportService
    {
        private readonly IFlagRepository _flagRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionReportRepository _questionReportRepository;

        public QuestionReportService(
            IFlagRepository flagRepository,
            IUserRepository userRepository,
            IQuestionRepository questionRepository,
            IQuestionReportRepository questionReportRepository)
        {
            _flagRepository = flagRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _questionReportRepository = questionReportRepository;
        }
        public BaseResponseModel CreateQuestionReport(CreateQuestionReportDto request)
        {
            var response = new BaseResponseModel();

            try
            {
                var reporter = _userRepository.Get(request.UserId);
                var question = _questionRepository.Get(request.QuestionId);

                if (reporter is null)
                {
                    response.Message = "User not found!";
                    return response;
                }

                if (question is null)
                {
                    response.Message = "Question not found!";
                    return response;
                }

                var questionReport = new QuestionReport
                {
                    UserId = reporter.Id,
                    User = reporter,
                    QuestionId = question.Id,
                    Question = question,
                    AdditionalComment = request.AdditionalComment,
                    CreatedBy = reporter.Id.ToString(),
                    DateCreated = DateTime.Now,
                };

                var flags = _flagRepository.GetAllByIds(request.FlagIds);

                var questionFlags = new HashSet<QuestionReportFlag>();

                foreach (var flag in flags)
                {
                    var questionReportFlag = new QuestionReportFlag
                    {
                        FlagId = flag.Id,
                        QuestionReportId = questionReport.Id,
                        Flag = flag,
                        QuestionReport = questionReport
                    };

                    questionFlags.Add(questionReportFlag);
                }

                questionReport.QuestionReportFlags = questionFlags;

                _questionReportRepository.Create(questionReport);

                response.Status = true;
                response.Message = "Report created successfully!";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.StackTrace}";
            }

            return response;
        }

        public BaseResponseModel DeleteQuestionReport(int id)
        {
            var response = new BaseResponseModel();

            var isQuestionReportExist = _questionReportRepository.Exists(c => c.Id == id);

            if (!isQuestionReportExist)
            {
                response.Message = "Comment report does not exist!";
                return response;
            }

            var questionReport = _questionReportRepository.Get(id);
            questionReport.IsDeleted = true;

            try
            {
                _questionReportRepository.Update(questionReport);
            }
            catch (Exception ex)
            {
                response.Message = $"Question report delete failed: {ex.Message}";
                return response;
            }

            response.Status = true;
            response.Message = "Question report deleted successfully!";
            return response;
        }

        public QuestionReportsResponseModel GetAllQuestionReport()
        {
            var response = new QuestionReportsResponseModel();

            var questionReports = _questionReportRepository.GetAll();

            response.Reports = questionReports.Select(qr => new ViewQuestionReportDto
            {
                Id = qr.Id,
                AdditionalComment = qr.AdditionalComment,
                QuestionId = qr.Question.Id,
                QuestionReporter = qr.User.UserName,
                QuestionText = qr.Question.QuestionText,
                FlagNames = qr.QuestionReportFlags.Select(f => f.Flag.FlagName).ToList(),
            }).ToList();

            response.Status = true;
            response.Message = "Success";

            return response;
        }

        public QuestionReportResponseModel GetQuestionReport(int id)
        {
            var response = new QuestionReportResponseModel();

            var isQuestionReportExist = _questionReportRepository.Exists(c => c.Id == id);

            if (!isQuestionReportExist)
            {
                response.Message = $"CommentReport with id {id} does not exist!";
                return response;
            }

            var questionReport = _questionReportRepository.Get(id);

            response.Message = "Success";
            response.Status = true;

            response.Report = new ViewQuestionReportDto
            {
                Id = id,
                AdditionalComment = questionReport.AdditionalComment,
                QuestionId = questionReport.Question.Id,
                QuestionReporter = questionReport.User.UserName,
                QuestionText = questionReport.Question.QuestionText,
                FlagNames = questionReport.QuestionReportFlags.Select(f => f.Flag.FlagName).ToList(),
            };

            return response;
        }

        public BaseResponseModel UpdateQuestionReport(int id, UpdateQuestionReportDto request)
        {
            var response = new BaseResponseModel();

            var questionReportExist = _questionReportRepository.Exists(c => c.Id == id);

            if (!questionReportExist)
            {
                response.Message = "Question report does not exist!";
                return response;
            }

            var questionReport = _questionReportRepository.Get(id);

            questionReport.AdditionalComment = request.AdditionalComment;

            try
            {
                _questionReportRepository.Update(questionReport);
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the question report: {ex.Message}";
                return response;
            }

            response.Message = "Question report updated successfully!";

            return response;
        }
    }
}
