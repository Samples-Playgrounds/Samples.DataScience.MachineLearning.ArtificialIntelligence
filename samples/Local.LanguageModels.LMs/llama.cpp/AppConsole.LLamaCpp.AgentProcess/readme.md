


```csharp
string provider = "llama-run";
string models_root = Environment.GetEnvironmentVariable("HOME") + "/.lmstudio/models/";
string model = "lmstudio-community/DeepSeek-R1-0528-Qwen3-8B-GGUF";
string file = System.IO.Directory.GetFiles
                                    (
                                    $"{models_root}/{model}", 
                            "*.gguf",
                                        SearchOption.AllDirectories
                                    )
                                    [0];
System.Diagnostics.ProcessStartInfo psi = new ()
                                                {
                                                    FileName = provider,
                                                    Arguments = file,
                                                    RedirectStandardInput = true,
                                                    RedirectStandardOutput = true,
                                                    RedirectStandardError = true,
                                                    UseShellExecute = false,
                                                    CreateNoWindow = true,
                                                    WorkingDirectory = "."
                                                };

System.Diagnostics.Process p = new ()
                                    {
                                        StartInfo = psi,
                                        EnableRaisingEvents = true
                                    };

Cysharp.Text.Utf16ValueStringBuilder sb_output = Cysharp.Text.ZString.CreateStringBuilder();
Cysharp.Text.Utf16ValueStringBuilder sb_error = Cysharp.Text.ZString.CreateStringBuilder();
bool output_ready = false;
string input = "";
string output = "";

p.OutputDataReceived += (s, e) =>
{
    sb_output.Append(e.Data);
    
    output_ready = true;
    
    return;
};
p.ErrorDataReceived += (s, e) =>
{
    sb_error.Append(e.Data);
    
    return;
};

p.Start();
p.BeginOutputReadLine();
p.BeginErrorReadLine();

Console.WriteLine("Interactive Shell (type 'exit' to quit)");
string[] terminators = [ "exit", "quit", "q" ];

bool loop = true;

while (loop)
{
    Input();
    Output();
}

p.WaitForExit();

return;

void Output()
{
    if (sb_output.Length == 0)
    {
        return;
    }

    if (output_ready == true && sb_output.Length > 0)
    {
        output = sb_output.ToString();
        Console.WriteLine($"[{provider}{model}] : {output}");
        sb_output.Clear();
    }
    
    return;
}

void Input()
{
    if (output_ready == true )
    {
        return;
    }
    
    using (StreamWriter writer = p.StandardInput)
    {
        Console.Write($"[user] : ");
        input = Console.ReadLine();

        if (writer.BaseStream.CanWrite)
        {
            // !Array.Exists(terminators, element => element == input.Trim().ToLower())
            writer.WriteLine(input);
            writer.Flush();
            Console.WriteLine($" sent... ''{input}''");
        }
    }
    
    return;
}
```