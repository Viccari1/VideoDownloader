using YoutubeExplode;

namespace VideoDownloader
{
    public static class YoutubeHelper
    {
        /// <summary>
        /// Instância única compartilhada do YoutubeClient.
        /// O YoutubeClient é thread-safe e reutiliza a pool interna de conexões HTTP (evitando esgotamento de sockets).
        /// </summary>
        public static YoutubeClient Client { get; } = new YoutubeClient();
    }
}
