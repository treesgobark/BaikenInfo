using Microsoft.AspNetCore.Components;

namespace Client.Shared;

public class Combo
{
    public Combo(List<Move> moves, string videoName = "", List<RenderFragment>? additionalNotes = null)
    {
        Moves     = moves;
        VideoName = videoName;

        if (additionalNotes is not null)
        {
            AdditionalNotes = additionalNotes;
        }
    }

    public List<RenderFragment> AdditionalNotes { get; } = [];
    public List<Move> Moves { get; init; }
    public string VideoName { get; init; }
    public string ComboString => string.Join(" > ", Moves.Select(m => m.NotationName));
}