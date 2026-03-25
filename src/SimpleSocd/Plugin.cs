using System.Security.Permissions;
using BepInEx;
using BepInEx.Logging;
using Config_ = OneLetterShor.SimpleSocd.Config;
using Logger_ = OneLetterShor.SimpleSocd.Logging.Logger;
using SecurityAction = System.Security.Permissions.SecurityAction;

#pragma warning disable CS0618
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification=true)]
#pragma warning restore CS0618
[assembly: AssemblyVersion(Plugin.Version)]

namespace OneLetterShor.SimpleSocd;

[BepInDependency(Compat.GeneralCompat.ExtraInfo.RainMeadowGuid, BepInDependency.DependencyFlags.SoftDependency)]
[BepInPlugin(Guid, Name, Version)]
public sealed class Plugin : BaseUnityPlugin
{
    public const string
        Guid = "OneLetterShor.SimpleSocd",
        Name = "SimpleSocd",
        Version = "0.0.0";
    public static ModManager.Mod Mod { get; private set; } = null!;
    public static Plugin Instance { get; private set; } = null!;
    public static bool IsFullyInitialized { get; private set; } = false;
    
    public void ApplyHooksAndEvents()
    {
        InputHandler.ApplyHooksAndEvents();
        
        if (Compat.GeneralCompat.IsRainMeadowEnabled)
            ApplyRainMeadowHooksAndEvents();
        
        return;
        
        void ApplyRainMeadowHooksAndEvents()
        {
            Utils.RainMeadowHookCache.__Initialize();
        }
    }
    
    private Plugin()
    {
        Assert(!IsFullyInitialized);
        Instance = this;
    }
    
    private void OnEnable()
    {
        Assert(!IsFullyInitialized);
        On.RainWorld.OnModsInit += On_RainWorld_OnModsInit;
    }
    
    private void On_RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld rainWorld)
    {
        if (IsFullyInitialized) { orig(rainWorld); return; }
        IsFullyInitialized = true;
        
        try
        {
            Mod = Utils.ModFinder.Get(Guid);
            Compat.GeneralCompat.CheckMods();
            ApplyHooksAndEvents();
            MachineConnector.SetRegisteredOI(Mod.id, Config_.Options.Instance);
        }
        catch(Exception exception)
        {
            Logger_.Fatal(exception);
        }
        
        orig(rainWorld);
    }
    
    internal static void __Log(LogLevel logLevel, object data) => Instance.Logger.Log(logLevel, data);
}