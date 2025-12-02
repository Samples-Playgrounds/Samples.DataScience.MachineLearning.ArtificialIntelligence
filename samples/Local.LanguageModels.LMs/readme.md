#

|                      |                                          |  Model Type      |                     |
| -------------------- |----------------------------------------- | ---------------- | ------------------- |
| llama.cpp            |                                          | GGUF             |                     |
|                      |                                          |                  | llama-cli           |
|                      |                                          |                  | llama-run           |
|                      |                                          |                  | llama-server        |
|                      |                                          |                  | llama-simple        |
|                      |                                          |                  | llama-simple-chat   |
| llamafile            |                                          |                  |                     |
|                      |                                          |                  | llamafile           |
| ollama               | $HOME/.ollama/models/                    |                  |                     |
| Microsoft.foundry    | $HOME/.foundry/cache/models/             | ONNX             |                     |
| LM Studio (lms)      | $HOME/.lmstudio/models/                  | GGUF, MLX        |                     |
|                      |                                          |                  |                     |


| Host AKA Provider    |  Nuget             | Links                                  |
| -------------------- | ------------------ | -------------------------------------- |
| llama.cpp            | LLamaSharp         | [LLamaSharp][11]                       |
| llamafile            |                    |                                        |
| ollama               |                    |                                        |
| Microsoft.foundry    |                    |                                        |
| LM Studio (lms)      | AutoGen.LMStudio   | [AutoGen] [21][22]                     |
|                      | OpenAI             | [OpenAi]  [31]                         |

*   [11] https://github.com/SciSharp/LLamaSharp

*   [21] https://microsoft.github.io/autogen-for-net/articles/Consume-LLM-server-from-LM-Studio.html

*   [22] https://microsoft.github.io/autogen-for-net/api/AutoGen.LMStudio.LMStudioAgent.html

*   [31] https://www.reddit.com/r/devsarg/comments/1irarsq/tutorial_accediendo_a_un_servidor_compatible_con/?tl=en




```
$HOME/.ollama/models/manifests/registry.ollama.ai/library/
```


```shell
llamafile [--chat] [flags...] -m model.gguf
llamafile [--server] [flags...] -m model.gguf [--mmproj vision.gguf]
llamafile [--cli] [flags...] -m model.gguf -p prompt
llamafile [--cli] [flags...] -m model.gguf --mmproj vision.gguf --image
```

```shell
$HOME/.foundry/cache/models/foundry.modelinfo.json
```

## `Process`

```csharp
System.Diagnostic.Process[] 
                processes_ollama = System.Diagnostic.Process.
                                                    GetProcessesByName("ollama");
System.Diagnostic.Process[] 
                processes_ollama = System.Diagnostic.Process.
                                                    GetProcessesByName("lms");
System.Diagnostic.Process[] 
                processes_llama_cpp = System.Diagnostic.Process.
                                                    /*
                                                    brew install llama.cpp

                                                    https://formulae.brew.sh/formula/llama.cpp

                                                    */
                                                    GetProcessesByName("llama.cpp");
System.Diagnostic.Process[] 
                processes_llama_cpp = System.Diagnostic.Process.
                                                    /*
                                                    https://github.com/Mozilla-Ocho/llamafile
                                                    */
                                                    GetProcessesByName("llamafile");


```

*   https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.getprocesses

