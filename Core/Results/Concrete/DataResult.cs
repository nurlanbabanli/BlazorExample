using Core.Results.Abstract;

namespace Core.Results.Concrete
{
    public class DataResult<TData> : Result, IDataResult<TData>
    {
        public DataResult(TData data, bool internalServerError = false) : this(data, string.Empty,internalServerError)
        {

        }

        public DataResult(TData data, string message, bool internalServerError = false) : this(data, message, false, internalServerError)
        {

        }

        public DataResult(TData data, bool isSuccess, bool internalServerError = false) : this(data, string.Empty, isSuccess,internalServerError)
        {

        }

        public DataResult(TData data, string message, bool isSuccess, bool internalServerError=false) : base(message, isSuccess,internalServerError)
        {
            Data = data;
        }

        public TData Data { get; }
    }
}
