﻿

namespace BlazorWasmApp.Results.Abstract
{
    public interface IResult
    {
        string Message { get; }
        bool IsSuccess { get; }
    }
}
