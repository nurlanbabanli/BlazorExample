using BlazorWasmApp.Results.Abstract;


namespace BlazorWasmApp.Results.Concrete
{
    public class Result : IResult
    {
        public Result(string message, bool isSuccess) : this(isSuccess)
        {
            Message = message;
        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public string Message { get; }
        public bool IsSuccess { get; }
    }
}
