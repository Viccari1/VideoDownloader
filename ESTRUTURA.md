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
| **Modificar a lógica de obtenção/seleção de qualidade de vídeo** | [Video.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Video.cs)<br>[TelaQualidade.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaQualidade.cs)<br>[Global.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Global.cs) | [TelaVideo.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaVideo.cs) |
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
        ├── Global.cs                        # Estado estático compartilhado (qualidades de vídeo)
        ├── Logger.cs                        # Adaptador para IProgress<int> e Label de status
        ├── FFmpegHelper.cs                  # Utilitário: detecção no PATH/disco e download automático do FFmpeg
        │
        ├── Video.cs                         # Regra de negócio: Download de vídeos avulsos e multiplexação FFmpeg
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
        ├── TelaVideo.cs                     # Form para download de vídeo/áudio avulso
        ├── TelaVideo.Designer.cs            # Layout e componentes visuais de TelaVideo
        ├── TelaVideo.resx                   # Recursos da TelaVideo
        │
        ├── TelaPlaylist.cs                  # Form para download de playlists completas
        ├── TelaPlaylist.Designer.cs         # Layout e componentes visuais de TelaPlaylist
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
- **Quando mexer:** Inicialização global de temas, manipulação de exceções não tratadas (`Application.ThreadException`, `AppDomain.CurrentDomain.UnhandledException`) ou splash screens.

### 🔹 [Global.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Global.cs)
- **Campos:**
  - `List<string> qualidades`: Armazena os rótulos de resolução descobertos do vídeo (ex: "1080p", "720p", "480p").
  - `string qualidadeDefinida`: Armazena a resolução escolhida na `TelaQualidade`.
- **Quando mexer:** Se for substituir o padrão de variáveis estáticas por injeção ou parâmetros de construtor na abertura do formulário.

### 🔹 [Logger.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Logger.cs)
- **Responsabilidade:** Atualizar a barra de progresso (`ProgressBar` via `IProgress<int>`) e o texto descritivo do status (`Label`).
- **Métodos:**
  - `Log(int value, string text)`: Atualiza porcentagem e mensagem simultaneamente.
- **Quando mexer:** Para formatar porcentagens, adicionar logs em arquivo, ou desacoplar o controle do Windows Forms.

### 🔹 [Video.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Video.cs)
- **Responsabilidade:** Orquestra a extração e mesclagem de streams para vídeos avulsos.
- **Métodos principais:**
  - `VerifyVideoOK()`: Verifica se a URL do vídeo é válida via `youtube.Videos.GetAsync`.
  - `ExistFile()`: Garante nome único para o arquivo gerado dentro de `[Minhas Vídeos]\Baixadas` no formato `arquivo{N}.mp4`.
  - `GetStreams(StreamManifest manifest)`: Seleciona o melhor áudio e o stream de vídeo correspondente à qualidade selecionada.
  - `GetQuality()`: Retorna lista de resoluções disponíveis para o vídeo.
  - `Download(Logger logger)`: Faz o download de ambos os streams temporários (`Path.GetTempPath()`) e invoca `ffmpeg.exe` via `ProcessStartInfo` com argumentos `-i "videoPath" -i "audioPath" -c:v copy -c:a aac "arquivo"`. Limpa os temporários ao final.
- **Quando mexer:** Ajuste nos comandos FFmpeg, mudança no nome padrão salvo (usar o título real do vídeo em vez de "arquivo"), escolha de codecs ou formatos de contêiner (ex: MKV, WebM).

### 🔹 [Audio.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Audio.cs)
- **Responsabilidade:** Download e conversão para MP3 de áudio avulso.
- **Métodos principais:**
  - `VerifyVideoOK()`: Checagem de integridade da URL do vídeo.
  - `ExistFile()`: Gera caminho único em `[Minha Música]\Baixadas\arquivo{N}.mp3`.
  - `GetStreamAudio(StreamManifest manifest)`: Identifica melhor stream de áudio por bitrate.
  - `Download(Logger logger)`: Utiliza o utilitário nativo do `YoutubeExplode.Converter` (`youtube.Videos.DownloadAsync(url, arquivo, o => o.SetContainer("mp3").SetPreset(ConversionPreset.Fast).SetFFmpegPath(ffmpegPath))`).
- **Quando mexer:** Alterar formato (FLAC, WAV, M4A), qualidade do preset de conversão, ou salvar com o nome do vídeo original.

