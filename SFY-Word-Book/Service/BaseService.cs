using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.TeamFoundation.SourceControl.WebApi.Legacy;
using RestSharp;
using SFY_Word_Book.Api.Serviece;
using SFY_Word_Book.Shared;
using SFY_Word_Book.Shared.Models;
using SFY_Word_Book.Shared.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryParameter = SFY_Word_Book.Shared.Parameter.QueryParameter;

namespace SFY_Word_Book.Service
{
    public class BaseService<TEntity>:IBaseService<TEntity> where TEntity : class 
    {
        private readonly HttpRestClient client;
        private readonly string serviceName;

        public BaseService(HttpRestClient client,string serviceName) 
        {
            this.client = client;
            this.serviceName = serviceName;
        }

        public async Task<APIResponse<TEntity>> AddAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/Add";
            request.Parameter = entity;
            return await client.ExecuteAsync<TEntity>(request);
        }

        public async Task<APIResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Delete;
            request.Route = $"api/{serviceName}/Delete?id={id}";
            return await client.ExecuteAsync(request);
        }

        public async Task<APIResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter queryParameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{serviceName}/GetAll?pageIndex={queryParameter.PageIndex}&pageSize={queryParameter.PageSize}&search={queryParameter.Search}";
            request.Parameter = queryParameter;
            return await client.ExecuteAsync<PagedList<TEntity>>(request);
        }

        public async Task<APIResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/{serviceName}/Get?id={id}";
            return await client.ExecuteAsync<TEntity>(request);
        }

        public async Task<APIResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/Update";
            request.Parameter = entity;
            return await client.ExecuteAsync<TEntity>(request);
        }
    }
}
