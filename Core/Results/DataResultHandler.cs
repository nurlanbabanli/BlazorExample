using Core.Results.Abstract;
using Core.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public static class DataResultHandler
    {
        public static IDataResult<TResult> FilterDataResults<TData, TResult>(IDataResult<TData> dataResult)
        {
            if (dataResult==null) return new ErrorDataResult<TResult>(default(TResult), "Get "+nameof(dataResult.Data)+" error", internalServerError: true);
            if (dataResult.InternalServerError) return new ErrorDataResult<TResult>(default(TResult), dataResult.Message, internalServerError: true);
            if (!dataResult.IsSuccess) return new ErrorDataResult<TResult>(default(TResult), dataResult.Message);

            return null;
        }
    }
}
