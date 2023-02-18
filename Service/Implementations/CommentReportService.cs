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

            var commentFlags = new HashSet<CommentReportFlag>();
            foreach(var flagId in createCommentReportDto.FlagIds)
            {
                var flag = _flagRepository.Get(flagId);
                if(flag is not null)
                {
                    var commentReportFlag = new CommentReportFlag
                    {
                        FlagId = flagId,
                        CommentReportId = commentReport.Id,
                        Flag = flag,
                        CommentReport = commentReport
                    };
                    commentFlags.Add(commentReportFlag);
                }
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
            throw new NotImplementedException();
        }

        public CommentReportsResponseModel GetAllCommentReport()
        {
            throw new NotImplementedException();
        }

        public CommentReportReesponseModel GetCommentReport(int commentReportId)
        {
            var response = new CommentReportReesponseModel();

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
            throw new NotImplementedException();
        }
    }
}
