namespace Kadam.DTO
{
    public class ServiceResponseDTO
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Result { get; set; }
    }
}
