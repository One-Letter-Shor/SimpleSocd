namespace OneLetterShor.SimpleSocd.Compat;

[Flags]
public enum SoftDependencies
{
    None = 0,
    RainMeadow = 1 << 0,
    All = RainMeadow
}