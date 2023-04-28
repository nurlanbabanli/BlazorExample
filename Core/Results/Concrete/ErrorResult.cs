using Core.Results.Abstract;

namespace Core.Results.Concrete
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
