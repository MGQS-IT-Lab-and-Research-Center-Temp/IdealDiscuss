using IdealDiscuss.Entities;
using IdealDiscuss.Models;
using IdealDiscuss.Models.Comment;
using IdealDiscuss.Models.Question;
using IdealDiscuss.Models.QuestionReport;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using System.Linq.Expressions;
using System.Security.Claims;

namespace IdealDiscuss.Service.Implementations;

public class QuestionService : IQuestionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public QuestionService(
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponseModel> Create(CreateQuestionViewModel request)
    {
        var response = new BaseResponseModel();
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = await _unitOfWork.Users.GetAsync(userIdClaim);

        var question = new Question
        {
            UserId = user.Id,
            QuestionText = request.QuestionText,
            ImageUrl = request.ImageUrl
        };

        var categories = await _unitOfWork.Categories.GetAllByIdsAsync(request.CategoryIds);

        var categoryQuestions = new HashSet<CategoryQuestion>();

        foreach (var category in categories)
        {
            var categoryQuestion = new CategoryQuestion
            {
                CategoryId = category.Id,
                QuestionId = question.Id,
                Category = category,
                Question = question
            };

            categoryQuestions.Add(categoryQuestion);
        }

        question.CategoryQuestions = categoryQuestions;

        try
        {
            await _unitOfWork.Questions.CreateAsync(question);
            await _unitOfWork.SaveChangesAsync();
            response.Message = "Question created successfully!";
            response.Status = true;

            return response;
        }
        catch (Exception ex)
        {
            response.Message = $"Failed to create question: {ex.Message}";
            return response;
        }
    }

    public async Task<BaseResponseModel> Update(string questionId, UpdateQuestionViewModel request)
    {
        var response = new BaseResponseModel();
        var questionExist = await _unitOfWork.Questions.ExistsAsync(c => c.Id == questionId);
        var hasComment = await _unitOfWork.Comments.ExistsAsync(c => c.Id == questionId);
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = await _unitOfWork.Users.GetAsync(userIdClaim);

        if (!questionExist)
        {
            response.Message = "Question does not exist!";
            return response;
        }

        if (hasComment is true)
        {
            response.Message = $"Could not update the Question";
            return response;
        }

        var question = await _unitOfWork.Questions.GetAsync(questionId);

        if (question.UserId != user.Id)
        {
            response.Message = "You cannot update this question";
            return response;
        }

        question.QuestionText = request.QuestionText;

        try
        {
            await _unitOfWork.Questions.UpdateAsync(question);
            await _unitOfWork.SaveChangesAsync();
            response.Message = "Question updated successfully!";
            response.Status = true;
            return response;
        }
        catch (Exception ex)
        {
            response.Message = $"Could not update the Question: {ex.Message}";
            return response;
        }
    }

    public async Task<BaseResponseModel> Delete(string questionId)
    {
        var response = new BaseResponseModel();

        var questionExist = await _unitOfWork.Questions.ExistsAsync(q => (q.Id == questionId)
                                    && (q.Id == questionId
                                    && q.IsDeleted == false
                                    && q.IsClosed == false));

        var hasComment = await _unitOfWork.Comments.ExistsAsync(c => c.Id == questionId);

        if (!questionExist)
        {
            response.Message = "Question does not exist!";
            return response;
        }

        if (hasComment is true)
        {
            response.Message = $"Could not delete the Question";
            return response;
        }

        var question = await _unitOfWork.Questions.GetAsync(questionId);
        question.IsDeleted = true;

        try
        {
            await _unitOfWork.Questions.RemoveAsync(question);
            await _unitOfWork.SaveChangesAsync();
            response.Message = "Question deleted successfully!";
            response.Status = true;

            return response;
        }
        catch (Exception ex)
        {
            response.Message = $"Question delete failed: {ex.Message}";
            return response;
        }
    }

    public async Task<QuestionsResponseModel> GetAllQuestion()
    {
        var response = new QuestionsResponseModel();

        try
        {
            var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            Expression<Func<Question, bool>> expression = q => q.UserId == userIdClaim;

            var questions = IsInRole ? await _unitOfWork.Questions.GetQuestions() : await _unitOfWork.Questions.GetQuestions(expression);

            if (questions.Count == 0)
            {
                response.Message = "No question found!";
                return response;
            }

            response.Data = questions
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
                    }).ToList(),
                    QuestionReports = question.QuestionReports
                    .Select(report => new QuestionReportViewModel
                    {
                        Id = report.Id
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

    public async Task<QuestionResponseModel> GetQuestion(string id)
    {
        var response = new QuestionResponseModel();
        var questionExist = await _unitOfWork.Questions.ExistsAsync(q => q.Id == id && q.IsDeleted == false);
        var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!questionExist)
        {
            response.Message = $"Question with id {id} does not exist!";
            return response;
        }

        var question = IsInRole ? await _unitOfWork.Questions.GetQuestion(q => q.Id == id && !q.IsDeleted) : await _unitOfWork.Questions.GetQuestion(q => q.Id == id
                                            && q.UserId == userIdClaim
                                            && !q.IsDeleted);

        if (question is null)
        {
            response.Message = "Question not found!";
            return response;
        }

        response.Message = "Success";
        response.Status = true;
        response.Data = new QuestionViewModel
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
                        }).ToList(),
            QuestionReports = question.QuestionReports
                              .Where(qr => !qr.IsDeleted)
                              .Select(qr => new QuestionReportViewModel
                              {
                                  Id = qr.Id,
                                  QuestionReporter = qr.User.UserName,
                                  AdditionalComment = qr.AdditionalComment
                              }).ToList()
        };

        return response;
    }

    public async Task<QuestionsResponseModel> GetQuestionsByCategoryId(string categoryId)
    {
        var response = new QuestionsResponseModel();

        try
        {
            var questions = await _unitOfWork.Questions.GetQuestionByCategoryId(categoryId);

            if (questions.Count == 0)
            {
                response.Message = "No question found!";
                return response;
            }

            response.Data = questions.Select(question => new QuestionViewModel
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

    public async Task<QuestionsResponseModel> DisplayQuestion()
    {
        var response = new QuestionsResponseModel();

        try
        {
            var questions = await _unitOfWork.Questions.GetQuestions();

            if (questions.Count == 0)
            {
                response.Message = "No question found!";
                return response;
            }

            response.Data = questions
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