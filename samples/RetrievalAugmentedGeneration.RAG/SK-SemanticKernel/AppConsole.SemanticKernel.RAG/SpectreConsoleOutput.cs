using Spectre.Console;

public static class
                                        SpectreConsoleOutput
{
    public static
        void
                                        DisplayTitle
                                        (
                                            string modelId = ""
                                        )
    {
        string title = $"{modelId} RAG";

        AnsiConsole.Write(new FigletText(title).Centered().Color(Color.Purple));

        return;
    }

    public static
        void
                                        DisplayTitleH2
                                        (
                                            string subtitle
                                        )
    {
        AnsiConsole.MarkupLine($"[bold][blue]=== {subtitle} ===[/][/]");
        AnsiConsole.MarkupLine($"");

        return;
    }

    public static
        void
                                        DisplayTitleH3
                                        (
                                            string subtitle
                                        )
    {
        AnsiConsole.MarkupLine($"[bold]>> {subtitle}[/]");
        AnsiConsole.MarkupLine($"");

        return;
    }

    public static
        void
                                        DisplaySeparator
                                        (
                                        )
    {
        AnsiConsole.MarkupLine($"");
        AnsiConsole.MarkupLine($"[bold][blue]==============[/][/]");
        AnsiConsole.MarkupLine($"");

        return;
    }

    public static
        void
                                        WriteGreen
                                        (
                                            string message
                                        )
    {
        try
        {
            AnsiConsole.Markup($"[green]{message}[/]");
        }
        catch
        {
            AnsiConsole.Write($"{message}");
        }

        return;
    }

    public static
        void
                                        DisplayQuestion
                                        (
                                            string question
                                        )
    {
        AnsiConsole.MarkupLine($"[bold][blue]>>Q: {question}[/][/]");
        AnsiConsole.MarkupLine($"");

        return;
    }
    public static
        void
                                        DisplayAnswerStart
                                        (
                                            string answerPrefix
                                        )
    {
        AnsiConsole.Markup($"[bold][blue]>> {answerPrefix}:[/][/]");

        return;
    }

    public static
        void
                                        DisplayFilePath
                                        (
                                            string prefix,
                                            string filePath
                                        )
    {
        TextPath path = new TextPath(filePath);
        AnsiConsole.Markup($"[bold][blue]>> {prefix}: [/][/]");
        AnsiConsole.Write(path);
        AnsiConsole.MarkupLine($"");

        return;
    }

    public static
        void
                                        DisplaySubtitle
                                        (
                                            string prefix,
                                            string content
                                        )
    {
        AnsiConsole.Markup($"[bold][blue]>> {prefix}: [/][/]");
        AnsiConsole.WriteLine(content);
        AnsiConsole.MarkupLine($"");
        return;
    }

    public static
        int
                                        AskForNumber
                                        (
                                            string question
                                        )
    {
        int number = AnsiConsole.Ask<int>(@$"[green]{question}[/]");
        return number;
    }

    public static
        string
                                        AskForString
                                        (
                                            string question
                                        )
    {
        string response = AnsiConsole.Ask<string>(@$"[green]{question}[/]");
        return response;
    }

    public static
        List<string>
                                        SelectScenarios
                                        (
                                        )
    {
        List<string> scenarios = AnsiConsole.Prompt
            (
            new MultiSelectionPrompt<string>()
                .Title("Select the [green]Phi 3 Vision scenarios[/] to run?")
                .PageSize(10)
                .Required(true)
                .MoreChoicesText("[grey](Move up and down to reveal more scenarios)[/]")
                .InstructionsText
                    (
                    "[grey](Press [blue]<space>[/] to toggle a scenario, "
                    +
                    "[green]<enter>[/] to accept)[/]"
                    )
                .AddChoiceGroup
                    (
                        "Select an image to be analuyzed",
                        new[]
                        {
                            "foggyday.png",
                            "foggydaysmall.png",
                            "petsmusic.png",
                            "ultrarunningmug.png",
                        }
                    )
                .AddChoices
                    (
                        new[]
                        {
                            "Type the image path to be analyzed",
                            "Type a question",
                        }
                    )
                );
        return scenarios;
    }
}