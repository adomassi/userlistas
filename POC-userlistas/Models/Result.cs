namespace POC_userlistas.Models
{
    public class Result<T>
    {
        public string? Error { get; set; }
        public bool Success { get; set; }
        public T? ResultObject { get; set; }
    }

    public class Result
    {
        public string? Error { get; set; }
        public bool Success { get; set; }
    }
}
