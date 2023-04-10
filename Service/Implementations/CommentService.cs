using IdealDiscuss.Entities;
using IdealDiscuss.Models;
using IdealDiscuss.Models.Comment;
using IdealDiscuss.Models.CommentReport;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using System.Security.Claims;
using static IdealDiscuss.Models.Comment.CommentResponse;

namespace IdealDiscuss.Service.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public BaseResponseModel CreateComment(CreateCommentViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);

            if (user is null)
            {
                response.Message = "User not found";
                return response;
            }

            var question = _unitOfWork.Questions.Get(request.QuestionId);

            if (question is null)
            {
                response.Message = "Question not found";
                return response;
            }

            if (string.IsNullOrWhiteSpace(request.CommentText))
            {
                response.Message = "Comment text is required!";
                return response;
            }

            var comment = new Comment
            {
                UserId = user.Id,
                User = user,
                QuestionId = question.Id,
                Question = question,
                CommentText = request.CommentText,
                CreatedBy = createdBy,
            };

            try
            {
                _unitOfWork.Comments.Create(comment);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Comment  created successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create comment . {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel DeleteComment(string commentId)
        {
            var response = new BaseResponseModel();
            var commentexist = _unitOfWork.Comments.Exists(c => c.Id == commentId);
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);

            if (!commentexist)
            {
                response.Message = "Comment  does not exist.";
                return response;
            }

            var comment = _unitOfWork.Comments.Get(commentId);
            if(comment.UserId != user.Id)
            {
                response.Message = "You can not delete this Comment!";
                return response;
            }

            comment.IsDeleted = true;

            try
            {
                _unitOfWork.Comments.Update(comment);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = $"Comment  delete failed {ex.Message}";
                return response;
            }

            response.Status = true;
            response.Message = "Comment  deleted successfully.";
            return response;
        }

        public CommentsResponseModel GetAllComment()
        {
            var response = new CommentsResponseModel();

            var comment = _unitOfWork.Comments.GetAll(c => c.IsDeleted == false);

            if (comment.Count == 0)
            {
                response.Message = "No comments yet!";
                return response;
            }

            response.Data = comment
                    .Select(comment => new CommentViewModel
                    {
                        Id = comment.Id,
                        QuestionId = comment.QuestionId,
                        UserId = comment.UserId,
                        CommentText = comment.CommentText
                    })
                    .ToList();

            response.Status = true;
            response.Message = "Success";

            return response;
        }

        public CommentResponseModel GetComment(string commentId)
        {
            var response = new CommentResponseModel();
            var commentexist = _unitOfWork.Comments.Exists(c => c.Id == commentId);

            if (!commentexist)
            {
                response.Message = $"Comment does not exist.";
                return response;
            }

            var comment = _unitOfWork.Comments.GetCommentWithReportList(commentId);

            response.Message = "Success";
            response.Status = true;
            response.Data = new CommentViewModel
            {
                Id = comment.Id,
                QuestionId = comment.QuestionId,
                UserId = comment.UserId,
                CommentText = comment.CommentText,
                CommentReports =  comment.CommentReports
                            .Where(c => !c.IsDeleted)
                            .Select(c => new CommentReportViewModel
                            {
                                Id = c.Id,
                                CommentReporter = c.User.UserName,
                                AdditionalComment  = c.AdditionalComment                       
                            })
                            .ToList()

            };
            return response;
        }

        public BaseResponseModel UpdateComment(string commentId, UpdateCommentViewModel request)
        {
            var response = new BaseResponseModel();
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var commentexist = _unitOfWork.Comments.Exists(c => c.Id == commentId);
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);

            if (!commentexist)
            {
                response.Message = "Comment  does not exist.";
                return response;
            }
            var comment = _unitOfWork.Comments.Get(commentId);

            if (comment.UserId != userIdClaim)
            {
                response.Message = "You can not update this comment";
                return response;
            }

            comment.CommentText = request.CommentText;
            comment.ModifiedBy = modifiedBy;
            
            try
            {
                _unitOfWork.Comments.Update(comment);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the comment : {ex.Message}";
                return response;
            }
            response.Status = true;
            response.Message = "Comment  updated successfully.";

            return response;
        }
    }
}
