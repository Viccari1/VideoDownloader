using System;

namespace VideoDownloader
{
    public static class YoutubeErrorHelper
    {
        public static string TratarExcecao(Exception ex)
        {
            if (ex == null) return "Erro desconhecido.";

            string erroCompleto = ex.ToString();
            if (erroCompleto.Contains("403") || erroCompleto.Contains("Forbidden"))
            {
                return "Erro 403 (Acesso Negado pelo YouTube):\n\n" +
                       "O YouTube restringiu temporariamente o acesso a este stream de mídia.\n" +
                       "Possíveis causas:\n" +
                       "• Restrição de idade ou conteúdo protegido por login/membros;\n" +
                       "• Bloqueio regional de exibição;\n" +
                       "• Excesso temporário de requisições do seu IP.\n\n" +
                       "Aguarde alguns minutos ou tente outro vídeo.";
            }

            if (ex is ArgumentException)
            {
                return "A URL informada é inválida. Verifique o link e tente novamente.";
            }

            return ex.Message;
        }
    }
}
