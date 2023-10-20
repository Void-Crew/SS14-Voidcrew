using System.Linq;
using System.Numerics;
using Content.Shared.ShipSpawn;
using Content.Server.GameTicking;
using Content.Shared.GameTicking;
using Content.Server.Maps;
using Content.Server.Station.Systems;
using Robust.Server.Maps;
using Robust.Server.Player;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server.ShipSpawn;

public sealed class ShipSpawnSystem : EntitySystem
{
    public override void Initialize()
    {
        SubscribeNetworkEvent<RequestShipSpawnMessage>(SpawnShip);
    }
    private void SpawnShip(RequestShipSpawnMessage message, EntitySessionEventArgs args)
    {
        var random = IoCManager.Resolve<IRobustRandom>();
        var prototypeManager = IoCManager.Resolve<IPrototypeManager>();
        var entityManager = IoCManager.Resolve<IEntityManager>();
        var ticker = entityManager.System<GameTicker>();
        var stationJobs = entityManager.EntitySysManager.GetEntitySystem<StationJobsSystem>();
        var stationSystem = entityManager.EntitySysManager.GetEntitySystem<StationSystem>();
        var mapManager = IoCManager.Resolve<IMapManager>();
        var gameTicker = entityManager.EntitySysManager.GetEntitySystem<GameTicker>();
        var shipId = message.Id;
        var loadOptions = new MapLoadOptions()
        {
            LoadMap = false,
            Offset = new Vector2(random.Next(-200, 200), random.Next(-200, 200))
        };
        if (ticker.PlayerGameStatuses.TryGetValue(args.SenderSession.UserId, out var status) && status == PlayerGameStatus.JoinedGame)
        {
            return;
        }
        Logger.DebugS("info", $"Spawning ship: {shipId}");
        if (prototypeManager.TryIndex<GameMapPrototype>(shipId, out var gameMap))
        {
            var grids = gameTicker.LoadGameMap(gameMap, mapManager.GetAllMapIds().First(), loadOptions);
            var station = stationSystem.GetOwningStation(grids.First()).GetValueOrDefault();
            ticker.MakeJoinGame((IPlayerSession) args.SenderSession, station, stationJobs.GetAvailableJobs(station).First());
        }

    }
}
