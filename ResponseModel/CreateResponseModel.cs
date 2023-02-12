namespace IdealDiscuss.ResponseModel
{
    public class CreateResponseModel : BaseResponse
    {
        public static readonly CreateResponseModel NotPermitted = new CreateResponseModel(
           false,
          "",
           "You don't have sufficient permissions to perform this action.");



        public CreateResponseModel(bool status,
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
