using Core.Results.Abstract;

namespace Core.Results.Concrete
{
    public class SuccessDataResult<TData> : DataResult<TData>, IDataResult<TData>
    {
        public SuccessDataResult(TData data, string message) : base(data, message, true)
        {

        }
        public SuccessDataResult(TData data) : base(data, true)
        {

        }
    }
}
