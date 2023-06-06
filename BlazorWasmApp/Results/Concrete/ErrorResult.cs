using BlazorWasmApp.Results.Abstract;
using BlazorWasmApp.Results.Concrete;

namespace BlazorWasmApp.Results.Concrete
{
    public class ErrorResult : Result, IResult
    {
        public ErrorResult(string message) : base(message, false)
        {

        }

        public ErrorResult() : base(false)
        {

        }
    }
}
