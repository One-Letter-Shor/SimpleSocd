using System.Runtime.CompilerServices;
using Menu.Remix;

namespace OneLetterShor.SimpleSocd.Utils;

public static class ModFinder
{
    internal static ModManager.Mod Get(
        string guid,
        bool isSilent = false,
        [CallerMemberName] string callerMemberName = "",
        [CallerLineNumber] int callerLineNumber = 0,
        [CallerFilePath] string callerFilePath = "")
    {
        if (!TryGet(guid, out ModManager.Mod mod, isSilent: true)) throw new NullModException(guid); 
        
        if (!isSilent) Logger.Message($"Mod with id \"{guid}\" was found.", callerMemberName, callerLineNumber, callerFilePath);
        
        return mod;
    }
    
    internal static bool TryGet(
        string guid,
        out ModManager.Mod mod,
        bool isSilent = false,
        [CallerMemberName] string callerMemberName = "",
        [CallerLineNumber] int callerLineNumber = 0,
        [CallerFilePath] string callerFilePath = "")
    {
        mod = ModManager.ActiveMods.Find(mod => mod.id == guid);
        
        bool exists = mod is not null;
        
        if (!isSilent) Logger.Message($"Mod with id \"{guid}\" was {(exists ? "" : "not ")} found.", callerMemberName, callerLineNumber, callerFilePath);
        
        return exists;
    }
}