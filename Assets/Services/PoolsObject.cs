using SpaceInvadersLeoEcs.Pooling;

namespace SpaceInvadersLeoEcs.Services
{
    public class PoolsObject
    {
        public PoolContainer Bullets { get; }
        public PoolContainer Mobs { get; }

        private const string pathBullet = "Prefabs/Bullet";
        private const string pathMob = "Prefabs/Mob";
        
        public PoolsObject()
        {
            Bullets = PoolContainer.CreatePool<PoolObjectExt>(pathBullet);
            Mobs = PoolContainer.CreatePool<PoolObjectExt>(pathMob);
        }
    }
}