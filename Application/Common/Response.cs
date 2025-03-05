namespace Application.Common
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public bool Status { get; set; } 
        public string Message { get; set; } = string.Empty;
    }
}
