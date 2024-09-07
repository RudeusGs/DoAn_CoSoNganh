using DragonAcc.Service.Common.IServices;
using Microsoft.Extensions.Configuration;

namespace DragonAcc.Service.Common.Services
{
    public class FtpDirectoryService : IFtpDirectoryService
    {
        private readonly string _ftpDirectory;

        public FtpDirectoryService(IConfiguration configuration)
        {
            _ftpDirectory = configuration["FtpServer:Directory"];
        }

        public string GetRootDirectory() => _ftpDirectory;

        public async Task<TransferResult> TransferToFtpDirectoryAsync(Stream fileStream, string outputDirectory, string outputFileName)
        {
            try
            {
                string remoteFilePath = $"{_ftpDirectory}{outputDirectory}/{outputFileName}";

                if(!Directory.Exists($"{_ftpDirectory}{outputDirectory}"))
                {
                    Directory.CreateDirectory($"{_ftpDirectory}{outputDirectory}");
                }

                fileStream.Seek(0, SeekOrigin.Begin);

                using (var file = new FileStream(remoteFilePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(file);
                }

                return new TransferResult();
            }
            catch (Exception ex)
            {
                return new TransferResult()
                {
                    Succeed = false,
                    InnerException = ex
                };
            }
        }

        public TransferResult GetFileStream(string remoteFilePath)
        {
            try
            {
                remoteFilePath = $"{_ftpDirectory}{remoteFilePath}";
                return new TransferResult()
                {
                    FileStream = new FileStream(remoteFilePath, FileMode.Open)
                };
            }
            catch (Exception ex)
            {
                return new TransferResult
                {
                    Succeed = false,
                    InnerException = ex,
                };
            }
        }
    }

    public class TransferResult
    {
        public bool Succeed { get; set; } = true;

        /// <summary>
        /// Inner exception when transferring result was failed.
        /// </summary>
        public Exception InnerException { get; set; } = null;

        /// <summary>
        /// File stream when get file from FTP directory was successfully.
        /// </summary>
        public FileStream FileStream { get; set; } = null;
    }
}
