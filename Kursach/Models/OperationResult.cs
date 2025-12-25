namespace SecurityCompanyWPF.Models
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static OperationResult Success(string message = "Операция выполнена успешно", object data = null)
            => new OperationResult { IsSuccess = true, Message = message, Data = data };

        public static OperationResult Failure(string message, object data = null)
            => new OperationResult { IsSuccess = false, Message = message, Data = data };

        public static OperationResult Error(string message, object data = null)
            => new OperationResult { IsSuccess = false, Message = $"Ошибка: {message}", Data = data };
    }
}