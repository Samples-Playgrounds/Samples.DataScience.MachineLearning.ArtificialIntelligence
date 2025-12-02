using System;
using System.Diagnostics;
using System.IO;

/*
    https://medium.com/@jepozdemir/how-to-launch-and-control-processes-in-c-net-applications-4ae6565410d6
    https://www.codeproject.com/Articles/4665/Launching-a-process-and-displaying-its-standard-ou
    
    $HOME/.ollama/models
    $HOME/Library/Application\ Support/Jan/data/models/
 */
// case sensitive MBP
string provider = "ollama";
string model = "gpt-oss:120b";
System.Diagnostics.ProcessStartInfo psi = new ()
                                                {
                                                    FileName = provider,
                                                    Arguments = $"run {model}",
                                                    RedirectStandardInput = true,
                                                    RedirectStandardOutput = true,
                                                    RedirectStandardError = true,
                                                    UseShellExecute = false,
                                                    CreateNoWindow = true,
                                                    
                                                };

System.Diagnostics.Process p = new ()
                                    {
                                        StartInfo = psi
                                    };
p.Start();
p.BeginOutputReadLine();
p.OutputDataReceived += (s, e) =>
{
    if (!string.IsNullOrEmpty(e.Data))
    {
        Console.WriteLine($"[{provider}-{model}]: {e.Data}");
    }
};

Console.WriteLine("Interactive Shell (type 'exit' to quit)");
string[] terminators = new string[] { "exit", "quit", "q" };

Input(p);

    
p.WaitForExit();

return;

void Input(Process process)
{
    using (StreamWriter writer = process.StandardInput)
    {
        if (writer.BaseStream.CanWrite)
        {
            string input = "";
            bool loop = true;
            // !Array.Exists(terminators, element => element == input.Trim().ToLower())
            while(loop)
            {
                Console.Write($"[user]:");
                input = Console.ReadLine();
                writer.WriteLine(input);
                writer.Flush();
                writer.Close();
            }
        }
    }
}
