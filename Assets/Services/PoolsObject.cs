using SpaceInvadersLeoEcs.Pooling;

namespace SpaceInvadersLeoEcs.Services
{
    public class PoolsObject
    {
        private const string pathBullet = "Prefabs/Bullet";
        private const string pathMob = "Prefabs/Mob";
        
        public PoolContainer Bullets { get; }
        public PoolContainer Mobs { get; }
        
        public PoolsObject()
        {
            Bullets = PoolContainer.CreatePool<PoolObjectExt>(pathBullet);
            Mobs = PoolContainer.CreatePool<PoolObjectExt>(pathMob);
        }
    }
}