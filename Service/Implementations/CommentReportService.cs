using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CommentReport;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;

namespace IdealDiscuss.Service.Implementations
{
    public class CommentReportService : ICommentReportService
    {
        private readonly IFlagRepository _flagRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentReportRepository _commentReportRepository;

        public CommentReportService(ICommentReportRepository commentReportRepository, IUserRepository userRepository, ICommentRepository commentRepository, IFlagRepository flagRepository)
        {            
            _userRepository = userRepository;            
            _flagRepository = flagRepository;
            _commentRepository = commentRepository;
            _commentReportRepository = commentReportRepository;
        }

        public BaseResponseModel CreateCommentReport(CreateCommentReportDto createCommentReportDto)
        {
            var response = new BaseResponseModel();
            var reporter = _userRepository.Get(createCommentReportDto.UserId);
            if (reporter is null)
            {
                response.Message = "User not found.";
                return response;
            }

            var comment = _commentRepository.Get(createCommentReportDto.UserId);
            if(comment is null)
            {
                response.Message = "Comment does not exist.";
                return response;
            }

            var commentReport = new CommentReport
            {
                UserId = reporter.Id,
                User = reporter,
                CommentId = comment.Id,
                Comment = comment,
                AdditionalComment = createCommentReportDto.AdditionalComment,
                CreatedBy = reporter.Id.ToString(),
                DateCreated = DateTime.Now,
            };

            var flags = _flagRepository.GetAllByIds(createCommentReportDto.FlagIds);

            var commentFlags = new HashSet<CommentReportFlag>();

            foreach(var flag in flags)
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
                _commentReportRepository.Create(commentReport);
            }catch(Exception ex)
            {
                response.Message = $"Failed to create comment report. {ex.Message}";
                return response;
            }
            response.Status = true;
            response.Message = "Comment Report created successfully.";
            
            return response;
        }

        public BaseResponseModel DeleteCommentReport(int commentReportId)
        {
            var response = new BaseResponseModel();

            if (!_commentReportRepository.Exists(c => c.Id == commentReportId))
            {
                response.Message = "Comment report does not exist.";
                return response;
            }

            var commentReport = _commentReportRepository.Get(commentReportId);
            commentReport.IsDeleted = true;

            try
            {
                _commentReportRepository.Update(commentReport);
            }
            catch(Exception ex)
            {
                response.Message = "Comment report delete failed.";
                return response;
            }

            response.Status = true;
            response.Message = "Comment report deleted successfully.";
            return response;
        }

        public CommentReportsResponseModel GetAllCommentReport()
        {
            var response = new CommentReportsResponseModel();

            var commentReports = _commentReportRepository.GetAll();

            response.Reports = commentReports.Select(commentReport => new ViewCommentReportDto
            {
                Id = commentReport.Id,
                AdditionalComment = commentReport.AdditionalComment,
                CommentId = commentReport.Comment.Id,
                CommentReporter = commentReport.User.UserName,
                CommentText = commentReport.Comment.CommentText,
                FlagNames = commentReport.CommentReportFlags.Select(f => f.Flag.FlagName).ToList(),

            }).ToList();

            response.Status = true;
            response.Message = "Success";

            return response;

        }

        public CommentReportResponseModel GetCommentReport(int commentReportId)
        {
            var response = new CommentReportResponseModel();

            if(!_commentReportRepository.Exists(c => c.Id == commentReportId))
            {
                response.Message = $"CommentReport with id {commentReportId} does not exist.";
                return response;
            }
            var commentReport = _commentReportRepository.Get(commentReportId);

            response.Message = "Success";
            response.Status = true;
            response.Report = new ViewCommentReportDto
            {
                Id = commentReportId,
                AdditionalComment = commentReport.AdditionalComment,
                CommentId = commentReport.Comment.Id,
                CommentReporter = commentReport.User.UserName,
                CommentText = commentReport.Comment.CommentText,
                FlagNames = commentReport.CommentReportFlags.Select(f => f.Flag.FlagName).ToList(),
            };

            return response;
        }

        public BaseResponseModel UpdateCommentReport(int commentReportId, UpdateCommentReportDto updateCommentReportDto)
        {
            var response = new BaseResponseModel();

            if(!_commentReportRepository.Exists(c => c.Id == commentReportId))
            {
                response.Message = "Comment report does not exist.";
                return response;
            }

            var commentReport = _commentReportRepository.Get(commentReportId);

            commentReport.AdditionalComment = updateCommentReportDto.AdditionalComment;

            try
            {
                _commentReportRepository.Update(commentReport);
            }
            catch(Exception ex)
            {
                response.Message = $"Could not update the comment report: {ex.Message}";
                return response;
            }
            response.Message = "Comment report updated successfully.";
            return response;
        }
    }
}
