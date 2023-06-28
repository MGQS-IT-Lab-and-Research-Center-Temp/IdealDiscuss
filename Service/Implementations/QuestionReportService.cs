﻿using IdealDiscuss.Entities;
using IdealDiscuss.Models;
using IdealDiscuss.Models.QuestionReport;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using System.Security.Claims;

namespace IdealDiscuss.Service.Implementations;

public class QuestionReportService : IQuestionReportService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public QuestionReportService(IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }
    public async Task<BaseResponseModel> CreateQuestionReport(CreateQuestionReportViewModel request)
    {
        var response = new BaseResponseModel();
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var reporter = await _unitOfWork.Users.GetAsync(userIdClaim);
        var question = await _unitOfWork.Questions.GetAsync(request.QuestionId);

        if (reporter is null)
        {
            response.Message = "User not found!";
            return response;
        }

        if (question is null)
        {
            response.Message = "Question not found!";
            return response;
        }

        var questionReport = new QuestionReport
        {
            UserId = reporter.Id,
            User = reporter,
            QuestionId = question.Id,
            Question = question,
            AdditionalComment = request.AdditionalComment
        };

        var flags = await _unitOfWork.Flags.GetAllByIdsAsync(request.FlagIds);

        var questionFlags = new HashSet<QuestionReportFlag>();

        foreach (var flag in flags)
        {
            var questionReportFlag = new QuestionReportFlag
            {
                FlagId = flag.Id,
                QuestionReportId = questionReport.Id,
                Flag = flag,
                QuestionReport = questionReport
            };

            questionFlags.Add(questionReportFlag);
        }
        questionReport.QuestionReportFlags = questionFlags;
        try
        {
            await _unitOfWork.QuestionReports.CreateAsync(questionReport);
            response.Status = true;
            response.Message = "Report created successfully!";
            await _unitOfWork.SaveChangesAsync();
            return response;

        }
        catch (Exception ex)
        {
            response.Message = $"An error occured: {ex.StackTrace}";
            return response;
        }

    }

    public async Task<BaseResponseModel> DeleteQuestionReport(string id)
    {
        var response = new BaseResponseModel();

        var isQuestionReportExist = await _unitOfWork.QuestionReports.ExistsAsync(c => c.Id == id);

        if (!isQuestionReportExist)
        {
            response.Message = "Report does not exist!";
            return response;
        }

        var questionReport = await _unitOfWork.QuestionReports.GetAsync(id);

        try
        {
            await _unitOfWork.QuestionReports.RemoveAsync(questionReport);
        }
        catch (Exception ex)
        {
            response.Message = $"Question report delete failed: {ex.Message}";
            return response;
        }

        response.Status = true;
        response.Message = "Question report deleted successfully!";
        await _unitOfWork.SaveChangesAsync();
        return response;
    }

    public async Task<QuestionReportResponseModel> GetQuestionReport(string id)
    {
        var response = new QuestionReportResponseModel();

        var isQuestionReportExist = await _unitOfWork.QuestionReports.ExistsAsync(c => c.Id == id);

        if (!isQuestionReportExist)
        {
            response.Message = $"Report with id {id} does not exist!";
            return response;
        }

        var questionReport = await _unitOfWork.QuestionReports.GetQuestionReport(id);

        response.Message = "Success";
        response.Status = true;

        response.Data = new QuestionReportViewModel
        {
            Id = id,
            AdditionalComment = questionReport.AdditionalComment,
            QuestionId = questionReport.Question.Id,
            QuestionReporter = questionReport.User.UserName,
            QuestionText = questionReport.Question.QuestionText,
            FlagNames = questionReport.QuestionReportFlags
                                .Select(f => f.Flag.FlagName)
                                .ToList(),
        };

        return response;
    }

    public async Task<QuestionReportsResponseModel> GetQuestionReports(string id)
    {
        var response = new QuestionReportsResponseModel();

        try
        {
            var questionWithReports = await _unitOfWork.QuestionReports.GetQuestionReports(id);

            response.Data = questionWithReports
                .Select(qr => new QuestionReportViewModel
                {
                    Id = qr.Id,
                    QuestionId = qr.QuestionId,
                    QuestionReporter = qr.User.UserName,
                    AdditionalComment = qr.AdditionalComment,
                    FlagNames = qr.QuestionReportFlags
                                    .Select(f => f.Flag.FlagName)
                                    .ToList()
                }).ToList();

            response.Status = true;
            response.Message = "Success";

            return response;
        }
        catch (Exception ex)
        {
            response.Message = $"An error occured: {ex.Message}";
            return response;
        }
    }

    public async Task<BaseResponseModel> UpdateQuestionReport(string id, UpdateQuestionReportViewModel request)
    {
        var response = new BaseResponseModel();

        var questionReportExist = await _unitOfWork.QuestionReports.ExistsAsync(c => c.Id == id);

        if (!questionReportExist)
        {
            response.Message = "Question report does not exist!";
            return response;
        }

        var questionReport = await _unitOfWork.QuestionReports.GetAsync(id);

        questionReport.AdditionalComment = request.AdditionalComment;

        try
        {
            await _unitOfWork.QuestionReports.UpdateAsync(questionReport);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            response.Message = $"Could not update the question report: {ex.Message}";
            return response;
        }

        response.Message = "Question report updated successfully!";

        return response;
    }
}
