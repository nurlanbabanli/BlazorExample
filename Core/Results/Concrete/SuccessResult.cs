using Core.Results.Abstract;

namespace Core.Results.Concrete
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
