using Content.Server.GameTicking;
using Content.Server.Maps;
using Content.Server.Station;
using Content.Server.Station.Systems;
using Robust.Server.Maps;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server._Voidcrew.FTLPoints.Effects;

[DataDefinition]
public sealed partial class SpawnStationEffect : FTLPointEffect
{
    [DataField("stationIds", required: true)]
    public List<string> StationIds { set; get; } = new List<string>()
    {
        "TradeStation"
    };

    public override void Effect(FTLPointEffectArgs args)
    {
        var protoManager = IoCManager.Resolve<IPrototypeManager>();
        var random = IoCManager.Resolve<IRobustRandom>();
        var gameTicker = args.EntityManager.System<GameTicker>();
        var gameMap = protoManager.Index<GameMapPrototype>(random.Pick(StationIds));
        gameTicker.LoadGameMap(gameMap, args.MapId, null);
    }
}
