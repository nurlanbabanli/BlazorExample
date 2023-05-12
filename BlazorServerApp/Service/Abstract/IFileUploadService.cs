using Core.Results.Abstract;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorServerApp.Service.Abstract
{
    public interface IFileUploadService
    {
        Task<IDataResult<string>> UploadFile(IBrowserFile browserFile);
        Task<Core.Results.Abstract.IResult> DeleteFile(string filePath);
    }
}
