using IdealDiscuss.Context;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;

namespace IdealDiscuss.Repository.Implementations
{
    public class FlagRepository : BaseRepository<Flag>, IFlagRepository
    {
        public FlagRepository(IdealDiscussContext context) : base(context)
        {
        }
    }
}
