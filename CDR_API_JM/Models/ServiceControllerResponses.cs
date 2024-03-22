namespace CDR_API_JM.Models
{
    public class SuccessResponse<T>
    {
        public bool Success { get; set; } = true;
        public T Data { get; set; }
    }

    public class ErrorResponse
    {
        public bool Success { get; set; } = false;
        public string ErrorMessage { get; set; }
    }
}
