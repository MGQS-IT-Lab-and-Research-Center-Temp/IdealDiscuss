using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CommentDto;
using IdealDiscuss.Dtos.QuestionDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using System.Linq.Expressions;
using System.Security.Claims;

namespace IdealDiscuss.Service.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryQuestionRepository _categoryQuestionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(
            IQuestionRepository questionRepository,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            ICategoryQuestionRepository categoryQuestionRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _httpContextAccessor = httpContextAccessor;
            _categoryQuestionRepository = categoryQuestionRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel Create(CreateQuestionDto request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _userRepository.Get(userIdClaim);

            if (request.CategoryIds is null)
            {
                response.Message = "You can't create a question without selecting one or more categories";
                return response;
            }

            if (string.IsNullOrWhiteSpace(request.QuestionText))
            {
                response.Message = "Question text is required!";
                return response;
            }

            if (request.QuestionText.Length < 20 || request.QuestionText.Length > 150)
            {
                response.Message = "Question text can only be between 20 - 150 characters";
                return response;
            }

            var question = new Question
            {
                UserId = user.Id,
                QuestionText = request.QuestionText,
                ImageUrl = request.ImageUrl,
                CreatedBy = createdBy,
                DateCreated = DateTime.Now
            };

            try
            {
                _questionRepository.Create(question);

                foreach (var item in request.CategoryIds)
                {
                    var categoryData = _categoryRepository.Get(item);

                    CategoryQuestion categoryQuestion = new()
                    {
                        CategoryId = item,
                        QuestionId = question.Id,
                        Category = categoryData,
                        Question = question,
                        CreatedBy = createdBy,
                        DateCreated = DateTime.Now
                    };

                    _categoryQuestionRepository.Create(categoryQuestion);
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create question: {ex.Message}";
                return response;
            }

            _unitOfWork.SaveChanges();
            response.Status = true;
            response.Message = "Question created successfully!";

            return response;
        }

        public BaseResponseModel Update(string questionId, UpdateQuestionDto updateQuestionDto)
        {
            var response = new BaseResponseModel();
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var questionExist = _questionRepository.Exists(c => c.Id == questionId);

            if (!questionExist)
            {
                response.Message = "Question does not exist!";
                return response;
            }

            var question = _questionRepository.Get(questionId);

            question.QuestionText = updateQuestionDto.QuestionText;
            question.ModifiedBy = modifiedBy;
            question.LastModified = DateTime.Now;

            try
            {
                _questionRepository.Update(question);
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the Question: {ex.Message}";
                return response;
            }
            response.Message = "Question updated successfully!";
            return response;
        }

        public BaseResponseModel Delete(string questionId)
        {
            var response = new BaseResponseModel();

            Expression<Func<Question, bool>> expression = (q => (q.Id == questionId)
                                        && (q.Id == questionId
                                        && q.IsDeleted == false
                                        && q.IsClosed == false));

            var questionExist = _questionRepository.Exists(expression);

            if (!questionExist)
            {
                response.Message = "Question does not exist!";
                return response;
            }

            var question = _questionRepository.Get(questionId);
            question.IsDeleted = true;

            try
            {
                _questionRepository.Update(question);
            }
            catch (Exception ex)
            {
                response.Message = $"Question delete failed: {ex.Message}";
                return response;
            }

            response.Status = true;
            response.Message = "Question deleted successfully!";
            return response;
        }

        public QuestionsResponseModel GetAllQuestion()
        {
            var response = new QuestionsResponseModel();

            try
            {
                var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                Expression<Func<Question, bool>> expression = q => q.UserId == userIdClaim;

                var questions = IsInRole ? _questionRepository.GetQuestions() : _questionRepository.GetQuestions(expression);

                if (questions.Count == 0)
                {
                    response.Message = "No question found!";
                    return response;
                }

                response.Questions = questions
                    .Where(q => q.IsDeleted == false)
                    .Select(question => new ViewQuestionDto
                    {
                        Id = question.Id,
                        QuestionText = question.QuestionText,
                        UserName = question.User.UserName,
                        ImageUrl = question.ImageUrl,
                        Comments = question.Comments
                        .Select(c => new ListCommentDto
                        {
                            Id = c.Id,
                            CommentText = c.CommentText,
                            UserName = c.User.UserName,
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
                var user = _userRepository.Get(userIdClaim);

                Expression<Func<Question, bool>> expression = q => (q.UserId == user.Id)
                                                    && (q.IsDeleted == false);
                var questions = _questionRepository.GetQuestions(expression);

                if (questions.Count == 0)
                {
                    response.Message = "No question found!";
                    return response;
                }

                response.Questions = questions
                    .Select(question => new ViewQuestionDto
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
            var questionExist = _questionRepository.Exists(q => q.Id == questionId && q.IsDeleted == false);
            var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var question = new Question();

            if (!questionExist)
            {
                response.Message = $"Question with id {questionId} does not exist!";
                return response;
            }

            question = IsInRole ? _questionRepository.GetQuestion(q => q.Id == questionId && !q.IsDeleted) : _questionRepository.GetQuestion(q => q.Id == questionId
                                                && q.UserId == userIdClaim
                                                && !q.IsDeleted);

            if (question is null)
            {
                response.Message = "Question not found!";
                return response;
            }

            response.Message = "Success";
            response.Status = true;
            response.Question = new ViewQuestionDto
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                UserId = question.UserId,
                UserName = question.User.UserName,
                ImageUrl = question.ImageUrl,
                Comments = question.Comments
                            .Where(c => !c.IsDeleted)
                            .Select(c => new ListCommentDto
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
                var questions = _questionRepository.GetQuestionByCategoryId(categoryId);

                if (questions.Count == 0)
                {
                    response.Message = "No question found!";
                    return response;
                }

                response.Questions = questions
                                    .Select(question => new ViewQuestionDto
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
                var questions = _questionRepository.GetQuestions();

                if (questions.Count == 0)
                {
                    response.Message = "No question found!";
                    return response;
                }

                response.Questions = questions
                    .Where(q => !q.IsDeleted)
                    .Select(question => new ViewQuestionDto
                    {
                        Id = question.Id,
                        UserId = question.UserId,
                        QuestionText = question.QuestionText,
                        UserName = question.User.UserName,
                        ImageUrl = question.ImageUrl,
                        Comments = question.Comments
                            .Where(c => !c.IsDeleted)
                            .Select(c => new ListCommentDto
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