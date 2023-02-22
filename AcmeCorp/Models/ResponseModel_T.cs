namespace AcmeCorp.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {
            IsSuccess = true;
            Message = string.Empty;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
