namespace ApiAlerts.Common.util;

internal class Logger
{
    private bool _debug;
    
    internal void Configure(bool debug = false)
    {
        _debug = debug;
    }
        
    internal void Success(string message)
    {
        Print($"✓ (apialerts.com) {message}");
    }
        
    internal void Warning(string message)
    {
        Print($"! (apialerts.com) Warning: {message}");
    }
        
    internal void Error(string message)
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