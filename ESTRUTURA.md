# Estrutura e Mapa de Modificação do Projeto VideoDownloader

Este documento mapeia detalhadamente todos os arquivos, fluxos e responsabilidades do **VideoDownloader**. Ele foi estruturado para servir de guia de consulta rápida e assertiva para agentes de IA e desenvolvedores identificarem exatamente quais arquivos alterar para cada tipo de modificação ou correção de bug.

---

## 📌 1. Visão Geral do Projeto

- **Tecnologia:** C# / .NET Framework 4.8 / Windows Forms.
- **Solução / Projeto:** [src/VideoDownloader.sln](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader.sln) e [src/VideoDownloader/VideoDownloader.csproj](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/VideoDownloader.csproj).
- **Dependências Principais:**
  - `YoutubeExplode` e `YoutubeExplode.Converter` (Download de streams e conversão).
  - `CliWrap` / `Process` para orquestração de processos externos.
  - `ffmpeg.exe` (executável colocado na pasta de saída `bin/Debug` ou `bin/Release` para multiplexar áudio e vídeo em MP4 ou converter MP3).
  - `AngleSharp` (parsing HTML interno do YoutubeExplode).
- **Pastas de Saída dos Downloads:**
  - Vídeos: `[Minhas Vídeos]/Baixadas`
  - Áudios: `[Minha Música]/Baixadas`
  - Playlists: Subpastas nomeadas conforme o título da playlist dentro de `Baixadas`.

---

## 🎯 2. Guia de Modificação Rápida: "O que alterar quando..."

| Se você precisa... | Arquivos Primários | Arquivos Secundários |
| :--- | :--- | :--- |
| **Alterar pasta ou regra de nomenclatura dos arquivos baixados** | [Video.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Video.cs)<br>[Audio.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Audio.cs)<br>[Playlist.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Playlist.cs) | [TelaVideo.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaVideo.cs)<br>[TelaPlaylist.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPlaylist.cs) |
| **Modificar a lógica de obtenção/seleção de qualidade de vídeo** | [Video.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Video.cs)<br>[TelaVideo.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaVideo.cs) | [VideoDetails.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/VideoDetails.cs) |
| **Alterar o fluxo de download de vídeo avulso (FFmpeg / união de streams)** | [Video.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Video.cs) | [TelaVideo.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaVideo.cs) |
| **Alterar o fluxo de download de áudio avulso (conversão MP3 / bitrate)** | [Audio.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Audio.cs) | [TelaVideo.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaVideo.cs) |
| **Alterar lógica de download ou tratamento de Playlists** | [Playlist.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Playlist.cs) | [TelaPlaylist.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPlaylist.cs) |
| **Alterar a interface ou comportamento da tela de download de vídeos** | [TelaVideo.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaVideo.cs)<br>[TelaVideo.Designer.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaVideo.Designer.cs) | [TelaVideo.resx](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaVideo.resx) |
| **Alterar a interface ou comportamento da tela de playlists** | [TelaPlaylist.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPlaylist.cs)<br>[TelaPlaylist.Designer.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPlaylist.Designer.cs) | [TelaPlaylist.resx](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPlaylist.resx) |
| **Alterar a tela inicial (menu de opções/navegação)** | [TelaPrincipal.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPrincipal.cs)<br>[TelaPrincipal.Designer.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPrincipal.Designer.cs) | [TelaPrincipal.resx](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPrincipal.resx) |
| **Alterar como o progresso e as mensagens de status são exibidos** | [Logger.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Logger.cs) | [TelaVideo.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaVideo.cs)<br>[TelaPlaylist.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPlaylist.cs) |
| **Download e detecção automática do executável FFmpeg** | [FFmpegHelper.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/FFmpegHelper.cs)<br>[TelaDownloadFFmpeg.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaDownloadFFmpeg.cs) | [TelaPrincipal.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPrincipal.cs)<br>[Video.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Video.cs) |
| **Atualizar pacotes NuGet, redirecionamentos de binding ou versão do C#/.NET** | [packages.config](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/packages.config)<br>[App.config](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/App.config)<br>[VideoDownloader.csproj](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/VideoDownloader.csproj) | - |
| **Metadados da aplicação (versão, autor, ícone, título da janela)** | [AssemblyInfo.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Properties/AssemblyInfo.cs) | [Resources.resx](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Properties/Resources.resx) |

---

## 📂 3. Árvore de Diretórios e Detalhamento dos Arquivos

