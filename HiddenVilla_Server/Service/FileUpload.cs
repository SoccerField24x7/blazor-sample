using System.Threading.Tasks;
using HiddenVilla_Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;

namespace HiddenVilla_Server.Service
{
    public class FileUpload : IFileUpload
    {
        public bool DeleteFile(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> UploadFile(IBrowserFile file)
        {
            throw new System.NotImplementedException();
        }
    }
}