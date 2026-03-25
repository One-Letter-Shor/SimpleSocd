using Menu.Remix.MixedUI;

namespace OneLetterShor.SimpleSocd.Config;

public sealed class Options : OptionInterface
{
    public static Options Instance { get; } = new();
    public static OpTab GeneralTab { get; private set; } = null!;
    
    private Options() { }
    
    public override void Initialize()
    {
        ResetUI();
        
        GeneralTab = new OpTab(this, nameof(GeneralTab));
        Tabs = [ GeneralTab ];
        
        
        GeneralTab.AddItems(
            
        );
    }
    
    private void ResetUI()
    {
        
    }
}