```
VideoDownloader/
├── LICENSE.md                               # Licença de uso (MIT + Commons Clause)
├── README.md                                # Documentação e instruções de compilação
├── ESTRUTURA.md                             # (Este arquivo) Mapa arquitetural de modificação
├── doc/
│   └── img/                                 # Screenshots da aplicação (telainicial.png, telavideos.png, telaplaylists.png)
└── src/
    ├── VideoDownloader.sln                  # Arquivo de solução Visual Studio
    └── VideoDownloader/
        ├── App.config                       # Configurações de runtime .NET e binding redirects
        ├── packages.config                  # Definições de pacotes NuGet
        ├── VideoDownloader.csproj           # Arquivo de projeto MSBuild C#
        │
        ├── Program.cs                       # Ponto de entrada (Main) da aplicação WinForms
        ├── IDownloader.cs                   # Interface comum para Downloaders (Video e Audio)
        ├── Logger.cs                        # Adaptador para IProgress<int> e Label de status
        ├── FileHelper.cs                    # Utilitário centralizado de caminhos, sanitização de nomes e pastas
        ├── FFmpegHelper.cs                  # Utilitário: detecção no PATH/disco, download automático e multiplexação FFmpeg
        ├── ThumbnailHelper.cs               # Utilitário: carregamento assíncrono e seguro de capas GDI+ com placeholders
        ├── YoutubeHelper.cs                 # Gerenciamento de instância única/compartilhada do YoutubeClient
        ├── YoutubeErrorHelper.cs            # Tratamento centralizado e formatação amigável de erros (403, URLs inválidas)
        │
        ├── Video.cs                         # Regra de negócio: Download de vídeos avulsos e multiplexação FFmpeg
        ├── VideoDetails.cs                  # Modelo de dados: metadados do vídeo, thumbnail, duração e peso por qualidade
        ├── Audio.cs                         # Regra de negócio: Download e conversão de áudio MP3 avulso
        ├── Playlist.cs                      # Regra de negócio: Download em lote de vídeos e áudios de playlists
        │
        ├── TelaPrincipal.cs                 # Form inicial: Seleção entre Vídeos e Playlists
        ├── TelaPrincipal.Designer.cs        # Código gerado pelo WinForms Designer para TelaPrincipal
        ├── TelaPrincipal.resx               # Recursos da TelaPrincipal
        │
        ├── TelaDownloadFFmpeg.cs            # Form modal de progresso para o download automático do FFmpeg
        ├── TelaDownloadFFmpeg.Designer.cs   # Layout e componentes de TelaDownloadFFmpeg
        ├── TelaDownloadFFmpeg.resx          # Recursos de TelaDownloadFFmpeg
        │
        ├── TelaVideo.cs                     # Dashboard interativo integrado para download de vídeo/áudio avulso com thumbnail
        ├── TelaVideo.Designer.cs            # Layout e card de preview com thumbnail, canal, duração e pesos
        ├── TelaVideo.resx                   # Recursos da TelaVideo
        │
        ├── TelaPlaylist.cs                  # Dashboard interativo integrado para playlists com capa, total de vídeos e resoluções
        ├── TelaPlaylist.Designer.cs         # Layout e card de preview da playlist
        ├── TelaPlaylist.resx                # Recursos da TelaPlaylist
        │
        ├── TelaQualidade.cs                 # Form modal/dialog para seleção da resolução do vídeo
        ├── TelaQualidade.Designer.cs        # Componentes do seletor de qualidade (ComboBox, Botão)
        ├── TelaQualidade.resx               # Recursos da TelaQualidade
        │
        └── Properties/
            ├── AssemblyInfo.cs              # Metadados do executável (versão, copyright, GUID)
            ├── Resources.Designer.cs        # Recursos fortemente tipados
            ├── Resources.resx               # Recursos globais
            ├── Settings.Designer.cs         # Configurações fortemente tipadas
            └── Settings.settings            # Configurações da aplicação
```

---

## 🔍 4. Detalhamento dos Componentes Principais

### 🔹 [Program.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Program.cs)
- **Função:** Inicia o `Application.Run(new TelaPrincipal())`.
- **Quando mexer:** Inicialização global de temas, manipulação de exceções não tratadas (`Application.ThreadException`, `AppDomain.CurrentDomain.UnhandledException`).

### 🔹 [FileHelper.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/FileHelper.cs)
- **Responsabilidade:** Centraliza regras de arquivos, sanitização de caracteres inválidos (`SanitizarNomeArquivo`), detecção de colisão de nomes com contadores incrementais `(1), (2)` (`ObterCaminhoDisponivel`) e garantia/criação das pastas de saída `[Minhas Vídeos]\Baixadas` e `[Minha Música]\Baixadas`.
- **Quando mexer:** Modificar pastas padrão de download ou regras de caracteres especiais.

