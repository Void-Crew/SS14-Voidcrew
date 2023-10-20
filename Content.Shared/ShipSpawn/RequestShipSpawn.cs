using Robust.Shared.Serialization;

namespace Content.Shared.ShipSpawn;

[Serializable, NetSerializable]
public sealed class RequestShipSpawnMessage : EntityEventArgs
{
    public string Id { get; }

    public RequestShipSpawnMessage(string id)
    {
        Id = id;
    }
}
