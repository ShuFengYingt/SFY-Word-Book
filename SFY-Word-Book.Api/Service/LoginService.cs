using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using SFY_Word_Book.Api.Context;
using SFY_Word_Book.Api.Serviece;
using SFY_Word_Book.Shared;
using SFY_Word_Book.Shared.Dtos;
using SFY_Word_Book.Shared.Extension;
using APIResponse = SFY_Word_Book.Api.Serviece.APIResponse;

namespace SFY_Word_Book.Api.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LoginService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<APIResponse> LoginAsync(string account, string password)
        {
            try
            {
                //MD5加密
                password = password.GetMD5();
                //注意，GetRepository是传输层的,泛型也要填传输层的类
                var model = await unitOfWork.GetRepository<UserInfo>().GetFirstOrDefaultAsync(predicate: x => x.userName.Equals(account) && x.Password.Equals(password));
                if (model == null)
                {
                    return new APIResponse("账号或密码错误，请重试");
                }
                return new APIResponse(true, model);

            }
            catch (Exception ex)
            {
                return new APIResponse(false, "登录失败");
            }
        }

        public async Task<APIResponse> RegisterAsync(UserDto user)
        {
            try
            {
                var model = mapper.Map<UserInfo>(user);
                var userRepository = unitOfWork.GetRepository<UserInfo>();

                var userModel = await userRepository.GetFirstOrDefaultAsync(predicate:x => x.Account.Equals(model.Account));

                if (userModel != null)
                {
                    return new APIResponse($"账号:{model.Account}已存在,请重新注册");
                }
                model.CreateDate = DateTime.Now;
                //MD5加密
                model.Password = model.Password.GetMD5();
                await userRepository.InsertAsync(model);

                if (await unitOfWork.SaveChangesAsync() > 0)
                {
                    return new APIResponse(true, model);
                }
                return new APIResponse("注册失败，请稍后重试");
            }
            catch (Exception ex)
            {
                return new APIResponse("注册账号失败");

            }
        }
    }
}
