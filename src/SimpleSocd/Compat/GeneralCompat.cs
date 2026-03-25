namespace OneLetterShor.SimpleSocd.Compat;

public static class GeneralCompat
{
    internal static class ExtraInfo
    {
        internal const string RainMeadowGuid = "henpemaz_rainmeadow";
        internal static ModManager.Mod RainMeadowMod = null!;
    }
    
    public static SoftDependencies ActiveSoftDependencies { get; internal set; } = SoftDependencies.None;
    public static bool IsRainMeadowEnabled => ActiveSoftDependencies.HasFlag(SoftDependencies.RainMeadow);
    public static bool HasCheckedMods { get; private set; } = false;
    
    public static void CheckMods()
    {
        Assert(!HasCheckedMods, "Mods may not be checked multiple times.");
        HasCheckedMods = true;
        
        if (Utils.ModFinder.TryGet(ExtraInfo.RainMeadowGuid, out ExtraInfo.RainMeadowMod)) ActiveSoftDependencies |= SoftDependencies.RainMeadow;
        
#if CAN_LOG_SOFT_DEPENDENCY_CHANGE
        Logger.Message($"Active soft dependencies: {ActiveSoftDependencies}.");
#endif
    }
}