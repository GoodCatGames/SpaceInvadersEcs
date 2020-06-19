using System;
using SpaceInvadersLeoEcs.Blueprints;

namespace SpaceInvadersLeoEcs.AppData
{
    [Serializable]
    public class PlayerConfig
    {
        public GunBlueprint GunPlayer = default;
        public float Speed = 0f;
    }
}