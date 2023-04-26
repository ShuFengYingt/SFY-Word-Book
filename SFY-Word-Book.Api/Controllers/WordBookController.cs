using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using SFY_Word_Book.Api.Context;
using SFY_Word_Book.Api.Service;
using SFY_Word_Book.Api.Serviece;
using SFY_Word_Book.Shared;
using SFY_Word_Book.Shared.Dtos;
using System.Threading.Tasks;
using APIResponse = SFY_Word_Book.Api.Serviece.APIResponse;

namespace SFY_Word_Book.Api.Controllers
{
    /// <summary>
    /// 控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WordBookController:ControllerBase  
    {
        private readonly IUserInfoService userInfoServiece;

        public WordBookController(IUserInfoService userInfoServiece) 
        {
            this.userInfoServiece = userInfoServiece;
        }

        [HttpGet]
        public async Task<APIResponse> Get(int id) => await userInfoServiece.GetSingleAsync(id);


        [HttpPost]
        public async Task<APIResponse> Add([FromBody] UserDto model) => await userInfoServiece.AddAsync(model);

        [HttpPost]
        public async Task<APIResponse> Update([FromBody] UserDto model) => await userInfoServiece.UpdateAsync(model);

        [HttpDelete]
        public async Task<APIResponse> Delete(int id) => await userInfoServiece.DeleteAsync(id);

    }
}
 