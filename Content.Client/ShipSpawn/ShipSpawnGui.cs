using System.Numerics;
using Content.Client.GameTicking.Managers;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Prototypes;
using static Robust.Client.UserInterface.Controls.BoxContainer;

namespace Content.Client.ShipSpawn
{
    public sealed class ShipSpawnGui : DefaultWindow
    {
        [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
        [Dependency] private readonly IEntitySystemManager _entitySystem = default!;
        private readonly Control _base;
        private readonly ClientGameTicker _gameTicker;
        private readonly ShipSpawnSystem _shipSpawn;

        public ShipSpawnGui()
        {
            MinSize = SetSize = new Vector2(360, 560);
            IoCManager.InjectDependencies(this);
            Title = Loc.GetString("ship-spawn-gui-title");
            _gameTicker = _entitySystem.GetEntitySystem<ClientGameTicker>();
            _shipSpawn = _entitySystem.GetEntitySystem<ShipSpawnSystem>();

            _base = new BoxContainer()
            {
                Orientation = LayoutOrientation.Vertical,
            };
            var scroll = new ScrollContainer()
            {
                VerticalExpand = true,
            };
            var shipContainer = new BoxContainer
            {
                Orientation = LayoutOrientation.Vertical
            };
            _base.AddChild(scroll);
            scroll.AddChild(shipContainer);

            Contents.AddChild(_base);
            foreach (var (id, stuff) in _gameTicker.ShipList)
            {
                var shipSpawnButton = new Button()
                {
                    Text = id,

                };
                shipSpawnButton.OnPressed += _ =>
                {
                    _shipSpawn.SpawnShip(stuff);
                    Close();
                };
                shipContainer.AddChild(shipSpawnButton);
            }
        }

    }
}
