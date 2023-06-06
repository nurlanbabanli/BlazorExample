using BlazorWasmApp.Results.Abstract;

namespace BlazorWasmApp.Results.Concrete
{
    public class SuccessResult : Result, IResult
    {
        public SuccessResult(string message) : base(message, true)
        {

        }

        public SuccessResult() : base(true)
        {

        }
    }
}
