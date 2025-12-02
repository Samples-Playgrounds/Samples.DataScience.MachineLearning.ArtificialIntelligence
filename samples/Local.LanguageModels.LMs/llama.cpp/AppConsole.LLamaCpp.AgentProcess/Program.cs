using System;
using System.Diagnostics;
using System.IO;
using HolisticWare.AI.GenAI.LanguageModels;

/*
    https://medium.com/@jepozdemir/how-to-launch-and-control-processes-in-c-net-applications-4ae6565410d6
    https://www.codeproject.com/Articles/4665/Launching-a-process-and-displaying-its-standard-ou
    
    $HOME/.ollama/models
    $HOME/Library/Application\ Support/Jan/data/models/
    ~/.cache/huggingface/hub
    ~/.lmstudio/models/
    ~/.lmstudio/hub/
    $HOME/.ollama/models/
    Library/Caches/llama.cpp/ggml-org_gpt-oss-120b-GGUF_gpt-oss-120b-mxfp4-00003-of-00003.gguf
    
    llama-run \
        -m $HOME/.lmstudio/models/lmstudio-community/DeepSeek-R1-0528-Qwen3-8B-GGUF/DeepSeek-R1-0528-Qwen3-8B-Q8_0.gguf
 */
// case sensitive MBP

Dictionary
    <
        string, 
        HolisticWare.AI.GenAI.LanguageModels.ProviderHost
    >
                                        providers_hosts_known 
                                        = new()
                                        {
                                            ["llama-run"] = null,
                                            ["llama-server"]  = null,
                                            ["ollama"] = null,
                                            ["lms"] = null,
                                            ["foundry"] = null,
                                            ["llamafile"] = null,
                                            
                                        };


foreach (KeyValuePair<string, ProviderHost> kvp in providers_hosts_known)
{
    HolisticWare.AI.GenAI.LanguageModels.ProviderHost ph = new();
    ph.TestInstallation(kvp.Key);
    
    providers_hosts_known[kvp.Key] = ph;
}


return;