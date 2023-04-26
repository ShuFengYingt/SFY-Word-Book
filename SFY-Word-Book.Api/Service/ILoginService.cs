using SFY_Word_Book.Api.Serviece;
using SFY_Word_Book.Shared;
using SFY_Word_Book.Shared.Dtos;
using System.Text;
using APIResponse = SFY_Word_Book.Api.Serviece.APIResponse;

namespace SFY_Word_Book.Api.Service
{
    public interface ILoginService
    {
        Task<APIResponse> LoginAsync(string account, string password);
        Task<APIResponse> RegisterAsync(UserDto user);
    }
}
