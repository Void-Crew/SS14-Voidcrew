using Robust.Server.GameObjects;
using Robust.Shared.Random;
using Robust.Shared.Utility;
using Serilog;

namespace Content.Server._Voidcrew.FTLPoints.Effects;

[DataDefinition]
public sealed partial class SpawnMapEffect : FTLPointEffect
{
    [DataField("mapPaths", required: true)]
    public List<ResPath> MapPaths { set; get; } = new List<ResPath>()
    {
        new ResPath("/Maps/_FTL/trade-station.yml")
    };

    public override void Effect(FTLPointEffectArgs args)
    {
        var mapLoader = args.EntityManager.System<MapLoaderSystem>();
        var random = IoCManager.Resolve<IRobustRandom>();
        if (mapLoader.TryLoad(args.MapId, random.Pick(MapPaths).ToString(), out var rootUids))
        {
            Log.Debug("Successfully loaded map.");
        }
    }
}
