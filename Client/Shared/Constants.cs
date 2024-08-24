namespace Client.Shared;

public static class Constants
{
    public static string TitlePrefix => "How 2 Baiken |";

    public static string VideoUri(string name)
    {
        return $"https://github.com/treesgobark/BaikenInfoMedia/raw/main/{name}.webm";
    }
}
