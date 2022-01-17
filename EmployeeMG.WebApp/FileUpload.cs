using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EmployeeMG.WebApp
{
    public class FileUpload: IUploadFile
    {
        private IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file)
        {
            if (file == null) return "";

            var pathDirectory = $"{_webHostEnvironment.WebRootPath}//Images//";
            if (!Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory);
            }

            var filename = file.FileName;

            var filePath = pathDirectory + filename;
            using (var output = System.IO.File.Create(filePath))
            {
                file.CopyTo(output);
            }
            return $"{filename}";

        }

        public void DeleteFile(string file)
        {
            var pathDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images", file);
            if (File.Exists(pathDirectory))
            {
                File.Delete(pathDirectory);
            }
        }

    }
}
