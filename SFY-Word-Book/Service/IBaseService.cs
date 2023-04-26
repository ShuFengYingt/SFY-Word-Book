using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using Microsoft.VisualStudio.Services.WebApi;
using SFY_Word_Book.Shared.Parameter;
using SFY_Word_Book.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFY_Word_Book.Api.Serviece;
using APIResponse = SFY_Word_Book.Shared.APIResponse;

namespace SFY_Word_Book.Service
{
    /// <summary>
    /// 基本服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseService<TEntity> where TEntity : class//约束
    {
        Task<APIResponse<TEntity>> AddAsync(TEntity entity); 
        Task<APIResponse<TEntity>> UpdateAsync(TEntity entity);
        Task<APIResponse> DeleteAsync(int id);
        Task<APIResponse<TEntity>> GetFirstOfDefaultAsync(int id);
        Task<APIResponse<Shared.Models.PagedList<TEntity>>> GetAllAsync(QueryParameter queryParameter);
        
        
    }
}
