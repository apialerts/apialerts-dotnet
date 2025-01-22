namespace APIAlerts.util;

internal class Logger
{
    private bool _debug;

    internal void Configure(bool debug = false)
    {
        _debug = debug;
    }

    internal virtual void Success(string message)
    {
        Print($"✓ (apialerts.com) {message}");
    }

    internal virtual void Warning(string message)
    {
        Print($"! (apialerts.com) Warning: {message}");
    }

    internal virtual void Error(string message)
    {
        Print($"x (apialerts.com) Error: {message}");
    }

    private void Print(string message)
    {
        if (_debug)
        {
            Console.WriteLine(message);
        }
    }
}