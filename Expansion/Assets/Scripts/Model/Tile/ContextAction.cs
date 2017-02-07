using System;

public class ContextAction
{
    public string DisplayText { get; set; }
    public Action TheAction { get; set; }

    public ContextAction(string text, Action action)
    {
        DisplayText = text;
        TheAction = action;
    }
}