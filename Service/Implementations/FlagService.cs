using IdealDiscuss.Entities;
using IdealDiscuss.Models;
using IdealDiscuss.Models.Flag;
using IdealDiscuss.Repository.Implementations;
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

        public FlagService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public BaseResponseModel CreateFlag(CreateFlagViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var createdDate = DateTime.Now;

            var isFlagExist = _unitOfWork.Flags.Exists(c => c.FlagName == request.FlagName);

            if (isFlagExist)
            {
                response.Message = $"Flag with {request.FlagName} already exist!";
                return response;
            }

            if (string.IsNullOrWhiteSpace(request.FlagName))
            {
                response.Message = "Flag name is required!";
                return response;
            }

            var flag = new Flag()
            {
                FlagName = request.FlagName,
                Description = request.Description,
                CreatedBy = createdBy,
                DateCreated = createdDate
            };

            try
            {
                _unitOfWork.Flags.Create(flag);
                _unitOfWork.SaveChanges();
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

        public BaseResponseModel DeleteFlag(string flagId)
        {
            var response = new BaseResponseModel();
            var flagExist = _unitOfWork.Flags.Exists(x => x.Id == flagId);

            if (!flagExist)
            {
                response.Message = "Flag does not exist!";
                return response;
            }

            var flags = _unitOfWork.Flags.Get(flagId);
            flags.IsDeleted = true;

            try
            {
                _unitOfWork.Flags.Update(flags);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Flag deleted successfully.";

                return response;
            }
            catch (Exception)
            {
                response.Message = "Flag delete failed";
                return response;
            }
        }

        public FlagsResponseModel GetAllFlag()
        {
            var response = new FlagsResponseModel();

            try
            {
                var flags = _unitOfWork.Flags.GetAll(f => f.IsDeleted == false);

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

        public FlagResponseModel GetFlag(string flagId)
        {
            var response = new FlagResponseModel();
            var flagExist = _unitOfWork.Flags.Exists(f =>
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
                var flags = _unitOfWork.Flags.Get(flagId);

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

        public BaseResponseModel UpdateFlag(string flagId, UpdateFlagViewModel request)
        {
            var response = new BaseResponseModel();
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var modifiedDate = DateTime.Now;
            Expression<Func<Flag, bool>> expression = f =>
                                                (f.Id == flagId)
                                                && (f.Id == flagId
                                                && f.IsDeleted == false);

            var isFlagExist = _unitOfWork.Flags.Exists(expression);

            if (!isFlagExist)
            {
                response.Message = "Flag does not exist!";
                return response;
            }

            if (string.IsNullOrWhiteSpace(request.FlagName))
            {
                response.Message = "Flag name cannot be null!";
                return response;
            }

            var flag = _unitOfWork.Flags.Get(flagId);

            flag.FlagName = request.FlagName;
            flag.Description = request.Description;
            flag.ModifiedBy = modifiedBy;
            flag.LastModified = modifiedDate;

            try
            {
                _unitOfWork.Flags.Update(flag);
                _unitOfWork.SaveChanges();
                response.Message = "Flag updated successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the flag: {ex.Message}";
                return response;
            }
        }

        public IEnumerable<SelectListItem> SelectFlags()
        {
            return _unitOfWork.Flags.SelectAll().Select(f => new SelectListItem()
            {
                Text = f.FlagName,
                Value = f.Id
            });
        }
    }
}

