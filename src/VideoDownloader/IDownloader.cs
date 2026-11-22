using System.Threading.Tasks;

namespace VideoDownloader
{
    internal interface IDownloader
    {
        string arquivo { get; }
        Task<(bool, string)> VerifyVideoOKAsync();
        Task Download(Logger logger);
    }
}