### 🔹 [FFmpegHelper.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/FFmpegHelper.cs)
- **Responsabilidade:** Detecção dinâmica do FFmpeg (no diretório local e no PATH do sistema), download automático resiliente da versão mais recente via GitHub Releases, extração do ZIP e orquestração assíncrona de comandos sem deadlocks de pipe buffer:
  - `MesclarAudioVideoAsync(videoTemp, audioTemp, destinoMp4)`: multiplexa streams de áudio e vídeo com `-c:v copy -c:a aac -b:a 192k`.
  - `ConverterParaMp3Async(tempAudio, destinoMp3)`: converte o fluxo para MP3 a 320 kbps com `-c:a libmp3lame -b:a 320k`.
- **Quando mexer:** Atualizar parâmetros de codificação de áudio/vídeo ou fontes de download do FFmpeg.

### 🔹 [ThumbnailHelper.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/ThumbnailHelper.cs)
- **Responsabilidade:** Download seguro de thumbnails com fallback em cascata para endpoints canônicos JPEG do YouTube (`maxresdefault`, `sddefault`, `hqdefault`, `mqdefault`), cópia desvinculada de stream para prevenir falhas de renderização no Windows GDI+ e geração de placeholders dinâmicos.
- **Quando mexer:** Ajustar tamanhos, dimensões ou estilos dos placeholders de thumbnail.

### 🔹 [YoutubeHelper.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/YoutubeHelper.cs) e [YoutubeErrorHelper.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/YoutubeErrorHelper.cs)
- **Responsabilidade:** `YoutubeHelper` fornece a instância thread-safe única `Client` para reutilização do pool de conexões HTTP do .NET. `YoutubeErrorHelper` centraliza a formatação amigável de erros (Erro 403 / Forbidden, restrições regionais/idade e links inválidos).

### 🔹 [Logger.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Logger.cs)
- **Responsabilidade:** Atualizar a barra de progresso (`ProgressBar` via `IProgress<int>`) e o texto descritivo do status (`Label`) de forma unificada e thread-safe.

### 🔹 [Video.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Video.cs)
- **Responsabilidade:** Download de vídeos avulsos, descoberta de qualidades com cálculo de peso estimado (`ObterDetalhesAsync`) e mesclagem assíncrona via FFmpeg com o título original sanitizado.

### 🔹 [Audio.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Audio.cs)
- **Responsabilidade:** Download do stream nativo de áudio do YouTube e conversão direta para MP3 a 320 kbps com o título original sanitizado.

### 🔹 [Playlist.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Playlist.cs)
- **Responsabilidade:** Obtenção de metadados da playlist (`ObterDetalhesPlaylistAsync`) e download em lote resiliente de vídeos ou áudios MP3 para subpastas dedicadas dentro de `Baixadas`.

### 🔹 [TelaPrincipal.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPrincipal.cs)
- **Responsabilidade:** Menu inicial com verificação automática da disponibilidade do FFmpeg e navegação para download de vídeos avulsos ou playlists.

### 🔹 [TelaVideo.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaVideo.cs) e [TelaPlaylist.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPlaylist.cs)
- **Responsabilidade:** Dashboards interativos Single-Screen com preview imediato de thumbnail, título, canal, duração, seleção de formato e resolução com pesos estimados, atalho com Enter e botões de atalho para as pastas de saída.

---

## ⚙️ 5. Boas Práticas e Recomendações Técnicas

1. **Garantia do FFmpeg:**
   - O aplicativo gerencia automaticamente a presença do FFmpeg via `FFmpegHelper.GarantirFFmpeg()`. Caso não esteja instalado, a modal `TelaDownloadFFmpeg` faz o download sem intervenção manual do usuário.
2. **Execução de Processos Externos:**
   - Sempre utilize `FFmpegHelper.ExecutarFFmpegAsync` para novos comandos do FFmpeg a fim de garantir que `StandardError` seja lido continuamente, prevenindo deadlocks de buffer de pipes no Windows.
3. **Reúso de Streams e Cache:**
   - As classes `Video` e `Playlist` possuem campos de cache (`_cachedVideo`, `_cachedManifest`, `_cachedPlaylist`). Ao analisar o link, os dados ficam em memória e o clique posterior em "Baixar" inicia o download instantaneamente sem refazer as consultas de rede.
