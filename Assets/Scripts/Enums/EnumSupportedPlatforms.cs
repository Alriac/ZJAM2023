[System.Flags]
public enum EnumSupportedPlatforms
{
    Windows = 1,
    Mac = 2,
    Linux = 4,
    Android = 8,
    IOS = 16,
    Web = 32,
    Desktop = EnumSupportedPlatforms.Windows | EnumSupportedPlatforms.Mac | EnumSupportedPlatforms.Linux,
    Mobile = EnumSupportedPlatforms.Android | EnumSupportedPlatforms.IOS,
}