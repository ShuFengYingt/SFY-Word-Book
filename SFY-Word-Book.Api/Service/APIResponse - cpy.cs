namespace SFY_Word_Book.Api.Serviece
{
    /// <summary>
    /// 通用返回
    /// </summary>
    public class APIResponse
    {
        public APIResponse(string message, bool statue = false)
        {
            this.Message = message; this.Statue = statue;
        }

        public APIResponse(bool statue, object result)
        {
            this.Statue = statue;
            this.Result = result;
        }

        public string Message { get; set; }

        public bool Statue { get; set; }

        public object Result { get; set; }
    }
}
