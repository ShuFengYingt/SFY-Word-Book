using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.IdentityModel.Abstractions;
using SFY_Word_Book.Api.Context;
using SFY_Word_Book.Api.Service;

namespace SFY_Word_Book.Api.Serviece
{
    /// <summary>
    /// 用户实现
    /// </summary>
    public class UserService : IUserInfoServiece
    {
        private readonly IUnitOfWork work;

        public UserService(IUnitOfWork work)
        {
            this.work = work;
        }


        async Task<APIResponse> IBaseService<UserInfo>.AddAsync(UserInfo model)
        {
            try
            {
                await work.GetRepository<UserInfo>().InsertAsync(model);
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

        async Task<APIResponse> IBaseService<UserInfo>.DeleteAsync(int id)
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

        async Task<APIResponse> IBaseService<UserInfo>.GetAllAsync()
        {
            try
            {
                //获得数据库
                var repository = work.GetRepository<UserInfo>();
                //查找
                var users = await repository.GetAllAsync(); 
                return new APIResponse(true,users);

            }
            catch (Exception ex)
            {
                return new APIResponse(ex.Message);
            }
        }

        async Task<APIResponse> IBaseService<UserInfo>.GetSingleAsync(int id)
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

        async Task<APIResponse> IBaseService<UserInfo>.UpdateAsync(UserInfo model)
        {
            try
            {
                //获得数据库
                var repository = work.GetRepository<UserInfo>();
                //查找
                var user = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));

                user.Name = model.Name;
                user.Email = model.Email;
                user.Password = model.Password;

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
