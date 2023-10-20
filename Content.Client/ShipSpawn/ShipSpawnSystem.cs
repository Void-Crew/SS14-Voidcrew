using Content.Shared.ShipSpawn;

namespace Content.Client.ShipSpawn;

public sealed class ShipSpawnSystem : EntitySystem
{
    public void SpawnShip(string uid)
    {
        RaiseNetworkEvent(new RequestShipSpawnMessage(uid));
    }
}
