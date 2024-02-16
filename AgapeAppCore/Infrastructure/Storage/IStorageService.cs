using Microsoft.AspNetCore.Components.Forms;
using System.IO;
using System.Threading.Tasks;

namespace AgapeAppCore.Infrastructure.Storage
{
    public interface IStorageManager
    {
        Task<string> SaveAsync(Stream data, string objectName,  string contentType);

        Task<string> SaveInputFileAsync(IBrowserFile browserFile);

        Task EnsureCreated();
    }
}