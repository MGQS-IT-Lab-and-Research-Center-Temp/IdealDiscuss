using IdealDiscuss.Entities;
using IdealDiscuss.Models;
using IdealDiscuss.Models.CommentReport;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using System.Security.Claims;

namespace IdealDiscuss.Service.Implementations
{
    public class CommentReportService : ICommentReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentReportService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public BaseResponseModel CreateCommentReport(CreateCommentReportViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var reporter = _unitOfWork.Users.Get(userIdClaim);
            var comment = _unitOfWork.Comments.Get(request.CommentId);
            var reportExist = _unitOfWork.CommentReports.Exists(cr =>
                                        cr.UserId == reporter.Id
                                        && cr.CommentId == comment.Id);

            if (reportExist)
            {
                response.Message = "This comment was already reported by you";
                return response;
            }

            if (reporter is null)
            {
                response.Message = "User not found.";
                return response;
            }

            if (comment is null)
            {
                response.Message = "Comment does not exist!";
                return response;
            }

            if (request.FlagIds is null)
            {
                response.Message = "One or more flagId required to report this comment!";
                return response;
            }

            var commentReport = new CommentReport
            {
                UserId = reporter.Id,
                User = reporter,
                CommentId = comment.Id,
                Comment = comment,
                AdditionalComment = request.AdditionalComment,
                CreatedBy = createdBy,
                DateCreated = DateTime.Now,
            };

            var flags = _unitOfWork.Flags.GetAllByIds(request.FlagIds);

            var commentFlags = new HashSet<CommentReportFlag>();

            foreach (var flag in flags)
            {
                var commentReportFlag = new CommentReportFlag
                {
                    FlagId = flag.Id,
                    CommentReportId = commentReport.Id,
                    Flag = flag,
                    CommentReport = commentReport
                };

                commentFlags.Add(commentReportFlag);
            }

            commentReport.CommentReportFlags = commentFlags;

            try
            {
                _unitOfWork.CommentReports.Create(commentReport);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Comment Report created successfully!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create comment report: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel DeleteCommentReport(string id)
        {
            var response = new BaseResponseModel();

            var commentReport = _unitOfWork.CommentReports.Get(id);

            if (commentReport is null)
            {
                response.Message = $"CommentReport with id {id} does not exist!";
                return response;
            }

            commentReport.IsDeleted = true;

            try
            {
                _unitOfWork.CommentReports.Update(commentReport);
                _unitOfWork.SaveChanges();

                response.Status = true;
                response.Message = "Comment report deleted successfully!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Comment report delete failed: {ex.Message}";
                return response;
            }
        }

        public CommentReportsResponseModel GetAllCommentReport()
        {
            var response = new CommentReportsResponseModel();

            try
            {
                var commentReports = _unitOfWork.CommentReports.GetCommentReports();

                response.Data = commentReports.Select(commentReport => new ViewCommentReportViewModel
                {
                    Id = commentReport.Id,
                    AdditionalComment = commentReport.AdditionalComment,
                    CommentId = commentReport.Comment.Id,
                    CommentReporter = commentReport.User.UserName,
                    CommentText = commentReport.Comment.CommentText,
                    FlagNames = commentReport.CommentReportFlags
                        .Select(f => f.Flag.FlagName)
                        .ToList(),
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

        public CommentReportResponseModel GetCommentReport(string id)
        {
            var response = new CommentReportResponseModel();

            try
            {
                var commentReport = _unitOfWork.CommentReports.GetCommentReport(id);

                if (commentReport is null)
                {
                    response.Message = $"CommentReport with id {id} does not exist!";
                    return response;
                }

                response.Message = "Success";
                response.Status = true;
                response.Data = new ViewCommentReportViewModel
                {
                    Id = id,
                    AdditionalComment = commentReport.AdditionalComment,
                    CommentId = commentReport.Comment.Id,
                    CommentReporter = commentReport.User.UserName,
                    CommentText = commentReport.Comment.CommentText,
                    FlagNames = commentReport.CommentReportFlags
                                    .Select(f => f.Flag.FlagName)
                                    .ToList(),
                };
            }
            catch (Exception ex)
            {
                response.Message = ex.StackTrace;
                return response;
            }

            return response;
        }

        public BaseResponseModel UpdateCommentReport(string id, UpdateCommentReportViewModel request)
        {
            var response = new BaseResponseModel();
            var commentReport = _unitOfWork.CommentReports.Get(id);

            if (commentReport is null)
            {
                response.Message = $"CommentReport with id {id} does not exist!";
                return response;
            }

            commentReport.AdditionalComment = request.AdditionalComment;

            try
            {
                _unitOfWork.CommentReports.Update(commentReport);
                _unitOfWork.SaveChanges();
                response.Message = "Comment report updated successfully!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the comment report: {ex.Message}";
                return response;
            }
        }
    }
}
