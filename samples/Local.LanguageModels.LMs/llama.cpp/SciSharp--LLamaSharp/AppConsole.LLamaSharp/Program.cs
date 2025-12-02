
using LLama;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string model_path = $"{home}/Downloads/holisticware/gen-ai/models/phi-2.Q5_K_S.gguf";

LLama.Common.ModelParams model_parameters = new (model_path)
                                            {
                                                // max size of context/memory for single pass
                                                // max number of context from chat history
                                                ContextSize = 4096,
                                                // number of model layers to be offloaded/processed on GPU
                                                GpuLayerCount = 5,
                                            };

using LLama.LLamaWeights model = LLama.LLamaWeights.LoadFromFile(model_parameters);
using LLama.LLamaContext context = model.CreateContext(model_parameters);
LLama.InteractiveExecutor executor = new (context);

LLama.Common.ChatHistory chat_history = new ();
// Add a system message to set the context for the conversation
chat_history.AddMessage
                    (
                        LLama.Common.AuthorRole.System,
                        """
                        Act as a helpful AI scientific and coding assistant.
                        Instructions/input and responses/output are in markdown format.
                        Assistant is an intelligent chatbot designed to help users answer technical questions about 
                            *   .NET
                            *   C#
                            *   MAUI
                            *   Blazor
                        Assistant answers questions using the context below and if it is not sure of an answer, the output
                        can be:
                        
                            "Not familiar with this topic!!"
                        """
                    );


ChatSession session = new(executor, chat_history);
LLama.Common.InferenceParams inferenceParams = new ()
                                                {
                                                    // No more than 256 tokens should appear in answer.
                                                    // Remove it if antiprompt is enough for control.
                                                    MaxTokens = 256,
                                                    // Stop generation once antiprompts appear.
                                                    AntiPrompts = new List<string>
                                                                        {
                                                                            "User:"
                                                                        },
                                                    SamplingPipeline = new LLama.Sampling.DefaultSamplingPipeline(),
                                                };

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write
            (
                """
                The chat session has started.

                User: 
                """
            );
Console.ForegroundColor = ConsoleColor.Green;
string userInput = Console.ReadLine() ?? "";

while (userInput != "exit")
{
    await foreach
        (
            // Generate the response streamingly.
            string text in session.ChatAsync
                                    (
                                        new LLama.Common.ChatHistory.Message
                                                                       (
                                                                           LLama.Common.AuthorRole.User,
                                                                           userInput
                                                                        ),
                       inferenceParams))
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(text);
    }
    Console.ForegroundColor = ConsoleColor.Green;
    userInput = Console.ReadLine() ?? "";
}
