using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.QuestionDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;

namespace IdealDiscuss.Service.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _questionRepository = questionRepository;
        }

        public BaseResponseModel Create(CreateQuestionDto createQuestionDto)
        {
            var response = new BaseResponseModel();
            var user = _userRepository.Get(createQuestionDto.UserId);
            
            var question = new Question
            {
                UserId = createQuestionDto.UserId,
                User = user,
                QuestionText = createQuestionDto.QuestionText,
                ImageUrl = createQuestionDto.ImageUrl,
                CreatedBy = user.Id.ToString(),
                DateCreated = DateTime.Now,
            };


            try
            {
                _questionRepository.Create(question);
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create question: {ex.Message}";
                return response;
            }

            response.Status = true;
            response.Message = "Comment question successfully!";

            return response;
        }

        public BaseResponseModel Update(int questionId, UpdateQuestionDto updateQuestionDto)
        {
            var response = new BaseResponseModel();

            if (!_questionRepository.Exists(c => c.Id == questionId))
            {
                response.Message = "Question does not exist!";
                return response;
            }

            var question = _questionRepository.Get(questionId);

            question.QuestionText = updateQuestionDto.QuestionText;

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

        public BaseResponseModel Delete(int questionId)
        {
            var response = new BaseResponseModel();

            if (!_questionRepository.Exists(c => c.Id == questionId))
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

            var questions = _questionRepository.GetAll();

            response.Reports = questions.Select(question => new ViewQuestionDto
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                UserName = question.User.UserName,
                ImageUrl = question.ImageUrl
            }).ToList();

            response.Status = true;
            response.Message = "Success";

            return response;
        }

        public QuestionResponseModel GetQuestion(int questionId)
        {
            var response = new QuestionResponseModel();

            if (!_questionRepository.Exists(c => c.Id == questionId))
            {
                response.Message = $"Question with id {questionId} does not exist!";
                return response;
            }
            var question = _questionRepository.Get(questionId);

            response.Message = "Success";
            response.Status = true;
            response.Report = new ViewQuestionDto
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                UserName = question.User.UserName,
                ImageUrl = question.ImageUrl
            };

            return response;
        }
    }
}