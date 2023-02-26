
using IdealDiscuss.Dtos;
using IdealDiscuss.Dtos.CommentReport;
using IdealDiscuss.Dtos.FlagDto;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Implementations;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Service.Interface;

namespace IdealDiscuss.Service.Implementations
{
    public class FlagService : IFlagService
    {
        private readonly IFlagRepository _flagRepository;

        public FlagService(IFlagRepository flagRepository)
        {
            _flagRepository = flagRepository;
        }

        public BaseResponseModel CreateFlag(CreateFlagDto createFlagDto)
        {
            var response = new BaseResponseModel();

            var isFlagExist = _flagRepository.Exists(c => c.FlagName == createFlagDto.FlagName);
            if (isFlagExist)
            {
                response.Message = "Flag does not exist.";
                return response;
            }

            var flag = new Flag
            {
                FlagName = response.Message
            };

            try
            {
                _flagRepository.Create(flag);
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create Flag. {ex.Message}";
                return response;
            }
            response.Status = true;
            response.Message = "Flag created successfully.";

            return response;

        }

        public BaseResponseModel DeleteFlag(int flagId)
        {
            var response = new BaseResponseModel();

            if (!_flagRepository.Exists(x => x.Id == flagId))
            {
                response.Message = "Flag does not exist.";
                return response;
            }

            var flags = _flagRepository.Get(flagId);
            flags.IsDeleted = true;

            try
            {
                _flagRepository.Update(flags);
            }
            catch (Exception ex)
            {
                response.Message = "Flag delete failed.";
                return response;
            }

            response.Status = true;
            response.Message = "Flag deleted successfully.";
            return response;
        }

        public FlagsResponseModel GetAllFlag()
        {
            var response = new FlagsResponseModel();

            var flags = _flagRepository.GetAll();

            response.Reports = flags.Select(flags => new ViewFlagDto
            {
                Id = flags.Id,
                FlagName = flags.FlagName,
                Description = flags.Description

            }).ToList();

            response.Status = true;
            response.Message = "Success";

            return response;

        }
        public FlagResponseModel GetFlag(int flagId)
        {
            var response = new FlagResponseModel();

            if (!_flagRepository.Exists(c => c.Id == flagId))
            {
                response.Message = $"CommentReport with id {flagId} does not exist.";
                return response;
            }
            var flags = _flagRepository.Get(flagId);

            response.Message = "Success";
            response.Status = true;
            response.Report = new ViewFlagDto
            {
                Id = flags.Id,
                FlagName = flags.FlagName,
                Description = flags.Description
            };

            return response;
        }

        public BaseResponseModel UpdateFlag(int flagId, UpdateFlagDto updateFlagDto)
        {
            var response = new BaseResponseModel();

            if (!_flagRepository.Exists(x => x.Id == flagId))
            {
                response.Message = "Flag does not exist.";
                return response;
            }

            var flag = _flagRepository.Get(flagId);

            flag.Description = updateFlagDto.Description;

            try
            {
                _flagRepository.Update(flag);
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the flag: {ex.Message}";
                return response;
            }
            response.Message = "Flag updated successfully.";
            return response;
        }


    }
}

