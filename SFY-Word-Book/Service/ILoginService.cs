using SFY_Word_Book.Api.Serviece;
using SFY_Word_Book.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIResponse = SFY_Word_Book.Shared.APIResponse;


namespace SFY_Word_Book.Service
{
    public interface ILoginService
    {
        Task<APIResponse> LoginAsync(UserDto userDto);
        Task<APIResponse> RegisterAsync(UserDto userDto);

            
    }
}
