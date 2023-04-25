using Microsoft.AspNetCore.Mvc;
using SFY_Word_Book.Api.Service;
using SFY_Word_Book.Api.Serviece;
using SFY_Word_Book.Shared;
using SFY_Word_Book.Shared.Dtos;

namespace SFY_Word_Book.Api.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class LoginController : ControllerBase
    {
        private readonly ILoginService userInfoServiece;

        public LoginController(ILoginService userInfoServiece)
        {
            this.userInfoServiece = userInfoServiece;
        }

        [HttpPost]
        public async Task<APIResponse> Login([FromBody] UserDto  userDto) => await userInfoServiece.LoginAsync(userDto.Account, userDto.Password);

        [HttpPost]
        public async Task<APIResponse> Register([FromBody] UserDto user) => await userInfoServiece.RegisterAsync(user);

    }
}
