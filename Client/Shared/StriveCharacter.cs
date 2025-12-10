namespace Client.Shared;

public enum StriveCharacter
{
    Default,
    Aba,
    Anji,
    Asuka,
    Axl,
    Baiken,
    Bedman,
    Bridget,
    Chipp,
    Elphelt,
    Faust,
    Giovanna,
    Goldlewis,
    HappyChaos,
    Ino,
    Jacko,
    Johnny,
    Ky,
    Leo,
    Lucy,
    May,
    Millia,
    Nagoriyuki,
    Potemkin,
    Dizzy,
    Ramlethal,
    Sin,
    Slayer,
    Sol,
    Testament,
    Unika,
    Venom,
    Zato,
}

public static class StriveCharacterExtensions
{
    public static string FriendlyName(this StriveCharacter character) => character.ToString().Replace("_", " ");
}