using BlazorServerApp.Service.Abstract;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorServerApp.Service.Concrete
{
    public class FileUploadManager : IFileUploadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadManager(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment=webHostEnvironment;
        }

        public async Task<Core.Results.Abstract.IResult> DeleteFile(string filePath)
        {
            if (File.Exists(_webHostEnvironment.WebRootPath+filePath))
            {
                File.Delete(_webHostEnvironment.WebRootPath+filePath);
                return new SuccessResult(); 
            }

            return new ErrorResult();
        }

        public async Task<IDataResult<string>>UploadFile(IBrowserFile browserFile)
        {
            FileInfo fileInfo = new FileInfo(browserFile.Name);
            var fileName=Guid.NewGuid().ToString()+fileInfo.Extension ;
            var folederDirectory = _webHostEnvironment.WebRootPath+"\\images\\product";
            if (!Directory.Exists(folederDirectory))
            {
                Directory.CreateDirectory(folederDirectory);
            }

            var filePath=Path.Combine(folederDirectory, fileName);
            await using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            await browserFile.OpenReadStream(maxAllowedSize:20480000).CopyToAsync(fileStream);

            return new SuccessDataResult<string>($"/images/product/{fileName}");
                
        }
    }
}
