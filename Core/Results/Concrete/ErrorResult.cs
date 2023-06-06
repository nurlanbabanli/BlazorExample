using Core.Results.Abstract;

namespace Core.Results.Concrete
{
    public class ErrorResult : Result, IResult
    {

        public ErrorResult(string message, bool internalServerError = false) : base(message, false, internalServerError)
        {
        }

        public ErrorResult(bool internalServerError = false) : base(false, internalServerError)
        {
        }
    }
}
