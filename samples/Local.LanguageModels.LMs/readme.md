#


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

