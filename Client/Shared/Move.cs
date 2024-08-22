using System.Collections.Immutable;

namespace Client.Shared;

public enum NamePreference { Notation, English, Friendly }
public enum AttackButton { P, K, S, H, D, None }

public record Move(
    string         NotationName,
    string         EnglishName,
    string         FriendlyName,
    NamePreference NamePreference,
    AttackButton   AttackButton,
    string         ImageName,
    string         DustloopTag)
{
    public string DisplayText => string.Join(" ", Prefixes.Append(NotationName).Concat(Suffixes));
    
    private ImmutableList<string> Prefixes { get; set; } = [];
    private ImmutableList<string> Suffixes { get; set; } = [];

    public Move AddPrefix(string prefix)
    {
        var newMove = new Move(this);

        newMove.Prefixes = newMove.Prefixes.Add(prefix);
        
        return newMove;
    }

    public Move AddSuffix(string suffix)
    {
        var newMove = new Move(this);

        newMove.Suffixes = newMove.Suffixes.Add(suffix);
        
        return newMove;
    }
}

public static class Moves
{
    public static Move _2D      { get; } = new Move("2D",      "Crouching Dust", "Sweep", NamePreference.Notation, AttackButton.D, "2D", "2D");
    public static Move _2H      { get; } = new Move("2H",      "Crouching Heavy Slash", "", NamePreference.Notation, AttackButton.H, "2H", "2H");
    public static Move _2K      { get; } = new Move("2K",      "Crouching Kick", "", NamePreference.Notation, AttackButton.K, "2K", "2K");
    public static Move _2P      { get; } = new Move("2P",      "Crouching Punch", "", NamePreference.Notation, AttackButton.P, "2P", "2P");
    public static Move _2S      { get; } = new Move("2S",      "Crouching Slash", "", NamePreference.Notation, AttackButton.S, "2S", "2S");
    public static Move _5D      { get; } = new Move("5D",      "Standing Dust", "Dust", NamePreference.Notation, AttackButton.D, "5D", "5D");
    public static Move _5H      { get; } = new Move("5H",      "Standing Heavy Slash", "", NamePreference.Notation, AttackButton.H, "5H", "5H");
    public static Move _5K      { get; } = new Move("5K",      "Standing Kick", "", NamePreference.Notation, AttackButton.K, "5K", "5K");
    public static Move _5P      { get; } = new Move("5P",      "Standing Punch", "", NamePreference.Notation, AttackButton.P, "5P", "5P");
    public static Move _6H      { get; } = new Move("6H",      "Forward Heavy Slash", "", NamePreference.Notation, AttackButton.H, "6H", "6H");
    public static Move _6K      { get; } = new Move("6K",      "Forward Kick", "", NamePreference.Notation, AttackButton.K, "6K", "6K");
    public static Move _6P      { get; } = new Move("6P",      "Forward Punch", "", NamePreference.Notation, AttackButton.P, "6P", "6P");
    public static Move WildAssault    { get; } = new Move("236D",    "Wild Assault", "", NamePreference.Notation, AttackButton.D, "236D", "Wild_Assault");
    public static Move Tatami    { get; } = new Move("236K",    "Tatami Gaeshi", "Tatami", NamePreference.Notation, AttackButton.K, "236K", "Tatami_Gaeshi");
    public static Move Hiiragi    { get; } = new Move("236P",    "Hiiragi", "Parry", NamePreference.Notation, AttackButton.P, "236P", "Hiiragi");
    public static Move HKab  { get; } = new Move("41236H",  "Kabari (HS version)", "hkab(1)", NamePreference.Notation, AttackButton.H, "41236H", "Kabari");
    public static Move HkabH { get; } = new Move("~H", "Kabari (HS version) Follow-up", "hkabh", NamePreference.Notation, AttackButton.H, "41236HH", "Kabari");
    public static Move SKab  { get; } = new Move("41236S",  "Kabari (S version)", "skab", NamePreference.Notation, AttackButton.S, "41236S", "Kabari");
    public static Move Youzansen  { get; } = new Move("j.236S",  "Youzansen", "yzn", NamePreference.Notation, AttackButton.S, "j236S", "Youzansen");
    public static Move cS       { get; } = new Move("c.S",     "Close Slash", "", NamePreference.Notation, AttackButton.S, "cS", "c.S");
    public static Move fS       { get; } = new Move("f.S",     "Far Slash", "", NamePreference.Notation, AttackButton.S, "fS", "f.S");
    public static Move jP       { get; } = new Move("j.P",     "Aerial Punch", "", NamePreference.Notation, AttackButton.P, "jP", "j.P");
    public static Move jK       { get; } = new Move("j.K",     "Aerial Kick", "", NamePreference.Notation, AttackButton.K, "jK", "j.K");
    public static Move jS       { get; } = new Move("j.S",     "Aerial Slash", "", NamePreference.Notation, AttackButton.S, "jS", "j.S");
    public static Move jH       { get; } = new Move("j.H",     "Aerial Heavy Slash", "", NamePreference.Notation, AttackButton.H, "jH", "j.H");
    public static Move jD       { get; } = new Move("j.D",     "Aerial Dust", "", NamePreference.Notation, AttackButton.D, "jD", "j.D");
    public static Move Throw    { get; } = new Move("4D/6D",   "Ground Throw", "Throw", NamePreference.Friendly, AttackButton.D, "GroundThrow", "Ground_Throw_(Tether)");
    public static Move AirThrow    { get; } = new Move("j.4D/j.6D",   "Air Throw", "", NamePreference.Friendly, AttackButton.D, "GroundThrow", "Air_Throw");
    public static Move Dash    { get; } = new Move("66",   "Dash", "", NamePreference.English, AttackButton.None, "Dash", "");
    public static Move AirDash    { get; } = new Move("j.66",   "Air Dash", "", NamePreference.English, AttackButton.None, "Dash", "");
    public static Move IAD    { get; } = new Move("IAD",   "Instant Air Dash", "", NamePreference.English, AttackButton.None, "Dash", "");
}
