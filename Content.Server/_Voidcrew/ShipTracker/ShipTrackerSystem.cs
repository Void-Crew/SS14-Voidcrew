using Content.Server._Voidcrew.FTLPoints;
using Content.Server.AlertLevel;
using Content.Server.Chat.Managers;
using Content.Server.Chat.Systems;
using Content.Server.Explosion.EntitySystems;
using Content.Server.GameTicking.Events;
using Content.Server.Popups;
using Content.Server.Shuttles.Events;
using Content.Shared.CCVar;
using Content.Shared.Pinpointer;
using Robust.Server.GameObjects;
using Robust.Shared.Configuration;
using Robust.Shared.Map;
using Robust.Shared.Random;

namespace Content.Server._Voidcrew.ShipTracker;

/// <summary>
/// This handles tracking ships, healths and more
/// </summary>
public sealed class ShipTrackerSystem : EntitySystem
{
    [Dependency] private readonly IMapManager _mapManager = default!;


    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<FTLCompletedEvent>(OnFTLCompletedEvent);
    }

    private void OnFTLCompletedEvent(ref FTLCompletedEvent args)
    {
        var mapId = Transform(args.MapUid).MapID;
        if (!_mapManager.IsMapInitialized(mapId))
        {
            _mapManager.DoMapInitialize(mapId);
        }

    }
}

