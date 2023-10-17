using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems;

public sealed class MentagAccentSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<MentagAccentComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, MentagAccentComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // hhhiss
        message = Regex.Replace(message, "h+", "hhh");
        // HHhiss
        message = Regex.Replace(message, "H+", "HHH");

        // Lust-Localization-Start
        // х => ххх
        message = Regex.Replace(
            message,
            "х+",
            _random.Pick(new List<string>() { "хх", "ххх" })
        );
        // Lust-Localization-End
        args.Message = message;
    }
}
