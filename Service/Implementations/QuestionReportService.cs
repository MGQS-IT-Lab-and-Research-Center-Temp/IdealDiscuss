using IdealDiscuss.Entities;
using IdealDiscuss.Models;
using IdealDiscuss.Models.QuestionReport;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using System.Security.Claims;

namespace IdealDiscuss.Service.Implementations
{
    public class QuestionReportService : IQuestionReportService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionReportService(IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        public BaseResponseModel CreateQuestionReport(CreateQuestionReportViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var reporter = _unitOfWork.Users.Get(userIdClaim);
            var question = _unitOfWork.Questions.Get(request.QuestionId);
       
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
                CreatedBy = createdBy
            };

            var flags = _unitOfWork.Flags.GetAllByIds(request.FlagIds);

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
            try
            {

                _unitOfWork.QuestionReports.Create(questionReport);
                response.Status = true;
                response.Message = "Report created successfully!";
                _unitOfWork.SaveChanges();
                return response;

            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.StackTrace}";
                return response;
            }

        }

        public BaseResponseModel DeleteQuestionReport(string id)
        {
            var response = new BaseResponseModel();

            var isQuestionReportExist = _unitOfWork.QuestionReports.Exists(c => c.Id == id);

            if (!isQuestionReportExist)
            {
                response.Message = "Report does not exist!";
                return response;
            }

            var questionReport = _unitOfWork.QuestionReports.Get(id);
            questionReport.IsDeleted = true;

            try
            {
                _unitOfWork.QuestionReports.Update(questionReport);
            }
            catch (Exception ex)
            {
                response.Message = $"Question report delete failed: {ex.Message}";
                return response;
            }

            response.Status = true;
            response.Message = "Question report deleted successfully!";
            _unitOfWork.SaveChanges();
            return response;
        }

        public QuestionReportsResponseModel GetAllQuestionReport()
        {
            var response = new QuestionReportsResponseModel();
            try
            { 
                var questionReports = _unitOfWork.QuestionReports.GetQuestionReports();

                response.Data = questionReports.Select(qr => new QuestionReportViewModel
                {
                    Id = qr.Id,
                    AdditionalComment = qr.AdditionalComment,
                    QuestionId = qr.Question.Id,
                    QuestionReporter = qr.User.UserName,
                    QuestionText = qr.Question.QuestionText,
                    FlagNames = qr.QuestionReportFlags
                                    .Select(f => f.Flag.FlagName)
                                    .ToList()
                }).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.Message}";
                return response;
            }
            return response;
        }

        public QuestionReportResponseModel GetQuestionReport(string id)
        {
            var response = new QuestionReportResponseModel();

            var isQuestionReportExist = _unitOfWork.QuestionReports.Exists(c => c.Id == id);

            if (!isQuestionReportExist)
            {
                response.Message = $"Report with id {id} does not exist!";
                return response;
            }

            var questionReport = _unitOfWork.QuestionReports.GetQuestionReport(id);

            response.Message = "Success";
            response.Status = true;

            response.Data = new QuestionReportViewModel
            {
                Id = id,
                AdditionalComment = questionReport.AdditionalComment,
                QuestionId = questionReport.Question.Id,
                QuestionReporter = questionReport.User.UserName,
                QuestionText = questionReport.Question.QuestionText,
                FlagNames = questionReport.QuestionReportFlags
                                    .Select(f => f.Flag.FlagName)
                                    .ToList(),
            };

            return response;
        }

        public BaseResponseModel UpdateQuestionReport(string id, UpdateQuestionReportViewModel request)
        {
            var response = new BaseResponseModel();

            var questionReportExist = _unitOfWork.QuestionReports.Exists(c => c.Id == id);

            if (!questionReportExist)
            {
                response.Message = "Question report does not exist!";
                return response;
            }

            var questionReport = _unitOfWork.QuestionReports.Get(id);

            questionReport.AdditionalComment = request.AdditionalComment;

            try
            {
                _unitOfWork.QuestionReports.Update(questionReport);
                _unitOfWork.SaveChanges();
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
