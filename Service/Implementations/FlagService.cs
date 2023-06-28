using IdealDiscuss.Entities;
using IdealDiscuss.Models;
using IdealDiscuss.Models.Flag;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace IdealDiscuss.Service.Implementations
{
    public class FlagService : IFlagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FlagService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponseModel> CreateFlag(CreateFlagViewModel request)
        {
            var response = new BaseResponseModel();
            var isFlagExist = await _unitOfWork.Flags.ExistsAsync(c => c.FlagName == request.FlagName);

            if (isFlagExist)
            {
                response.Message = $"Flag with name {request.FlagName} already exist!";
                return response;
            }

            var flag = new Flag()
            {
                FlagName = request.FlagName,
                Description = request.Description
            };

            try
            {
                await _unitOfWork.Flags.CreateAsync(flag);
                await _unitOfWork.SaveChangesAsync();
                response.Status = true;
                response.Message = "Flag created successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create Flag. {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponseModel> DeleteFlag(string flagId)
        {
            var response = new BaseResponseModel();
            var flagExist = await _unitOfWork.Flags.ExistsAsync(x => x.Id == flagId);

            if (!flagExist)
            {
                response.Message = "Flag does not exist!";
                return response;
            }

            var flags = await _unitOfWork.Flags.GetAsync(flagId);

            try
            {
                await _unitOfWork.Flags.RemoveAsync(flags);
                await _unitOfWork.SaveChangesAsync();
                response.Message = "Flag deleted successfully.";
                response.Status = true;

                return response;
            }
            catch (Exception)
            {
                response.Message = "Flag delete failed";
                return response;
            }
        }

        public async Task<FlagsResponseModel> GetAllFlag()
        {
            var response = new FlagsResponseModel();

            try
            {
                var flags = await _unitOfWork.Flags.GetAllAsync(f => f.IsDeleted == false);

                if (flags is null || flags.Count == 0)
                {
                    response.Message = "No flags found!";
                    return response;
                }

                response.Data = flags
                   .Select(f => new FlagViewModel
                   {
                       Id = f.Id,
                       FlagName = f.FlagName,
                       Description = f.Description
                   }).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = ex.StackTrace;
                return response;
            }

            return response;
        }

        public async Task<FlagResponseModel> GetFlag(string flagId)
        {
            var response = new FlagResponseModel();
            var flagExist = await _unitOfWork.Flags.ExistsAsync(f =>
                                (f.Id == flagId)
                                && (f.Id == flagId
                                && f.IsDeleted == false));

            if (!flagExist)
            {
                response.Message = $"Flag with id {flagId} does not exist.";
                return response;
            }

            try
            {
                var flags = await _unitOfWork.Flags.GetAsync(flagId);

                response.Message = "Success";
                response.Status = true;
                response.Data = new FlagViewModel
                {
                    Id = flags.Id,
                    FlagName = flags.FlagName,
                    Description = flags.Description
                };
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }

            return response;
        }

        public async Task<BaseResponseModel> UpdateFlag(string flagId, UpdateFlagViewModel request)
        {
            var response = new BaseResponseModel();

            var isFlagExist = await _unitOfWork.Flags.ExistsAsync(f =>
                                                (f.Id == flagId)
                                                && (f.Id == flagId
                                                && f.IsDeleted == false));

            if (!isFlagExist)
            {
                response.Message = "Flag does not exist!";
                return response;
            }

            var flag = await _unitOfWork.Flags.GetAsync(flagId);

            flag.FlagName = request.FlagName;
            flag.Description = request.Description;

            try
            {
                await _unitOfWork.Flags.UpdateAsync(flag);
                await _unitOfWork.SaveChangesAsync();
                response.Message = "Flag updated successfully.";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the flag: {ex.Message}";
                return response;
            }
        }

        public async Task<IEnumerable<SelectListItem>> SelectFlags()
        {
            var flags = await _unitOfWork.Flags.SelectAll();

            return flags.Select(f => new SelectListItem()
            {
                Text = f.FlagName,
                Value = f.Id
            });
        }
    }
}

