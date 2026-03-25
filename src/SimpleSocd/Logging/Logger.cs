using System.Runtime.CompilerServices;
using System.Threading;
using BepInEx.Logging;

namespace OneLetterShor.SimpleSocd.Logging;

public static class Logger
{
    public static LogLevel EnabledLogLevels { get; set; } = LogLevel.All;
    private static int _markIndex = 0;
    
    private static string TrimPath(string path)
    {
        string[] elements = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        int projectDirIndex = Array.IndexOf(elements, "src") + 1;
        return String.Join(Path.AltDirectorySeparatorChar.ToString(), elements.ToArray().Skip(projectDirIndex + 1).ToArray());
    }
    
    public static void Mark(
        LogLevel logLevel=LogLevel.Debug,
        [CallerMemberName] string callerMemberName="",
        [CallerLineNumber] int callerLineNumber=0,
        [CallerFilePath] string callerFilePath="")
    {
        Log(logLevel, $"============================== MARK {Interlocked.Increment(ref _markIndex)} ==============================", callerMemberName, callerLineNumber, callerFilePath);
    }
    
    public static void Fatal(
        object? data,
        [CallerMemberName] string callerMemberName="",
        [CallerLineNumber] int callerLineNumber=0,
        [CallerFilePath] string callerFilePath="")
    {
        Log(LogLevel.Fatal, data, callerMemberName, callerLineNumber, callerFilePath);
    }
    
    public static void Error(
        object? data,
        [CallerMemberName] string callerMemberName="",
        [CallerLineNumber] int callerLineNumber=0,
        [CallerFilePath] string callerFilePath="")
    {
        Log(LogLevel.Error, data, callerMemberName, callerLineNumber, callerFilePath);
    }
    
    public static void Warning(
        object? data,
        [CallerMemberName] string callerMemberName="",
        [CallerLineNumber] int callerLineNumber=0,
        [CallerFilePath] string callerFilePath="")
    {
        Log(LogLevel.Warning, data, callerMemberName, callerLineNumber, callerFilePath);
    }
    
    public static void Message(
        object? data,
        [CallerMemberName] string callerMemberName="",
        [CallerLineNumber] int callerLineNumber=0,
        [CallerFilePath] string callerFilePath="")
    {
        Log(LogLevel.Message, data, callerMemberName, callerLineNumber, callerFilePath);
    }
    
    public static void Info(
        object? data,
        [CallerMemberName] string callerMemberName="",
        [CallerLineNumber] int callerLineNumber=0,
        [CallerFilePath] string callerFilePath="")
    {
        Log(LogLevel.Info, data, callerMemberName, callerLineNumber, callerFilePath);
    }
    
    public static void Debug(
        object? data,
        [CallerMemberName] string callerMemberName="",
        [CallerLineNumber] int callerLineNumber=0,
        [CallerFilePath] string callerFilePath="")
    {
        Log(LogLevel.Debug, data, callerMemberName, callerLineNumber, callerFilePath);
    }
    
    public static void Log(
        LogLevel logLevel,
        object? data,
        [CallerMemberName] string callerMemberName="",
        [CallerLineNumber] int callerLineNumber=0,
        [CallerFilePath] string callerFilePath="")
    {
        if (!EnabledLogLevels.HasFlag(logLevel)) return;
        
        string sourceInfo = $"{TrimPath(callerFilePath)}:{callerLineNumber}, {callerMemberName}()";
        
        Plugin.__Log(logLevel, $"{sourceInfo}: {data}");
    }
}