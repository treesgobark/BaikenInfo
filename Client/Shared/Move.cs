using System.Collections.Immutable;
using Microsoft.AspNetCore.Components;

namespace Client.Shared;

public enum NamePreference { Notation, English, Friendly }
public enum AttackButton { P, K, S, H, D, None }

public class Move
{
    public Move(string               NotationName,
                string               EnglishName,
                string               FriendlyName,
                AttackButton         AttackButton,
                string               ImageName,
                string               DustloopTag,
                List<string> AdditionalNotes = null)
    {
        this.NotationName    = NotationName;
        this.EnglishName     = EnglishName;
        this.FriendlyName    = FriendlyName;
        this.AttackButton    = AttackButton;
        this.ImageName       = ImageName;
        this.DustloopTag     = DustloopTag;
        this.AdditionalNotes = AdditionalNotes is null ? [] : AdditionalNotes;
    }
    
    public Move(Move other)
    {
        NotationName    = other.NotationName;
        EnglishName     = other.EnglishName;
        FriendlyName    = other.FriendlyName;
        AttackButton    = other.AttackButton;
        ImageName       = other.ImageName;
        DustloopTag     = other.DustloopTag;
        AdditionalNotes = [..other.AdditionalNotes];
    }

    public string DisplayNotationText => string.Join(" ", Prefixes.Append(NotationName).Concat(Suffixes));
    public string DisplayEnglishText => string.Join(" ", Prefixes.Append(EnglishName).Concat(Suffixes));
    public string DisplayFriendlyText => string.Join(" ", Prefixes.Append(FriendlyName).Concat(Suffixes));
    
    private ImmutableList<string> Prefixes { get; set; } = [];
    private ImmutableList<string> Suffixes { get; set; } = [];
    public string NotationName { get; init; }
    public string EnglishName { get; init; }
    public string FriendlyName { get; init; }
    public AttackButton AttackButton { get; init; }
    public string ImageName { get; init; }
    public string DustloopTag { get; init; }
    public List<string> AdditionalNotes { get; init; }

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
    public static Move _2D      { get; } = new Move("2D",      "Crouching Dust", "Sweep", AttackButton.D, "2D", "2D");
    public static Move _2H      { get; } = new Move("2H",      "Crouching Heavy Slash", "", AttackButton.H, "2H", "2H");
    public static Move _2K      { get; } = new Move("2K",      "Crouching Kick", "", AttackButton.K, "2K", "2K");
    public static Move _2P      { get; } = new Move("2P",      "Crouching Punch", "", AttackButton.P, "2P", "2P");
    public static Move _2S      { get; } = new Move("2S",      "Crouching Slash", "", AttackButton.S, "2S", "2S");
    public static Move _5D      { get; } = new Move("5D",      "Standing Dust", "Dust", AttackButton.D, "5D", "5D");
    public static Move Charged_5D      { get; } = new Move("5[D]",      "Charged Dust", "", AttackButton.D, "5D", "5D", 
        ["To input this, you hold the D button."]);
    public static Move _5H      { get; } = new Move("5H",      "Standing Heavy Slash", "", AttackButton.H, "5H", "5H");
    public static Move _5K      { get; } = new Move("5K",      "Standing Kick", "", AttackButton.K, "5K", "5K");
    public static Move _5P      { get; } = new Move("5P",      "Standing Punch", "", AttackButton.P, "5P", "5P");
    public static Move _6H      { get; } = new Move("6H",      "Forward Heavy Slash", "", AttackButton.H, "6H", "6H");
    public static Move _6K      { get; } = new Move("6K",      "Forward Kick", "", AttackButton.K, "6K", "6K");
    public static Move _6P      { get; } = new Move("6P",      "Forward Punch", "", AttackButton.P, "6P", "6P");
    public static Move WildAssault    { get; } = new Move("236D",    "Wild Assault", "", AttackButton.D, "236D", "Wild_Assault");
    public static Move Tatami    { get; } = new Move("236K",    "Tatami Gaeshi", "Tatami", AttackButton.K, "236K", "Tatami_Gaeshi");
    public static Move AerialTatami    { get; } = new Move("j.236K",    "Aerial Tatami Gaeshi", "j.Tatami", AttackButton.K, "236K", "Tatami_Gaeshi");
    public static Move Hiiragi    { get; } = new Move("236P",    "Hiiragi", "Parry", AttackButton.P, "236P", "Hiiragi");
    public static Move HKab  { get; } = new Move("41236H",  "Kabari (HS version)", "hkab(1)", AttackButton.H, "41236H", "Kabari");
    public static Move HkabH { get; } = new Move("~H", "Kabari (HS version) Follow-up", "hkabh", AttackButton.H, "41236HH", "Kabari",
                                                 ["Note: never hit the input for this move more than once. It will cause you to drop combos." +
                                                  " Sometimes you may have to delay it, though, if your opponent is too high."]);
    public static Move SKab  { get; } = new Move("41236S",  "Kabari (S version)", "skab", AttackButton.S, "41236S", "Kabari");
    public static Move Youzansen  { get; } = new Move("j.236S",  "Youzansen", "yzn", AttackButton.S, "j236S", "Youzansen");
    public static Move cS       { get; } = new Move("c.S",     "Close Slash", "", AttackButton.S, "cS", "c.S");
    public static Move fS       { get; } = new Move("f.S",     "Far Slash", "", AttackButton.S, "fS", "f.S");
    public static Move jP       { get; } = new Move("j.P",     "Aerial Punch", "", AttackButton.P, "jP", "j.P");
    public static Move jK       { get; } = new Move("j.K",     "Aerial Kick", "", AttackButton.K, "jK", "j.K");
    public static Move jS       { get; } = new Move("j.S",     "Aerial Slash", "", AttackButton.S, "jS", "j.S");
    public static Move jH       { get; } = new Move("j.H",     "Aerial Heavy Slash", "", AttackButton.H, "jH", "j.H");
    public static Move jD       { get; } = new Move("j.D",     "Aerial Dust", "", AttackButton.D, "jD", "j.D");
    public static Move Throw    { get; } = new Move("4D/6D",   "Ground Throw", "Throw", AttackButton.D, "GroundThrow", "Ground_Throw_(Tether)");
    public static Move AirThrow    { get; } = new Move("j.4D/j.6D",   "Air Throw", "", AttackButton.D, "GroundThrow", "Air_Throw");
    public static Move Dash    { get; } = new Move("66",   "Dash", "", AttackButton.None, "Dash", "");
    public static Move AirDash    { get; } = new Move("j.66",   "Air Dash", "", AttackButton.None, "AirDash", "");
    public static Move IAD    { get; } = new Move("IAD",   "Instant Air Dash", "", AttackButton.None, "AirDash", "");
    public static Move Delay    { get; } = new Move("dl.",   "Wait a few frames", "delay", AttackButton.None, "Stand", "");
}
