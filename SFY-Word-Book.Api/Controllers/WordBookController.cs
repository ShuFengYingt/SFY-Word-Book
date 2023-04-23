using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using SFY_Word_Book.Api.Context;
using SFY_Word_Book.Api.Service;
using SFY_Word_Book.Api.Serviece;
using System.Threading.Tasks;

namespace SFY_Word_Book.Api.Controllers
{
    /// <summary>
    /// 控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WordBookController:ControllerBase
    {
        private readonly IUserInfoServiece userInfoServiece;

        public WordBookController(IUserInfoServiece userInfoServiece) 
        {
            this.userInfoServiece = userInfoServiece;
        }

        [HttpGet]
        public async Task<APIResponse> Get(int id) => await userInfoServiece.GetSingleAsync(id);

        [HttpGet]
        public async Task<APIResponse> GetAll(int id) => await userInfoServiece.GetAllAsync();

        [HttpPost]
        public async Task<APIResponse> Add([FromBody]UserInfo model) => await userInfoServiece.AddAsync(model);

        [HttpPost]
        public async Task<APIResponse> Update([FromBody]UserInfo model) => await userInfoServiece.UpdateAsync(model);

        [HttpDelete]
        public async Task<APIResponse> Delete(int id) => await userInfoServiece.DeleteAsync(id);

    }
}
 