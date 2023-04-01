using IdealDiscuss.Entities;
using IdealDiscuss.Models;
using IdealDiscuss.Models.Comment;
using IdealDiscuss.Models.Question;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using System.Linq.Expressions;
using System.Security.Claims;

namespace IdealDiscuss.Service.Implementations

{
    public class QuestionService : IQuestionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel Create(CreateQuestionViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);

            var question = new Question
            {
                UserId = user.Id,
                QuestionText = request.QuestionText,
                ImageUrl = request.ImageUrl,
                CreatedBy = createdBy,
                DateCreated = DateTime.Now
            };

            var categories = _unitOfWork.Categories.GetAllByIds(request.CategoryIds);

            var categoryQuestions = new HashSet<CategoryQuestion>();

            foreach (var category in categories)
            {
                var categoryQuestion = new CategoryQuestion
                {
                    CategoryId = category.Id,
                    QuestionId = question.Id,
                    Category = category,
                    Question = question,
                    CreatedBy = createdBy,
                    DateCreated = DateTime.Now
                };

                categoryQuestions.Add(categoryQuestion);
            }

            question.CategoryQuestions = categoryQuestions;

            try
            {
                _unitOfWork.Questions.Create(question);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Question created successfully!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create question: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel Update(string questionId, UpdateQuestionViewModel request)
        {
            var response = new BaseResponseModel();
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var questionExist = _unitOfWork.Questions.Exists(c => c.Id == questionId);

            if (!questionExist)
            {
                response.Message = "Question does not exist!";
                return response;
            }

            var question = _unitOfWork.Questions.Get(questionId);

            question.QuestionText = request.QuestionText;
            question.ModifiedBy = modifiedBy;
            question.LastModified = DateTime.Now;

            try
            {
                _unitOfWork.Questions.Update(question);
                _unitOfWork.SaveChanges();
                response.Message = "Question updated successfully!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the Question: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel Delete(string questionId)
        {
            var response = new BaseResponseModel();

            Expression<Func<Question, bool>> expression = (q => (q.Id == questionId)
                                        && (q.Id == questionId
                                        && q.IsDeleted == false
                                        && q.IsClosed == false));

            var questionExist = _unitOfWork.Questions.Exists(expression);

            if (!questionExist)
            {
                response.Message = "Question does not exist!";
                return response;
            }

            var question = _unitOfWork.Questions.Get(questionId);
            question.IsDeleted = true;

            try
            {
                _unitOfWork.Questions.Update(question);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Question deleted successfully!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Question delete failed: {ex.Message}";
                return response;
            }
        }

        public QuestionsResponseModel GetAllQuestion()
        {
            var response = new QuestionsResponseModel();

            try
            {
                var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                Expression<Func<Question, bool>> expression = q => q.UserId == userIdClaim;

                var questions = IsInRole ? _unitOfWork.Questions.GetQuestions() : _unitOfWork.Questions.GetQuestions(expression);

                if (questions.Count == 0)
                {
                    response.Message = "No question found!";
                    return response;
                }

                response.Questions = questions
                    .Where(q => q.IsDeleted == false)
                    .Select(question => new QuestionViewModel
                    {
                        Id = question.Id,
                        QuestionText = question.QuestionText,
                        UserName = question.User.UserName,
                        ImageUrl = question.ImageUrl,
                        Comments = question.Comments
                        .Select(comment => new CommentViewModel
                        {
                            Id = comment.Id,
                            CommentText = comment.CommentText,
                            UserName = comment.User.UserName,
                        }).ToList()
                    }).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.StackTrace}";
                return response;
            }

            return response;
        }

        public QuestionsResponseModel GetUserQuestions()
        {
            var response = new QuestionsResponseModel();

            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var user = _unitOfWork.Questions.Get(userIdClaim);

                Expression<Func<Question, bool>> expression = q => (q.UserId == user.Id)
                                                    && (q.IsDeleted == false);
                var questions = _unitOfWork.Questions.GetQuestions(expression);

                if (questions.Count == 0)
                {
                    response.Message = "No question found!";
                    return response;
                }

                response.Questions = questions
                    .Select(question => new QuestionViewModel
                    {
                        Id = question.Id,
                        QuestionText = question.QuestionText,
                        UserName = question.User.UserName,
                        ImageUrl = question.ImageUrl,
                    }).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.StackTrace}";
                return response;
            }

            return response;
        }

        public QuestionResponseModel GetQuestion(string questionId)
        {
            var response = new QuestionResponseModel();
            var questionExist = _unitOfWork.Questions.Exists(q => q.Id == questionId && q.IsDeleted == false);
            var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var question = new Question();

            if (!questionExist)
            {
                response.Message = $"Question with id {questionId} does not exist!";
                return response;
            }

            question = IsInRole ? _unitOfWork.Questions.GetQuestion(q => q.Id == questionId && !q.IsDeleted) : _unitOfWork.Questions.GetQuestion(q => q.Id == questionId
                                                && q.UserId == userIdClaim
                                                && !q.IsDeleted);

            if (question is null)
            {
                response.Message = "Question not found!";
                return response;
            }

            response.Message = "Success";
            response.Status = true;
            response.Question = new QuestionViewModel
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                UserId = question.UserId,
                UserName = question.User.UserName,
                ImageUrl = question.ImageUrl,
                Comments = question.Comments
                            .Where(c => !c.IsDeleted)
                            .Select(c => new CommentViewModel
                            {
                                Id = c.Id,
                                UserId = c.UserId,
                                CommentText = c.CommentText,
                                UserName = c.User.UserName
                            })
                            .ToList()
            };

            return response;
        }

        public QuestionsResponseModel GetQuestionsByCategoryId(string categoryId)
        {
            var response = new QuestionsResponseModel();

            try
            {
                var questions = _unitOfWork.Questions.GetQuestionByCategoryId(categoryId);

                if (questions.Count == 0)
                {
                    response.Message = "No question found!";
                    return response;
                }

                response.Questions = questions
                                    .Select(question => new QuestionViewModel
                                    {
                                        Id = question.Id,
                                        QuestionText = question.Question.QuestionText,
                                        UserName = question.Question.User.UserName,
                                        ImageUrl = question.Question.ImageUrl,
                                    }).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.StackTrace}";
                return response;
            }

            return response;
        }

        public QuestionsResponseModel DisplayQuestion()
        {
            var response = new QuestionsResponseModel();

            try
            {
                var questions = _unitOfWork.Questions.GetQuestions();

                if (questions.Count == 0)
                {
                    response.Message = "No question found!";
                    return response;
                }

                response.Questions = questions
                    .Where(q => !q.IsDeleted)
                    .Select(question => new QuestionViewModel
                    {
                        Id = question.Id,
                        UserId = question.UserId,
                        QuestionText = question.QuestionText,
                        UserName = question.User.UserName,
                        ImageUrl = question.ImageUrl,
                        Comments = question.Comments
                            .Where(c => !c.IsDeleted)
                            .Select(c => new CommentViewModel
                            {
                                Id = c.Id,
                                UserId = c.UserId,
                                CommentText = c.CommentText,
                                UserName = c.User.UserName
                            })
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

        
    }
}