### 🔹 [Playlist.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Playlist.cs)
- **Responsabilidade:** Gerenciamento do download de playlists inteiras.
- **Métodos principais:**
  - `VerifyPlaylistOK()`: Valida existência da playlist.
  - `CreateDirVideo(string dir)` / `CreateDirAudio(string dir)`: Cria diretório sanitizado com o nome da playlist.
  - `RemoveInvalidChars(string nome)`: Substitui caracteres ilegais para nomes de arquivo do Windows por `_`.
  - `DownloadVideos(Logger logger)`: Itera sobre todos os vídeos da playlist, baixa vídeo + áudio e mescla usando FFmpeg.
  - `DownloadAudios(Logger logger)`: Itera sobre todos os vídeos e faz download/conversão para MP3.
- **Quando mexer:** Tratamento de vídeos indisponíveis/privados no meio da playlist, paralelização de downloads, ou customização de nomes de arquivo.

### 🔹 [TelaPrincipal.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPrincipal.cs)
- **Responsabilidade:** Roteador da aplicação. Oculta a si mesma e abre `TelaVideo` ou `TelaPlaylist`. Ao fechar os formulários filhos, reaparece via evento `TelaFormClosed`.

### 🔹 [TelaVideo.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaVideo.cs)
- **Controles principais:** `text_link`, `rb_video`, `rb_audio`, `bar_progresso`, `label_status`, `btn_baixar`, `btn_voltar`, `btn_pasta_video`, `btn_pasta_audio`.
- **Eventos:**
  - `btn_baixar_Click`: Trava interface (`emUso = true`), extrai qualidades se for vídeo, abre modal `TelaQualidade`, instancia `Video` ou `Audio` e inicia o download assíncrono. Ao terminar, pergunta ao usuário via `MessageBox` se deseja abrir a pasta e seleciona o arquivo via `explorer.exe /select`.
  - `btn_pasta_video_Click` / `btn_pasta_audio_Click`: Abre as respectivas pastas no Windows Explorer.

### 🔹 [TelaPlaylist.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaPlaylist.cs)
- **Controles principais:** Semelhante a `TelaVideo`, mas possui `panel3` com `rb_alta` para alternar entre qualidade alta ou baixa (360p) nas playlists.
- **Eventos:**
  - `btn_baixar_Click`: Executa `downloader.DownloadVideos(logger)` ou `downloader.DownloadAudios(logger)`.

### 🔹 [TelaQualidade.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/TelaQualidade.cs)
- **Controles:** `comboBox1` e `button1`.
- **Eventos:** Preenche a ComboBox a partir de `Global.qualidades` e salva a opção selecionada em `Global.qualidadeDefinida`.

---

## ⚠️ 5. Pontos Críticos e Cuidados Técnicos

1. **Dependência do FFmpeg:**
   - O código busca `ffmpeg.exe` através de:
     ```csharp
     Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe")
     ```
   - O executável **deve** estar copiado no diretório de compilação de saída (`bin/Debug/` ou `bin/Release/`). Sem ele, o download de vídeo e a conversão de áudio falham.

2. **Validações Assíncronas sem Await:**
   - Em `Video.cs`, `Audio.cs` e `Playlist.cs`, o método `TryFindVideo()` / `TryFindPlaylist()` chama `youtube.Videos.GetAsync(url)` sem `await` e sem `.GetAwaiter().GetResult()`.
   - *Atenção ao alterar:* Se for refatorar validação, torne esses métodos explicitamente assíncronos (`async Task<bool>`) para evitar possíveis race conditions ou exceções não capturadas.

3. **Nomenclatura Padrão de Arquivos:**
   - Atualmente, `Video.cs` e `Audio.cs` salvam arquivos usando o prefixo estático `"arquivo"` (`arquivo.mp4`, `arquivo1.mp4`), ignorando o título real do vídeo do YouTube. Caso seja solicitado salvar com o nome do vídeo, altere o método `ExistFile()` em [Video.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Video.cs) e [Audio.cs](file:///E:/Arquivos/Projetos/VideoDownloader/src/VideoDownloader/Audio.cs). Em `Playlist.cs`, o título do vídeo já é utilizado.

4. **Variáveis Estáticas Globais:**
   - A comunicação de resolução entre `TelaVideo` e `TelaQualidade` depende de `Global.qualidades` e `Global.qualidadeDefinida`. Cuidado com concorrência ou valores residuais entre downloads subsequentes.
