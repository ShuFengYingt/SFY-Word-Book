using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.IdentityModel.Abstractions;
using SFY_Word_Book.Api.Context;
using SFY_Word_Book.Api.Service;
using SFY_Word_Book.Shared.Dtos;
using SFY_Word_Book.Shared.Parameter;

namespace SFY_Word_Book.Api.Serviece
{
    /// <summary>
    /// 用户实现
    /// </summary>
    public class UserService : IUserInfoService
    {
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork work,IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        public Task<APIResponse> GetAllAsync(QueryParameter queryParameter)
        {
            throw new NotImplementedException();
        }

        async Task<APIResponse> IBaseService<UserDto>.AddAsync(UserDto model)
        {
            try
            {
                var user =  mapper.Map<UserInfo>(model);

                await work.GetRepository<UserInfo>().InsertAsync(user);
                if (await work.SaveChangesAsync() > 0)
                {
                    return new APIResponse(true, model);
                }
                return new APIResponse("数据添加失败");

            }
            catch (Exception ex)
            {
                return new APIResponse(ex.Message);
            }
        }

        async Task<APIResponse> IBaseService<UserDto>.DeleteAsync(int id)
        {
            try
            {
                //获得数据库
                var repository = work.GetRepository<UserInfo>();
                //查找
                var user =  await repository.GetFirstOrDefaultAsync(predicate:x=>x.Id.Equals(id));
                //删除
                repository.Delete(user);

                if (await work.SaveChangesAsync() > 0)
                {
                    return new APIResponse(true,"");
                }
                return new APIResponse("数据删除失败");

            }
            catch (Exception ex)
            {
                return new APIResponse(ex.Message);
            }
        }


        async Task<APIResponse> IBaseService<UserDto>.GetSingleAsync(int id)
        {
            try
            {
                //获得数据库
                var repository = work.GetRepository<UserInfo>();
                //查找
                var user = await repository.GetFirstOrDefaultAsync(predicate: x=> x.Id.Equals(id));
                return new APIResponse(true,user);

            }
            catch (Exception ex)
            {
                return new APIResponse(ex.Message);
            }
        }

        async Task<APIResponse> IBaseService<UserDto>.UpdateAsync(UserDto model)
        {
            try
            {
                var dbUser = mapper.Map<UserInfo>(model);

                //获得数据库
                var repository = work.GetRepository<UserInfo>();
                //查找
                var user = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbUser.Id));

                user.userName = dbUser.userName;
                user.Email = dbUser.Email;
                user.Password = dbUser.Password;

                repository.Update(user);

                if (await work.SaveChangesAsync() > 0)
                {
                    return new APIResponse(true, user);

                }

                return new APIResponse( "数据更新异常");

            }
            catch (Exception ex)
            {
                return new APIResponse(ex.Message);
            }
        }
    }
}
