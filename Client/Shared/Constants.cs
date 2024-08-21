namespace Client.Shared;

public static class Constants
{
    public static string TitlePrefix => "How 2 Baiken |";

    public static string ImageUri(string name) => $"Moves/{name}.png";
    public static string VideoUri(string name)
    {
        name = name.Replace(" ", "%20");
        return $"https://baikenstorage.blob.core.windows.net/baiken-media/{name}.mp4";
    }
}