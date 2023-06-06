using Core.Results.Abstract;

namespace Core.Results.Concrete
{
    public class ErrorDataResult<TData> : DataResult<TData>, IDataResult<TData>
    {

        public ErrorDataResult(TData data, string message, bool internalServerError = false) : base(data, message, false,internalServerError)
        {
        }

        public ErrorDataResult(TData data, bool internalServerError = false) : base(data, false, internalServerError)
        {
        }

    }
}
