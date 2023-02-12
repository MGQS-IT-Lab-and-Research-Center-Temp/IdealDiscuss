namespace IdealDiscuss.Dtos.FlagDto
{
    public class UpdateFlag
    {
        public string FlagName { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }
}
