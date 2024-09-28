using DragonAcc.Service.Common.Services;

namespace DragonAcc.Service.Common.IServices
{
    public interface IFtpDirectoryService
    {
        /// <summary>
        /// Gets the root directory.
        /// </summary>
        /// <returns></returns>
        string GetRootDirectory();

        /// <summary>
        /// Transfers to FTP directory asynchronous.
        /// </summary>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="outputDirectory">The output directory.</param>
        /// <param name="outputFileName">Name of the output file.</param>
        /// <returns></returns>
        Task<TransferResult> TransferToFtpDirectoryAsync(Stream fileStream, string outputDirectory, string outputFileName);

        /// <summary>
        /// Gets the file stream.
        /// </summary>
        /// <param name="remoteFilePath">The remote file path.</param>
        /// <returns></returns>
        TransferResult GetFileStream(string remoteFilePath);
    }
}
