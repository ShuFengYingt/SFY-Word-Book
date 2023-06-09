﻿using SFY_Word_Book.Api.Serviece;
using SFY_Word_Book.Shared.Parameter;

namespace SFY_Word_Book.Api.Service
{
    /// <summary>
    /// 通用接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T>
    {
        Task<APIResponse> GetAllAsync(QueryParameter queryParameter);
        Task<APIResponse> GetSingleAsync(int id);

        Task<APIResponse> AddAsync(T model);
        Task<APIResponse> UpdateAsync(T model);
        Task<APIResponse> DeleteAsync(int id);
    }
}
