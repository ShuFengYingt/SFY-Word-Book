using Newtonsoft.Json;
using RestSharp;
using SFY_Word_Book.Api.Serviece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFY_Word_Book.Shared;
using APIResponse = SFY_Word_Book.Shared.APIResponse;

namespace SFY_Word_Book.Service
{
    /// <summary>
    /// 客户端配置
    /// </summary>
    public class HttpRestClient
    {
        /// <summary>
        /// api请求地址
        /// </summary>
        private readonly string apiUrl;
        protected  readonly RestClient restClient;

        public HttpRestClient(string apiUrl) 
        {
            this.apiUrl = apiUrl;
            restClient = new RestClient();
        }

        /// <summary>
        /// 执行请求通用方法
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        public async Task<APIResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameter != null)
            {
                request.AddParameter("param",JsonConvert.SerializeObject(baseRequest.Parameter),ParameterType.RequestBody );
            }
            restClient.BaseUrl = new Uri(apiUrl + baseRequest.Route);
            var response = await restClient.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<APIResponse>(response.Content);

        }
        public async Task<APIResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameter != null)
            {
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            }
            restClient.BaseUrl = new Uri(apiUrl + baseRequest.Route);
            var response = await restClient.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<APIResponse<T>>(response.Content);

        }

    }
}
