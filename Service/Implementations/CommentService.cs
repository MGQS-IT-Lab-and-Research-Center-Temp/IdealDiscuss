﻿using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CommentDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using Org.BouncyCastle.Bcpg;
using static IdealDiscuss.Dtos.CommentDto.CommentResponse;

namespace IdealDiscuss.Service.Implementations
{
    public class CommentService : ICommentService
    {

        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommentService(IUserRepository userRepository, ICommentRepository commentRepository, IQuestionRepository questionRepository, IHttpContextAccessor httpContextAccessor)
        {

            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _questionRepository = questionRepository;
            _httpContextAccessor = httpContextAccessor; 
        }

        public BaseResponseModel CreateComment(CreateCommentDto createCommentDto)
        {
            var response = new BaseResponseModel();
            var user = _userRepository.Get(createCommentDto.UserId);
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var createdDate = DateTime.Now;
            if (user is null)
            {
                response.Message = "User not found";
                return response;
            }
            var question = _questionRepository.Get(createCommentDto.QuestionId);
            if (question is null)
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
                CommentText = createCommentDto.CommentText,
                CreatedBy = createdBy,
                DateCreated = createdDate
            };

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
            var commentexist = _commentRepository.Exists(c => c.Id == commentId);
            if (!commentexist)
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
                response.Message = "Comment  delete failed";
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
                var user = _userRepository.GetAll();
               

               response.Comments = comment.Select(comment => new ViewCommentDto
               {

                    Id = comment.Id,
                    CommentText = comment.CommentText,
                    QuestionId = comment.QuestionId,
                    UserId = comment.UserId
                   

               }).ToList();


                
                response.Status = true;
                response.Message = "Success";

                return response;
            }
        }

        public CommentResponseModel GetComment(int commentId)
        {
            var response = new CommentResponseModel();
            var commentexist = _commentRepository.Exists(c => c.Id == commentId);
            if (!commentexist)
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
                QuestionId = comment.QuestionId,
                UserId = comment.UserId,
            };
            return response;
        }

        public BaseResponseModel UpdateComment(int commentId, UpdateCommentDto updateCommentDto)
        {

            var response = new BaseResponseModel();
            var commentexist = _commentRepository.Exists(c => c.Id == commentId);
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var modifiedDate = DateTime.Now;


            if (!commentexist)
            {
                response.Message = "Comment  does not exist.";
                return response;
            }

            var comment = _commentRepository.Get(commentId);

            comment.CommentText = updateCommentDto.CommentText;
            comment.ModifiedBy = modifiedBy;
            comment.DateCreated = modifiedDate;



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
