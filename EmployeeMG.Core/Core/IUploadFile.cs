using Microsoft.AspNetCore.Http;

namespace EmployeeMG.Core.Core
{
    public interface IUploadFile
    {
        string Upload(IFormFile file);
        void DeleteFile(string file);
    }

}