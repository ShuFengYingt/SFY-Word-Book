using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace SFY_Word_Book.Api.Context.Repository
{
    public class UserInfoRepository : Repository<UserInfo>, IRepository<UserInfo>
    {
        public UserInfoRepository(SFYWordBookContext dbContext) : base(dbContext)
        {
        }
    }
}
