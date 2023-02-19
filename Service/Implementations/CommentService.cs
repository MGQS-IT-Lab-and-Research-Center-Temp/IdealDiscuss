using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CommentDto;
using IdealDiscuss.Dtos.CommentReport;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Implementations;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using static IdealDiscuss.Dtos.CommentDto.CommentResponse;

namespace IdealDiscuss.Service.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentReportRepository _commentReportRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IQuestionRepository _questionRepository;
        public CommentService(ICommentReportRepository commentReportRepository, IUserRepository userRepository, ICommentRepository commentRepository, IQuestionRepository questionRepository)
        {
            _commentReportRepository = commentReportRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _questionRepository = questionRepository;
        }

        public BaseResponseModel CreateComment(CreateCommentDto createCommentDto)
        {
            var response = new BaseResponseModel();
            var user = _userRepository.Get(createCommentDto.UserId);
            if (user is null)
            {
                response.Message = "User not found";
                return response;
            }
            var question = _questionRepository.Get(createCommentDto.QuestionId);
            if(question is null)
            {
                response.Message = "Question not found";
                return response;
            }
            var comment = new Comment
            {
                UserId = user.Id,
                User = user,
                QuestionId = question.Id,
                Question = question,
                CommentText=createCommentDto.CommentText,
                CreatedBy = user.Id.ToString(),
                DateCreated = DateTime.Now,
            };
            var commentreport = _commentReportRepository.GetAllByIds(createCommentDto.CommentReportIds);
            comment.CommentReports = commentreport;
        
            try
            {
                _commentRepository.Create(comment);
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create comment . {ex.Message}";
                return response;
            }
            response.Status = true;
            response.Message = "Comment  created successfully.";
            return response;
        }

        public BaseResponseModel DeleteComment(int commentId)
        {
            var response = new BaseResponseModel();

            if (!_commentRepository.Exists(c => c.Id == commentId))
            {
                response.Message = "Comment  does not exist.";
                return response;
            }

            var comment = _commentRepository.Get(commentId);
            comment.IsDeleted = true;

            try
            {
                _commentRepository.Update(comment);
            }
            catch (Exception ex)
            {
                response.Message = "Comment  delete failed.";
                return response;
            }

            response.Status = true;
            response.Message = "Comment  deleted successfully.";
            return response;
        }

        public CommentsResponseModel GetAllComment()
        {
            {
                var response = new CommentsResponseModel();

                var comment = _commentRepository.GetAll();

                response.Comments = comment.Select(comment => new ViewCommentDto
                {
              
                    Id = comment.Id,
                    CommentText = comment.CommentText,
                    QuestionId = comment.Question.Id,
                    UserId = comment.User.Id,

                }).ToList();

                response.Status = true;
                response.Message = "Success";

                return response;
            }
        }

        public CommentResponseModel GetComment(int commentId)
        {
            var response = new CommentResponseModel();

            if (!_commentRepository.Exists(c => c.Id == commentId))
            {
                response.Message = $"Comment with id {commentId} does not exist.";
                return response;
            }
            var comment = _commentRepository.Get(commentId);

            response.Message = "Success";
            response.Status = true;
            response.Comment = new ViewCommentDto
            {
                Id = commentId,
                CommentText = comment.CommentText,
                QuestionId = comment.Question.Id,
                UserId = comment.User.Id,   
            };
            return response;
        }

        public BaseResponseModel UpdateComment(int commentId, UpdateCommentDto updateCommentDto)
        {

            var response = new BaseResponseModel();

            if (!_commentReportRepository.Exists(c => c.Id == commentId))
            {
                response.Message = "Comment  does not exist.";
                return response;
            }

            var comment = _commentRepository.Get(commentId);

            comment.CommentText = updateCommentDto.CommentText;

            try
            {
                _commentRepository.Update(comment);
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the comment : {ex.Message}";
                return response;
            }
            response.Message = "Comment  updated successfully.";
            return response;
        }

    }
}
