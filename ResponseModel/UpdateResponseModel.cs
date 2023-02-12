namespace IdealDiscuss.ResponseModel
{
    public class UpdateResponseModel : BaseResponse
    {
        public static readonly UpdateResponseModel NotPermitted = new UpdateResponseModel(
           false,
          "",
           "You don't have sufficient permissions to perform this action.");



        public UpdateResponseModel(bool status,
                                  string code,
                                  string message,
                                  Guid? id = (Guid?)null,
                                  string field = "",
                                  int count = 0) : base(status,
                                                           code,
                                                           message,
                                                           field,
                                                           count)
        {
            Id = id;
        }
        public Guid? Id { get; }
    }
}
