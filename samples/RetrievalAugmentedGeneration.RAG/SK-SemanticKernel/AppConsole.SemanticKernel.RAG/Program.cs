using Microsoft.KernelMemory;
using Microsoft.KernelMemory.AI.Ollama;
using Microsoft.SemanticKernel;

string ollamaEndpoint = "http://localhost:11434";
string modelIdEmbeddings = "all-minilm";

// Create a kernel with Azure OpenAI chat completion

(string model, string description)[] model_id_chats = 
                                                        [
                                                            (
                                                                model: "phi",
                                                                description: "non-reasoning"
                                                            ),
                                                            (
                                                                model: "deepseek-r1",
                                                                description: "reasoning"
                                                            )
                                                        ];

// questions
string[] questions = new []
                        {
                            "What is Bruno's favourite super hero?",
                            "who watched venom 3?"
                        };

List<string> information_list_rag = new()
                                    {
                                        "Gisela's favourite super hero is Batman",
                                        "The last super hero movie watched by Gisela was Venom 3",
                                        "Bruno's favourite super hero is Invincible",
                                        "The last super hero movie watched by Bruno was Venom 3",
                                        "Bruno don't like the super hero movie: Eternals"
                                    };


foreach (var model_id_chat in model_id_chats)
{
    // Create a kernel with Azure OpenAI chat completion
    IKernelBuilder builder = Kernel
                                .CreateBuilder()
                                // Type is for evaluation purposes only and is subject to change or removal in future updates. 
                                #pragma warning disable SKEXP0070, SKEXP0003, SKEXP0001, SKEXP0011, SKEXP0052, SKEXP0055, SKEXP0050
                                .AddOllamaChatCompletion
                                (
                                    modelId: model_id_chat.model,
                                    endpoint: new Uri(ollamaEndpoint)
                                )
                                #pragma warning restore SKEXP0070
                                ;

    // Create the kernel
    Kernel kernel = builder.Build();

    OllamaConfig ollama_config = new ()
                                    {
                                        Endpoint = ollamaEndpoint,
                                        TextModel = new OllamaModelConfig(model_id_chat.model),
                                        EmbeddingModel = new OllamaModelConfig
                                                                (
                                                                    modelIdEmbeddings,
                                                                    2048
                                                                )
                                    };
    IKernelMemory memory = new KernelMemoryBuilder()
                                .WithOllamaTextGeneration(ollama_config)
                                .WithOllamaTextEmbeddingGeneration(ollama_config)
                                .Build()
                                ;

    // intro
    SpectreConsoleOutput.DisplayTitle(model_id_chat.model + Environment.NewLine + model_id_chat.description);

    SpectreConsoleOutput.DisplayTitleH2($"This program will answer the following question:");

    foreach (string question in questions)
    {
        AskWithoutRAGAsync
                    (
                        question,
                        model_id_chat,
                        kernel
                    );

        // separator
        Console.WriteLine("");
        SpectreConsoleOutput.DisplaySeparator();

        await AskWithRAGAsync
                    (
                        question,
                        information_list_rag,
                        model_id_chat,
                        memory
                    );
    }
}

static async
    Task
                                        AskWithoutRAGAsync
                                        (
                                            string question,
                                            (string model, string description) model_id_chat,
                                            Kernel kernel
                                        )
{
    SpectreConsoleOutput.DisplayTitleH2($"{model_id_chat} response (no memory / no RAG).");
    Console.WriteLine("");
    SpectreConsoleOutput.WriteGreen("Q:");
    SpectreConsoleOutput.WriteGreen($"{question}");
    Console.WriteLine("");
    SpectreConsoleOutput.WriteGreen("A:");

    IAsyncEnumerable<object?> response = kernel.InvokePromptStreamingAsync(question);

    await foreach (object? result in response)
    {
        SpectreConsoleOutput.WriteGreen(result.ToString());
    }

    return;
}

static async
    Task
                                        AskWithRAGAsync
                                        (
                                            string question,
                                            (string model, string description) model_id_chat,
                                            IKernelMemory memory,
                                            List<string> information_list_rag
                                        )
{
    SpectreConsoleOutput.DisplayTitleH2($"{model_id_chat} response (using semantic memory / RAG).");
    SpectreConsoleOutput.WriteGreen("Q:");
    SpectreConsoleOutput.WriteGreen($"{question}");
    Console.WriteLine("");
    SpectreConsoleOutput.WriteGreen("A:");
    Console.WriteLine("");

    SpectreConsoleOutput.DisplayTitleH2($"Information List");
    Console.WriteLine("");

    // Add information to the memory
    foreach (string info in information_list_rag)
    {
        SpectreConsoleOutput.WriteGreen($"{info}");
        Console.WriteLine("");
        await memory.ImportTextAsync(info);
    }
    IAsyncEnumerable<object?> answer = memory.AskStreamingAsync(question);
    await foreach (object? result in answer)
    {
        SpectreConsoleOutput.WriteGreen(result.ToString());
    }
}
