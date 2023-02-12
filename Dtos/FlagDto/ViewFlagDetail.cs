namespace IdealDiscuss.Dtos.FlagDto
{
    public class ViewFlagDetail
    {
        public bool IsDeleted { get; set; }
        public string FlagName { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastModified { get; set; }
    }
}
