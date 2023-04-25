using SFY_Word_Book.Api.Serviece;
using SFY_Word_Book.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Service
{
    public class LoginService : ILoginService
    {
        private readonly HttpRestClient httpRestClient;

        //服务名称Login
        private readonly string serviceName = "Login";
        public LoginService(HttpRestClient httpRestClient) 
        { 
            this.httpRestClient = httpRestClient;
        }

        /// <summary>
        /// 异步执行登录操作，服务层
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<APIResponse> LoginAsync(UserDto userDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/Login";
            request.Parameter = userDto;
            return await httpRestClient.ExecuteAsync(request);
        }

        /// <summary>
        /// 异步执行注册操作，服务层
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<APIResponse> RegisterAsync(UserDto userDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/Register";
            request.Parameter = userDto;
            return await httpRestClient.ExecuteAsync(request);
        }
    }  
}
