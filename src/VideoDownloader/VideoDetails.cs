using System;
using System.Collections.Generic;

namespace VideoDownloader
{
    public class QualityOption
    {
        public string Label { get; set; }
        public long VideoSizeBytes { get; set; }
        public long AudioSizeBytes { get; set; }
        public long TotalSizeBytes => VideoSizeBytes + AudioSizeBytes;
        public string DisplayText { get; set; }

        public override string ToString()
        {
            return DisplayText ?? Label ?? "Padrão";
        }
    }

    public class VideoDetails
    {
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public TimeSpan? Duration { get; set; }
        public string ThumbnailUrl { get; set; }
        public List<QualityOption> Qualities { get; set; } = new List<QualityOption>();
        public long AudioOnlySizeBytes { get; set; }

        public string GetFormattedDuration()
        {
            if (!Duration.HasValue) return "Desconhecido";
            var d = Duration.Value;
            return d.TotalHours >= 1
                ? $"{(int)d.TotalHours}:{d.Minutes:D2}:{d.Seconds:D2}"
                : $"{d.Minutes:D2}:{d.Seconds:D2}";
        }

        public static string FormatBytes(long bytes)
        {
            if (bytes <= 0) return "N/D";
            double kb = bytes / 1024.0;
            double mb = kb / 1024.0;
            double gb = mb / 1024.0;

            if (gb >= 1.0)
                return $"{gb:F1} GB";
            if (mb >= 1.0)
                return $"{mb:F1} MB";
            return $"{kb:F0} KB";
        }
    }

    public class PlaylistDetails
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int TotalVideos { get; set; }
        public string ThumbnailUrl { get; set; }
        public string FirstVideoId { get; set; }
    }
}
