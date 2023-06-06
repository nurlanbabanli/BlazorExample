namespace BlazorWasmApp.Results.Abstract
{
    public interface IDataResult<TData> : IResult
    {
        TData Data { get; }
    }
}
