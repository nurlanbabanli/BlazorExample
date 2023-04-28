using Core.Results.Abstract;

namespace Core.Results.Concrete
{
    public class ErrorDataResult<TData> : DataResult<TData>, IDataResult<TData>
    {
        public ErrorDataResult(TData data, string message) : base(data, message, false)
        {

        }

        public ErrorDataResult(TData data) : base(data, false)
        {

        }
    }
}
