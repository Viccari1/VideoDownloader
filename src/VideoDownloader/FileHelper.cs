using System;
using System.IO;

namespace VideoDownloader
{
    public static class FileHelper
    {
        public static string ObterPastaVideos()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Baixadas");
        }

        public static string ObterPastaMusicas()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Baixadas");
        }

        public static void GarantirDiretoriosPadrao()
        {
            string videos = ObterPastaVideos();
            if (!Directory.Exists(videos))
            {
                Directory.CreateDirectory(videos);
            }

            string musicas = ObterPastaMusicas();
            if (!Directory.Exists(musicas))
            {
                Directory.CreateDirectory(musicas);
            }
        }

        public static string SanitizarNomeArquivo(string nome, string fallback = "arquivo")
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return fallback;
            }

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                nome = nome.Replace(c, '_');
            }

            nome = nome.Trim();
            return string.IsNullOrWhiteSpace(nome) ? fallback : nome;
        }

        public static string ObterCaminhoDisponivel(string pastaBase, string nomeBase, string extensao)
        {
            if (!Directory.Exists(pastaBase))
            {
                Directory.CreateDirectory(pastaBase);
            }

            if (!extensao.StartsWith("."))
            {
                extensao = "." + extensao;
            }

            string nomeLimpo = SanitizarNomeArquivo(nomeBase);
            string nomeFinal = nomeLimpo;
            int contador = 1;

            while (true)
            {
                string caminhoCompleto = Path.Combine(pastaBase, nomeFinal + extensao);
                if (!File.Exists(caminhoCompleto))
                {
                    return caminhoCompleto;
                }

                nomeFinal = $"{nomeLimpo} ({contador})";
                contador++;
            }
        }
    }
}
