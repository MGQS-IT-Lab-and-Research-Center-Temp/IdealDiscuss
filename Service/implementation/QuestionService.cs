using IdealDiscuss.Dto;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository;
using IdealDiscuss.Repository.Implementations;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.ResponseModel;
using IdealDiscuss.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace IdealDiscuss.Service.implementation
{
    public class QuestionService : IQuestionService
    {
        
        private readonly IQuestionRepository _questionRepository; 
        private readonly IRepository<Question> _repository;
        private readonly ILogger<QuestionService> _logger;

        public QuestionService(
                              IQuestionRepository questionRepository,
                              IRepository<Question> repository,
                              ILogger<QuestionService> logger)
        {
            _logger = logger;
            _questionRepository = questionRepository;
            _repository = repository;
        }

        public CreateResponseModel CloseQuestion(int id)
        {
            var deleteQuestion = _repository.Get(id);

            if (deleteQuestion == null)
            {

                return new CreateResponseModel(false,
                                             "",
                                              "No such question exists");
            }


            try
            {
                _repository.Remove(deleteQuestion);
                _repository.SaveChanges();
            }
            catch (Exception)
            {
                return new CreateResponseModel(false,
                                              "",
                                              "unable to delete question..");
            }

            return new CreateResponseModel(true,
                                          "",
                                          "Question was successfully closed");
     
        }

        public CreateResponseModel CreateQuestion(Question request)
        {
            try
            {

                var question = new Question()
                {
                    Id = request.Id,
                    IsClosed = false,
                    ImageUrl = request.ImageUrl,
                    QuestionText = request.QuestionText,
                    UserId = request.UserId,
                    DateCreated = DateTime.Now,
                    CreatedBy = "Admin",
                    User = request.User
                };


                var checkQuestion = _repository.Get(question.Id);

                if (checkQuestion == null)
                {
                    _repository.Create(question);
                    _repository.SaveChanges();
                }
                else
                {
                    return new CreateResponseModel(false,
                                              "",
                                              "question already exist..");
                }

            }
            catch (Exception)
            {
                return new CreateResponseModel(false,
                                             "",
                                             "Something Went wrong..");
            }

            return new CreateResponseModel(true,
                                              "",
                                              "Question successfully asked..");

        }

        public List<Question> PrintAllQuestion()
        {
            return _questionRepository.GetAllQuestionCount();
        }

        public QuestionDto PrintQuestionDetail(int id)
        {
            return _questionRepository.GetQuestionDetail(id);
        }

        public UpdateResponseModel UpdateQuestion(int id, Question updateQuestion)
        {
            DateTime modified = DateTime.Now;
            try
            {
                var std = _repository.Get(id);

                if (std != null)
                {
                    std.LastModified = modified;
                    std.ModifiedBy = "Admin";
                    std.QuestionText = updateQuestion.QuestionText;
                    std.ImageUrl = updateQuestion.ImageUrl;
                    std.IsClosed = false;

                    _repository.SaveChanges();
                }
                else
                {
                    return new UpdateResponseModel(false,
                                                 "",
                                                 "question doesnt exist");
                }
            }
            catch (Exception)
            {
                return new UpdateResponseModel(false,
                                                 "",
                                                 "Something went wrong");
            }
            return new UpdateResponseModel(true,
                                                 "",
                                                 "question Successfully Updated");

        }

    }
}
}
